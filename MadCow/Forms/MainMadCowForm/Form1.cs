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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //We update this variable with the current supported D3 client after parsing the required version.
        private static string _mooegeSupportedVersion;
        //Parsing Console into a textbox
        private TextWriter _writer;
        //TO access controls from outside classes
        public static Form1 GlobalAccess;
        //For tray icon
        private ContextMenu m_menu;

        private Repository _selectedRepo;

        public Form1()
        {
            InitializeComponent();
            GlobalAccess = this;
        }

        ///////////////////////////////////////////////////////////
        //Form Load
        ///////////////////////////////////////////////////////////
        #region OnFormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            var splash = new SplashScreen.SpashScreen();
            splash.Show();
            splash.Update();
            ChatDisplayBox.MaxLength = 250; //Chat lenght
            Helper.CheckForInternet();//We check for Internet connection at start!.
            _writer = new TextBoxStreamWriter(ConsoleOutputTxtBox);
            Console.SetOut(_writer);
            Console.WriteLine("Welcome to MadCow!");
            var toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 1800;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 100;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Default buttons status. 
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(UpdateMooegeButton, "Update mooege from GitHub to latest version");
            toolTip1.SetToolTip(CopyMPQButton, "Copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(FindDiabloButton, "Find Diablo.exe so MadCow can work properly");
            toolTip1.SetToolTip(RemoteServerLaunchButton, "Connects to public server you have entered in");
            toolTip1.SetToolTip(ResetRepoFolder, "Resets Repository folder in case of errors");
            toolTip1.SetToolTip(DownloadMPQSButton, "Downloads ALL MPQs needed to run Mooege");
            toolTip1.SetToolTip(RestoreDefaultsLabel, "Resets Server Control settings");
            toolTip1.SetToolTip(PlayDiabloButton, "Time to play Diablo 3 through Mooege!");
            InitializeFindPath(); //Search if a Diablo client path already exist.
            LoadProfile(Configuration.MadCow.CurrentProfile); //We try to Load the last used profile by the user.
            RetrieveMpqList.GetfileList(); //Load MPQ list from Blizz server. Todo: This might slow down a bit MadCow loading, maybe we could place it somewhere else?.
            Helper.KillUpdater(); //This will kill MadCow updater if its running.
            ApplySettings(); //This loads Mooege settings over Mooege tab.
            splash.Hide();
        }
        #endregion
        /////////////////////////////
        //UPDATE MOOEGE: This will compare ur current revision, if outdated proceed to download
        /////////////////////////////
        #region UpdateMooege
        private void Update_Mooege_Click(object sender, EventArgs e)
        {
            ((Repository)repoComboBox.SelectedItem).Update();
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Play Diablo Button
        ///////////////////////////////////////////////////////////
        #region PlayDiablo
        private void PlayDiablo_Click(object sender, EventArgs e)
        {
            if (ErrorFinder.HasMpqs()) //We check for MPQ files count before allowing the user to proceed to play.
            {
                var repo = repoComboBox.SelectedItem as Repository;
                if (repo != null)
                {
                    if (!string.IsNullOrEmpty(repo.LastRevision) && repo.LastRevision != repo.LocalRevision)
                    {
                        var result = MessageBox.Show(this, "A new Mooege version is available. Should it be downloaded?",
                                                     "Update available",
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            repo.Update();
                        }
                        else if (result == DialogResult.No)
                        {
                            new Thread(ThreadProc).Start();
                        }
                    }
                    new Thread(ThreadProc).Start();

                }
                else
                {
                    MessageBox.Show("Please select a repository first!");
                    repoComboBox.DroppedDown = true;
                }

            }
            else if (Diablo3UserPathSelection != null && !ErrorFinder.HasMpqs())
            {
                var errorAnswer = MessageBox.Show("You haven't copied MPQ files." + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    MpqProcedure.StartCopyProcedure();
                    PlayDiabloButton.Enabled = false;
                    CopyMPQButton.Enabled = false;
                }
            }
            else
                Console.WriteLine("[FATAL] You are missing MPQ files!" + "\nPlease use CopyMpq button or copy Diablo3 MPQ's folder content into MadCow MPQ folder.");
        }

        public void ThreadProc()
        {
            Diablo.Play((Repository)repoComboBox.SelectedItem);

            //We add ErrorFinder call here, in order to know if Mooege had issues loading.
            if (!File.Exists(Environment.CurrentDirectory + @"\logs\mooege.log")) return;
            if (!ErrorFinder.SearchLogs("Fatal")) return;
            //We delete de Log file HERE. Nowhere else!.
            DeleteHelper.Delete(0);
            if (ErrorFinder.ErrorFileName.Contains("d3-update-base-")) //This will handle corrupted mpqs and missing mpq files.
            {
                var errorAnswer = MessageBox.Show(string.Format("Missing or Corrupted file [{0}]" +
                                                                "\nWould you like MadCow to fix this for you?",
                                                                ErrorFinder.ErrorFileName),
                                                  "Found corrupted file!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    //We execute the procedure to start downloading the corrupted file @ FixMpq();
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("CoreData"))
            {
                var errorAnswer = MessageBox.Show(string.Format("Corrupted file [{0}.mpq]" + "\nWould you like MadCow to fix this for you?", ErrorFinder.ErrorFileName), "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("ClientData"))
            {
                var errorAnswer = MessageBox.Show(string.Format("Corrupted file [{0}.mpq]" + "\nWould you like MadCow to fix this for you?", ErrorFinder.ErrorFileName), "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("MajorFailure"))
            {
                var errorAnswer = MessageBox.Show("One or more MPQ files are corrupted and MadCow is unable" +
                                                  " to detect which file/s are causing this.\nPlease visit" +
                                                  " Mooege Forum or Irc and ask for support.",
                                                  "Found Corrupted Files!",
                                                  MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                Console.WriteLine("[ERROR] Mooege can't run and MadCow was unable to handle the exception.");
                Console.WriteLine(ErrorFinder.ErrorFileName);
            }
            //If the user closes Repo selection and we already went through fixing the MPQ, then Mooege.log will not exist and
            //Madcow would crash when trying to read mooege.log.
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Copy MPQ files from D3 to MadCow MPQ Folder.
        ///////////////////////////////////////////////////////////
        private void CopyMPQs_Click(object sender, EventArgs e)
        {
            MpqProcedure.StartCopyProcedure();
        }

        ///////////////////////////////////////////////////////////
        //Remote Server Settings
        ///////////////////////////////////////////////////////////
        #region RemoteServerSettings
        private void RemoteServer_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
            var proc1 = new Process { StartInfo = new ProcessStartInfo(Diablo3UserPathSelection.Text) };
            var hostIP = remoteHostTxtBox.Text;
            var port = remotePortTxtBox.Text;
            var serverHost = hostIP + @":" + port;
            proc1.StartInfo.Arguments = @" -launch -auroraaddress " + serverHost;
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
            var repo = repoComboBox.SelectedItem as Repository;
            if (repo != null)
            {
                if (!string.IsNullOrEmpty(repo.LastRevision) && repo.LastRevision != repo.LocalRevision)
                {
                    var result = MessageBox.Show(this, "A new Mooege version is available. Should it be downloaded?",
                                                 "Update available",
                                                 MessageBoxButtons.YesNoCancel,
                                                 MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        repo.Update();
                    }
                    else if (result == DialogResult.No)
                    {
                        new Thread(ThreadProc2).Start();
                    }
                }
                new Thread(ThreadProc2).Start();
            }
            else
            {
                MessageBox.Show("Please select a repository first!");
                Tabs.SelectTab(UpdatesTab);
                repoComboBox.DroppedDown = true;
            }
        }

        public void ThreadProc2()
        {
            Console.WriteLine("Starting Mooege..");
            var mooege = new Process();
            mooege.StartInfo = new ProcessStartInfo(Paths.GetMooegeExePath((Repository)repoComboBox.SelectedItem));
            mooege.Start();
            if (!File.Exists(Environment.CurrentDirectory + @"\logs\mooege.log")) return;
            if (!ErrorFinder.SearchLogs("Fatal")) return;
            //We delete de Log file HERE. Nowhere else!.
            DeleteHelper.Delete(0);
            if (ErrorFinder.ErrorFileName.Contains("d3-update-base-"))
            {
                var errorAnswer = MessageBox.Show(@"Missing or Corrupted file [" + ErrorFinder.ErrorFileName + @"]" + "\nWould you like MadCow to fix this for you?", "Found corrupted file!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    //We execute the procedure to start downloading the corrupted file @ FixMpq();
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("CoreData"))
            {
                var errorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.ErrorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("ClientData"))
            {
                var errorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.ErrorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    FixMpq();
                }
            }
            if (ErrorFinder.ErrorFileName.Contains("MajorFailure"))
            {
                var errorAnswer = MessageBox.Show(@"Seems some major files are corrupted." + "\nWould you like MadCow to fix this for you?", "Found Corrupted Files!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(HelpTab)));
                    DownloadSelectedMpqs.RunWorkerAsync();
                }
            }
            else
            {
                Console.WriteLine("Unknown Exception");
                Console.WriteLine(ErrorFinder.ErrorFileName);
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////
        //Diablo Path Stuff
        ///////////////////////////////////////////////////////////
        #region Diablo3Path
        private void InitializeFindPath()
        {
            try
            {
                if (string.IsNullOrEmpty(Configuration.MadCow.DiabloPath))
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
                    Diablo3UserPathSelection.Text = Configuration.MadCow.DiabloPath;
                    CopyMPQButton.Enabled = true;
                    PlayDiabloButton.Enabled = true;
                    remoteHostTxtBox.Enabled = true;
                    remotePortTxtBox.Enabled = true;
                    RemoteServerLaunchButton.Enabled = true;
                    //Freezes at the start longer, but at least you get verified when you load!
                    VerifyDiablo3Version.RunWorkerAsync(); //Compares Versions of D3 with Mooege
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed while trying to verify D3 Version or Writting INI");
                Console.WriteLine(e);
            }
        }

        //Find Diablo Path Dialog
        private void FindDiablo_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            var findD3Exe = new OpenFileDialog
                                {
                                    Title = "MadCow By Wesko",
                                    InitialDirectory = Path.Combine(new[]
                                                                        {
                                                                            Environment.GetFolderPath(
                                                                                Environment.SpecialFolder.ProgramFilesX86),
                                                                            "Diablo III Beta",
                                                                        }),
                                    Filter = "Diablo III|Diablo III.exe"
                                };
            var response = findD3Exe.ShowDialog();
            if (response == DialogResult.OK) // If user was able to locate Diablo III.exe
            {
                // Get the directory name.
                var dirName = Path.GetDirectoryName(findD3Exe.FileName);
                // Output Name
                Diablo3UserPathSelection.Text = findD3Exe.FileName;
                //Bottom three are Enabled on Remote Server
                remoteHostTxtBox.Enabled = true;
                remotePortTxtBox.Enabled = true;
                RemoteServerLaunchButton.Enabled = true;

                //First we modify the Mooege INI storage path.
                Configuration.MadCow.DiabloPath = findD3Exe.FileName;
                Configuration.MadCow.MpqDiablo = Path.Combine(new[]
                                                                  {
                                                                      dirName,
                                                                      "Data_D3",
                                                                      "PC",
                                                                      "MPQs"
                                                                  });
                Console.WriteLine("Saved Diablo 3 client path.");
                Console.WriteLine("Verifying Diablo...");

                VerifyDiablo3Version.RunWorkerAsync(); //Compares versions of D3 with Mooege
            }//If the user opens the dialog to select a d3 path and he closes the dialog but already had a d3 path, then no warning will be triggered. (BUG FIXED-wesko)
            else if (response == DialogResult.Cancel && Diablo3UserPathSelection.TextLength == 35)//If user didn't select a Diablo III.exe, we show a warning and ofc, we dont save any path.
            {
                MessageBox.Show("You didn't select a Diablo III client", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            try
            {
                var client = new WebClient();
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                client.DownloadStringCompleted += Checkversions;
                var uri = new Uri("https://raw.github.com/mooege/mooege/master/src/Mooege/Common/Versions/VersionInfo.cs");
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
                if (File.Exists(Configuration.MadCow.DiabloPath))
                {
                    var parseVersion = e.Result;
                    var d3Version = FileVersionInfo.GetVersionInfo(Diablo3UserPathSelection.Text);
                    var parsePointer = parseVersion.IndexOf("RequiredPatchVersion = ", StringComparison.Ordinal);
                    var mooegeVersion = parseVersion.Substring(parsePointer + 23, 4); //Gets required version by Mooege
                    _mooegeSupportedVersion = mooegeVersion; //Public String to display over D3 path validation.
                    var currentD3VersionSupported = Convert.ToInt32(mooegeVersion);
                    var localD3Version = d3Version.FilePrivatePart;

                    //DISABLED MATCHING CLIENT VERSIONS FOR NOW.
                    if (localD3Version == currentD3VersionSupported || localD3Version != currentD3VersionSupported)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Found the correct Mooege supported version of Diablo III [{0}]", currentD3VersionSupported);
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
                    Configuration.MadCow.DiabloPath = string.Empty;
                    Configuration.MadCow.MpqDiablo = string.Empty;
                    MessageBox.Show("Could not find Diablo III.exe, please select the proper path again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Diablo3UserPathSelection.Invoke(new Action(() => { Diablo3UserPathSelection.Text = "Please Select your Diablo III path."; }));
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
            const string pattern = @"(([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|255[0-5])\.){3}([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])";
            var check = new Regex(pattern);
            string[] ipFields = { BnetServerIp.Text, GameServerIp.Text, PublicServerIp.Text };
            var i = 0; //Foreach IP counter.
            var j = 0; //Foreach PORT counter.
            var invalidIP = false;

            //Error handling if textbox null -> Giving feedback in the textbox field.
            for (var x = 0; x < ipFields.Length; x++)
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
                foreach (var value in ipFields)
                {
                    for (var lp = 0; lp < 999; lp++)
                    {
                        var ipAddress = string.Format("{0}.{0}.{0}.{0}", lp);

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

            string[] portFields = { BnetServerPort.Text, GameServerPort.Text };
            int Number;
            bool isNumber;

            for (var x = 0; x < portFields.Length; x++)
            {
                if (string.IsNullOrEmpty(portFields[x]))
                {
                    PortErrorHandler(x);
                    invalidIP = true;
                }
            }

            foreach (var value in portFields)
            {
                isNumber = int.TryParse(portFields[j], out Number);

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

            if (invalidIP) return;
            //If invalidIP == false which means all fields are valid, we hide any error cross that might be around.
            ErrorBnetServerIp.Visible = false;
            ErrorBnetServerPort.Visible = false;
            ErrorGameServerIp.Visible = false;
            ErrorGameServerPort.Visible = false;
            ErrorGameServerPort.Visible = false;
            ErrorPublicServerIp.Visible = false;

            //If invalidIP == false which means all fields are valid, we show all green ticks!
            TickBnetServerIP.Visible = true;
            TickBnetServerPort.Visible = true;
            TickGameServerIp.Visible = true;
            TickGameServerPort.Visible = true;
            TickGameServerPort.Visible = true;
            TickPublicServerIp.Visible = true;

            //We proceed to ask the user where to save the file.
            var saveProfile = new SaveFileDialog
                                  {
                                      Title = "Save Server Profile",
                                      DefaultExt = ".mdc",
                                      Filter = "MadCow Profile|*.mdc",
                                      InitialDirectory = Paths.ServerProfilesPath
                                  };
            saveProfile.ShowDialog();

            if (string.IsNullOrEmpty(saveProfile.FileName))
            {
                Console.WriteLine("You didn't specify a profile name");
            }
            else
            {
                var profile = new ServerProfile(saveProfile.FileName)
                                  {
                                      MooNetServerIp = BnetServerIp.Text,
                                      GameServerIp = GameServerIp.Text,
                                      NatIp = PublicServerIp.Text,
                                      MooNetServerPort = BnetServerPort.Text,
                                      GameServerPort = GameServerPort.Text,
                                      MooNetServerMotd = MotdTxtBox.Text,
                                      NatEnabled = NATcheckBox.Checked
                                  };
                Console.WriteLine("Saved profile [{0}] succesfully.", profile);
                profile.Save();
                Configuration.MadCow.CurrentProfile = profile;
            }
        }

        private void IpErrorHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerIp
                    BnetServerIp.Text = "Invalid IP";
                    ErrorBnetServerIp.Visible = true;
                    TickBnetServerIP.Visible = false;
                    break;
                case 1: //GameServerIp
                    GameServerIp.Text = "Invalid IP";
                    ErrorGameServerIp.Visible = true;
                    TickGameServerIp.Visible = false;
                    break;
                case 2: //PublicServerIp
                    PublicServerIp.Text = "Invalid IP";
                    ErrorPublicServerIp.Visible = true;
                    TickPublicServerIp.Visible = false;
                    break;
            }
        }

        private void IpCorrectHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerIp
                    ErrorBnetServerIp.Visible = false;
                    TickBnetServerIP.Visible = true;
                    break;
                case 1: //GameServerIp
                    ErrorGameServerIp.Visible = false;
                    TickGameServerIp.Visible = true;
                    break;
                case 2: //PublicServerIp
                    ErrorPublicServerIp.Visible = false;
                    TickPublicServerIp.Visible = true;
                    break;
            }
        }

        private void PortErrorHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerPort
                    BnetServerPort.Text = "Invalid Port";
                    ErrorBnetServerPort.Visible = true;
                    TickBnetServerPort.Visible = false;
                    break;
                case 1: //GameServerPort
                    GameServerPort.Text = "Invalid Port";
                    ErrorGameServerPort.Visible = true;
                    TickGameServerPort.Visible = false;
                    break;
            }
        }

        private void PortCorrectHandler(int i)
        {
            switch (i)
            {
                case 0: //BnetServerPort
                    ErrorBnetServerPort.Visible = false;
                    TickBnetServerPort.Visible = true;
                    break;
                case 1: //GameServerPort
                    ErrorGameServerPort.Visible = false;
                    TickGameServerPort.Visible = true;
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
            var openProfile = new OpenFileDialog
                                  {
                                      Title = "Save Server Profile",
                                      Filter = "MadCow Profile|*.mdc",
                                      InitialDirectory = Paths.ServerProfilesPath
                                  };
            openProfile.ShowDialog();
            if (openProfile.FileName == "")
            {
                Console.WriteLine("You didn't select a profile name");
            }

            else
            {
                LoadProfile(new ServerProfile(openProfile.FileName));
            }
        }

        internal void LoadProfile(ServerProfile profile)
        {
            try
            {
                if (profile == null) return;
                BnetServerIp.Text = profile.MooNetServerIp;
                GameServerIp.Text = profile.GameServerIp;
                PublicServerIp.Text = profile.NatIp;
                BnetServerPort.Text = profile.MooNetServerPort;
                GameServerPort.Text = profile.GameServerPort;
                MotdTxtBox.Text = profile.MooNetServerMotd;
                NATcheckBox.Checked = profile.NatEnabled;

                //Loading a profile means it has the correct values for every box, so first we disable every red cross that might be out there.
                ErrorBnetServerIp.Visible = false;
                ErrorBnetServerPort.Visible = false;
                ErrorGameServerIp.Visible = false;
                ErrorGameServerPort.Visible = false;
                ErrorGameServerPort.Visible = false;
                ErrorPublicServerIp.Visible = false;
                //Loading a profile means it has the correct values for every box, so we change everything to green ticked.
                TickBnetServerIP.Visible = true;
                TickBnetServerPort.Visible = true;
                TickGameServerIp.Visible = true;
                TickGameServerPort.Visible = true;
                TickGameServerPort.Visible = true;
                TickPublicServerIp.Visible = true;
                Console.WriteLine("Loaded server profile [{0}] succesfully.", profile);
                Configuration.MadCow.CurrentProfile = profile;
            }
            catch (Exception e)
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
        private void repoComboBox_TextChanged(object sender, EventArgs e)
        {
            //var currentText = RevisionParser.CommitFile;
            //BranchComboBox.Visible = false;
            //RepositoryHintLabel.Visible = true;
            //BranchSelectionLabel.Visible = false;
            //UpdateMooegeButton.Enabled = false;
            //AutoUpdateValue.Enabled = false; //If user is typing a new URL Update and Autoupdate
            //EnableAutoUpdateBox.Enabled = false;      //Functions gets disabled
            //try
            //{
            //    if (currentText == "Incorrect repository entry")
            //    {
            //        repoComboBox.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        repoComboBox.ForeColor = Color.Black;
            //        pictureBox1.Hide();//Error Image (Cross)
            //        pictureBox2.Hide();//Correct Image (Tick)
            //    }
            //}
            //catch
            //{
            //    repoComboBox.ForeColor = SystemColors.ControlText;
            //}
        }

        //Color handler for BnetServerIp
        private void BnetServerIp_TextChanged(object sender, EventArgs e)
        {
            var currentText = BnetServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    BnetServerIp.ForeColor = Color.Red;
                }
                else
                {
                    BnetServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                BnetServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for GameServerIp
        private void GameServerIp_TextChanged(object sender, EventArgs e)
        {
            var currentText = GameServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    GameServerIp.ForeColor = Color.Red;
                }
                else
                {
                    GameServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                GameServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for PublicServerIp
        private void PublicServerIp_TextChanged(object sender, EventArgs e)
        {
            var currentText = PublicServerIp.Text;
            try
            {
                if (currentText == "Invalid IP")
                {
                    Console.WriteLine("Check for input errors!");
                    PublicServerIp.ForeColor = Color.Red;
                }
                else
                {
                    PublicServerIp.ForeColor = Color.Black;
                }
            }
            catch
            {
                PublicServerIp.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for BnetServerPort
        private void BnetServerPort_TextChanged(object sender, EventArgs e)
        {
            var currentText = BnetServerPort.Text;
            try
            {
                if (currentText == "Invalid Port")
                {
                    Console.WriteLine("Check for input errors!");
                    BnetServerPort.ForeColor = Color.Red;
                }
                else
                {
                    BnetServerPort.ForeColor = Color.Black;
                }
            }
            catch
            {
                BnetServerPort.ForeColor = SystemColors.ControlText;
            }
        }
        //Color handler for GameServerPort
        private void GameServerPort_TextChanged(object sender, EventArgs e)
        {
            var currentText = GameServerPort.Text;
            try
            {
                if (currentText == "Invalid Port")
                {
                    Console.WriteLine("Check for input errors!");
                    GameServerPort.ForeColor = Color.Red;
                }
                else
                {
                    GameServerPort.ForeColor = Color.Black;
                }
            }
            catch
            {
                GameServerPort.ForeColor = SystemColors.ControlText;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Download MPQs selected by the user.
        ////////////////////////////////////////////////////////////////////////
        #region DownloadMpqsBySelection
        private void DownloadMPQSButton_Click(object sender, EventArgs e)
        {
            var t = new Thread(MPQThread);
            t.Start();
        }

        private void MPQThread()
        {
            Application.Run(new MPQDownloader());
        }

        private void DownloadMPQS(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }
            int i; //We use this variable to select save path destination.            
            //Will use this to determinate the correct save path.
            string[] mpqDestination = {
                                          Path.Combine(Configuration.MadCow.MpqServer, "base"),
                                          Configuration.MadCow.MpqServer
                                      };
            var speedTimer = new Stopwatch();
            foreach (var value in MPQDownloader.MpqSelection)
            {
                var url = new Uri(value);
                var request = (HttpWebRequest)WebRequest.Create(url);
                if (Proxy.proxyStatus)
                    request.Proxy = proxy;
                var response = (HttpWebResponse)request.GetResponse();

                //Parsing the file name.
                var fullName = url.LocalPath.TrimStart('/');
                var name = Path.GetFileNameWithoutExtension(fullName);
                var ext = Path.GetExtension(fullName);
                //End Parsing.              
                response.Close();
                //Setting save path
                if (name == "CoreData" || name == "ClientData") i = 1; //Path \MPQ\
                else i = 0; //Path \MPQ\base\

                var iSize = response.ContentLength;
                long iRunningByteTotal = 0;

                using (var client = new WebClient())
                {
                    if (Proxy.proxyStatus)
                        client.Proxy = proxy;
                    using (var streamRemote = client.OpenRead(new Uri(value)))
                    {
                        using (Stream streamLocal = new FileStream(mpqDestination[i] + @"\" + name + ext, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            //We start the timer to measure speed - This still needs testing not sure if speed its accuarate. - wesko
                            speedTimer.Start();
                            DownloadFileNameLabel.Invoke(new Action(() =>
                            {
                                DownloadFileNameLabel.Text = string.Format("Downloading File: {0}{1}", name, ext);
                            }
                            ));

                            int iByteSize;
                            var byteBuffer = new byte[iSize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                iRunningByteTotal += iByteSize;

                                double dIndex = iRunningByteTotal;
                                double dTotal = byteBuffer.Length;
                                var dProgressPercentage = (dIndex / dTotal);
                                var iProgressPercentage = (int)(dProgressPercentage * 100);

                                //We calculate the download speed.
                                var ts = speedTimer.Elapsed;
                                var bytesReceivedSpeed = (iRunningByteTotal / 1024d) / ts.TotalSeconds;
                                DownloadSpeedLabel.Invoke(new Action(() =>
                                {
                                    DownloadSpeedLabel.Text = string.Format("Downloading Speed: {0}Kbps", Convert.ToInt32(bytesReceivedSpeed));
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
            MPQDownloader.MpqSelection.Clear();//Reset the Array values after downloading.
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
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            var downloadBaseFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/" + ErrorFinder.ErrorFileName + ".mpq";
            var downloadFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + ErrorFinder.ErrorFileName + ".mpq";
            var speedTimer = new Stopwatch();

            //TODO should be simplified somehow.
            //Parsing the file name.
            var url = ErrorFinder.ErrorFileName.Contains("CoreData") ||
                      ErrorFinder.ErrorFileName.Contains("ClientData")
                          ? new Uri(downloadFileUrl)
                          : new Uri(downloadBaseFileUrl);
            //var fullName = url.LocalPath.TrimStart('/');
            var dest = ErrorFinder.ErrorFileName.Contains("CoreData") ||
                       ErrorFinder.ErrorFileName.Contains("ClientData")
                           ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                           : Path.Combine(new[]
                                              {
                                                  Configuration.MadCow.MpqDiablo,
                                                  "base",
                                                  Path.GetFileName(url.AbsolutePath)
                                              });

            var copy = ErrorFinder.ErrorFileName.Contains("CoreData") ||
                       ErrorFinder.ErrorFileName.Contains("ClientData")
                           ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                           : Path.Combine(new[]
                                              {
                                                  Configuration.MadCow.MpqDiablo,
                                                  "base",
                                                  ErrorFinder.ErrorFileName + ".mpq"
                                              });
            var copyDest = ErrorFinder.ErrorFileName.Contains("CoreData") ||
                           ErrorFinder.ErrorFileName.Contains("ClientData")
                               ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                               : Path.Combine(new[]
                                                  {
                                                      Configuration.MadCow.MpqServer,
                                                      "base",
                                                      ErrorFinder.ErrorFileName + ".mpq"
                                                  });

            //End Parsing.

            var request = (HttpWebRequest)WebRequest.Create(url);
            if (Proxy.proxyStatus)
                request.Proxy = proxy;
            var response = (HttpWebResponse)request.GetResponse();
            response.Close();
            var iSize = response.ContentLength;
            long iRunningByteTotal = 0;

            using (var client = new WebClient())
            {
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                using (var streamRemote = client.OpenRead(new Uri(downloadFileUrl)))
                {
                    using (var streamLocal = new FileStream(dest, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        Console.WriteLine("Starting download...");
                        speedTimer.Start();
                        DownloadFileNameLabel.Invoke(new Action(() =>
                        {
                            DownloadFileNameLabel.Text = "Downloading File: " + Path.GetFileName(url.AbsolutePath);
                        }
                        ));

                        int iByteSize;
                        var byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            double dIndex = iRunningByteTotal;
                            double dTotal = byteBuffer.Length;
                            var dProgressPercentage = (dIndex / dTotal);
                            var iProgressPercentage = (int)(dProgressPercentage * 100);

                            //We calculate the download speed.
                            var ts = speedTimer.Elapsed;
                            var bytesReceivedSpeed = (iRunningByteTotal / 1024d) / ts.TotalSeconds;
                            DownloadSpeedLabel.Invoke(new Action(() =>
                            {
                                DownloadSpeedLabel.Text = string.Format("Downloading Speed: {0}Kbps", Convert.ToInt32(bytesReceivedSpeed));
                            }
                            ));
                            ErrorFilesDownloaders.ReportProgress(iProgressPercentage);
                        }
                        //streamLocal.Close(); Not needed, called by destructor.
                    }
                    //streamRemote.Close();
                }
            }
            speedTimer.Stop();
            File.Copy(copy, copyDest, true);
            //if (ErrorFinder.errorFileName.Contains("CoreData") || ErrorFinder.errorFileName.Contains("ClientData"))
            //{
            //}
            //else
            //{
            //    File.Copy(downloadDestination + @"\base\" + ErrorFinder.errorFileName + @".MPQ",
            //        Program.programPath + @"\MPQ\" + @"\base\" + ErrorFinder.errorFileName + @".MPQ", true);
            //}
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
            var downloadSource = Configuration.MadCow.MpqDiablo;
            var downloadDestination = Path.Combine(Configuration.MadCow.MpqServer, @"base\");
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
                var response = MessageBox.Show("MadCow Fixer",
                "MadCow succesfully fixed:" + ErrorFinder.ErrorFileName + @".MPQ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (response == DialogResult.OK) //We take this as the OK response.
                {
                    //Since problem must be fixed, we take the user to the Update tab & execute repo selection form again
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab(UpdatesTab)));
                    var t = new Thread(ThreadProc);
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
        //Changelog.
        ////////////////////////////////////////////////////////////////////////
        #region Changelog
        private void SelectRepoChngLogComboBox_DropDown(object sender, EventArgs e)
        {
            SelectRepoChngLogComboBox.Items.Clear();
            SelectRepoChngLogComboBox.DisplayMember = "Developer";
            SelectRepoChngLogComboBox.Items.AddRange(Repository.Repositories.ToArray());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRepoChngLogComboBox.Enabled = false;
            _selectedRepo = (Repository)SelectRepoChngLogComboBox.SelectedItem;
            var bgWorker = new BackgroundWorker();
            bgWorker.DoWork += ChangeLogDownloader_DoWork;
            //bgWorker.RunWorkerCompleted += (s, ea) => Invoke(new Action(() => SelectRepoChngLogComboBox.Enabled = true));

            //Proceed to download the commit file and parse.
            bgWorker.RunWorkerAsync();
        }

        private void ChangeLogDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            try
            {
                var client = new WebClient();
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                client.DownloadFileAsync(new Uri(_selectedRepo.Url.AbsoluteUri + "/commits/master.atom"),
                                         Paths.CommitsFilePath);
                client.DownloadFileCompleted += DisplayChangelog;
            }
            catch
            {
                Console.WriteLine("Check yor internet connection");
            }
        }

        //Parse commit file and display into the textbox.
        private void DisplayChangelog(object sender, AsyncCompletedEventArgs e)
        {
            changeLogTreeView.Invoke(new Action(() => changeLogTreeView.Nodes.Clear()));

            var newNode = new TreeNode();
            var oldNode = new TreeNode();
            var comment = string.Empty;
            var date = default(DateTime);
            var developer = string.Empty;
            var revision = string.Empty;

            foreach (var line in File.ReadAllLines(Paths.CommitsFilePath).Skip(7))
            {
                //For revision.
                if (Regex.IsMatch(line, "<id>"))
                {
                    revision = new Regex("Commit/(.*)</id>").Match(line).Groups[1].Value.Substring(7, 7);
                }

                //For commits comment.
                if (Regex.IsMatch(line, "<title>"))
                {
                    comment = new Regex("<title>(.*)</title>").Match(line).Groups[1].Value;
                }

                //For update date/time.
                if (Regex.IsMatch(line, "<updated>"))
                {
                    date = Convert.ToDateTime(new Regex("<updated>(.*)</updated>").Match(line).Groups[1].Value);
                }

                //For developer that pushed.
                if (Regex.IsMatch(line, "<name>"))
                {
                    developer = new Regex("<name>(.*)</name>").Match(line).Groups[1].Value;
                }


                if (Regex.IsMatch(line, "</entry>"))
                {

                    var node = new TreeNode(string.Format("{0} at {1} by {2}", revision.ToUpper(), date, developer), new[] { new TreeNode(comment) });

                    if (date.CompareTo(_selectedRepo.Date) == 1)
                    {
                        newNode.Nodes.Add(node);
                    }
                    else
                    {
                        oldNode.Nodes.Add(node);
                    }

                    comment = string.Empty;
                    date = default(DateTime);
                    developer = string.Empty;
                }
                newNode.Text = string.Format("New Revisions ({0})", newNode.Nodes.Count);
                oldNode.Text = string.Format("Old Revisions ({0})", oldNode.Nodes.Count);
            }

            Invoke(new Action(() =>
                                  {
                                      changeLogTreeView.Nodes.AddRange(new[] { newNode, oldNode });
                                      changeLogTreeView.Nodes[0].Expand();
                                      SelectRepoChngLogComboBox.Enabled = true;
                                  }));
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Tray Icon Stuff
        ////////////////////////////////////////////////////////////////////////
        #region TrayIcon
        public int NotifyCount;

        public void loadTrayMenu()
        {
            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0, new MenuItem("Check Updates", Tray_CheckUpdates));
            m_menu.MenuItems.Add(1, new MenuItem("Show", Show_Click));
            m_menu.MenuItems.Add(2, new MenuItem("Hide", Hide_Click));
            m_menu.MenuItems.Add(3, new MenuItem("Exit", Exit_Click));
            MadCowTrayIcon.ContextMenu = m_menu;
        }

        private void Tray_CheckUpdates(object sender, EventArgs e)
        {
            if (UpdateMooegeButton.Enabled)
            {
                var repo = (Repository)repoComboBox.SelectedItem;
                if (repo != null)
                {
                    repo.Update();
                }
            }
            else
            {
                Tray.ShowBalloonTip("You must select and validate a repository first!");
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                if (Configuration.MadCow.TrayEnabled)
                {
                    Hide();
                    if (NotifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
                    {
                        Tray.ShowBalloonTip("MadCow will continue running minimized.");
                        NotifyCount++;
                    }
                }
            }
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        protected void Exit_Click(Object sender, EventArgs e)
        {
            Close();
        }
        protected void Hide_Click(Object sender, EventArgs e)
        {
            Hide();

            if (NotifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
            {
                Tray.ShowBalloonTip("MadCow will continue running minimized.");
                NotifyCount++;
            }
        }
        protected void Show_Click(Object sender, EventArgs e)
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
            //var proxy = new WebProxy();
            //if (Proxy.proxyStatus)
            //{
            //    proxy.Address = new Uri(Proxy.proxyUrl);
            //    proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            //}

            //BranchComboBox.Invoke(new Action(() => SelectedBranch = BranchComboBox.SelectedItem.ToString()));
            //var client = new WebClient();
            //if (Proxy.proxyStatus)
            //    client.Proxy = proxy;
            //client.DownloadStringCompleted += BranchParse;
            //var uri = new Uri(repoComboBox.Text + "/commits/" + SelectedBranch + ".atom");
            //client.DownloadStringAsync(uri);
        }

        private void BranchParse(object sender, DownloadStringCompletedEventArgs e)
        {
            //var result = e.Result;
            //var pos2 = result.IndexOf("Commit/", StringComparison.Ordinal);
            //var revision = result.Substring(pos2 + 7, 7);
            //ParseRevision.LastRevision = result.Substring(pos2 + 7, 7);
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
        private void desktopShortcutToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.ShortcutEnabled = desktopShortcutToolStripMenuItem.Checked;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////
        // BalloonTips Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region BalloonTips
        private void enableTrayNotificationsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.TrayNotificationsEnabled = enableTrayNotificationsToolStripMenuItem.Checked;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////
        // Tray Icon Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region TrayIcon
        private void enableTrayToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.TrayEnabled = enableTrayToolStripMenuItem.Checked;
            enableTrayNotificationsToolStripMenuItem.Enabled = enableTrayToolStripMenuItem.Checked;
        }
        #endregion

        private void compileAsDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.MadCow.CompileAsDebug = compileAsDebugToolStripMenuItem.Checked;
        }

        private void checkUpdatesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.CheckMooegeUpdates = checkUpdatesToolStripMenuItem.Checked;
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Mooege Settings
        ////////////////////////////////////////////////////////////////////////
        #region Mooege Settings
        private void BrowseMPQPathButton_Click(object sender, EventArgs e)
        {
            if (MpqPathBrowserDialog.ShowDialog() != DialogResult.OK) return;

            if (Directory.Exists(Configuration.MadCow.MpqServer))
            {
                var result = MessageBox.Show(this,
                                             "Do you want to delete the old folder and its contents?",
                                             "MadCow",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result == DialogResult.Yes)
                {
                    Directory.Delete(Configuration.MadCow.MpqServer, true);
                }
            }

            Configuration.MadCow.MpqServer = MpqPathBrowserDialog.SelectedPath;
            MPQDestTextBox.Text = MpqPathBrowserDialog.SelectedPath;

            //We create the base folder here, else MadCow will cry somewhere.
            //TODO: We should do this only when we actually access the folder for writing.
            //In case the user reconsiders we potentially would have several folders left
            //the user may not know about.
            //Or we could delete the previous folder when switching.
            if (!Directory.Exists(Path.Combine(MpqPathBrowserDialog.SelectedPath, "base")))
            {
                Directory.CreateDirectory(Path.Combine(MpqPathBrowserDialog.SelectedPath, "base"));
                Console.WriteLine("Created base folder.");
            }
            //We modify every repository MadCow has to its new user selected path.
        }

        private void ApplySettings()
        {
            if (Configuration.MadCow.ShortcutEnabled)
            {
                ShortCut.Create();
            }

            MPQDestTextBox.Text = Configuration.MadCow.MpqServer;

            enableFileLoggingToolStripMenuItem.Checked = Configuration.Mooege.FileLogging;
            enablePacketLoggingToolStripMenuItem.Checked = Configuration.Mooege.PacketLogging;
            enableTasksToolStripMenuItem.Checked = Configuration.Mooege.Tasks;
            enableLazyLoadingToolStripMenuItem.Checked = Configuration.Mooege.LazyLoading;
            enableNoPasswordCheckToolStripMenuItem.Checked = Configuration.Mooege.PasswordCheck;

            enableTrayToolStripMenuItem.Checked = Configuration.MadCow.TrayEnabled;
            enableTrayNotificationsToolStripMenuItem.Checked = Configuration.MadCow.TrayNotificationsEnabled;
            desktopShortcutToolStripMenuItem.Checked = Configuration.MadCow.ShortcutEnabled;
            compileAsDebugToolStripMenuItem.Checked = Configuration.MadCow.CompileAsDebug;
            checkUpdatesToolStripMenuItem.Checked = Configuration.MadCow.CheckMooegeUpdates;
        }

        private void enableFileLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Mooege.FileLogging = enableFileLoggingToolStripMenuItem.Checked;
        }

        private void enablePacketLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Mooege.PacketLogging = enablePacketLoggingToolStripMenuItem.Checked;
        }

        private void enableTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Mooege.Tasks = enableTasksToolStripMenuItem.Checked;
        }

        private void enableLazyLoadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Mooege.LazyLoading = enableLazyLoadingToolStripMenuItem.Checked;
        }

        private void enableNoPasswordCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Mooege.PasswordCheck = enableNoPasswordCheckToolStripMenuItem.Checked;
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //MadCow Self Updater
        ///////////////////////////////////////////////////////////
        #region MadCowUpdater
        private void button5_Click(object sender, EventArgs e)
        {
            //var upd = new MadCowUpdater.Form1();
            //upd.ShowDialog();
            var firstProc = new Process { StartInfo = { FileName = @"MadCowUpdater\MadCowUpdater.exe" } };
            firstProc.Start();
        }
        #endregion

        //Download Net 4 link.
        private void DownloadNetLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.microsoft.com/download/en/details.aspx?id=17851");
        }

        ///////////////////////////////////////////////////////////
        //MadCow Live Help
        ///////////////////////////////////////////////////////////
        #region MadCow Live Help
        public static Thread ircThread;

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ircThread != null) //This is temporary to prevent connecting again and IRC malfunctioning.
            {
                MessageBox.Show("There is a bug with current IRC implementation \nthat i'm not able to takle."
                    + "\nIn order to connect again you need to restart MadCow."
                    + "\nSorry for the inconvenience. -Wesko", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //We hide the current tab contents.
                Rules.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                BotonAlerta.Visible = false;
                Advertencia.Visible = false;
                ConnectButton.Visible = false;

                //Show the chat content.
                ChatDisplayBox.Visible = true;
                ChatUsersBox.Visible = true;
                PleaseWaitLabel.Visible = true;

                //Start our IRC client in a new thread.
                ircThread = new Thread(ThreadFunction);
                ircThread.Start();
            }
        }


        private void ThreadFunction()
        {
            Client.Run();
        }

        private void textBox3_MouseMove(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectionLength = 0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; //Disable annoying beep sound!
                Client.SendMessage(ChatMessageBox.Text);
                ChatDisplayBox.SelectionStart = ChatDisplayBox.Text.Length;
                ChatDisplayBox.ScrollToCaret();
                ChatMessageBox.Clear();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (ChatMessageBox.TextLength > 250)
            {
                e.SuppressKeyPress = true;
            }
            if (e.KeyValue == (char)13 && ChatMessageBox.TextLength > 250)
            {
                e.Handled = true;
                Client.SendMessage(ChatMessageBox.Text);
                ChatDisplayBox.SelectionStart = ChatDisplayBox.Text.Length;
                ChatDisplayBox.ScrollToCaret();
                ChatMessageBox.Clear();
            }
            if (e.KeyValue == (char)8)
            {
                e.SuppressKeyPress = false;
                e.Handled = true;
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            ChatUsersBox.Clear();
            ChatDisplayBox.Clear();
            ChatMessageBox.Clear();
            Client.irc.Disconnect();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Crashes on exit
            //if (Client.irc != null) Client.irc.Disconnect();
            Configuration.Save();
            File.WriteAllLines(Paths.RepositoriesListPath,
                               Repository.Repositories.Select(
                                   repo => string.Format("{0}@{1}", repo.Url.AbsoluteUri, repo.Branch)).ToList());
        }

        private void repositoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RepositorySelection().ShowDialog();
        }

        private void repoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.LastRepository = repoComboBox.SelectedItem.ToString();
            UpdateMooegeButton.Enabled = repoComboBox.SelectedItem is Repository;
            PlayDiabloButton.Enabled = repoComboBox.SelectedItem is Repository &&
                                       !string.IsNullOrEmpty(Configuration.MadCow.DiabloPath);

            var repository = repoComboBox.SelectedItem as Repository;
            if (repository != null && Configuration.MadCow.CheckMooegeUpdates)
            {
                repository.UpdateRevision();
            }
        }

        private void repoComboBox_DropDown(object sender, EventArgs e)
        {
            PopulateRepositories();
        }

        private void PopulateRepositories()
        {
            repoComboBox.Items.Clear();
            repoComboBox.Items.AddRange(Repository.Repositories.Where(r => r.IsDownloaded).ToArray());
            if (repoComboBox.Items.Count == 0)
            {
                new RepositorySelection().ShowDialog();
                repoComboBox.Items.AddRange(Repository.Repositories.Where(r => r.IsDownloaded).ToArray());
            }

            repoComboBox.SelectedItem = Repository.Repositories.FirstOrDefault(r => r.Name == Configuration.MadCow.LastRepository);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            PopulateRepositories();
        }
    }
}