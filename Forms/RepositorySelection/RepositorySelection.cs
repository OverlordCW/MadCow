// Copyright (C) 2011 Iker Ruiz Arnauda (Wesko)
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MadCow
{
    internal partial class RepositorySelection : Form
    {
        private Repository _selectedRepository;

        internal RepositorySelection()
        {
            InitializeComponent();
            RefreshAvailableRepositories();
        }

        internal void RefreshAvailableRepositories() //Adds available repos to the list.
        {
            listView1.Items.Clear();
            foreach (var repository in Repository.Repositories)
            {
                var item = new ListViewItem(repository.Url.AbsoluteUri);
                item.SubItems.AddRange(new[]
                                           {
                                               repository.Branch,
                                               string.IsNullOrEmpty(repository.LocalRevision)
                                                   ? " "
                                                   : repository.LocalRevision,
                                                   string.IsNullOrEmpty(repository.LastRevision)
                                                   ? " "
                                                   : repository.LastRevision,
                                               repository.IsDownloaded
                                                   ? repository.Date.ToShortDateString()
                                                   : "Never"
                                           });
                listView1.Items.Add(item);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1) return;
            var dialogResult = MessageBox.Show(this,
                                               "Are you sure?",
                                               "Confirmation",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Repository.Repositories[listView1.SelectedIndices[0]].Delete();
            }
            //listView1.Items.Remove(listView1.SelectedItems[0]);
            RefreshAvailableRepositories();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var b = false;
            //foreach (var repo in from ListViewItem item in listView1.SelectedItems
            //                     select (Repository)item.Tag)
            //{
            //    b = repo.IsUpdating;
            //}
            updateButton.Enabled = listView1.SelectedIndices.Count > 0 &&
                                   !Repository.Repositories[listView1.SelectedIndices[0]].IsUpdating;
            deleteButton.Enabled = listView1.SelectedIndices.Count > 0;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            _selectedRepository = Repository.Repositories[listView1.SelectedIndices[0]];
            _selectedRepository.RunWorkerCompleted += Download_RunWorkerCompleted;
            _selectedRepository.Update();
        }

        private void Download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Form1.GlobalAccess.statusStripProgressBar.Value = 0;
            Form1.GlobalAccess.statusStripProgressBar.Visible = false;
            Console.WriteLine("Update Complete!");
            Tray.ShowBalloonTip("Update Complete!");
            Form1.GlobalAccess.statusStripStatusLabel.Text = "Update Complete!";
            _selectedRepository.RunWorkerCompleted -= Download_RunWorkerCompleted;
            RefreshAvailableRepositories();
            Enabled = true;
        }

        private void checkUpdatesButton_Click(object sender, EventArgs e)
        {
            checkUpdatesButton.Enabled = false;
            updateButton.Enabled = false;
            Form1.GlobalAccess.statusStripProgressBar.Visible = true;
            Form1.GlobalAccess.statusStripStatusLabel.Text = "Checking for updates.";
            var bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            bgWorker.RunWorkerCompleted += (s, ea) =>
                                               {
                                                   Form1.GlobalAccess.statusStripProgressBar.Value = 0;
                                                   Form1.GlobalAccess.statusStripProgressBar.Visible = false;
                                                   Form1.GlobalAccess.statusStripStatusLabel.Text = "Ready";
                                                   checkUpdatesButton.Enabled = true;
                                                   updateButton.Enabled = listView1.SelectedIndices.Count > 0;
                                                   RefreshAvailableRepositories();
                                               };
            bgWorker.DoWork += (s, ea) =>
                                   {
                                       for (var i = 0; i < Repository.Repositories.Count; i++)
                                       {
                                           bgWorker.ReportProgress(i * 100 / Repository.Repositories.Count);
                                           Repository.Repositories[i].UpdateRevision();
                                       }
                                   };
            bgWorker.ProgressChanged +=
                (s, ea) => Form1.GlobalAccess.statusStripProgressBar.Value = ea.ProgressPercentage;
            bgWorker.RunWorkerAsync();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var editRepo = new EditRepository();
            if (editRepo.ShowDialog() == DialogResult.OK)
            {
                Repository.Repositories.Add(editRepo.SelectedRepository);
                RefreshAvailableRepositories();
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            var editRepo = new EditRepository();
            if (editRepo.ShowDialog() == DialogResult.OK)
            {
                _selectedRepository.Url = editRepo.SelectedRepository.Url;
                _selectedRepository.Branch = editRepo.SelectedRepository.Branch;
                RefreshAvailableRepositories();
            }
        }
    }
}
