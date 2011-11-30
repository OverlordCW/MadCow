using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //Timing
        //private int tik;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;

        }

        //-------------------------//
        // Unused Items in Form //
        //-------------------------//
        private void Form1_Load(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }

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
                        button2.Enabled = false;
                    }

                    else if (ParseRevision.commitFile == "Incorrect repository entry")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        button2.Enabled = false;
                    }
                    
                    else if (ParseRevision.revisionUrl.EndsWith("/"))
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = "Incorrect repository entry";
                        pictureBox1.Show();
                        button2.Enabled = false;
                    }
                    else

                    {
                        pictureBox2.Show();
                        textBox1_Repository_Url.ForeColor = Color.Green;
                        textBox1_Repository_Url.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        button2.Enabled = true;
                    }
                }
                catch (Exception)
                {
                    pictureBox2.Hide();
                    textBox1_Repository_Url.Text = ParseRevision.errorSender;
                    pictureBox1.Show();
                }
        }

        private void button2_Click_Update_Mooege(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                Console.WriteLine("You have latest [" + ParseRevision.developerName + "] Mooege revision: " + ParseRevision.lastRevision);
            }

            else if (Directory.Exists(Program.programPath + "/MPQ")) //Checks for MPQ Folder
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Found default MadCow MPQ folder");
                Console.ForegroundColor = ConsoleColor.White;
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }

            else
            {
                Console.WriteLine("Creating MadCow MPQ folder...");
                Directory.CreateDirectory(Program.programPath + "/MPQ");
                Console.ForegroundColor = ConsoleColor.Green;
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        //-------------------------//
        // First Run Through Button //
        //-------------------------//
        private void button5_Click(object sender, EventArgs e)
        {
            //creates folders needed, copies over MPQs
            //MadCowRunProcedure.RunMadCow(1);
        }

        //-------------------------//
        // Play Diablo //
        //-------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            //Starts Mooege
            //Run Diablo - Local Host
        }

        //-------------------------//
        // Update MPQS //
        //-------------------------//

        //-------------------------//
        // Remote Server Settings //
        //-------------------------//
        private void button7_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Remote Server Host IP
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Remote Server Host Port
        }

        //-------------------------//
        // Server Control Settings //
        //-------------------------//
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //Bnet Server IP
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //Bnet Server Port
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //Game Server IP
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //Game Server Port
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //Public Server IP
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //enable or disable NAT
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //restores default settings
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //only launch mooege (mostly for servers)
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Updates Mooege does not check for Diablo Client
            //MadCowRunProcedure.RunMadCow(0);
        }

        //-------------------------//
        // Timer Stuff //
        //-------------------------//
        /*
                private void checkBox1_CheckedChanged(object sender, EventArgs e)
                {
            
                    if (checkBox1.Checked == true)
                    {
                        tik = (int)this.numericUpDown1.Value * 60;
                        timer1.Start();
                    }
                    else
                    {
                        timer1.Stop();
                        label5.Text = " ";
                    }
                }

                private void timer1_Tick(object sender, EventArgs e)
                {
                    tik--;
                    if (tik == 0)
                    {
                        label5.Text = "Checking..";
                        Commands.RunUpdate();
                        timer1.Stop();
                    }
                    else
                        label5.Text = "Check in " + tik.ToString();
                }
        */
        //-------------------------//
        // Diablo 3 Path Stuff //
        //-------------------------//
        private void button9_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            OpenFileDialog d3folder = new OpenFileDialog();
            d3folder.Title = "Diablo 3.exe";
            d3folder.InitialDirectory = @"C:\Program Files x86\Diablo III Beta\";
            if (d3folder.ShowDialog() == DialogResult.OK) // Test result.
            {
                // Get the directory name.
                string dirName = System.IO.Path.GetDirectoryName(d3folder.FileName);
                // Output Name
                textBox4.Text = d3folder.FileName;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //Diablo Path
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

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

        //PROCEED WITH THE PROCESS ONCE THE DOWNLOAD ITS COMPLETE
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Commands.RunUpdate();
        }

        //URL TEXT FIELD COLOR MANAGEMENT
        private void textBox1_Repository_Url_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                if (textBox1_Repository_Url.Text == "Incorrect repository entry." || textBox1_Repository_Url.Text == "Check your internet connection.")
                {
                    textBox1_Repository_Url.ForeColor = Color.Red;
                    this.label4.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    textBox1_Repository_Url.ForeColor = Color.Black;
                    this.label4.BackColor = System.Drawing.Color.Transparent;
                }
            }
            catch
            {
                textBox1_Repository_Url.ForeColor = SystemColors.ControlText;
            }
        }
    }
}
