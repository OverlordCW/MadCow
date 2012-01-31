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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Nini.Config;

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
            var foldersArray = Directory.GetDirectories(Program.programPath + @"\" + @"Repositories\");
            foreach (var info in foldersArray.Select(name => new DirectoryInfo(name)))
            {
                checkedListBox1.Items.Add(info.Name);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When a user selects a repository from the RepoSelectionForm MadCow writes the correct values into that specific repository INI config file. Its better this way cause
            //handling different repositories, MPQ Storage destination changes and different profiles its tough thing :P, atleast for me.
            var selected = checkedListBox1.SelectedIndex;
            if (selected != -1)
            {   //We set the correct values into the Mooege config.ini of the selected repository. According to the profile loaded.       
                //Compile.CurrentMooegeExePath = Program.programPath + @"\" + @"Repositories\" + checkedListBox1.Items[selected] + @"\src\Mooege\bin\Debug\Mooege.exe";
                //var _repoIniPath = Program.programPath + @"\" + @"Repositories\" + checkedListBox1.Items[selected] + @"\src\Mooege\bin\Debug\config.ini";
                var repoIniPath = new IniConfigSource(Compile.MooegeINI);
                //Global settings:
                #region SetSettings
                repoIniPath.Configs["Storage"].Set("MPQRoot", Configuration.MadCow.MpqServer);
                repoIniPath.Configs["ServerLog"].Set("Enabled", Configuration.Mooege.FileLogging);
                repoIniPath.Configs["PacketLog"].Set("Enabled", Configuration.Mooege.PacketLogging);
                repoIniPath.Configs["Storage"].Set("EnableTasks", Configuration.Mooege.Tasks);
                repoIniPath.Configs["Storage"].Set("LazyLoading", Configuration.Mooege.LazyLoading);
                repoIniPath.Configs["Authentication"].Set("DisablePasswordChecks", Configuration.Mooege.PasswordCheck);
                //We set the server variables:
                //MooNet-Server IP
                repoIniPath.Configs["MooNet-Server"].Set("BindIP", Configuration.MadCow.CurrentProfile.MooNetServerIp);
                //Game-Server IP
                repoIniPath.Configs["Game-Server"].Set("BindIP", Configuration.MadCow.CurrentProfile.GameServerIp);
                //Public IP
                repoIniPath.Configs["NAT"].Set("PublicIP", Configuration.MadCow.CurrentProfile.NatIp);
                //MooNet-Server Port
                repoIniPath.Configs["MooNet-Server"].Set("Port", Configuration.MadCow.CurrentProfile.MooNetServerPort);
                //Game-Server Port
                repoIniPath.Configs["Game-Server"].Set("Port", Configuration.MadCow.CurrentProfile.GameServerPort);
                //MOTD
                repoIniPath.Configs["MooNet-Server"].Set("MOTD", Configuration.MadCow.CurrentProfile.MooNetServerMotd);
                //NAT
                repoIniPath.Configs["NAT"].Set("Enabled", Configuration.MadCow.CurrentProfile.NatEnabled);
                repoIniPath.Save();
                #endregion
                Console.WriteLine("Current Profile: " + Configuration.MadCow.CurrentProfile);
                Console.WriteLine("Set Mooege config.ini according to your profile " + Configuration.MadCow.CurrentProfile);
                Console.WriteLine(checkedListBox1.Items[selected] + " is ready to go.");
            }
            LaunchServerButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProcessFinder.FindProcess("Mooege"))
            {
                var answer = MessageBox.Show("Mooege is already Running. Do you want to restart Mooege?", "Attention",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    ProcessFinder.KillProcess("Mooege");
                    var proc0 = new Process();
                    proc0.StartInfo = new ProcessStartInfo(Compile.CurrentMooegeExePath);
                    proc0.Start();
                    Close();
                }
            }
            else
            {
                ProcessFinder.KillProcess("Mooege");
                var proc0 = new Process();
                proc0.StartInfo = new ProcessStartInfo(Compile.CurrentMooegeExePath);
                proc0.Start();
                Thread.Sleep(3000);
                if (ErrorFinder.SearchLogs("Fatal"))
                {
                    Console.WriteLine("Closing Mooege due Fatal Exception");
                    ProcessFinder.KillProcess("Mooege");
                }
                Close();
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 1)
            {
                var isCheckedItemBeingUnchecked = (e.CurrentValue == CheckState.Checked);
                if (isCheckedItemBeingUnchecked)
                {
                    e.NewValue = CheckState.Checked;
                }
                else
                {
                    var checkedItemIndex = checkedListBox1.CheckedIndices[0];
                    checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
                    checkedListBox1.SetItemChecked(checkedItemIndex, false);
                    checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
                }
            }
        }
    }
}
