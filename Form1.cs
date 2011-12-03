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

namespace MadCow
{

    public partial class Form1 : Form
    {
        //Timing
        public static String MooegeSupporterVersion;
        private int Tick;

        public Form1()
        {
            InitializeComponent();
            UpdateMooegeButton.Enabled = false;
            button3.Enabled = false;
            AutoUpdateValue.Enabled = false;
            EnableAutoUpdateBox.Enabled = false;
            button4.Enabled = false;
        }

        //-------------------------//
        // Unused Items in Form //
        //-------------------------//
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 1800;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(this.UpdateMooegeButton, "This will update mooege to latest version");
            toolTip1.SetToolTip(this.button3, "This will copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(this.button8, "This will check pre-requirements and update Mooege Server");

            //Diablo 3 Path Saving
            if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini"))
            {
                //TODO:When Ini Saves beforehand as Blank, it will copy over the blank instead of using Please Select your Path text.
                IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                String Src = source.Configs["DiabloPath"].Get("D3Path");
                Diablo3UserPathSelection.Text = Src;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button7.Enabled = true;

            }
            else Diablo3UserPathSelection.Text = "Please Select your Diablo III path.";

        }
        
                /*
            textBox13.Text = "0.0.0.0";
            textBox12.Text = "1345";
            textBox11.Text = "0.0.0.0";
            textBox10.Text = "1999";
            textBox9.Text = "0.0.0.0";
            checkBox3.Checked = false;
            textBox1.Text = "Welcome to mooege development server!";
                 */


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

        //-------------------------//
        // Update Mooege //
        //-------------------------//
        private void button1_Click_Validate_Repository(object sender, EventArgs e)
        {
            //Update Mooege - does not start Diablo

            ParseRevision.revisionUrl = textBox1_Repository_Url.Text;  
            try
                {
                    ParseRevision.GetRevision();

                    if (ParseRevision.commitFile == "ConnectionFailure")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        label2.Text = "Internet Problems.";
                    }

                    else if (ParseRevision.commitFile == "Incorrect repository entry")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false; //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;      //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        label2.Text = "Please try a different Repo.";
                    }
                    
                    else if (ParseRevision.revisionUrl.EndsWith("/"))
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = "Incorrect repository entry";
                        pictureBox1.Show();
                        AutoUpdateValue.Enabled = false;  //If validation fails we set Update and Autoupdate
                        EnableAutoUpdateBox.Enabled = false;       //functions disabled!.
                        UpdateMooegeButton.Enabled = false;
                        label2.Text = "Delete the last '/' on the repo.";
                    }
                    else

                    {
                        pictureBox2.Show();
                        textBox1_Repository_Url.ForeColor = Color.Green;
                        textBox1_Repository_Url.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        UpdateMooegeButton.Enabled = true;
                        AutoUpdateValue.Enabled = true;
                        EnableAutoUpdateBox.Enabled = true;
                        textBox13.Enabled = true;
                        textBox12.Enabled = true;
                        textBox11.Enabled = true;
                        textBox10.Enabled = true;
                        textBox9.Enabled = true;
                        checkBox3.Enabled = true;
                        textBox1.Enabled = true;
                        label2.Text = "Repository Validated!";
                    }
                }
                catch (Exception)
                {
                    pictureBox2.Hide();
                    textBox1_Repository_Url.Text = ParseRevision.errorSender;
                    pictureBox1.Show();
                }
        }

        //-------------------------//
        //   UPDATE MOOEGE: This will validate ur current revision, if outdated proceed to download calling ->backgroundWorker1.RunWorkerAsync()->backgroundWorker1_RunWorkerCompleted.
        //-------------------------//
        private void Update_Mooege_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                label2.Text = "You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision;
                
                if (EnableAutoUpdateBox.Checked == true)
                {
                Tick = (int)this.AutoUpdateValue.Value;
                label1.Text = "Update in " + Tick + " minutes.";
                }
            }

            else if (Directory.Exists(Program.programPath + @"/MPQ")) //Checks for MPQ Folder
            {
                label2.Text = "Found default MadCow MPQ folder";
                UpdateMooegeButton.Enabled = false;
                
                if (EnableAutoUpdateBox.Checked == true)
                {
                    timer1.Stop();
                    label1.Text = "Updating...";
                    backgroundWorker1.RunWorkerAsync();
                }
                label2.Text = "Updating...";
                backgroundWorker1.RunWorkerAsync();
            }

            else
            {
                if (EnableAutoUpdateBox.Checked == true)
                {
                    timer1.Stop();
                    label1.Text = "Updating...";
                }
                label2.Text = "Updating...";
                Directory.CreateDirectory(Program.programPath + "/MPQ");
                UpdateMooegeButton.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }


        //-------------------------//
        // Play Diablo Button      //
        //-------------------------//
        private void PlayDiablo_Click(object sender, EventArgs e)
        {
            //TODO: waiting time between mooege starting up and diablo?

            //Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            //Process proc0 = new Process();
            //proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
            //proc0.Start();
            //MessageBox.Show(Compile.currentMooegeExePath);
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(Diablo3UserPathSelection.Text);
            proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
            proc1.Start();
            label2.Text = "Starting Diablo..";
            
        }


        //-------------------------//
        // Update MPQS             //
        //-------------------------//

        private void UpdateMPQ_Click(object sender, EventArgs e)
        {
            Commands.RunUpdateMPQ();
        }


        //-------------------------//
        // Remote Server Settings  //
        //-------------------------//
        private void button7_Click(object sender, EventArgs e)
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
            //proc1.Start();
            label2.Text = "Starting Diablo..";
            
        }


        //-------------------------//
        // Server Control Settings //
        //-------------------------//
        private void textBox13_TextChanged(object sender, EventArgs e) { /*Bnet Server IP*/ }
        private void textBox12_TextChanged(object sender, EventArgs e) { /*Bnet Server Port*/ }
        private void textBox11_TextChanged(object sender, EventArgs e) { /*Game Server IP*/ }
        private void textBox10_TextChanged(object sender, EventArgs e) { /*Game Server Port*/ }
        private void textBox9_TextChanged(object sender, EventArgs e) { /*Public Server IP*/ }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { /*enable or disable NAT*/ }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Restore Default Server Control Settings
            textBox13.Text = "0.0.0.0";
            textBox12.Text = "1345";
            textBox11.Text = "0.0.0.0";
            textBox10.Text = "1999";
            textBox9.Text = "0.0.0.0";
            checkBox3.Checked = false;
            textBox1.Text = "Welcome to mooege development server!";
            /*IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
            IConfig config = source.Configs["MooNet-Server"];
            config.Set("BindIP", textBox13.Text);
            IConfig config1 = source.Configs["MooNet-Server"];
            config1.Set("Port", textBox12.Text);
            IConfig config2 = source.Configs["Game-Server"];
            config2.Set("BindIP", textBox11.Text);
            IConfig config3 = source.Configs["Game-Server"];
            config3.Set("Port", textBox10.Text);
            IConfig config4 = source.Configs["NAT"];
            config4.Set("PublicIP", textBox9.Text);
            IConfig config5 = source.Configs["NAT"];
            config5.Set("Enabled", "false");
            IConfig config6 = source.Configs["MooNet-Server"];
            config6.Set("MOTD", textBox1.Text);
            source.Save();*/
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                //First we modify the Mooege INI storage path.
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");

                    Console.WriteLine("Modifying MooNet-Server IP...");
                    IConfig config = source.Configs["MooNet-Server"];
                    config.Set("BindIP", textBox13.Text);
                    Console.WriteLine("Modifying MooNet-Server Port...");
                    IConfig config1 = source.Configs["MooNet-Server"];
                    config1.Set("Port", textBox12.Text);
                    Console.WriteLine("Modifying Game-Server IP...");
                    IConfig config2 = source.Configs["Game-Server"];
                    config2.Set("BindIP", textBox11.Text);
                    Console.WriteLine("Modifying Game-Server Port...");
                    IConfig config3 = source.Configs["Game-Server"];
                    config3.Set("Port", textBox10.Text);
                    Console.WriteLine("Modifying Public IP...");
                    IConfig config4 = source.Configs["NAT"];
                    config4.Set("PublicIP", textBox9.Text);
                    Console.WriteLine("Modifying MOTD...");
                    IConfig config7 = source.Configs["MooNet-Server"];
                    config7.Set("MOTD", textBox1.Text);

                if (checkBox3.Checked == true)
                    {
                        Console.WriteLine("Modifying NAT...");
                        IConfig config5 = source.Configs["NAT"];
                        config5.Set("Enabled", "true");
                        source.Save();
                    }
                    else
                    {
                        Console.WriteLine("Keeping NAT the same...");
                        IConfig config6 = source.Configs["NAT"];
                        config6.Set("Enabled", "false");
                        source.Save();
                    }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not modify Mooege INI FILE");
                Console.WriteLine("Do you have mooege?");
                label2.Text = "Update Mooege Server";
                Console.ForegroundColor = ConsoleColor.White;

            }
            /*
            Thread.Sleep(2000);
            Process proc0 = new Process();
            proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
            proc0.Start();
            */
            MessageBox.Show(Compile.currentMooegeExePath);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Updates Mooege does not check for Diablo Client
            //Instead of MadCowRunProcedure.RunMadCow(0);
 	     	//PreRequeriments.CheckPrerequeriments();
 	    	//Commands.RunUpdate();
        }


        //-------------------------//
        //   Timer For AutoUpdate  // Complex shit :P.
        //-------------------------//   
   
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
        
        //-------------------------//
        //   Diablo 3 Path Stuff   //
        //-------------------------//

        private void button9_Click(object sender, EventArgs e)
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
                    button3.Enabled = true;
                    button4.Enabled = true;
                    //Bottom three are Enabled on Remote Server
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    button7.Enabled = true;

                //We always save the path, even if the client its not compatible with current mooege.
                //Wlly*** please modify this, have a premade INI with the tags and values in blank and instead of writing the whole file over and over, just modify the tags values.
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
                }

                if (CompareD3Versions() == false) //We verify if the current version is the required by Mooege by calling VerifyVersion(), if returns false we warn him.
                {                                 //This will tae about 2-3 seconds, since it parse it right from the Mooege repo files. Similar to the little hang that happens when you fist validate a repo.
                    MessageBox.Show("You need Diablo III Client version [" + MooegeSupporterVersion + "] in order to play over Mooege.\nPlease Update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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
                 using (Stream streamLocal = new FileStream(Program.programPath + "/Mooege.zip", FileMode.Create, FileAccess.Write, FileShare.None))
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

        //
        //PROCEED WITH THE PROCESS ONCE THE DOWNLOAD ITS COMPLETE
        //
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.PerformStep();
            Commands.RunUpdate();
            label2.Text = "Update Complete";
            UpdateMooegeButton.Enabled = true;
            if (EnableAutoUpdateBox.Checked == true)
            {
                Tick = (int)this.AutoUpdateValue.Value;
                timer1.Start();
                label1.Text = "Update in " + Tick + " minutes.";
            }
        }

        //
        //URL TEXT FIELD COLOR MANAGEMENT
        //This has the function on turning letters red if Error, Black if normal.
        private void textBox1_Repository_Url_TextChanged(object sender, EventArgs e)
        {
            UpdateMooegeButton.Enabled = false;
            AutoUpdateValue.Enabled = false; //If user is typing a new URL Update and Autoupdate
            EnableAutoUpdateBox.Enabled = false;      //Functions gets disabled
            try
            {
                if (textBox1_Repository_Url.Text == "Incorrect repository entry." || textBox1_Repository_Url.Text == "Check your internet connection.")
                {
                    textBox1_Repository_Url.ForeColor = Color.Red;
                }
                else
                {
                    textBox1_Repository_Url.ForeColor = Color.Black;
                    this.label4.BackColor = System.Drawing.Color.Transparent;
                    pictureBox1.Hide();//Error Image (Cross)
                    pictureBox2.Hide();//Correct Image (Tick)
                }
            }
            catch
            {
                textBox1_Repository_Url.ForeColor = SystemColors.ControlText;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SimpleFileDelete.Delete(1);
        }
       //----------------------------------------------------------------------
       //---------------------TESTINGGGGGGGGGGGGGGGGGGG------------------------
       //Tried a lot of crap Wlly, as u see progressbar already works, but its hard
       //to actually pass the Description of what the program its doing.
       //----------------------------------------------------------------------
       
        /*public void ProgressStatusCommunicator(int procedure)
        {
            switch (procedure)
            {

                case 1:
                    label1.Text = "Download Complete";
                    progressBar1.PerformStep();
                    break;
                case 2:
                    label1.Text = "Uncompress Complete";
                    progressBar1.PerformStep();
                    break;
                case 3:
                    label1.Text = "Compile Complete";
                    progressBar1.PerformStep();
                    break;
                case 4:
                    label1.Text = "Mooege Config.ini Complete";
                    progressBar1.PerformStep();
                    break;
                case 5:
                    label1.Text = "MPQ folder Created Complete";
                    progressBar1.PerformStep();
                    break;
                case 6:
                    label1.Text = "MPQ folder Found Complete";
                    progressBar1.PerformStep();
                    break;
                case 7:
                    label1.Text = "Copying MPQs Complete";
                    progressBar1.PerformStep();
                    break;*/

        //-----------------------Test Part 2 Wlly's Code---------------------------------
        /*if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
        {
            // This path is a file
            label1.Text = "Uncompress Done";
        }
        else if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\"))
        {
            // This path is a directory
            label1.Text = "Compile Complete";
        }
        else if (Directory.Exists(Program.programPath + "/MPQ"))
        {
            label1.Text = "MPQ folder Created";
        }
        else if (Directory.Exists(Program.programPath + "/MPQ/base") && File.Exists(Program.programPath + "/MPQ/CoreData.mpq") && File.Exists(Program.programPath + "/MPQ/ClientData.mpq"))
        {
            label1.Text = "Copying MPQs Complete";
        }
        else
        {
            label1.Text = "";
        }
    }
}*/

//-------------------------//
//  Find and Kill Process  //
//-------------------------//

        public bool FindAndKillProcess(string name)
        {
        //see if process is running.
          foreach (Process clsProcess in Process.GetProcesses())
          {
               if (clsProcess.ProcessName.Contains(name))
               {
                   // Kill Kill Kill
                   clsProcess.Kill();
                   return true;
               }
          }
            //otherwise do not kill process because it's not there
            return false;
        }
        //-------------------------//
        //  ReDownload 7841 MPQ    //
        //-------------------------//
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
                    System.IO.File.Delete(MPQpath + @"\base\d3-update-base-7841.MPQ");
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
            MessageBox.Show("Download completed!");
        }

        private void button11_Click(object sender, EventArgs e)//Starts validating MD5's
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

        ////////////////////////////////////////
        //Server Control Refresh From Config.Ini
        ////////////////////////////////////////

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(Program.programPath + "\\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + "\\src\\Mooege\\bin\\Debug\\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src1 = source.Configs["MooNet-Server"].Get("BindIP");
                textBox13.Text = Src1;
            }
            else textBox13.Text = "0.0.0.0";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src2 = source.Configs["MooNet-Server"].Get("Port");
                textBox12.Text = Src2;
            }
            else textBox12.Text = "1345";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src3 = source.Configs["Game-Server"].Get("BindIP");
                textBox11.Text = Src3;
            }
            else textBox11.Text = "0.0.0.0";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src4 = source.Configs["Game-Server"].Get("Port");
                textBox10.Text = Src4;
            }
            else textBox10.Text = "1999";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src5 = source.Configs["NAT"].Get("PublicIP");
                textBox9.Text = Src5;
            }
            else textBox9.Text = "0.0.0.0";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src6 = source.Configs["MooNet-Server"].Get("MOTD");
                textBox1.Text = Src6;
            }
            else textBox1.Text = "Welcome to mooege development server!";

            if (File.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
                string Src7 = source.Configs["NAT"].Get("Enabled");
                if (Src7 == "true")
                {
                    checkBox3.Checked = true;
                }
                else
                    checkBox3.Checked = false;
            }
            else checkBox3.Checked = false;
        }

        ///////////////////////////////////////////////////////////
        //Verify Diablo 3 Version compared to Mooege supported one.
        ///////////////////////////////////////////////////////////
        
        protected Boolean CompareD3Versions()
        {
            //FileVersionInfo.GetVersionInfo(textBox4.Text);
            FileVersionInfo d3Version = FileVersionInfo.GetVersionInfo(Diablo3UserPathSelection.Text);

            try
            {
                WebClient client = new WebClient();
                String parseVersion = client.DownloadString("https://raw.github.com/mooege/mooege/master/src/Mooege/Common/Versions/VersionInfo.cs");
                Int32 ParsePointer = parseVersion.IndexOf("RequiredPatchVersion = ");
                String MooegeVersion = parseVersion.Substring(ParsePointer + 23, 4); //Gets required version by Mooege
                MooegeSupporterVersion = MooegeVersion; //Public String to display over D3 path validation.
                int CurrentD3VersionSupported = Convert.ToInt32(MooegeVersion);
                int LocalD3Version = d3Version.FilePrivatePart;

                if (LocalD3Version == CurrentD3VersionSupported)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Found the correct Mooege supported version of Diablo III [" + CurrentD3VersionSupported + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                    return true;
                }

                else if (LocalD3Version != CurrentD3VersionSupported)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mooege needs Diablo III version [" + MooegeVersion + "]"
                        + "\nYou currently own version [" + LocalD3Version + "]. Please Update.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx);
                return false;
            }
            return true;
        }
    }
 }
