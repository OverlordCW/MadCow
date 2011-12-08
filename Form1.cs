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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Nini.Config;
using System.Threading;
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

        public Form1()
        {
            InitializeComponent();
            AutoUpdateValue.Enabled = false;
            EnableAutoUpdateBox.Enabled = false;
            PlayDiabloButton.Enabled = false;
            SimpleFileDelete.HideFile();
        }

        ///////////////////////////////////////////////////////////
        //Unused shit
        ///////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            Console.WriteLine("Welcome to MadCow!");
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 1800;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(this.UpdateMooegeButton, "This will update mooege to latest version");
            toolTip1.SetToolTip(this.CopyMPQButton, "This will copy MPQ's if you have D3 installed");
            InitializeFindPath();
        }

        ///////////////////////////////////////////////////////////
        //Update Mooege
        ///////////////////////////////////////////////////////////
        private void Validate_Repository_Click(object sender, EventArgs e)
        {
            //Update Mooege - does not start Diablo

            ParseRevision.revisionUrl = comboBox1.Text;  
            try
                {
                    ParseRevision.GetRevision();

                    if (ParseRevision.commitFile == "ConnectionFailure")
                    {
                        pictureBox2.Hide();
                        comboBox1.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Internet Problems.");
                    }

                    else if (ParseRevision.commitFile == "Incorrect repository entry")
                    {
                        pictureBox2.Hide();
                        comboBox1.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Please try a different Repository.");
                    }
                    
                    else if (ParseRevision.revisionUrl.EndsWith("/"))
                    {
                        pictureBox2.Hide();
                        comboBox1.Text = "Incorrect repository entry";
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false;  //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;       //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        Console.WriteLine("Delete the last '/' on the repository.");
                    }
                    else

                    {
                        pictureBox2.Show();
                        comboBox1.ForeColor = Color.Green;
                        comboBox1.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        UpdateMooegeButton.Enabled = true;
                        AutoUpdateValue.Enabled = true;
                        EnableAutoUpdateBox.Enabled = true;
                        BnetServerIp.Enabled = true;
                        BnetServerPort.Enabled = true;
                        GameServerIp.Enabled = true;
                        GameServerPort.Enabled = true;
                        PublicServerIp.Enabled = true;
                        NATcheckBox.Enabled = true;
                        MOTD.Enabled = true;
                        Console.WriteLine("Repository Validated!");
                    }
                }
                catch (Exception)
                {
                    pictureBox2.Hide();
                    comboBox1.Text = ParseRevision.errorSender;
                    pictureBox1.Show();
                }
        }

        /////////////////////////////
        //UPDATE MOOEGE: This will validate ur current revision, if outdated proceed to download calling ->backgroundWorker1.RunWorkerAsync()->backgroundWorker1_RunWorkerCompleted.
        /////////////////////////////
        private void Update_Mooege_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                Console.WriteLine("You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision);
                
                if (EnableAutoUpdateBox.Checked == true)
                {
                Tick = (int)this.AutoUpdateValue.Value;
                label1.Text = "Update in " + Tick + " minutes.";
                }
            }

            else if (Directory.Exists(Program.programPath + @"/MPQ")) //Checks for MPQ Folder
            {
                Console.WriteLine("Found default MadCow MPQ folder");
                UpdateMooegeButton.Enabled = false;
                
                if (EnableAutoUpdateBox.Checked == true)
                {
                    timer1.Stop();
                    Console.WriteLine("Updating...");
                    backgroundWorker1.RunWorkerAsync();
                }
                Console.WriteLine("Updating...");
                backgroundWorker1.RunWorkerAsync();
            }

            else
            {
                if (EnableAutoUpdateBox.Checked == true)
                {
                    timer1.Stop();
                    Console.WriteLine("Updating...");
                }
                Console.WriteLine("Updating...");
                Directory.CreateDirectory(Program.programPath + "/MPQ");
                UpdateMooegeButton.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }


        ///////////////////////////////////////////////////////////
        //Play Diablo Button
        ///////////////////////////////////////////////////////////
        private void PlayDiablo_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.Start();
        }

        public static void ThreadProc()
        {
            Application.Run(new RepositorySelectionPlay());
        }

        ///////////////////////////////////////////////////////////
        //Copy MPQ files from D3 to MadCow MPQ Folder.
        ///////////////////////////////////////////////////////////

        private void CopyMPQs_Click(object sender, EventArgs e)
        {
            Commands.RunUpdateMPQ();
        }


        ///////////////////////////////////////////////////////////
        //Remote Server Settings
        ///////////////////////////////////////////////////////////
        private void RemoteServer_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(Diablo3UserPathSelection.Text);
            String HostIP = textBox2.Text;
            String Port = textBox3.Text;
            String ServerHost = HostIP + @":" + Port;
            proc1.StartInfo.Arguments = @" -launch -auroraaddress " + ServerHost;
            MessageBox.Show(proc1.StartInfo.Arguments);
            proc1.Start();
            Console.WriteLine("Starting Diablo...");           
        }


        ///////////////////////////////////////////////////////////
        //Server Control Settings
        ///////////////////////////////////////////////////////////
        private void RestoreDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Restore Default Server Control Settings
            BnetServerIp.Text = "0.0.0.0";
            BnetServerPort.Text = "1345";
            GameServerIp.Text = "0.0.0.0";
            GameServerPort.Text = "1999";
            PublicServerIp.Text = "0.0.0.0";
            NATcheckBox.Checked = false;
            MOTD.Text = "Welcome to mooege development server!";            
        }

        private void LaunchServer_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc2));
            t.Start();
        }

        public static void ThreadProc2()
        {
            Application.Run(new RepositorySelectionServer());
        }

        ///////////////////////////////////////////////////////////
        //Timer stuff for AutoUpdate
        ///////////////////////////////////////////////////////////
   
        private void AutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Tick = (int)this.AutoUpdateValue.Value;

            if (EnableAutoUpdateBox.Checked == true)
            {
                label1.Text = "Update in " + Tick + " minutes.";
                timer1.Start();
                AutoUpdateValue.Enabled = false;
            }

            else if (EnableAutoUpdateBox.Checked == false)
            {
                timer1.Stop();
                label1.Text = " ";
                AutoUpdateValue.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           Tick--;
           if (Tick == 0)
           {
                UpdateMooegeButton.PerformClick(); //Runs Update
                Tick = (int)this.AutoUpdateValue.Value;
           }
           else
               label1.Text = "Update in " + Tick + " minutes.";
        }

        ///////////////////////////////////////////////////////////
        //Diablo Path Stuff
        ///////////////////////////////////////////////////////////

        private void InitializeFindPath()
        {
            if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini"))
            {
                try
                {
                    IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                    String Src = source.Configs["DiabloPath"].Get("D3Path");

                    if (Src.Contains("MODIFY")) //If a d3 path exist, then we wont find "MODIFY" and program will proceed.
                    {
                        //TODO: Even though Diablo3UserPathSelection Information below is changed, it will bypass these below
                        //      and believe that they should be enabled, because there is "info" in the Src from madcow.ini, even
                        //      though it seems empty.
                        Diablo3UserPathSelection.Text = "Please Select your Diablo III path.";
                        CopyMPQButton.Enabled = false;
                        PlayDiabloButton.Enabled = false;
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        RemoteServerButton.Enabled = false;
                    }
                    else
                    {
                        Diablo3UserPathSelection.Text = Src;
                        CopyMPQButton.Enabled = true;
                        PlayDiabloButton.Enabled = true;
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                        RemoteServerButton.Enabled = true;
                        //Freezes at the start longer, but at least you get verified when you load!
                        backgroundWorker2.RunWorkerAsync(); //Compares Versions of D3 with Mooege
                        ValidateMPQButton.Enabled = true;
                        RedownloadMPQButton.Enabled = true;
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
            String madCowIni = "madcow.ini"; //Our INI setting file.
            //Opens path to find Diablo3
            OpenFileDialog FindD3Exe = new OpenFileDialog();
            FindD3Exe.Title = "MadCow By Wesko";
            FindD3Exe.InitialDirectory = @"C:\Program Files (x86)\Diablo III Beta\";
            FindD3Exe.Filter = "Diablo III|Diablo III.exe";

            if (FindD3Exe.ShowDialog() == DialogResult.OK) // If user was able to locate Diablo III.exe
            {
                    // Get the directory name.
                    String dirName = System.IO.Path.GetDirectoryName(FindD3Exe.FileName);
                    // Output Name
                    Diablo3UserPathSelection.Text = FindD3Exe.FileName;
                    //Bottom three are Enabled on Remote Server
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    RemoteServerButton.Enabled = true;
                    RedownloadMPQButton.Enabled = true;
                    ValidateMPQButton.Enabled = true;

                if (File.Exists(Program.programPath + "\\Tools\\" + madCowIni))
                {
                    //First we modify the Mooege INI storage path.
                    IConfigSource source = new IniConfigSource(Program.programPath + "\\Tools\\" + madCowIni);
                    IConfig config = source.Configs["DiabloPath"];
                    config.Set("D3Path", Diablo3UserPathSelection.Text);
                    IConfig config1 = source.Configs["DiabloPath"];
                    config1.Set("MPQpath", new FileInfo(Diablo3UserPathSelection.Text).DirectoryName + "\\Data_D3\\PC\\MPQs");
                    source.Save();
                    Console.WriteLine("MODIFIED MADCOW.INI WITH D3 PATHS");
                    Console.WriteLine("Verifying Diablo..");
                }

                backgroundWorker2.RunWorkerAsync(); //Compares versions of D3 with Mooege
            }
            else //If user didn't select a Diablo III.exe, we show a warning and ofc, we dont save any path.
            {
                MessageBox.Show("You didn't select a Diablo III client", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        /////////////////////////////////
        //DOWNLOAD SOURCE FROM REPOSITORY
        /////////////////////////////////

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {     
            Uri url = new Uri(ParseRevision.revisionUrl + "/zipball/master");
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            // Gets bytes.
            Int64 iSize = response.ContentLength;

            // Keeping track of downloaded bytes.
            Int64 iRunningByteTotal = 0;

            // Open Webclient.
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
            // Open the file at the remote path.
            using (System.IO.Stream streamRemote = client.OpenRead(new Uri(ParseRevision.revisionUrl + "/zipball/master")))
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
                      backgroundWorker1.ReportProgress(iProgressPercentage);
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
            progressBar2.Value = e.ProgressPercentage;
        }
                
        //PROCEED WITH THE PROCESS ONCE THE DOWNLOAD ITS COMPLETE
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.PerformStep();
            Commands.RunUpdate();
            Console.WriteLine("Update Complete!");
            UpdateMooegeButton.Enabled = true;
            if (EnableAutoUpdateBox.Checked == true)
            {
                Tick = (int)this.AutoUpdateValue.Value;
                timer1.Start();
                label1.Text = "Update in " + Tick + " minutes.";
            }
        }

        ///////////////////////////////////////////////////////////
        //ResetRepoFolder
        ///////////////////////////////////////////////////////////
        
        private void ResetRepoFolder_Click(object sender, EventArgs e)
        {
            SimpleFileDelete.Delete(1);
        }

        ///////////////////////////////////////////////////////////
        //Redownload 7841
        ///////////////////////////////////////////////////////////

        private void ReDownloadMPQ_Click(object sender, EventArgs e)
        {  
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            String MPQpath = source.Configs["DiabloPath"].Get("MPQpath");

            if (System.IO.Directory.Exists(MPQpath))
            {
                var answer = MessageBox.Show("Are you sure you want to delete current 7841.MPQ file?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    if (ProcessFind.FindProcess("Diablo") == true)
                    {
                        ProcessFind.KillProcess("Diablo");
                        Thread.Sleep(500);
                        System.IO.File.Delete(MPQpath + @"\base\d3-update-base-7841.MPQ");
                    }
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                        webClient.DownloadFileAsync(new Uri("http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7841.MPQ"), MPQpath + @"\base\d3-update-base-7841.MPQ");
                    }
                    catch (Exception web)
                    {
                        Console.WriteLine(web);
                    }
                }
                else
                {
                    //Do nothing!
                }
            }

            else //If the file doesn't exist already we dont ask the user.
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webClient.DownloadFileAsync(new Uri("http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7841.MPQ"), MPQpath + @"\base\d3-update-base-7841.MPQ");
                }
                catch (Exception web)
                {
                    Console.WriteLine(web);
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)//Download MPQ Progress bar.
        {
            progressBar3.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download completed!");
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string Src = source.Configs["DiabloPath"].Get("MPQpath");
            String Dst = Program.programPath + @"/MPQ";
            File.Copy(Src + @"/base/d3-update-base-7841.MPQ", Dst + @"/base/d3-update-base-7841.MPQ", true);
            Console.WriteLine("Moved New MPQ to Mooege's MPQ Folder.");
        }

        ///////////////////////////////////////////////////////////
        //Validate MPQ's-MD5
        ///////////////////////////////////////////////////////////

        private void ValidateMPQs_Click(object sender, EventArgs e)//Starts validating MD5's
        {
            MPQprocedure.ValidateMD5();

            if (MPQprocedure.ValidateMD5() == true)
            {
                MessageBox.Show("Found correct hashes for MPQ files"); //WLLY AT SOME POINT WE NEED TO USE ANOTHER OUTPUT FOR INFORMATION, MAYBE OVER A LABEL.
                //USING POP UP BOXES ITS AGAINST GOOD INTERFACE DESIGN SINCE IT STOPS THE PROGRAM "FLOW".
            }
            else
            {
                MessageBox.Show("Found Incorrect Hashes! Please use our Help Tab.", "Warning", //TODO: Give specific corrupted file feedback.
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///////////////////////////////////////////////////////////
        //Verify Diablo 3 Version compared to Mooege supported one.
        ///////////////////////////////////////////////////////////
        
        //We open a WebClient in the backgroundworker so it doesnt freeze MadCow UI.
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Checkversions);
                Uri uri = new Uri("https://raw.github.com/mooege/mooege/master/src/Mooege/Common/Versions/VersionInfo.cs");
                client.DownloadStringAsync(uri);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }

        //After Asyn download complete, we proceed to parse the required version by Mooege in VersionInfo.cs.
        private void Checkversions(Object sender, DownloadStringCompletedEventArgs e)
        {
            String parseVersion = e.Result;
            FileVersionInfo d3Version = FileVersionInfo.GetVersionInfo(Diablo3UserPathSelection.Text);
            Int32 ParsePointer = parseVersion.IndexOf("RequiredPatchVersion = ");
            String MooegeVersion = parseVersion.Substring(ParsePointer + 23, 4); //Gets required version by Mooege
            MooegeSupportedVersion = MooegeVersion; //Public String to display over D3 path validation.
            int CurrentD3VersionSupported = Convert.ToInt32(MooegeVersion);
            int LocalD3Version = d3Version.FilePrivatePart;

            if (LocalD3Version == CurrentD3VersionSupported)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Found the correct Mooege supported version of Diablo III [" + CurrentD3VersionSupported + "]");
                Console.ForegroundColor = ConsoleColor.White;
                //Following code its needed to access Form1 Objects from a different thread
                //Remember this function its called from backgroundworker2, so its running on a different thread than Control's Main thread.
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
            else if (LocalD3Version != CurrentD3VersionSupported)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong client version FOUND!");
                Console.ForegroundColor = ConsoleColor.White;
                MessageBox.Show("You need Diablo III Client version [" + MooegeSupportedVersion + "] in order to play over Mooege.\nPlease Update.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Following code its needed to access Form1 Objects from a different thread
                //Remember this function its called from backgroundworker2, so its running on a different thread than Control's Main thread.
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
            }
        }

        ///////////////////////////////////////////////////////////
        //Save Profile (We write a .mdc (Madcow :P) file into "ServerProfiles" Folder. //We validate first the IP & Port Entries.
        ///////////////////////////////////////////////////////////
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
                    tw.WriteLine(this.MOTD.Text);
                    //tw.WriteLine("NAT");
                    tw.WriteLine(this.NATcheckBox.Checked);
                    tw.Close();
                    Console.Write("Saved profile " + saveProfile.FileName + " succesfully");
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

        ///////////////////////////////////////////////////////////
        //Load Profile - We load from a .mdc file, update CurrenProfile variable and parse the values into the boxes.
        ///////////////////////////////////////////////////////////
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
                this.MOTD.Text = tr.ReadLine();
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
                Console.Write("Loaded Profile " + OpenProfile.FileName + " succesfully");
            }
        }

        ////////////////////////////////////////////////////////////////////////
        //URL TEXT FIELD COLOR MANAGEMENT
        //This has the function on turning letters red if Error, Black if normal.
        ////////////////////////////////////////////////////////////////////////

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMooegeButton.Enabled = false;
            AutoUpdateValue.Enabled = false; //If user is typing a new URL Update and Autoupdate
            EnableAutoUpdateBox.Enabled = false;      //Functions gets disabled
            try
            {
                if (comboBox1.Text == "Incorrect repository entry." || comboBox1.Text == "Check your internet connection.")
                {
                    this.comboBox1.ForeColor = Color.Red;
                }
                else
                {
                    comboBox1.ForeColor = Color.Black;
                    this.label4.BackColor = System.Drawing.Color.Transparent;
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

        private void tabPage1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        private void toolTip1_Popup(object sender, PopupEventArgs e) { }
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox13_TextChanged(object sender, EventArgs e) { /*Bnet Server IP*/ }
        private void textBox12_TextChanged(object sender, EventArgs e) { /*Bnet Server Port*/ }
        private void textBox11_TextChanged(object sender, EventArgs e) { /*Game Server IP*/ }
        private void textBox10_TextChanged(object sender, EventArgs e) { /*Game Server Port*/ }
        private void textBox9_TextChanged(object sender, EventArgs e) { /*Public Server IP*/ }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { /*enable or disable NAT*/ }
    }
}
