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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nini.Config;
using System.Diagnostics;
using System.Threading;

namespace MadCow
{
    public partial class RepositorySelectionServer : Form
    {
        public RepositorySelectionServer()
        {
            InitializeComponent();
            AddAvailableRepositories();
            LaunchServerButton.Enabled = false;
        }

        public void AddAvailableRepositories() //Adds available repos to the list.
        {
            string[] FoldersArray = Directory.GetDirectories(Program.programPath + @"\" + @"Repositories\");
            foreach (string name in FoldersArray)
            {
                DirectoryInfo info = new DirectoryInfo(name);
                this.checkedListBox1.Items.Add(info.Name);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When a user selects a repository from the RepoSelectionForm MadCow writes the correct values into that specific repository INI config file. Its better this way cause
            //handling different repositories, MPQ Storage destination changes and different profiles its tough thing :P, atleast for me.
            int selected = checkedListBox1.SelectedIndex;
            if (selected != -1)
            {   //We set the correct values into the Mooege config.ini of the selected repository. According to the profile loaded.       
                Compile.currentMooegeExePath = Program.programPath + @"\" + @"Repositories\" + checkedListBox1.Items[selected].ToString() + @"\src\Mooege\bin\Debug\Mooege.exe";
                var _repoINIpath = Program.programPath + @"\" + @"Repositories\" + checkedListBox1.Items[selected].ToString() + @"\src\Mooege\bin\Debug\config.ini";
                IConfigSource repoINIpath = new IniConfigSource(_repoINIpath);
                //Global settings:
                #region SetSettings
                repoINIpath.Configs["Storage"].Set("MPQRoot", Form1.GlobalAccess.MPQDestTextBox.Text);
                repoINIpath.Configs["ServerLog"].Set("Enabled", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(0));
                repoINIpath.Configs["PacketLog"].Set("Enabled", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(1));
                repoINIpath.Configs["Storage"].Set("EnableTasks", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(2));
                repoINIpath.Configs["Storage"].Set("LazyLoading", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(3));
                repoINIpath.Configs["Authentication"].Set("DisablePasswordChecks", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(4));
                //We set the server variables:
                TextReader tr = new StreamReader(Configuration.MadCow.CurrentProfile);
                //The next values are set in an SPECIFIC ORDER, changing the order will make INI modifying FAIL.
                //MooNet-Server IP
                repoINIpath.Configs["MooNet-Server"].Set("BindIP", tr.ReadLine());
                //Game-Server IP
                repoINIpath.Configs["Game-Server"].Set("BindIP", tr.ReadLine());
                //Public IP
                repoINIpath.Configs["NAT"].Set("PublicIP", tr.ReadLine());
                //MooNet-Server Port
                repoINIpath.Configs["MooNet-Server"].Set("Port", tr.ReadLine());
                //Game-Server Port
                repoINIpath.Configs["Game-Server"].Set("Port", tr.ReadLine());
                //MOTD
                repoINIpath.Configs["MooNet-Server"].Set("MOTD", tr.ReadLine());
                //NAT
                repoINIpath.Configs["NAT"].Set("Enabled", tr.ReadLine());
                repoINIpath.Save();
                tr.Close();
                #endregion
                Console.WriteLine("Current Profile: " + Path.GetFileName(Configuration.MadCow.CurrentProfile));
                Console.WriteLine("Set Mooege config.ini according to your profile " + Path.GetFileName(Configuration.MadCow.CurrentProfile));
                Console.WriteLine(checkedListBox1.Items[selected].ToString() + " is ready to go.");
            }
            LaunchServerButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProcessFinder.FindProcess("Mooege") == true)
            {
                var answer = MessageBox.Show("Mooege is already Running. Do you want to restart Mooege?", "Attention",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    ProcessFinder.KillProcess("Mooege");
                    Process proc0 = new Process();
                    proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                    proc0.Start();
                    this.Close();
                }
                else
                {
                    //Do Nothing
                }
            }
            else
            {
                ProcessFinder.KillProcess("Mooege");
                Process proc0 = new Process();
                proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                proc0.Start();
                Thread.Sleep(3000);
                if (ErrorFinder.SearchLogs("Fatal") == true)
                {
                    Console.WriteLine("Closing Mooege due Fatal Exception");
                    ProcessFinder.KillProcess("Mooege");
                }
                this.Close();
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 1)
            {
                Boolean isCheckedItemBeingUnchecked = (e.CurrentValue == CheckState.Checked);
                if (isCheckedItemBeingUnchecked)
                {
                    e.NewValue = CheckState.Checked;
                }
                else
                {
                    Int32 checkedItemIndex = checkedListBox1.CheckedIndices[0];
                    checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
                    checkedListBox1.SetItemChecked(checkedItemIndex, false);
                    checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
                }

                return;
            }
        }
    }
}
