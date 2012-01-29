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
        public static String MooegeSupportedVersion;
        //Timing for autoupdate
        private int _tick;
        //Parsing Console into a textbox
        private TextWriter _writer;
        //TO access controls from outside classes
        public static Form1 GlobalAccess;
        //For tray icon
        private ContextMenu m_menu;

        public static String SelectedBranch;

        private readonly string _repoListPath = Path.Combine(new[]
                                                                {
                                                                    Program.programPath,
                                                                    "Tools",
                                                                    "RepoList.txt"
                                                                });

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
            Helper.DefaultFolderCreator(); //We create default MadCow needed folders.
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
            AutoUpdateValue.Enabled = false;
            EnableAutoUpdateBox.Enabled = false;
            PlayDiabloButton.Enabled = false;
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(UpdateMooegeButton, "Update mooege from GitHub to latest version");
            toolTip1.SetToolTip(CopyMPQButton, "Copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(FindDiabloButton, "Find Diablo.exe so MadCow can work properly");
            toolTip1.SetToolTip(ValidateRepoButton, "Validate the repository so MadCow can download it");
            toolTip1.SetToolTip(EnableAutoUpdateBox, "Enable updates to a repository every 'X' minutes");
            toolTip1.SetToolTip(RemoteServerLaunchButton, "Connects to public server you have entered in");
            toolTip1.SetToolTip(ResetRepoFolder, "Resets Repository folder in case of errors");
            toolTip1.SetToolTip(DownloadMPQSButton, "Downloads ALL MPQs needed to run Mooege");
            toolTip1.SetToolTip(RestoreDefaultsLabel, "Resets Server Control settings");
            toolTip1.SetToolTip(PlayDiabloButton, "Time to play Diablo 3 through Mooege!");
            InitializeFindPath(); //Search if a Diablo client path already exist.
            RepoCheck(); //Checks for duplicities.
            RepoList(); //Loads Repos from RepoList.txt
            Changelog(); //Loads Changelog comobox values.
            LoadLastUsedProfile(); //We try to Load the last used profile by the user.
            Helper.Helpers();//Loads the correct nameplate for shortcut/balloon/LastRepo enabled/disabled
            RetrieveMpqList.getfileList(); //Load MPQ list from Blizz server. Todo: This might slow down a bit MadCow loading, maybe we could place it somewhere else?.
            Helper.KillUpdater(); //This will kill MadCow updater if its running.
            ApplySettings(); //This loads Mooege settings over Mooege tab.
            splash.Hide();
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
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }
            comboBox1.Invoke(new Action(() => { ParseRevision.revisionUrl = comboBox1.Text; }));
            try
            {
                var client = new WebClient();
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                client.DownloadStringCompleted += backgroundWorker5_RunWorkerCompleted;
                try
                {
                    var uri = new Uri(ParseRevision.revisionUrl + "/commits/master.atom");
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

            else if (e.Result != null && e.Error == null)
            {
                ParseRevision.commitFile = e.Result;
                var pos2 = ParseRevision.commitFile.IndexOf("Commit/", StringComparison.Ordinal);
                var revision = ParseRevision.commitFile.Substring(pos2 + 7, 7);
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
            //We set or "reset" progressbar value to zero.
            generalProgressBar.Value = 0;
            generalProgressBar.Update();
            var repPath = Path.Combine(new[]
                                           {
                                               Program.programPath,
                                               "Repositories",
                                               ParseRevision.developerName + "-" +
                                               ParseRevision.branchName + "-" +
                                               ParseRevision.lastRevision
                                           });
            if (Directory.Exists(repPath))
            {
                Console.WriteLine("You have latest [{0}] revision: {1}",
                                  ParseRevision.developerName,
                                  ParseRevision.lastRevision);

                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    MadCowTrayIcon.ShowBalloonTip(1000,
                                                  "MadCow",
                                                  string.Format("You have latest [{0}] revision: {1}",
                                                                ParseRevision.developerName,
                                                                ParseRevision.lastRevision),
                                                  ToolTipIcon.Info);
                }

                if (EnableAutoUpdateBox.Checked) //Using AutoUpdate:
                {
                    _tick = (int)AutoUpdateValue.Value;
                    AutoUpdateTimerLabel.Text = string.Format("Update in {0} minutes.", _tick);
                }
            }

            else if (Directory.Exists(Path.Combine(Program.programPath, "MPQ"))) //Checks for MPQ Folder
            {
                if (EnableAutoUpdateBox.Checked) //Using AutoUpdate:
                {
                    DownloadSpeedTimer.Stop();
                }

                Console.WriteLine("Found default MadCow MPQ folder");
                DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                UpdateMooegeButton.Enabled = false;
                Console.WriteLine("Downloading...");

                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                }

                DownloadRepository.RunWorkerAsync();
            }

            else
            {
                if (EnableAutoUpdateBox.Checked) //Using AutoUpdate:
                {
                    DownloadSpeedTimer.Stop();
                }

                DeleteHelper.DeleteOldRepoVersion(ParseRevision.developerName); //We delete old repo version.
                Console.WriteLine("Downloading...");

                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Downloading...", ToolTipIcon.Info);
                }

                Directory.CreateDirectory(Path.Combine(Program.programPath, "MPQ"));
                UpdateMooegeButton.Enabled = false;
                DownloadRepository.RunWorkerAsync();
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Play Diablo Button
        ///////////////////////////////////////////////////////////
        #region PlayDiablo
        private void PlayDiablo_Click(object sender, EventArgs e)
        {
            LastPlayedRepoReminderLabel.Visible = Configuration.MadCow.RememberLastRepository;

            if (ErrorFinder.hasMpqs()) //We check for MPQ files count before allowing the user to proceed to play.
            {
                var t = new System.Threading.Thread(ThreadProc);
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
            if (RepositorySelectionPlay.LastPlayed() && Configuration.MadCow.RememberLastRepository)
            {
                Diablo.Play();
            }

            else
            {
                Application.Run(new RepositorySelectionPlay());
            }
            //We add ErrorFinder call here, in order to know if Mooege had issues loading.
            if (!File.Exists(Program.programPath + @"\logs\mooege.log")) return;
            if (!ErrorFinder.SearchLogs("Fatal")) return;
            //We delete de Log file HERE. Nowhere else!.
            DeleteHelper.Delete(0);
            if (ErrorFinder.errorFileName.Contains("d3-update-base-")) //This will handle corrupted mpqs and missing mpq files.
            {
                var errorAnswer = MessageBox.Show(string.Format("Missing or Corrupted file [{0}]" + "\nWould you like MadCow to fix this for you?", ErrorFinder.errorFileName), "Found corrupted file!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    //We execute the procedure to start downloading the corrupted file @ FixMpq();
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("CoreData"))
            {
                var errorAnswer = MessageBox.Show(string.Format("Corrupted file [{0}.mpq]" + "\nWould you like MadCow to fix this for you?", ErrorFinder.errorFileName), "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("ClientData"))
            {
                var errorAnswer = MessageBox.Show(string.Format("Corrupted file [{0}.mpq]" + "\nWould you like MadCow to fix this for you?", ErrorFinder.errorFileName), "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("MajorFailure"))
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
                Console.WriteLine(ErrorFinder.errorFileName);
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
            var t = new System.Threading.Thread(ThreadProc2);
            t.Start();
        }

        public void ThreadProc2()
        {
            Application.Run(new RepositorySelectionServer());
            if (!File.Exists(Program.programPath + @"\logs\mooege.log")) return;
            if (!ErrorFinder.SearchLogs("Fatal")) return;
            //We delete de Log file HERE. Nowhere else!.
            DeleteHelper.Delete(0);
            if (ErrorFinder.errorFileName.Contains("d3-update-base-"))
            {
                var errorAnswer = MessageBox.Show(@"Missing or Corrupted file [" + ErrorFinder.errorFileName + @"]" + "\nWould you like MadCow to fix this for you?", "Found corrupted file!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    //We execute the procedure to start downloading the corrupted file @ FixMpq();
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("CoreData"))
            {
                var errorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("ClientData"))
            {
                var errorAnswer = MessageBox.Show(@"Corrupted file [" + ErrorFinder.errorFileName + @".mpq]" + "\nWould you like MadCow to fix this for you?", "Fatal Error!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    FixMpq();
                }
            }
            if (ErrorFinder.errorFileName.Contains("MajorFailure"))
            {
                var errorAnswer = MessageBox.Show(@"Seems some major files are corrupted." + "\nWould you like MadCow to fix this for you?", "Found Corrupted Files!",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (errorAnswer == DialogResult.Yes)
                {
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage4")));
                    DownloadSelectedMpqs.RunWorkerAsync();
                }
            }
            else
            {
                Console.WriteLine("Unknown Exception");
                Console.WriteLine(ErrorFinder.errorFileName);
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////
        //Timer stuff for AutoUpdate
        ///////////////////////////////////////////////////////////
        #region TimerStuff
        private void AutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            _tick = (int)AutoUpdateValue.Value;

            if (EnableAutoUpdateBox.Checked)
            {
                AutoUpdateTimerLabel.Text = "Update in " + _tick + " minutes.";
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
            _tick--;
            if (_tick == 0)
            {
                UpdateMooege(); //Runs Update //This was changed in the previous commit.
                _tick = (int)AutoUpdateValue.Value;
            }
            else
                AutoUpdateTimerLabel.Text = "Update in " + _tick + " minutes.";
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
                                                                      findD3Exe.FileName,
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

        /////////////////////////////////
        //DOWNLOAD SOURCE FROM REPOSITORY
        /////////////////////////////////
        #region DownloadRepository
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }
            //We get the selected branch first.
            BranchComboBox.Invoke(new Action(() => { SelectedBranch = BranchComboBox.SelectedItem.ToString(); }));
            var url = new Uri(ParseRevision.revisionUrl + "/zipball/" + SelectedBranch);
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (Proxy.proxyStatus)
                request.Proxy = proxy;
            var response = (HttpWebResponse)request.GetResponse();
            response.Close();
            // Gets bytes.
            var iSize = response.ContentLength;

            // Keeping track of downloaded bytes.
            long iRunningByteTotal = 0;

            // Open Webclient.
            using (var client = new WebClient())
            {
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                // Open the file at the remote path.
                using (var streamRemote = client.OpenRead(new Uri(ParseRevision.revisionUrl + "/zipball/" + SelectedBranch)))
                {
                    // We write those files into the file system.
                    using (Stream streamLocal = new FileStream(Program.programPath + "/Repositories/Mooege.zip", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // Loop the stream and get the file into the byte buffer
                        int iByteSize;
                        var byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            // Write the bytes to the file system at the file path specified
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            // Calculate the progress out of a base "100"
                            var dIndex = (double)(iRunningByteTotal);
                            var dTotal = (double)byteBuffer.Length;
                            var dProgressPercentage = (dIndex / dTotal);
                            var iProgressPercentage = (int)(dProgressPercentage * 100);

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

            if (Configuration.MadCow.TrayNotificationsEnabled)
            {
                GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Download Complete!", ToolTipIcon.Info);
            }

            DownloadRepoBar.Value = 0;
            DownloadRepoBar.Update();
            generalProgressBar.PerformStep();
            MadCowProcedure.RunWholeProcedure();
            UpdateMooegeButton.Enabled = true;
            if (EnableAutoUpdateBox.Checked)
            {
                _tick = (int)AutoUpdateValue.Value;
                DownloadSpeedTimer.Start();
                AutoUpdateTimerLabel.Text = "Update in " + _tick + " minutes.";
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
                    MooegeSupportedVersion = mooegeVersion; //Public String to display over D3 path validation.
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
                        var ipAddress = String.Format("{0}.{0}.{0}.{0}", lp);

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
                                      InitialDirectory = Path.Combine(Program.programPath, "ServerProfiles")
                                  };
            saveProfile.ShowDialog();

            if (string.IsNullOrEmpty(saveProfile.FileName))
            {
                Console.WriteLine("You didn't specify a profile name");
            }

            else
            {
                TextWriter tw = new StreamWriter(saveProfile.FileName);
                //tw.WriteLine("Bnet Server Ip");
                tw.WriteLine(BnetServerIp.Text);
                //tw.WriteLine("Game Server Ip");
                tw.WriteLine(GameServerIp.Text);
                //tw.WriteLine("Public Server Ip");
                tw.WriteLine(PublicServerIp.Text);
                //tw.WriteLine("Bnet Server Port");
                tw.WriteLine(BnetServerPort.Text);
                //tw.WriteLine("Game Server Port");
                tw.WriteLine(GameServerPort.Text);
                //tw.WriteLine("MOTD");
                tw.WriteLine(MotdTxtBox.Text);
                //tw.WriteLine("NAT");
                tw.WriteLine(NATcheckBox.Checked);
                tw.Close();
                Console.WriteLine("Saved profile [" + Path.GetFileName(saveProfile.FileName) + "] succesfully.");
                Configuration.MadCow.CurrentProfile = saveProfile.FileName;
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
                                      InitialDirectory = Path.Combine(Program.programPath, "ServerProfiles")
                                  };
            openProfile.ShowDialog();
            if (openProfile.FileName == "")
            {
                Console.WriteLine("You didn't select a profile name");
            }

            else
            {
                TextReader tr = new StreamReader(openProfile.FileName);
                BnetServerIp.Text = tr.ReadLine();
                GameServerIp.Text = tr.ReadLine();
                PublicServerIp.Text = tr.ReadLine();
                BnetServerPort.Text = tr.ReadLine();
                GameServerPort.Text = tr.ReadLine();
                MotdTxtBox.Text = tr.ReadLine();
                NATcheckBox.Checked = tr.ReadLine().Contains("True");
                tr.Close();
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
                Console.WriteLine("Loaded Profile [{0}] succesfully.", Path.GetFileName(openProfile.FileName));
                //Proceed to save the profile over our INI file.
                Configuration.MadCow.CurrentProfile = openProfile.FileName;
            }
        }

        public void LoadLastUsedProfile()
        {
            try
            {
                if (!string.IsNullOrEmpty(Configuration.MadCow.CurrentProfile))
                {
                    TextReader tr = new StreamReader(Configuration.MadCow.CurrentProfile);
                    BnetServerIp.Text = tr.ReadLine();
                    GameServerIp.Text = tr.ReadLine();
                    PublicServerIp.Text = tr.ReadLine();
                    BnetServerPort.Text = tr.ReadLine();
                    GameServerPort.Text = tr.ReadLine();
                    MotdTxtBox.Text = tr.ReadLine();
                    NATcheckBox.Checked = tr.ReadLine().Contains("True");
                    tr.Close();

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
                    Console.WriteLine("Loaded server profile [{0}] succesfully.", Path.GetFileName(Configuration.MadCow.CurrentProfile));
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
            var currentText = ParseRevision.commitFile;
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
                    comboBox1.ForeColor = Color.Red;
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
            foreach (var value in MPQDownloader.mpqSelection)
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
                                var bytesReceivedSpeed = (iRunningByteTotal / 1024) / ts.TotalSeconds;
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
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            var downloadBaseFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/" + ErrorFinder.errorFileName + ".mpq";
            var downloadFileUrl = "http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + ErrorFinder.errorFileName + ".mpq";
            var speedTimer = new Stopwatch();

            //TODO should be simplified somehow.
            //Parsing the file name.
            var url = ErrorFinder.errorFileName.Contains("CoreData") ||
                      ErrorFinder.errorFileName.Contains("ClientData")
                          ? new Uri(downloadFileUrl)
                          : new Uri(downloadBaseFileUrl);
            //var fullName = url.LocalPath.TrimStart('/');
            var dest = ErrorFinder.errorFileName.Contains("CoreData") ||
                       ErrorFinder.errorFileName.Contains("ClientData")
                           ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                           : Path.Combine(new[]
                                              {
                                                  Configuration.MadCow.MpqDiablo,
                                                  "base",
                                                  Path.GetFileName(url.AbsolutePath)
                                              });

            var copy = ErrorFinder.errorFileName.Contains("CoreData") ||
                       ErrorFinder.errorFileName.Contains("ClientData")
                           ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                           : Path.Combine(new[]
                                              {
                                                  Configuration.MadCow.MpqDiablo,
                                                  "base",
                                                  ErrorFinder.errorFileName + ".mpq"
                                              });
            var copyDest = ErrorFinder.errorFileName.Contains("CoreData") ||
                           ErrorFinder.errorFileName.Contains("ClientData")
                               ? Path.Combine(Configuration.MadCow.MpqDiablo, Path.GetFileName(url.AbsolutePath))
                               : Path.Combine(new[]
                                                  {
                                                      Configuration.MadCow.MpqServer,
                                                      "base",
                                                      ErrorFinder.errorFileName + ".mpq"
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
                            var bytesReceivedSpeed = (iRunningByteTotal / 1024) / ts.TotalSeconds;
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
                DialogResult response = MessageBox.Show("MadCow Fixer",
		        "MadCow succesfully fixed:" + ErrorFinder.errorFileName + @".MPQ",MessageBoxButtons.OK,MessageBoxIcon.Information);

                if (response == DialogResult.OK) //We take this as the OK response.
                {
                    //Since problem must be fixed, we take the user to the Update tab & execute repo selection form again
                    //We move the user to the Help tab so he can see the progress of the download.
                    Tabs.Invoke(new Action(() => Tabs.SelectTab("tabPage1")));
                    var t = new System.Threading.Thread(ThreadProc);
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
        private Int32 _repoListIndex;

        private void RepoList()
        {
            var sr = new StreamReader(_repoListPath);
            var line = sr.ReadLine();

            while (line != null)
            {
                comboBox1.Items.Add(line);
                line = sr.ReadLine();
                _repoListIndex++;
            }
            sr.Close();
        }
        private void RepoCheck()
        {
            var p = _repoListPath;
            var lines = File.ReadAllLines(_repoListPath);
            File.WriteAllLines(_repoListPath, lines.Distinct().ToArray());
        }

        private void RepoListAdd()
        {
            var str = File.AppendText(_repoListPath);
            str.WriteLine(comboBox1.Text);
            str.Close();
        }

        private void RepoListUpdate()
        {
            RepoCheck();
            comboBox1.Items.Clear();
            var sr = new StreamReader(_repoListPath);
            var line = sr.ReadLine();

            while (line != null)
            {
                comboBox1.Items.Add(line);
                line = sr.ReadLine();
                _repoListIndex++;
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
            var sr = new StreamReader(_repoListPath);
            var line = sr.ReadLine();

            while (line != null)
            {
                var s = line.Replace(@"https://github.com/", "");
                var d = s.Replace(@"/mooege", "");
                var e = d.Replace(@"/d3sharp", "");
                SelectRepoChngLogComboBox.Items.Add(e);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        //Update Changelog repository list in real time.
        private void ChangelogListUpdate()
        {
            SelectRepoChngLogComboBox.Items.Clear();
            var sr = new StreamReader(_repoListPath);
            var line = sr.ReadLine();
            while (line != null)
            {
                var s = line.Replace(@"https://github.com/", "");
                var d = s.Replace(@"/mooege", "");
                var e = d.Replace(@"/d3sharp", "");
                SelectRepoChngLogComboBox.Items.Add(e);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        //Parse commit file and display into the textbox.
        private void DisplayChangelog(object sender, AsyncCompletedEventArgs e)
        {
            ChangeLogTxtBox.Invoke(new Action(() => ChangeLogTxtBox.Clear()));
            using (var fileStream = new FileStream(Path.Combine(Program.programPath, "Commits.ATOM"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string line;
                    var i = 0; //This is to get rid of the first <title> tag.
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
                            ChangeLogTxtBox.Invoke(new Action(() => ChangeLogTxtBox.AppendText(@"Updated: " + match.Groups[1].Value + "\n")));
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
                client.DownloadFileAsync(new Uri(SelectedRepo + @"/commits/master.atom"), Program.programPath + @"\RuntimeDownloads\Commits.atom");
                client.DownloadFileCompleted += DisplayChangelog;
            }
            catch
            {
                Console.WriteLine("Check yor internet connection");
            }
        }

        public String SelectedRepo = "";
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //We first parse the repo name selected by the user.
            SelectedRepo = SelectRepoChngLogComboBox.Text;

            //We search for that repo URL over RepoList.txt
            var sr = new StreamReader(_repoListPath);
            var line = sr.ReadLine();

            while (line != null)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(line, SelectedRepo))
                {
                    //Pass the whole URL to selectedRepo string that we will use to create the new Uri.
                    SelectedRepo = line;
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
                UpdateMooege();
            }
            else
            {
                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "You must select and validate a repository first!", ToolTipIcon.Info);
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                if (Configuration.MadCow.TrayEnabled)
                {
                    Hide();
                    if (Configuration.MadCow.TrayNotificationsEnabled)
                    {
                        if (NotifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
                        {
                            MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "MadCow will continue running minimized.", ToolTipIcon.Info);
                            NotifyCount++;
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
        protected void Exit_Click(Object sender, EventArgs e)
        {
            Close();
        }
        protected void Hide_Click(Object sender, EventArgs e)
        {
            Hide();

            if (Configuration.MadCow.TrayNotificationsEnabled)
            {
                if (NotifyCount < 1) //This is to avoid displaying this Balloon everytime the user minimize, it will only show first time.
                {
                    MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "MadCow will continue running minimized.", ToolTipIcon.Info);
                    NotifyCount++;
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
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            BranchComboBox.Invoke(new Action(() => { SelectedBranch = BranchComboBox.SelectedItem.ToString(); }));
            var client = new WebClient();
            if (Proxy.proxyStatus)
                client.Proxy = proxy;
            client.DownloadStringCompleted += BranchParse;
            var uri = new Uri(comboBox1.Text + "/commits/" + SelectedBranch + ".atom");
            client.DownloadStringAsync(uri);
        }

        private void BranchParse(object sender, DownloadStringCompletedEventArgs e)
        {
            var result = e.Result;
            var pos2 = result.IndexOf("Commit/", StringComparison.Ordinal);
            var revision = result.Substring(pos2 + 7, 7);
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
        // Remember LastRepository Disabler
        ////////////////////////////////////////////////////////////////////////////////////////
        #region RememberLastRepository
        private void rememberLastRepositoryToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.MadCow.RememberLastRepository = rememberLastRepositoryToolStripMenuItem.Checked;
            LastPlayedRepoReminderLabel.Visible = rememberLastRepositoryToolStripMenuItem.Checked;
            mpqDestinationDisableLabel.Visible = rememberLastRepositoryToolStripMenuItem.Checked;
            BrowseMPQPathButton.Enabled = !rememberLastRepositoryToolStripMenuItem.Checked;
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
        #endregion

        ////////////////////////////////////////////////////////////////////////
        //Mooege Settings tab
        ////////////////////////////////////////////////////////////////////////
        #region Mooege Settings
        private void BrowseMPQPathButton_Click(object sender, EventArgs e)
        {
            if (MpqPathBrowserDialog.ShowDialog() == DialogResult.OK)
            {
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
        }

        private void SettingsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.Mooege.FileLogging = SettingsCheckedListBox.GetItemChecked(0);
            Configuration.Mooege.PacketLogging = SettingsCheckedListBox.GetItemChecked(1);
            Configuration.Mooege.Tasks = SettingsCheckedListBox.GetItemChecked(2);
            Configuration.Mooege.LazyLoading = SettingsCheckedListBox.GetItemChecked(3);
            Configuration.Mooege.PasswordCheck = SettingsCheckedListBox.GetItemChecked(4);
        }

        private void ApplySettings()
        {
            if (Configuration.MadCow.ShortcutEnabled)
            {
                ShortCut.Create();
            }

            MPQDestTextBox.Text = Configuration.MadCow.MpqServer;

            SettingsCheckedListBox.SetItemChecked(0, Configuration.Mooege.FileLogging);
            SettingsCheckedListBox.SetItemChecked(1, Configuration.Mooege.PacketLogging);
            SettingsCheckedListBox.SetItemChecked(2, Configuration.Mooege.Tasks);
            SettingsCheckedListBox.SetItemChecked(3, Configuration.Mooege.LazyLoading);
            SettingsCheckedListBox.SetItemChecked(4, Configuration.Mooege.PasswordCheck);

            enableTrayToolStripMenuItem.Checked = Configuration.MadCow.TrayEnabled;
            enableTrayNotificationsToolStripMenuItem.Checked = Configuration.MadCow.TrayNotificationsEnabled;
            rememberLastRepositoryToolStripMenuItem.Checked = Configuration.MadCow.RememberLastRepository;
            desktopShortcutToolStripMenuItem.Checked = Configuration.MadCow.ShortcutEnabled;
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
                    + "\nSorry for the inconvenience. -Wesko","Notice",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            var ab = new AboutBox();
            ab.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.irc.Disconnect();
            Configuration.Save();
        }
    }
}