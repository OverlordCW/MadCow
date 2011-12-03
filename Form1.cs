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
        private int tik;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            numericUpDown1.Enabled = false;
            checkBox1.Enabled = false;
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
            toolTip1.SetToolTip(this.button2, "This will update mooege to latest version");
            toolTip1.SetToolTip(this.button3, "This will copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(this.button8, "This will check pre-requirements and update Mooege Server");

            //Diablo 3 Path Saving
            if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                string Src = source.Configs["DiabloPath"].Get("D3Path");
                textBox4.Text = Src;
            }
            else textBox4.Text = "Please Select your Diablo III path.";

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
                        numericUpDown1.Enabled = false; //If validation fails we set Update and Autoupdate
                        checkBox1.Enabled = false;      //functions disabled!.
                        button2.Enabled = false;
                        label2.Text = "Internet Problems.";
                    }

                    else if (ParseRevision.commitFile == "Incorrect repository entry")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        numericUpDown1.Enabled = false; //If validation fails we set Update and Autoupdate
                        checkBox1.Enabled = false;      //functions disabled!.
                        button2.Enabled = false;
                        label2.Text = "Please try a different Repo.";
                    }
                    
                    else if (ParseRevision.revisionUrl.EndsWith("/"))
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = "Incorrect repository entry";
                        pictureBox1.Show();
                        numericUpDown1.Enabled = false;  //If validation fails we set Update and Autoupdate
                        checkBox1.Enabled = false;       //functions disabled!.
                        button2.Enabled = false;
                        label2.Text = "Delete the last '/' on the repo.";
                    }
                    else

                    {
                        pictureBox2.Show();
                        textBox1_Repository_Url.ForeColor = Color.Green;
                        textBox1_Repository_Url.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        button2.Enabled = true;
                        numericUpDown1.Enabled = true;
                        checkBox1.Enabled = true;
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
        //   UPDATE MOOEGE: This will validate ur current revision, if outdated prooced to download calling ->backgroundWorker1.RunWorkerAsync()->backgroundWorker1_RunWorkerCompleted.
        //-------------------------//
        private void button2_Click_Update_Mooege(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                label2.Text = "You have latest [" + ParseRevision.developerName + "] revision: " + ParseRevision.lastRevision;
                
                if (checkBox1.Checked == true)
                {
                tik = (int)this.numericUpDown1.Value;
                label1.Text = "Update in " + tik + " minutes.";
                }
            }

            else if (Directory.Exists(Program.programPath + @"/MPQ")) //Checks for MPQ Folder
            {
                label2.Text = "Found default MadCow MPQ folder";
                button2.Enabled = false;
                
                if (checkBox1.Checked == true)
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
                if (checkBox1.Checked == true)
                {
                    timer1.Stop();
                    label1.Text = "Updating...";
                }
                label2.Text = "Updating...";
                Directory.CreateDirectory(Program.programPath + "/MPQ");
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }


        //-------------------------//
        // Play Diablo Button      //
        //-------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            //TODO: waiting time between mooege starting up and diablo?

            //Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            //Process proc0 = new Process();
            //proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
            //proc0.Start();
            //MessageBox.Show(Compile.currentMooegeExePath);
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(textBox4.Text);
            proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
            proc1.Start();
            label2.Text = "Starting Diablo..";
            
        }


        //-------------------------//
        // Update MPQS             //
        //-------------------------//

        private void button3_Click(object sender, EventArgs e)
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
            proc1.StartInfo = new ProcessStartInfo(textBox4.Text);
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
            IConfigSource source = new IniConfigSource(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini");
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
            source.Save();
            
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
        //      Timer Stuff        //
        //-------------------------//   
   
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tik = (int)this.numericUpDown1.Value;

            if (checkBox1.Checked == true)
            {
                label1.Text = "Update in " + tik + " minutes.";
                timer1.Start();
                numericUpDown1.Enabled = false;
            }

            else if (checkBox1.Checked == false)
            {
                timer1.Stop();
                label1.Text = " ";
                numericUpDown1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           tik--;
           if (tik == 0)
           {
                button2.PerformClick(); //Runs Update
                tik = (int)this.numericUpDown1.Value;
           }
           else
               label1.Text = "Update in " + tik + " minutes.";
        }
        
        //-------------------------//
        //   Diablo 3 Path Stuff   //
        //-------------------------//

        private void button9_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            OpenFileDialog FindD3Exe = new OpenFileDialog();
            FindD3Exe.Title = "MadCow By Wesko";
            FindD3Exe.InitialDirectory = @"C:\Program Files (x86)\Diablo III Beta\";
            FindD3Exe.Filter = "Diablo III|Diablo III.exe";
            if (FindD3Exe.ShowDialog() == DialogResult.OK) // Test result.
            {
                // Get the directory name.
                string dirName = System.IO.Path.GetDirectoryName(FindD3Exe.FileName);
                // Output Name
                textBox4.Text = FindD3Exe.FileName;
                button3.Enabled = true;
                button4.Enabled = true;
                //Bottom three are Enabled on Remote Server
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button7.Enabled = true;
            }
            String val = "madcow.ini";
            if (File.Exists(Program.programPath + "\\Tools\\" + val))
            {
                FileInfo fi = new FileInfo(Program.programPath + "\\Tools\\" + val);
                StreamWriter sw = fi.CreateText();
                sw.WriteLine(@"[DiabloPath]");
                sw.WriteLine(@"D3Path = " + textBox4.Text);
                sw.WriteLine(@"MPQpath = " + new FileInfo(textBox4.Text).DirectoryName + "\\Data_D3\\PC\\MPQs");
                sw.Close();
                Console.WriteLine("CREATED MADCOW.INI");
            }
            else
            {
                FileInfo fi = new FileInfo(Program.programPath + "\\Tools\\" + val);
                StreamWriter sw = fi.CreateText();
                sw.WriteLine(@"[DiabloPath]");
                sw.WriteLine(@"D3Path = " + textBox4.Text);
                sw.WriteLine(@"MPQpath = " + new FileInfo(textBox4.Text).DirectoryName + "\\Data_D3\\PC\\MPQs");
                sw.Close();
                Console.WriteLine("CREATED MADCOW.INI");
            }
                    
        }

        private void textBox4_TextChanged(object sender, EventArgs e) { /*Diablo Path*/ }

        //DOWNLOAD SOURCE FROM REPOSITORY
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
            button2.Enabled = true;
            if (checkBox1.Checked == true)
            {
                tik = (int)this.numericUpDown1.Value;
                timer1.Start();
                label1.Text = "Update in " + tik + " minutes.";
            }
        }

        //
        //URL TEXT FIELD COLOR MANAGEMENT
        //This has the function on turning letters red if Error, Black if normal.
        private void textBox1_Repository_Url_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            numericUpDown1.Enabled = false; //If user is typing a new URL Update and Autoupdate
            checkBox1.Enabled = false;      //Functions gets disabled
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
        private void button10_Click(object sender, EventArgs e)
        {  
            SimpleFileDelete.Delete(0);
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string MPQpath = source.Configs["DiabloPath"].Get("MPQpath");
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7841.MPQ"), MPQpath + @"\base\d3-update-base-7841.MPQ");
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar3.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MPQprocedure.ValidateMD5();
        }

        //Server Control Refresh From Config.Ini
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
    }
 }
