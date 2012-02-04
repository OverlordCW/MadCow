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
using System.IO;
using System.Windows.Forms;
using Nini.Config;

namespace MadCow
{
    public partial class RepositorySelectionPlay : Form
    {
        public RepositorySelectionPlay()
        {
            InitializeComponent();
            AddAvailableRepositories();
            LaunchDiabloButton.Enabled = false;
        }

        internal string SelectedRepository { get { return listBox1.SelectedItem.ToString(); } }

        public void AddAvailableRepositories() //Adds available repos to the list.
        {
            //var foldersArray = Directory.GetDirectories(Program.programPath + @"\" + @"Repositories\");
            foreach (var info in Directory.GetDirectories("Repositories"))// foldersArray.Select(name => new DirectoryInfo(name)))
            {
                listBox1.Items.Add(info);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = listBox1.SelectedIndex;
            if (selected == -1) return;

            //Compile.CurrentMooegeExePath = Program.programPath + @"\" + @"Repositories\" + checkedListBox1.Items[selected] + @"\src\Mooege\bin\Debug\Mooege.exe";
            var _repoINIpath = Program.programPath + @"\" + @"Repositories\" + listBox1.Items[selected] + @"\src\Mooege\bin\Debug\config.ini";
            IConfigSource repoINIpath = new IniConfigSource(_repoINIpath);
            //For each selection we set the correct MPQ storage path & PacketLog|ServerLog settings on the config INI, this is the best way I could think to have the paths updated at everytime
            //We CANNOT call variable Compile.mooegeINI because that variable only saves latest compiled ini path for INSTANT writting after compiling a repository.
            //WE do not need to write different IPS / PORTS for this since its LOCAL function, We do that over RepositorySelectionSERVER.
            #region SetSettings
            repoINIpath.Configs["Storage"].Set("MPQRoot", Form1.GlobalAccess.MPQDestTextBox.Text);
            repoINIpath.Configs["ServerLog"].Set("Enabled", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(0));
            repoINIpath.Configs["PacketLog"].Set("Enabled", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(1));
            repoINIpath.Configs["Storage"].Set("EnableTasks", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(2));
            repoINIpath.Configs["Storage"].Set("LazyLoading", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(3));
            repoINIpath.Configs["Authentication"].Set("DisablePasswordChecks", Form1.GlobalAccess.SettingsCheckedListBox.GetItemChecked(4));
            //We set the server variables IP/PORTS/NAT back to Local configuration in case the user used the same repository over Server Mode only in the past.

            repoINIpath.Configs["MooNet-Server"].Set("BindIP", "0.0.0.0");
            repoINIpath.Configs["Game-Server"].Set("BindIP", "0.0.0.0");
            repoINIpath.Configs["NAT"].Set("PublicIP", "0.0.0.0");
            repoINIpath.Configs["MooNet-Server"].Set("Port", "1345");
            repoINIpath.Configs["Game-Server"].Set("Port", "1999");
            repoINIpath.Configs["MooNet-Server"].Set("MOTD", "Welcome to mooege development server!");
            repoINIpath.Configs["NAT"].Set("Enabled", "false");
            repoINIpath.Save();
            #endregion

            Console.WriteLine("Set default LAN settings for Mooege config.ini");
            Console.WriteLine("{0} is ready to go.", listBox1.Items[selected]);
            repoINIpath.Save();
            LaunchDiabloButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Diablo.Play();
            Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (checkedListBox1.CheckedItems.Count != 1) return;

            //var isCheckedItemBeingUnchecked = (e.CurrentValue == CheckState.Checked);
            //if (isCheckedItemBeingUnchecked)
            //{
            //    e.NewValue = CheckState.Checked;
            //}
            //else
            //{
            //    var checkedItemIndex = checkedListBox1.CheckedIndices[0];
            //    checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
            //    checkedListBox1.SetItemChecked(checkedItemIndex, false);
            //    checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            //}
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        // Last Played Repository | If its enabled, this function will set a value for currentMooegeExePath which we need to launch Mooege without actually selecting a repository from a list.
        ////////////////////////////////////////////////////////////////////////////////////////
        //public static Boolean LastPlayed()
        //{
        //    if (!string.IsNullOrEmpty(Configuration.MadCow.LastRepository))
        //    {
        //        Compile.CurrentMooegeExePath = Configuration.MadCow.LastRepository;
        //        return true;
        //    }
        //    return false;
        //}
    }
}
