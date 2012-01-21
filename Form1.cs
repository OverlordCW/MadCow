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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Nini.Config;
using System.Text.RegularExpressions;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //This variable its used to stablish the default profile loaded into the server control tab, if the user selects another profile this variable changes.
        public static String CurrentProfile = Program.programPath + @"\ServerProfiles\Default.mdc"; //We set this as the default profile loaded.
        //We update this variable with the current supported D3 client after parsing the required version.
        public static String MooegeSupportedVersion;
        //Timing for autoupdate
        private int Tick;
        //Parsing Console into a textbox
        TextWriter _writer = null;
        //TO access controls from outside classes
        public static Form1 GlobalAccess;
        //For tray icon
        private ContextMenu m_menu;

        public Form1()
        {
            InitializeComponent();
            GlobalAccess = this;
            CheckForProxy();
            //DeleteHelper.HideFile();
        }

        ///////////////////////////////////////////////////////////
        //Proxy
        ///////////////////////////////////////////////////////////
        public void CheckForProxy()
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            var proxyStatus = source.Configs["Proxy"].Get("Enabled");

            if (proxyStatus == "1")
            {
                Program.proxyEnabled = true;
            }
        }

        //We use this to get the values when we need to use a proxy.
        public string Proxy(String setting)
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            switch (setting)
            {
                case "password":
                    var password = source.Configs["Proxy"].Get("Password");
                    return password;
                case "username":
                    var username = source.Configs["Proxy"].Get("Username");
                    return username;
                case "proxyUrl":
                    var proxyUrl = source.Configs["Proxy"].Get("ProxyUrl");
                    return proxyUrl;
            }
            return "Ok";
        }
        ///////////////////////////////////////////////////////////
        //Form Load
        ///////////////////////////////////////////////////////////
        #region OnFormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            this.VersionLabel.Text = Application.ProductVersion;
            _writer = new TextBoxStreamWriter(ConsoleOutputTxtBox);
            Console.SetOut(_writer);
            Console.WriteLine("Welcome to MadCow!");
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 1800;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 100;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Default buttons status. 
            AutoUpdateValue.Enabled = false;
            EnableAutoUpdateBox.Enabled = false;
            PlayDiabloButton.Enabled = false;
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(this.UpdateMooegeButton, "Update mooege from GitHub to latest version");
            toolTip1.SetToolTip(this.CopyMPQButton, "Copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(this.FindDiabloButton, "Find Diablo.exe so MadCow can work properly");
            toolTip1.SetToolTip(this.ValidateRepoButton, "Validate the repository so MadCow can download it");
            toolTip1.SetToolTip(this.EnableAutoUpdateBox, "Enable updates to a repository every 'X' minutes");
            toolTip1.SetToolTip(this.RemoteServerLaunchButton, "Connects to public server you have entered in");
            toolTip1.SetToolTip(this.ResetRepoFolder, "Resets Repository folder in case of errors");
            toolTip1.SetToolTip(this.DownloadMPQSButton, "Downloads ALL MPQs needed to run Mooege");
            toolTip1.SetToolTip(this.RestoreDefaultsLabel, "Resets Server Control settings");
            toolTip1.SetToolTip(this.PlayDiabloButton, "Time to play Diablo 3 through Mooege!");
            InitializeFindPath(); //Search if a Diablo client path already exist.
            RepoCheck(); //Checks for duplicities.
            RepoList(); //Loads Repos from RepoList.txt
            Changelog(); //Loads Changelog comobox values.
            LoadLastUsedProfile(); //We try to Load the last used profile by the user.
            Helper.Helpers();//Loads the correct nameplate for shortcut/balloon/LastRepo enabled/disabled
            RetrieveMpqList.getfileList(); //Load MPQ list from Blizz server. Todo: This might slow down a bit MadCow loading, maybe we could place it somewhere else?.
            Helper.KillUpdater(); //This will kill MadCow updater if its running.
            ApplySettings(); //This loads Mooege settings over Mooege tab.
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Validate Repository
        ///////////////////////////////////////////////////////////
        #region ValidateRepository
        private void Validate_Repository_Click(object sender, EventArgs e)
        {
            ValidateRepository.RunWorkerAsync();
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }
            comboBox1.Invoke(new Action(() => { ParseRevision.revisionUrl = this.comboBox1.Text; }));
            try
            {
                WebClient client = new WebClient();
                if (Program.proxyEnabled)
                    client.Proxy = proxy;
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(backgroundWorker5_RunWorkerCompleted);
                try
                {
                    Uri uri = new Uri(ParseRevision.revisionUrl + "/commits/master.atom");
                    client.DownloadStringAsync(uri);
                }
                catch (UriFormatException)
                {
                    ActiveForm.Invoke(new Action(() =>
                    {
                        ParseRevision.commitFile = "Incorrect repository entry";
                        pictureBox2.Hide();
                        comboBox1.Text = ParseRevision.commitFile;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Please try a different Repository.");
                    }));
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        ParseRevision.commitFile = "Incorrect repository entry";
                    }
                }
                else if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    ParseRevision.commitFile = "ConnectionFailure";
                }
                else
                    ParseRevision.commitFile = "Incorrect repository entry";
            }
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                ParseRevision.commitFile = "Incorrect repository entry";
            }

            else if (e.Result.ToString() != null && e.Error == null)
            {
                ParseRevision.commitFile = e.Result.ToString();
                Int32 pos2 = ParseRevision.commitFile.IndexOf("Commit/");
                String revision = ParseRevision.commitFile.Substring(pos2 + 7, 7);
                ParseRevision.lastRevision = ParseRevision.commitFile.Substring(pos2 + 7, 7);
            }

            try
            {
                if (ParseRevision.commitFile == "ConnectionFailure")
                {
                    ActiveForm.Invoke(new Action(() =>
                    {
                        pictureBox2.Hide();
                        comboBox1.Text = ParseRevision.commitFile;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Internet Problems.");
                    }));
                }

                else if (ParseRevision.commitFile == "Incorrect repository entry")
                {
                    ActiveForm.Invoke(new Action(() =>
                    {
                        pictureBox2.Hide();
                        comboBox1.Text = ParseRevision.commitFile;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Please try a different Repository.");
                    }));
                }
                else
                {
                    ActiveForm.Invoke(new Action(() =>
                    {
                        pictureBox1.Hide();
                        pictureBox2.Show();
                        comboBox1.ForeColor = Color.Green;
                        comboBox1.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        UpdateMooegeButton.Enabled = true;
                        AutoUpdateValue.Enabled = true;
                        EnableAutoUpdateBox.Enabled = true;
                        Console.WriteLine("Repository Validated!");
                        RepoListAdd();
                        RepoListUpdate();
                        ChangelogListUpdate();
                        FindBranch.findBrach(comboBox1.Text);
                        RepositoryHintLabel.Visible = false;
                        BranchComboBox.Visible = true;
                        BranchSelectionLabel.Visible = true;
                    }));
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                ActiveForm.Invoke(new Action(() =>
                {
                    pictureBox2.Hide();
                    comboBox1.Text = ParseRevision.commitFile;
                    pictureBox1.Show();
                }));
            }
        }
        #endregion

        /////////////////////////////
        //UPDATE MOOEGE: This will compare ur current revision, if outdated proceed to download calling ->backgroundWorker1.RunWorkerAsync()->backgroundWorker1_RunWorkerCompleted.
        /////////////////////////////
        #region UpdateMooege
        private void Update_Mooege_Click(object sender, EventArgs e)
        {
            UpdateMooege();
        }

        private void UpdateMooege()
        {
            //We set or "reset" progressbar value to cero.
            generalProgressBar.Value = 0;
            generalProgressBar.Update();
            if (Directory.Exists(Program.programPath + @"\Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                if (EnableAutoUpdateBox.Checked == true) //Using AutoUpdate:
                {
                    Console.WriteLine("You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision);
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision, ToolTipIcon.Info);
                        }
                    }
                    Tick = (int)this.AutoUpdateValue.Value;
                    AutoUpdateTimerLabel.Text = "Update in " + Tick + " minutes.";
                }
                else //With out AutoUpdate:
                {
                    Console.WriteLine("You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision);
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision, ToolTipIcon.Info);
                        }
                    }
                }
            }

            else if (Directory.Exists(Program.programPath + @"/MPQ")) //Checks for MPQ Folder
            {
                if (EnableAutoUpdateBox.Checked == true) //Using AutoUpdate:
                {
                    DownloadSpeedTimer.Stop();
                    Console.WriteLine("Found default MadCow MPQ folder");
                    DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                    UpdateMooegeButton.Enabled = false;
                    Console.WriteLine("Downloading...");
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                        }
                    }
                    DownloadRepository.RunWorkerAsync();
                }
                else //With out AutoUpdate:
                {
                    Console.WriteLine("Found default MadCow MPQ folder");
                    DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                    UpdateMooegeButton.Enabled = false;
                    Console.WriteLine("Downloading...");
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                        }
                    }
                    DownloadRepository.RunWorkerAsync();
                }
            }

            else
            {
                if (EnableAutoUpdateBox.Checked == true) //Using AutoUpdate:
                {
                    DownloadSpeedTimer.Stop();
                    DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                    Console.WriteLine("Downloading...");
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                        }
                    }
                    Directory.CreateDirectory(Program.programPath + "/MPQ");
                    UpdateMooegeButton.Enabled = false;
                    DownloadRepository.RunWorkerAsync();
                }
                else //With out AutoUpdate:
                {
                    DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                    Console.WriteLine("Downloading...");
                    if (File.Exists(Program.madcowINI))
                    {
                        IConfigSource source = new IniConfigSource(Program.madcowINI);
                        String Src = source.Configs["Balloons"].Get("ShowBalloons");

                        if (Src.Contains("1"))
                        {
                            Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                        }
                    }
                    Directory.CreateDirectory(Program.programPath + "/MPQ");
                    UpdateMooegeButton.Enabled = false;
                    DownloadRepository.RunWorkerAsync();
                }
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Play Diablo Button
        ///////////////////////////////////////////////////////////
        #region PlayDiablo
        private void PlayDiablo_Click(object sender, EventArgs e)
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            String LastRepo = source.Configs["LastPlay"].Get("Enabled");

            if (LastRepo.Contains("1"))
            {
                LastPlayedRepoReminderLabel.Visible = true;
            }
            else
            {
                LastPlayedRepoReminderLabel.Visible = false;
            }

            if (ErrorFinder.hasMpqs() == true) //We check for MPQ files count before allowing the user to proceed to play.
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                t.Start();
            }
            else if (Diablo3UserPathSelection != null && ErrorFinder.hasMpqs() == false)
            {
                var ErrorAnswer = MessageBox.Show("You haven't copied MPQ files." + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (ErrorAnswer == DialogResult.Yes)
                {
                    MPQprocedure.StartCopyProcedure();
                    PlayDiabloButton.Enabled = false;
                    CopyMPQButton.Enabled = false;
                }
            }
            else
                Console.WriteLine("[FATAL] You are missing MPQ files!" + "\nPlease use CopyMpq button or copy Diablo3 MPQ's folder content into MadCow MPQ folder.");
        }

        public void ThreadProc()
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            String LastRepo = source.Configs["LastPlay"].Get("Enabled");

            if (RepositorySelectionPlay.LastPlayed() == true && LastRepo.Contains("1"))
            {
                Diablo.Play();
            }

            else
            {
                Application.Run(new RepositorySelectionPlay());
            }
            //We add ErrorFinder call here, in order to know if Mooege had issues loading.
            if (File.Exists(Program.programPath + @"\logs\mooege.log"))
            {
                if (ErrorFinder.SearchLogs("Fatal") == true)
                {
                    //We delete de Log file HERE. Nowhere else!.
                    DeleteHelper.Delete(0);
                    if (ErrorFinder.errorFileName.Contains("d3-update-base-")) //This will handle corrupted mpqs and missing mpq files.
                    {
                        var ErrorAnswer = MessageBox.Show(@"Missing or Corrupted file [" + ErrorFinder.errorFileName + @"]" + "\nWould you like MadCow to fix this for you?", "Found corrupted file!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            //We move the user to the Help tab so he can see the progress of the download.
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            //We execute the procedure to start downloading the corrupted file @ FixMpq();
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("CoreData"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("ClientData"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("MajorFailure"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"One or more MPQ files are corrupted and MadCow is unable to detect which file/s are causing this." + "\nPlease visit Mooege Forum or Irc and ask for support.", "Found Corrupted Files!",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        Console.WriteLine("[ERROR] Mooege can't run and MadCow was unable to handle the exception.");
                        Console.WriteLine(ErrorFinder.errorFileName);
                    }
                }
                else
                {
                    //nothing
                }
            }
            else
            {
                //Nothing!
                //If the user closes Repo selection and we already went through fixing the MPQ, then Mooege.log will not exist and
                //Madcow would crash when trying to read mooege.log.
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Copy MPQ files from D3 to MadCow MPQ Folder.
        ///////////////////////////////////////////////////////////
        private void CopyMPQs_Click(object sender, EventArgs e)
        {
            MPQprocedure.StartCopyProcedure();
        }

        ///////////////////////////////////////////////////////////
        //Remote Server Settings
        ///////////////////////////////////////////////////////////
        #region RemoteServerSettings
        private void RemoteServer_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(Diablo3UserPathSelection.Text);
            String HostIP = remoteHostTxtBox.Text;
            String Port = remotePortTxtBox.Text;
            String ServerHost = HostIP + @":" + Port;
            proc1.StartInfo.Arguments = @" -launch -auroraaddress " + ServerHost;
            MessageBox.Show(proc1.StartInfo.Arguments);
            proc1.Start();
            Console.WriteLine("Starting Diablo...");
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Server Control Settings
        ///////////////////////////////////////////////////////////
        #region ServerControlSettings
        private void RestoreDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Restore Default Server Control Settings
            BnetServerIp.Text = "0.0.0.0";
            BnetServerPort.Text = "1345";
            GameServerIp.Text = "0.0.0.0";
            GameServerPort.Text = "1999";
            PublicServerIp.Text = "0.0.0.0";
            NATcheckBox.Checked = false;
            MotdTxtBox.Text = "Welcome to mooege development server!";
        }

        private void LaunchServer_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc2));
            t.Start();
        }

        public void ThreadProc2()
        {
            Application.Run(new RepositorySelectionServer());
            if (File.Exists(Program.programPath + @"\logs\mooege.log"))
            {
                if (ErrorFinder.SearchLogs("Fatal") == true)
                {
                    //We delete de Log file HERE. Nowhere else!.
                    DeleteHelper.Delete(0);
                    if (ErrorFinder.errorFileName.Contains("d3-update-base-"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Missing or Corrupted file [" + ErrorFinder.errorFileName + @"]" + "\nWould you like MadCow to fix this for you?", "Found corrupted file!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            //We move the user to the Help tab so he can see the progress of the download.
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            //We execute the procedure to start downloading the corrupted file @ FixMpq();
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("CoreData"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("ClientData"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            FixMpq();
                        }
                    }
                    if (ErrorFinder.errorFileName.Contains("MajorFailure"))
                    {
                        var ErrorAnswer = MessageBox.Show(@"Seems some major files are corrupted." + "\nWould you like MadCow to fix this for you?", "Found Corrupted Files!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (ErrorAnswer == DialogResult.Yes)
                        {
                            Tabs.Invoke(new Action(() =>
                            {
                                this.Tabs.SelectTab("tabPage4");
                            }
                            ));
                            DownloadSelectedMpqs.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown Exception");
                        Console.WriteLine(ErrorFinder.errorFileName);
                    }
                }
                else
                {
                    //nothing
                }
            }
            else
            {
                //Nothing!
                //If the user closes Repo selection and we already went through fixing the MPQ, then Mooege.log will not exist and
                //Madcow would crash when trying to read mooege.log.
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Timer stuff for AutoUpdate
        ///////////////////////////////////////////////////////////
        #region TimerStuff
        private void AutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Tick = (int)this.AutoUpdateValue.Value;

            if (EnableAutoUpdateBox.Checked == true)
            {
                AutoUpdateTimerLabel.Text = "Update in " + Tick + " minutes.";
                DownloadSpeedTimer.Start();
                BranchComboBox.Enabled = false;
                AutoUpdateValue.Enabled = false;
                comboBox1.Enabled = false;
                ValidateRepoButton.Enabled = false;
                UpdateMooegeButton.Visible = false;
                AutoUpdateTimerLabel.Visible = true;
            }

            else if (EnableAutoUpdateBox.Checked == false)
            {
                DownloadSpeedTimer.Stop();
                AutoUpdateTimerLabel.Text = " ";
                BranchComboBox.Enabled = true;
                AutoUpdateValue.Enabled = true;
                comboBox1.Enabled = true;
                ValidateRepoButton.Enabled = true;
                UpdateMooegeButton.Visible = true;
                AutoUpdateTimerLabel.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tick--;
            if (Tick == 0)
            {
                UpdateMooege(); //Runs Update //This was changed in the previous commit.
                Tick = (int)this.AutoUpdateValue.Value;
            }
            else
                AutoUpdateTimerLabel.Text = "Update in " + Tick + " minutes.";
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Diablo Path Stuff
        ///////////////////////////////////////////////////////////
        #region Diablo3Path
        private void InitializeFindPath()
        {
            if (File.Exists(Program.madcowINI))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["DiabloPath"].Get("D3Path");

                    if (Src.Contains("MODIFY")) //If a d3 path exist, then we wont find "MODIFY" and program will proceed.
                    {
                        Diablo3UserPathSelection.Text = "Please Select your Diablo III path.";
                        CopyMPQButton.Enabled = false;
                        PlayDiabloButton.Enabled = false;
                        remoteHostTxtBox.Enabled = false;
                        remotePortTxtBox.Enabled = false;
                        RemoteServerLaunchButton.Enabled = false;
                    }
                    else
                    {
                        Diablo3UserPathSelection.Text = Src;
                        CopyMPQButton.Enabled = true;
                        PlayDiabloButton.Enabled = true;
                        remoteHostTxtBox.Enabled = true;
                        remotePortTxtBox.Enabled = true;
                        RemoteServerLaunchButton.Enabled = true;
                        //Freezes at the start longer, but at least you get verified when you load!
                        VerifyDiablo3Version.RunWorkerAsync(); //Compares Versions of D3 with Mooege
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Something failed while trying to verify D3 Version or Writting INI");
                    Console.WriteLine(Ex);
                }
            }
        }

        //Find Diablo Path Dialog
        private void FindDiablo_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            OpenFileDialog FindD3Exe = new OpenFileDialog();
            FindD3Exe.Title = "MadCow By Wesko";
            FindD3Exe.InitialDirectory = @"C:\Program Files (x86)\Diablo III Beta\";
            FindD3Exe.Filter = "Diablo III|Diablo III.exe";
            DialogResult response = new DialogResult();
            response = FindD3Exe.ShowDialog();
            if (response == DialogResult.OK) // If user was able to locate Diablo III.exe
            {
                // Get the directory name.
                String dirName = System.IO.Path.GetDirectoryName(FindD3Exe.FileName);
                // Output Name
                Diablo3UserPathSelection.Text = FindD3Exe.FileName;
                //Bottom three are Enabled on Remote Server
                remoteHostTxtBox.Enabled = true;
                remotePortTxtBox.Enabled = true;
                RemoteServerLaunchButton.Enabled = true;

                if (File.Exists(Program.madcowINI))
                {
                    //First we modify the Mooege INI storage path.
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    IConfig config = source.Configs["DiabloPath"];
                    config.Set("D3Path", Diablo3UserPathSelection.Text);
                    IConfig config1 = source.Configs["DiabloPath"];
                    config1.Set("MPQpath", new FileInfo(Diablo3UserPathSelection.Text).DirectoryName + "\\Data_D3\\PC\\MPQs");
                    source.Save();
                    Console.WriteLine("Saved Diablo 3 client path.");
                    Console.WriteLine("Verifying Diablo...");
                }

                VerifyDiablo3Version.RunWorkerAsync(); //Compares versions of D3 with Mooege
            }//If the user opens the dialog to select a d3 path and he closes the dialog but already had a d3 path, then no warning will be triggered. (BUG FIXED-wesko)
            else if (response == DialogResult.Cancel && this.Diablo3UserPathSelection.TextLength == 35)//If user didn't select a Diablo III.exe, we show a warning and ofc, we dont save any path.
            {
                MessageBox.Show("You didn't select a Diablo III client", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        /////////////////////////////////
        //DOWNLOAD SOURCE FROM REPOSITORY
        /////////////////////////////////
        #region DownloadRepository
        public static String selectedBranch;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }
            //We get the selected branch first.
            BranchComboBox.Invoke(new Action(() => { selectedBranch = BranchComboBox.SelectedItem.ToString(); }));
            Uri url = new Uri(ParseRevision.revisionUrl + "/zipball/" + selectedBranch);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            if (Program.proxyEnabled)
                request.Proxy = proxy;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            // Gets bytes.
            Int64 iSize = response.ContentLength;

            // Keeping track of downloaded bytes.
            Int64 iRunningByteTotal = 0;

            // Open Webclient.
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                if (Program.proxyEnabled)
                    client.Proxy = proxy;
                // Open the file at the remote path.
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(ParseRevision.revisionUrl + "/zipball/" + selectedBranch)))
                {
                    // We write those files into the file system.
                    using (Stream streamLocal = new FileStream(Program.programPath + "/Repositories/Mooege.zip", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // Loop the stream and get the file into the byte buffer
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            // Write the bytes to the file system at the file path specified
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            // Calculate the progress out of a base "100"
                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);

                            // Update the progress bar
                            DownloadRepository.ReportProgress(iProgressPercentage);
                        }

                        // Clean up the file stream
                        streamLocal.Close();
                    }

                    // Close the connection to the remote server
                    streamRemote.Close();
                }
            }

        }

        //UPDATE PROGRESS BAR
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadRepoBar.Value = e.ProgressPercentage;
        }

        //PROCEED WITH THE PROCESS ONCE THE DOWNLOAD ITS COMPLETE
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //We reset progressbar value after finishing.
            Console.WriteLine("Download Complete!");
            if (File.Exists(Program.madcowINI))
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                String Src = source.Configs["Balloons"].Get("ShowBalloons");

                if (Src.Contains("1"))
                {
                    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Download Complete!", ToolTipIcon.Info);
                }
            }
            DownloadRepoBar.Value = 0;
            DownloadRepoBar.Update();
            generalProgressBar.PerformStep();
            MadCowProcedure.RunWholeProcedure();
            UpdateMooegeButton.Enabled = true;
            if (EnableAutoUpdateBox.Checked == true)
            {
                Tick = (int)this.AutoUpdateValue.Value;
                DownloadSpeedTimer.Start();
                AutoUpdateTimerLabel.Text = "Update in " + Tick + " minutes.";
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //ResetRepoFolder
        ///////////////////////////////////////////////////////////////////////
        private void ResetRepoFolder_Click(object sender, EventArgs e)
        {
            DeleteHelper.Delete(1);
        }

        ///////////////////////////////////////////////////////////
        //Verify Diablo 3 Version compared to Mooege supported one.////////////
        ///////////////////////////////////////////////////////////////////////
        #region VerifyDiabloVersions
        //We open a WebClient in the backgroundworker so it doesnt freeze MadCow UI.
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }

            try
            {
                WebClient client = new WebClient();
                if (Program.proxyEnabled)
                    client.Proxy = proxy;
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Checkversions);
                Uri uri = new Uri("https://raw.github.com/mooege/mooege/master/src/Mooege/Common/Versions/VersionInfo.cs");
                client.DownloadStringAsync(uri);
            }
            catch
            {
                Console.WriteLine("Check yor internet connection");
            }
        }

        //After Asyn download complete, we proceed to parse the required version by Mooege in VersionInfo.cs.
        private void Checkversions(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (File.Exists(Diablo3UserPathSelection.Text))
                {
                    String parseVersion = e.Result;
                    FileVersionInfo d3Version = FileVersionInfo.GetVersionInfo(Diablo3UserPathSelection.Text);
                    Int32 ParsePointer = parseVersion.IndexOf("RequiredPatchVersion = ");
                    String MooegeVersion = parseVersion.Substring(ParsePointer + 23, 4); //Gets required version by Mooege
                    MooegeSupportedVersion = MooegeVersion; //Public String to display over D3 path validation.
                    int CurrentD3VersionSupported = Convert.ToInt32(MooegeVersion);
                    int LocalD3Version = d3Version.FilePrivatePart;

                    //DISABLED MATCHING CLIENT VERSIONS FOR NOW.
                    if (LocalD3Version == CurrentD3VersionSupported || LocalD3Version != CurrentD3VersionSupported)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Found the correct Mooege supported version of Diablo III [" + CurrentD3VersionSupported + "]");
                        Console.ForegroundColor = ConsoleColor.White;
                        PlayDiabloButton.Invoke(new Action(() =>
                        {
                            PlayDiabloButton.Enabled = true;
                        }
                        ));
                        CopyMPQButton.Invoke(new Action(() =>
                        {
                            CopyMPQButton.Enabled = true;
                        }
                        ));
                    }
                    //If the versions missmatch:
                    /*else if (LocalD3Version != CurrentD3VersionSupported)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong client version FOUND!");
                        Console.ForegroundColor = ConsoleColor.White;
                        MessageBox.Show("You need Diablo III Client version [" + MooegeSupportedVersion + "] in order to play over Mooege.\nPlease Update.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        PlayDiabloButton.Invoke(new Action(() =>
                        {
                            PlayDiabloButton.Enabled = false;
                        }
                        ));
                        CopyMPQButton.Invoke(new Action(() =>
                        {
                            CopyMPQButton.Enabled = false;
                        }
                        ));
                    }*/
                }
                else //If the user, once a Diablo 3 client path saved, deletes the Folder or moves it to another place, we go back to blank values over Diablo 3 path.
                {
                    IConfigSource madcowIni = new IniConfigSource(Program.madcowINI);
                    madcowIni.Configs["DiabloPath"].Set("D3Path", "MODIFY");
                    madcowIni.Configs["DiabloPath"].Set("MPQpath", "");
                    madcowIni.ReplaceKeyValues();
                    madcowIni.Save();
                    MessageBox.Show("Could not find Diablo III.exe, please select the proper path again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Diablo3UserPathSelection.Invoke(new Action(() => { this.Diablo3UserPathSelection.Text = "Please Select your Diablo III path."; }));
                    PlayDiabloButton.Invoke(new Action(() => { PlayDiabloButton.Enabled = false; }));
                    CopyMPQButton.Invoke(new Action(() => { CopyMPQButton.Enabled = false; }));
                }
            }
            catch
            {
                Console.WriteLine("[ERROR] Internet connection failed or GitHub site is down!");
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //Save Profile (We write a .mdc (Madcow :P) file into "ServerProfiles" Folder. //We validate first the IP & Port Entries.
        ///////////////////////////////////////////////////////////////////////
        #region SaveProfile
        private void SaveProfile_Click(object sender, EventArgs e)
        {
            //Match Pattern
            string pattern = @"(([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|255[0-5])\.){3}([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])";
            Regex check = new Regex(pattern);
            string[] ipFields = { this.BnetServerIp.Text, this.GameServerIp.Text, this.PublicServerIp.Text };
            int i = 0; //Foreach IP counter.
            int j = 0; //Foreach PORT counter.
            Boolean invalidIP = false;

            //Error handling if textbox null -> Giving feedback in the textbox field.
            for (int x = 0; x < ipFields.Length; x++)
            {
                if (string.IsNullOrEmpty(ipFields[x]))
                {
                    IpErrorHandler(x);
                    invalidIP = true;
                }
            }

            //VALIDATION FOR IP
            if (invalidIP == false)
            {
                foreach (string value in ipFields)
                {
                    for (int Lp = 0; Lp < 999; Lp++)
                    {
                        string IPAddress = String.Format("{0}.{0}.{0}.{0}", Lp);

                        if (Regex.Match(ipFields[i], pattern).Success)
                        {
                            IpCorrectHandler(i);
                        }
                        else
                        {
                            IpErrorHandler(i);
                            invalidIP = true;
                            break;
                        }
                    }
                    i++;
                }
            }

            //VALIDATION FOR PORT

            string[] portFields = { this.BnetServerPort.Text, this.GameServerPort.Text };
            int Number;
            bool isNumber;

            for (int x = 0; x < portFields.Length; x++)
            {
                if (string.IsNullOrEmpty(portFields[x]))
                {
                    PortErrorHandler(x);
                    invalidIP = true;
                }
            }

            foreach (string value in portFields)
            {
                isNumber = Int32.TryParse(portFields[j], out Number);

                if (!isNumber || portFields[j].Length > 4 || portFields[j].Length < 4)
                {
                    PortErrorHandler(j);
                    invalidIP = true;
                }
                else
                {
                    PortCorrectHandler(j);
                }
                j++;
            }

            if (invalidIP == false)
            {
                //If invalidIP == false which means all fields are valid, we hide any error cross that might be around.
                this.ErrorBnetServerIp.Visible = false;
                this.ErrorBnetServerPort.Visible = false;
                this.ErrorGameServerIp.Visible = false;
                this.ErrorGameServerPort.Visible = false;
                this.ErrorGameServerPort.Visible = false;
                this.ErrorPublicServerIp.Visible = false;

                //If invalidIP == false which means all fields are valid, we show all green ticks!
                this.TickBnetServerIP.Visible = true;
                this.TickBnetServerPort.Visible = true;
                this.TickGameServerIp.Visible = true;
                this.TickGameServerPort.Visible = true;
                this.TickGameServerPort.Visible = true;
                this.TickPublicServerIp.Visible = true;

                //We proceed to ask the user where to save the file.
                SaveFileDialog saveProfile = new SaveFileDialog();
                saveProfile.Title = "Save Server Profile";
                saveProfile.DefaultExt = ".mdc";
                saveProfile.Filter = "MadCow Profile|*.mdc";
                saveProfile.InitialDirectory = Program.programPath + @"\ServerProfiles";
                saveProfile.ShowDialog();

                if (saveProfile.FileName == "")
                {
                    Console.WriteLine("You didn't specify a profile name");
                }

                else
                {
                    CurrentProfile = saveProfile.FileName; //We set the global string value, we will grab this value from RepositorySelectionServer.
                    TextWriter tw = new StreamWriter(saveProfile.FileName);
                    //tw.WriteLine("Bnet Server Ip");
                    tw.WriteLine(this.BnetServerIp.Text);
                    //tw.WriteLine("Game Server Ip");
                    tw.WriteLine(this.GameServerIp.Text);
                    //tw.WriteLine("Public Server Ip");
                    tw.WriteLine(this.PublicServerIp.Text);
                    //tw.WriteLine("Bnet Server Port");
                    tw.WriteLine(this.BnetServerPort.Text);
                    //tw.WriteLine("Game Server Port");
                    tw.WriteLine(this.GameServerPort.Text);
                    //tw.WriteLine("MOTD");
                    tw.WriteLine(this.MotdTxtBox.Text);
                    //tw.WriteLine("NAT");
                    tw.WriteLine(this.NATcheckBox.Checked);
                    tw.Close();
                    Console.WriteLine("Saved profile [" + Path.GetFileName(saveProfile.FileName) + "] succesfully.");
                    //Proceed to save the profile over our INI file.
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    source.Configs["Profiles"].Set("Profile", CurrentProfile);
                    source.Save();
                }
            }
        }

        private void IpErrorHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerIp
                    this.BnetServerIp.Text = "Invalid IP";
                    this.ErrorBnetServerIp.Visible = true;
                    this.TickBnetServerIP.Visible = false;
                    break;
                case 1: //GameServerIp
                    this.GameServerIp.Text = "Invalid IP";
                    this.ErrorGameServerIp.Visible = true;
                    this.TickGameServerIp.Visible = false;
                    break;
                case 2: //PublicServerIp
                    this.PublicServerIp.Text = "Invalid IP";
                    this.ErrorPublicServerIp.Visible = true;
                    this.TickPublicServerIp.Visible = false;
                    break;
            }
        }

        private void IpCorrectHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerIp
                    this.ErrorBnetServerIp.Visible = false;
                    this.TickBnetServerIP.Visible = true;
                    break;
                case 1: //GameServerIp
                    this.ErrorGameServerIp.Visible = false;
                    this.TickGameServerIp.Visible = true;
                    break;
                case 2: //PublicServerIp
                    this.ErrorPublicServerIp.Visible = false;
                    this.TickPublicServerIp.Visible = true;
                    break;
            }
        }

        private void PortErrorHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerPort
                    this.BnetServerPort.Text = "Invalid Port";
                    this.ErrorBnetServerPort.Visible = true;
                    this.TickBnetServerPort.Visible = false;
                    break;
                case 1: //GameServerPort
                    this.GameServerPort.Text = "Invalid Port";
                    this.ErrorGameServerPort.Visible = true;
                    this.TickGameServerPort.Visible = false;
                    break;
            }
        }

        private void PortCorrectHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerPort
                    this.ErrorBnetServerPort.Visible = false;
                    this.TickBnetServerPort.Visible = true;
                    break;
                case 1: //GameServerPort
                    this.ErrorGameServerPort.Visible = false;
                    this.TickGameServerPort.Visible = true;
                    break;
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //Load Profile - We load from a .mdc file, update CurrenProfile variable and parse the values into the boxes.
        ///////////////////////////////////////////////////////////////////////
        #region LoadProfile
        private void LoadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenProfile = new OpenFileDialog();
            OpenProfile.Title = "Save Server Profile";
            OpenProfile.Filter = "MadCow Profile|*.mdc";
            OpenProfile.InitialDirectory = Program.programPath + @"\ServerProfiles";
            OpenProfile.ShowDialog();
            if (OpenProfile.FileName == "")
            {
                Console.WriteLine("You didn't select a profile name");
            }

            else
            {
                CurrentProfile = OpenProfile.FileName; //We set the global string value, we will grab this value from RepositorySelectionServer.
                TextReader tr = new StreamReader(OpenProfile.FileName);
                this.BnetServerIp.Text = tr.ReadLine();
                this.GameServerIp.Text = tr.ReadLine();
                this.PublicServerIp.Text = tr.ReadLine();
                this.BnetServerPort.Text = tr.ReadLine();
                this.GameServerPort.Text = tr.ReadLine();
                this.MotdTxtBox.Text = tr.ReadLine();
                if (tr.ReadLine().Contains("True"))
                {
                    this.NATcheckBox.Checked = true;
                }
                else
                {
                    this.NATcheckBox.Checked = false;
                }
                tr.Close();
                //Loading a profile means it has the correct values for every box, so first we disable every red cross that might be out there.
                this.ErrorBnetServerIp.Visible = false;
                this.ErrorBnetServerPort.Visible = false;
                this.ErrorGameServerIp.Visible = false;
                this.ErrorGameServerPort.Visible = false;
                this.ErrorGameServerPort.Visible = false;
                this.ErrorPublicServerIp.Visible = false;
                //Loading a profile means it has the correct values for every box, so we change everything to green ticked.
                this.TickBnetServerIP.Visible = true;
                this.TickBnetServerPort.Visible = true;
                this.TickGameServerIp.Visible = true;
                this.TickGameServerPort.Visible = true;
                this.TickGameServerPort.Visible = true;
                this.TickPublicServerIp.Visible = true;
                Console.WriteLine("Loaded Profile [" + Path.GetFileName(OpenProfile.FileName) + "] succesfully.");
                //Proceed to save the profile over our INI file.
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                source.Configs["Profiles"].Set("Profile", CurrentProfile);
                source.Save();
            }
        }

        public void LoadLastUsedProfile()
        {
            try
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                var _readProfile = source.Configs["Profiles"].Get("Profile");
                Form1.CurrentProfile = source.Configs["Profiles"].Get("Profile");

                if (_readProfile.Length > 0)
                {
                    TextReader tr = new StreamReader(_readProfile);
                    this.BnetServerIp.Text = tr.ReadLine();
                    this.GameServerIp.Text = tr.ReadLine();
                    this.PublicServerIp.Text = tr.ReadLine();
                    this.BnetServerPort.Text = tr.ReadLine();
                    this.GameServerPort.Text = tr.ReadLine();
                    this.MotdTxtBox.Text = tr.ReadLine();
                    if (tr.ReadLine().Contains("True"))
                    {
                        this.NATcheckBox.Checked = true;
                    }
                    else
                    {
                        this.NATcheckBox.Checked = false;
                    }
                    tr.Close();

                    //Loading a profile means it has the correct values for every box, so first we disable every red cross that might be out there.
                    this.ErrorBnetServerIp.Visible = false;
                    this.ErrorBnetServerPort.Visible = false;
                    this.ErrorGameServerIp.Visible = false;
                    this.ErrorGameServerPort.Visible = false;
                    this.ErrorGameServerPort.Visible = false;
                    this.ErrorPublicServerIp.Visible = false;
                    //Loading a profile means it has the correct values for every box, so we change everything to green ticked.
                    this.TickBnetServerIP.Visible = true;
                    this.TickBnetServerPort.Visible = true;
                    this.TickGameServerIp.Visible = true;
                    this.TickGameServerPort.Visible = true;
                    this.TickGameServerPort.Visible = true;
                    this.TickPublicServerIp.Visible = true;
                    Console.WriteLine("Loaded server profile [" + Path.GetFileName(Form1.CurrentProfile) + "] succesfully.");
                }
            }
            catch
            {
                Console.WriteLine("[Error] While loading server profile.");
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //URL TEXT FIELD COLOR MANAGEMENT
        //This has the function on turning letters red if Error, Black if normal.
        ////////////////////////////////////////////////////////////////////////
        #region ErrorColorHandler
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string currentText = ParseRevision.commitFile;
            BranchComboBox.Visible = false;
            RepositoryHintLabel.Visible = true;
            BranchSelectionLabel.Visible = false;
            UpdateMooegeButton.Enabled = false;
            AutoUpdateValue.Enabled = false; //If user is typing a new URL Update and Autoupdate
            EnableAutoUpdateBox.Enabled = false;      //Functions gets disabled
            try
            {
                if (currentText == "Incorrect repository entry")
                {
                    this.comboBox1.ForeColor = Color.Red;
                }
                else
                {
                    comboBox1.ForeColor = Color.Black;
                    pictureBox1.Hide();//Error Image (Cross)
                    pictureBox2.Hide();//Correct Image (Tick)
                }
            }
            catch
            {
                comboBox1.ForeColor = SystemColors.ControlText;
            }
        }

        //Color handler for BnetServerIp
        private void BnetServerIp_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.BnetServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    this.BnetServerIp.ForeColor = Color.Red;
                }
                else
                {
                    this.BnetServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                this.BnetServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for GameServerIp
        private void GameServerIp_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.GameServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    this.GameServerIp.ForeColor = Color.Red;
                }
                else
                {
                    this.GameServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                this.GameServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for PublicServerIp
        private void PublicServerIp_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.PublicServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    this.PublicServerIp.ForeColor = Color.Red;
                }
                else
                {
                    this.PublicServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                this.PublicServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for BnetServerPort
        private void BnetServerPort_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.BnetServerPort.Text;
            try
            {
                if (currentText == "Invalid Port")
                {
                    Console.WriteLine("Check for input errors!");
                    this.BnetServerPort.ForeColor = Color.Red;
                }
                else
                {
                    this.BnetServerPort.ForeColor = Color.Black;
                }
            }
            catch
            {
                this.BnetServerPort.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for GameServerPort
        private void GameServerPort_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.GameServerPort.Text;
            try
            {
                if (currentText == "Invalid Port")
                {
                    Console.WriteLine("Check for input errors!");
                    this.GameServerPort.ForeColor = Color.Red;
                }
                else
                {
                    this.GameServerPort.ForeColor = Color.Black;
                }
            }
            catch
            {
                this.GameServerPort.ForeColor = SystemColors.ControlText;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Download MPQs selected by the user.
        ////////////////////////////////////////////////////////////////////////
        #region DownloadMpqsBySelection
        private void DownloadMPQSButton_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(MPQThread));
            t.Start();
        }

        private void MPQThread()
        {
            Application.Run(new MPQDownloader());
        }

        private void DownloadMPQS(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            int i = 0; //We use this variable to select save path destination.            
            //Will use this to determinate the correct save path.
            String[] mpqDestination = {
                                          Path.Combine(source.Configs["DiabloPath"].Get("MPQDest"), @"base\"),
                                          source.Configs["DiabloPath"].Get("MPQDest")
                                      };
            Stopwatch speedTimer = new Stopwatch();
            foreach (string value in MPQDownloader.mpqSelection)
            {
                Uri url = new Uri(value);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                if (Program.proxyEnabled)
                    request.Proxy = proxy;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                //Parsing the file name.
                var fullName = url.LocalPath.TrimStart('/');
                var name = Path.GetFileNameWithoutExtension(fullName);
                var ext = Path.GetExtension(fullName);
                //End Parsing.              
                response.Close();
                //Setting save path
                if (name == "CoreData" || name == "ClientData") i = 1; //Path \MPQ\
                else i = 0; //Path \MPQ\base\

                Int64 iSize = response.ContentLength;
                Int64 iRunningByteTotal = 0;

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (Program.proxyEnabled)
                        client.Proxy = proxy;
                    using (System.IO.Stream streamRemote = client.OpenRead(new Uri(value)))
                    {
                        using (Stream streamLocal = new FileStream(mpqDestination[i] + @"\" + name + ext, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            //We start the timer to measure speed - This still needs testing not sure if speed its accuarate. - wesko
                            speedTimer.Start();
                            DownloadFileNameLabel.Invoke(new Action(() =>
                            {
                                this.DownloadFileNameLabel.Text = "Downloading File: " + name + ext;
                            }
                            ));

                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;

                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);

                                //We calculate the download speed.
                                TimeSpan ts = speedTimer.Elapsed;
                                double bytesReceivedSpeed = (iRunningByteTotal / 1024) / ts.TotalSeconds;
                                DownloadSpeedLabel.Invoke(new Action(() =>
                                {
                                    this.DownloadSpeedLabel.Text = "Downloading Speed: " + Convert.ToInt32(bytesReceivedSpeed) + "Kbps";
                                }
                                ));
                                DownloadSelectedMpqs.ReportProgress(iProgressPercentage);
                            }
                            streamLocal.Close();
                        }
                        streamRemote.Close();
                    }
                }
            } speedTimer.Stop();
        }

        private void downloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadFileNameLabel.Visible = true;
            DownloadSpeedLabel.Visible = true;
            DownloadMPQSprogressBar.Value = e.ProgressPercentage;
        }

        private void downloader_DownloadedComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            DownloadFileNameLabel.Visible = false;
            DownloadSpeedLabel.Visible = false;
            MPQDownloader.mpqSelection.Clear();//Reset the Array values after downloading.
            Console.WriteLine("Download complete.");
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Download Mpq files that could not be parsed by Mooege. TODO: Merge the patch handling in one function.
        ////////////////////////////////////////////////////////////////////////
        #region DownloadMpqFromErrorFinder
        public void FixMpq()
        {
            DeleteHelper.Delete(2); //Delete corrupted file.
            ErrorFilesDownloaders.RunWorkerAsync();
        }

        private void DownloadSpecificMPQS(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }

            var downloadBaseFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/" + ErrorFinder.errorFileName + @".MPQ";
            var downloadFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + ErrorFinder.errorFileName + @".mpq";

            IConfigSource source = new IniConfigSource(Program.madcowINI);
            String downloadDestination = source.Configs["DiabloPath"].Get("MPQpath");
            Stopwatch speedTimer = new Stopwatch();

            if (ErrorFinder.errorFileName.Contains("CoreData") || ErrorFinder.errorFileName.Contains("ClientData"))
            {
                Uri url = new Uri(downloadFileUrl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                if (Program.proxyEnabled)
                    request.Proxy = proxy;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                //Parsing the file name.
                var fullName = url.LocalPath.TrimStart('/');
                var name = Path.GetFileNameWithoutExtension(fullName);
                var ext = Path.GetExtension(fullName);
                //End Parsing.
                response.Close();
                Int64 iSize = response.ContentLength;
                Int64 iRunningByteTotal = 0;

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (Program.proxyEnabled)
                        client.Proxy = proxy;
                    using (System.IO.Stream streamRemote = client.OpenRead(new Uri(downloadFileUrl)))
                    {
                        using (Stream streamLocal = new FileStream(downloadDestination + @"\" + name + ext, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            Console.WriteLine("Starting download...");
                            speedTimer.Start();
                            DownloadFileNameLabel.Invoke(new Action(() =>
                            {
                                this.DownloadFileNameLabel.Text = "Downloading File: " + name + ext;
                            }
                            ));

                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;

                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);

                                //We calculate the download speed.
                                TimeSpan ts = speedTimer.Elapsed;
                                double bytesReceivedSpeed = (iRunningByteTotal / 1024) / ts.TotalSeconds;
                                DownloadSpeedLabel.Invoke(new Action(() =>
                                {
                                    this.DownloadSpeedLabel.Text = "Downloading Speed: " + Convert.ToInt32(bytesReceivedSpeed) + "Kbps";
                                }
                                ));
                                ErrorFilesDownloaders.ReportProgress(iProgressPercentage);
                            }
                            streamLocal.Close();
                        }
                        streamRemote.Close();
                    }
                }
                speedTimer.Stop();
                File.Copy(downloadDestination + @"\" + ErrorFinder.errorFileName + @".mpq", Program.programPath + @"\MPQ\" + ErrorFinder.errorFileName + @".mpq", true);
            }
            else
            {
                Uri url = new Uri(downloadBaseFileUrl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                if (Program.proxyEnabled)
                    request.Proxy = proxy;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                //Parsing the file name.
                var fullName = url.LocalPath.TrimStart('/');
                var name = Path.GetFileNameWithoutExtension(fullName);
                var ext = Path.GetExtension(fullName);
                //End Parsing.
                response.Close();
                Int64 iSize = response.ContentLength;
                Int64 iRunningByteTotal = 0;

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (Program.proxyEnabled)
                        client.Proxy = proxy;
                    using (System.IO.Stream streamRemote = client.OpenRead(new Uri(downloadBaseFileUrl)))
                    {
                        using (Stream streamLocal = new FileStream(downloadDestination + @"\base\" + name + ext, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            Console.WriteLine("Starting download...");
                            speedTimer.Start();
                            DownloadFileNameLabel.Invoke(new Action(() =>
                            {
                                this.DownloadFileNameLabel.Text = "Downloading File: " + name + ext;
                            }
                            ));

                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;

                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);

                                //We calculate the download speed.
                                TimeSpan ts = speedTimer.Elapsed;
                                double bytesReceivedSpeed = (iRunningByteTotal / 1024) / ts.TotalSeconds;
                                DownloadSpeedLabel.Invoke(new Action(() =>
                                {
                                    this.DownloadSpeedLabel.Text = "Downloading Speed: " + Convert.ToInt32(bytesReceivedSpeed) + "Kbps";
                                }
                                ));
                                ErrorFilesDownloaders.ReportProgress(iProgressPercentage);
                            }
                            streamLocal.Close();
                        }
                        streamRemote.Close();
                    }
                }
                speedTimer.Stop();
                File.Copy(downloadDestination + @"\base\" + ErrorFinder.errorFileName + @".MPQ", Program.programPath + @"\MPQ\" + @"\base\" + ErrorFinder.errorFileName + @".MPQ", true);
            }
        }

        private void downloader_ProgressChanged2(object sender, ProgressChangedEventArgs e)
        {
            DownloadFileNameLabel.Invoke(new Action(() =>
            {
                DownloadFileNameLabel.Visible = true;
                DownloadSpeedLabel.Visible = true;
                DownloadMPQSprogressBar.Value = e.ProgressPercentage;
            }
            ));
        }

        private void downloader_DownloadedComplete2(object sender, RunWorkerCompletedEventArgs e)
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            String downloadSource = source.Configs["DiabloPath"].Get("MPQpath");
            String downloadDestination = Path.Combine(source.Configs["DiabloPath"].Get("MPQDest"), @"base\");
            DownloadFileNameLabel.Invoke(new Action(() =>
            {
                DownloadFileNameLabel.Visible = false;
                DownloadSpeedLabel.Visible = false;
            }
            ));
            Console.WriteLine("Download complete.");
            try
            {
                //File.Copy(downloadSource + @"\base\" + ErrorFinder.errorFileName + @".MPQ", downloadDestination + ErrorFinder.errorFileName + @".MPQ", true);
                //Console.WriteLine("Copied new MPQ to MadCow MPQ home Folder.");
                //We give the user an announce of success.
                DialogResult response = DotNetPerls.BetterDialog.ShowDialog("MadCow Worker",
                "MadCow succesfully fixed:",
                ErrorFinder.errorFileName + @".MPQ",
                "", "OK", Properties.Resources.correct);
                if (response == DialogResult.Cancel) //We take this as the OK response.
                {
                    //Since problem must be fixed, we take the user to the Update tab & execute repo selection again
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() =>
                    {
                        this.Tabs.SelectTab("tabPage1");
                    }
                    ));
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                    t.Start();
                }
            }
            catch
            {
                Console.WriteLine("Error while copying new MPQ to MadCow MPQ home Folder");
            }

        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Dynamically Add Repos, but also remove duplicates.
        ////////////////////////////////////////////////////////////////////////
        #region Repositories
        private Int32 RepoListIndex;

        private void RepoList()
        {
            StreamReader sr = new StreamReader(Program.programPath + @"\Tools\RepoList.txt");
            string line = sr.ReadLine();

            while (line != null)
            {
                comboBox1.Items.Add(line);
                line = sr.ReadLine();
                RepoListIndex++;
            }
            sr.Close();
        }
        private void RepoCheck()
        {
            string[] lines = File.ReadAllLines(Program.programPath + @"\Tools\RepoList.txt");
            File.WriteAllLines(Program.programPath + @"\Tools\RepoList.txt", lines.Distinct().ToArray());
        }

        private void RepoListAdd()
        {
            StreamWriter str;
            str = File.AppendText(Program.programPath + @"\Tools\RepoList.txt");
            str.WriteLine(comboBox1.Text);
            str.Close();
        }

        private void RepoListUpdate()
        {
            RepoCheck();
            comboBox1.Items.Clear();
            StreamReader sr = new StreamReader(Program.programPath + @"\Tools\RepoList.txt");
            string line = sr.ReadLine();

            while (line != null)
            {
                comboBox1.Items.Add(line);
                line = sr.ReadLine();
                RepoListIndex++;
            }
            sr.Close();
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Changelog.
        ////////////////////////////////////////////////////////////////////////
        #region Changelog
        //Fill Changelog Repository ComboBox.
        private void Changelog()
        {
            StreamReader sr = new StreamReader(Program.programPath + @"\Tools\RepoList.txt");
            string line = sr.ReadLine();

            while (line != null)
            {
                string s = line.Replace(@"https://github.com/", "");
                string d = s.Replace(@"/mooege", "");
                string e = d.Replace(@"/d3sharp", "");
                SelectRepoChngLogComboBox.Items.Add(e);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        //Update Changelog repository list in real time.
        private void ChangelogListUpdate()
        {
            SelectRepoChngLogComboBox.Items.Clear();
            StreamReader sr = new StreamReader(Program.programPath + @"\Tools\RepoList.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                string s = line.Replace(@"https://github.com/", "");
                string d = s.Replace(@"/mooege", "");
                string e = d.Replace(@"/d3sharp", "");
                SelectRepoChngLogComboBox.Items.Add(e);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        //Parse commit file and display into the textbox.
        private void DisplayChangelog(object sender, AsyncCompletedEventArgs e)
        {
            ChangeLogTxtBox.Invoke(new Action(() => { ChangeLogTxtBox.Clear(); }));
            using (FileStream fileStream = new FileStream(Program.programPath + @"\Commits.ATOM", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string line;
                    int i = 0; //This is to get rid of the first <title> tag.
                    while ((line = reader.ReadLine()) != null)
                    {
                        //For commits comment.
                        if (System.Text.RegularExpressions.Regex.IsMatch(line, "<title>") && i > 0)
                        {
                            var regex = new Regex("<title>(.*)</title>");
                            var match = regex.Match(line);
                            ChangeLogTxtBox.Invoke(new Action(() => { ChangeLogTxtBox.AppendText(i + @".-" + match.Groups[1].Value + "\n"); }));
                            i++;
                        }
                        else if (System.Text.RegularExpressions.Regex.IsMatch(line, "<title>") && i == 0)
                        {
                            i++;
                        }

                        //For update date/time.
                        if (System.Text.RegularExpressions.Regex.IsMatch(line, "<updated>") && i > 1)
                        {
                            var regex = new Regex("<updated>(.*)</updated>");
                            var match = regex.Match(line);
                            ChangeLogTxtBox.Invoke(new Action(() => { ChangeLogTxtBox.AppendText(@"Updated: " + match.Groups[1].Value + "\n"); }));
                        }

                        //For developer that pushed.
                        if (System.Text.RegularExpressions.Regex.IsMatch(line, "<name>") && i > 0)
                        {
                            var regex = new Regex("<name>(.*)</name>");
                            var match = regex.Match(line);
                            ChangeLogTxtBox.Invoke(new Action(() =>
                            {
                                ChangeLogTxtBox.AppendText(@"Author: " + match.Groups[1].Value + "\n");
                                ChangeLogTxtBox.AppendText(Environment.NewLine);
                            }));
                        }
                    }
                }
            }
            //We scroll the content up to show latest commits first.
            ChangeLogTxtBox.Invoke(new Action(() =>
            {
                ChangeLogTxtBox.SelectionStart = 0;
                ChangeLogTxtBox.ScrollToCaret();
            }));
        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }

            try
            {
                WebClient client = new WebClient();
                if (Program.proxyEnabled)
                    client.Proxy = proxy;
                client.DownloadFileAsync(new Uri(selectedRepo + @"/commits/master.atom"), @"Commits.atom");
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DisplayChangelog);
            }
            catch
            {
                Console.WriteLine("Check yor internet connection");
            }
        }

        public String selectedRepo = "";
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //We first parse the repo name selected by the user.
            selectedRepo = this.SelectRepoChngLogComboBox.Text;

            //We search for that repo URL over RepoList.txt
            StreamReader sr = new StreamReader(Program.programPath + @"\Tools\RepoList.txt");
            string line = sr.ReadLine();

            while (line != null)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(line, selectedRepo))
                {
                    //Pass the whole URL to selectedRepo string that we will use to create the new Uri.
                    selectedRepo = line;
                }
                line = sr.ReadLine();
            }
            sr.Close();
            //Proceed to download the commit file and parse.
            ChangelogDownloader.RunWorkerAsync();
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Tray Icon Stuff
        ////////////////////////////////////////////////////////////////////////
        #region TrayIcon
        public Int32 notifyCount = 0;

        public void loadTrayMenu()
        {
            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0, new MenuItem("Check Updates", new System.EventHandler(Tray_CheckUpdates)));
            m_menu.MenuItems.Add(1, new MenuItem("Show", new System.EventHandler(Show_Click)));
            m_menu.MenuItems.Add(2, new MenuItem("Hide", new System.EventHandler(Hide_Click)));
            m_menu.MenuItems.Add(3, new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            MadCowTrayIcon.ContextMenu = m_menu;
        }

        private void Tray_CheckUpdates(object sender, EventArgs e)
        {
            if (UpdateMooegeButton.Enabled == true)
            {
                UpdateMooege();
            }
            else
                if (File.Exists(Program.madcowINI))
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["Balloons"].Get("ShowBalloons");

                    if (Src.Contains("1"))
                    {
                        MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "You must select and validate a repository first!", ToolTipIcon.Info);
                    }
                }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            String Balloons = source.Configs["Balloons"].Get("ShowBalloons");
            String TrayIcon = source.Configs["Tray"].Get("Enabled");

            if (FormWindowState.Minimized == WindowState)
            {
                if (TrayIcon.Contains("1"))
                {
                    Hide();
                    if (File.Exists(Program.madcowINI))
                    {
                        if (Balloons.Contains("1"))
                        {
                            if (notifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
                            {
                                MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "MadCow will continue running minimized.", ToolTipIcon.Info);
                                notifyCount++;
                            }
                        }
                    }
                }
            }
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
            if (File.Exists(Program.madcowINI))
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                String Src = source.Configs["Balloons"].Get("ShowBalloons");

                if (Src.Contains("1"))
                {
                    if (notifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
                    {
                        MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "MadCow will continue running minimized.", ToolTipIcon.Info);
                        notifyCount++;
                    }
                }
            }
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        // Branching:
        // BranchComboBox Selected Index Change. We use this to update the revision ID that we need to properly find the folder and compile.
        ////////////////////////////////////////////////////////////////////////
        #region Branches
        private void BranchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var proxy = new WebProxy();
            if (Program.proxyEnabled)
            {
                proxy.Address = new Uri(Proxy("proxyUrl"));
                proxy.Credentials = new NetworkCredential(Proxy("username"), Proxy("password"));
            }

            BranchComboBox.Invoke(new Action(() => { selectedBranch = BranchComboBox.SelectedItem.ToString(); }));
            WebClient client = new WebClient();
            if (Program.proxyEnabled)
                client.Proxy = proxy;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(BranchParse);
            Uri uri = new Uri(comboBox1.Text + "/commits/" + selectedBranch + ".atom");
            client.DownloadStringAsync(uri);
        }

        private void BranchParse(object sender, DownloadStringCompletedEventArgs e)
        {
            String result = e.Result.ToString();
            Int32 pos2 = result.IndexOf("Commit/");
            String revision = result.Substring(pos2 + 7, 7);
            ParseRevision.lastRevision = result.Substring(pos2 + 7, 7);
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        // MadCow Settings
        ////////////////////////////////////////////////////////////////////////
        #region MadCowSettings

        ////////////////////////////////////////////////////////////////////////////////////////
        // Shortcut Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region ShortCut
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.madcowINI))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["ShortCut"].Get("Shortcut");

                    if (Src.Contains("1"))
                    {
                        source.Configs["ShortCut"].Set("Shortcut", 0);
                        source.Save();
                        SrtCutStatusLabel.ResetText();
                        SrtCutStatusLabel.Text = "Disabled";
                        SrtCutStatusLabel.ForeColor = Color.DimGray;
                    }
                    else
                    {
                        source.Configs["ShortCut"].Set("Shortcut", 1);
                        source.Save();
                        SrtCutStatusLabel.ResetText();
                        SrtCutStatusLabel.Text = "Enabled";
                        SrtCutStatusLabel.ForeColor = Color.SeaGreen;
                    }
                }
                catch
                {
                    Console.WriteLine("[Error] At ShortCut Disabler.");
                }
            }
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////
        // BalloonTips Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region BalloonTips
        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.madcowINI))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["Balloons"].Get("ShowBalloons");

                    if (Src.Contains("1"))
                    {
                        source.Configs["Balloons"].Set("ShowBalloons", 0);
                        source.Save();
                        TrayNotificationsStatusLabel.ResetText();
                        TrayNotificationsStatusLabel.Text = "Disabled";
                        TrayNotificationsStatusLabel.ForeColor = Color.DimGray;
                        ChainPicture.Visible = false;
                    }
                    else
                    {
                        source.Configs["Balloons"].Set("ShowBalloons", 1);
                        source.Save();
                        TrayNotificationsStatusLabel.ResetText();
                        TrayNotificationsStatusLabel.Text = "Enabled";
                        TrayNotificationsStatusLabel.ForeColor = Color.SeaGreen;
                        //Tray Icon (We need Tray Icon Enabled)
                        source.Configs["Tray"].Set("Enabled", 1);
                        source.Save();
                        MinimizeTrayStatusLabel.ResetText();
                        MinimizeTrayStatusLabel.Text = "Enabled";
                        MinimizeTrayStatusLabel.ForeColor = Color.SeaGreen;
                        ChainPicture.Visible = true;
                    }
                }
                catch
                {
                    Console.WriteLine("[Error] At ShowBalloons Disabler.");
                }
            }
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////
        // Remember LastRepository Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region RememberLastRepository
        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.madcowINI))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["LastPlay"].Get("Enabled");

                    if (Src.Contains("1"))
                    {
                        source.Configs["LastPlay"].Set("Enabled", 0);
                        source.Save();
                        RememberLastRepoStatusLabel.ResetText();
                        RememberLastRepoStatusLabel.Text = "Disabled";
                        RememberLastRepoStatusLabel.ForeColor = Color.DimGray;
                        LastPlayedRepoReminderLabel.Visible = false;
                        ChainPicture.Visible = false;
                        BrowseMPQPathButton.Enabled = true;
                        MPQDestTextBox.Text = source.Configs["DiabloPath"].Get("MPQDest");
                    }
                    else
                    {
                        source.Configs["LastPlay"].Set("Enabled", 1);
                        source.Save();
                        RememberLastRepoStatusLabel.ResetText();
                        RememberLastRepoStatusLabel.Text = "Enabled";
                        RememberLastRepoStatusLabel.ForeColor = Color.SeaGreen;
                        LastPlayedRepoReminderLabel.Visible = true;
                        ChainPicture.Visible = false;
                        BrowseMPQPathButton.Enabled = false;
                        MPQDestTextBox.Text = "To modify this, disable Remember Last Repository over Help.";
                    }
                }
                catch
                {
                    Console.WriteLine("[Error] At LastRepository Disabler.");
                }
            }
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////
        // Tray Icon Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region TrayIcon
        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.madcowINI))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.madcowINI);
                    String Src = source.Configs["Tray"].Get("Enabled");

                    if (Src.Contains("1"))
                    {
                        //Tray Icon Functionality.
                        source.Configs["Tray"].Set("Enabled", 0);
                        source.Save();
                        MinimizeTrayStatusLabel.ResetText();
                        MinimizeTrayStatusLabel.Text = "Disabled";
                        MinimizeTrayStatusLabel.ForeColor = Color.DimGray;
                        //Balloons (We dont want balloons if we dont want tray icon)
                        source.Configs["Balloons"].Set("ShowBalloons", 0);
                        source.Save();
                        TrayNotificationsStatusLabel.ResetText();
                        TrayNotificationsStatusLabel.Text = "Disabled";
                        TrayNotificationsStatusLabel.ForeColor = Color.DimGray;
                        ChainPicture.Visible = true;

                    }
                    else
                    {
                        source.Configs["Tray"].Set("Enabled", 1);
                        source.Save();
                        MinimizeTrayStatusLabel.ResetText();
                        MinimizeTrayStatusLabel.Text = "Enabled";
                        MinimizeTrayStatusLabel.ForeColor = Color.SeaGreen;
                        ChainPicture.Visible = false;
                    }
                }
                catch
                {
                    Console.WriteLine("[Error] At LastRepository Disabler.");
                }
            }
        }
        #endregion
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Mooege Settings tab
        ////////////////////////////////////////////////////////////////////////
        #region Mooege Settings
        private void BrowseMPQPathButton_Click(object sender, EventArgs e)
        {
            if (MpqPathBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var source = new IniConfigSource(Program.madcowINI);
                source.Configs["DiabloPath"].Set("MPQDest", MpqPathBrowserDialog.SelectedPath);
                source.Save();
                MPQDestTextBox.Text = MpqPathBrowserDialog.SelectedPath;
                //We create the base folder here, else MadCow will cry somewhere.
                if (Directory.Exists(MpqPathBrowserDialog.SelectedPath + @"\base") == false)
                {
                    Directory.CreateDirectory(MpqPathBrowserDialog.SelectedPath + @"\base");
                    Console.WriteLine("Created base folder.");
                }
                //We modify every repository MadCow has to its new user selected path.
            }
        }

        private void SettingsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SaveSettings();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            if (!File.Exists(Program.madcowINI)) return;
            var source = new IniConfigSource(Program.madcowINI);
            source.Configs["Mooege"].Set("FileLogging", SettingsCheckedListBox.GetItemChecked(0).ToString(CultureInfo.InvariantCulture));
            source.Configs["Mooege"].Set("PacketLogging", SettingsCheckedListBox.GetItemChecked(1).ToString(CultureInfo.InvariantCulture));
            source.Configs["Mooege"].Set("Tasks", SettingsCheckedListBox.GetItemChecked(2).ToString(CultureInfo.InvariantCulture));
            source.Configs["Mooege"].Set("LazyLoading", SettingsCheckedListBox.GetItemChecked(3).ToString(CultureInfo.InvariantCulture));
            source.Configs["Mooege"].Set("PasswordCheck", SettingsCheckedListBox.GetItemChecked(4).ToString(CultureInfo.InvariantCulture));
            source.Configs["DiabloPath"].Set("MPQDest", MPQDestTextBox.Text);
            source.Save();
        }

        private void ApplySettings()
        {
            if (!File.Exists(Program.madcowINI)) return;
            var source = new IniConfigSource(Program.madcowINI);
            var src = source.Configs["ShortCut"].Get("Shortcut");
            if (src.Contains("1"))
            {
                ShortCut.Create();
            }
            MPQDestTextBox.Text = source.Configs["DiabloPath"].Get("MPQDest", Path.Combine(Program.programPath, "MPQ"));
            SettingsCheckedListBox.SetItemChecked(0, Convert.ToBoolean(source.Configs["Mooege"].Get("FileLogging", "true")));
            SettingsCheckedListBox.SetItemChecked(1, Convert.ToBoolean(source.Configs["Mooege"].Get("PacketLogging", "false")));
            SettingsCheckedListBox.SetItemChecked(2, Convert.ToBoolean(source.Configs["Mooege"].Get("Tasks", "true")));
            SettingsCheckedListBox.SetItemChecked(3, Convert.ToBoolean(source.Configs["Mooege"].Get("LazyLoading", "true")));
            SettingsCheckedListBox.SetItemChecked(4, Convert.ToBoolean(source.Configs["Mooege"].Get("PasswordCheck", "true")));
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //MadCow Self Updater
        ///////////////////////////////////////////////////////////
        #region MadCowUpdater
        private void button5_Click(object sender, EventArgs e)
        {
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = @"MadCowUpdater\MadCowUpdater.exe";
            firstProc.Start();
        }
        #endregion
    }
}