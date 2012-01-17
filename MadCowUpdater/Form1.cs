using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace MadCowUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Check.GetCurrentUserVersion();
            backgroundWorker1.RunWorkerAsync();
        }

        //Parsing for Latest Version.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String[] commiters = { "Wesko", "wetwlly" };              
            foreach (string commiter in commiters)
            {
                try
                {
                    WebClient client = new WebClient();         
                    Uri uri = new Uri("https://raw.github.com/" + commiter + "/MadCow/master/Properties/AssemblyInfo.cs");
                    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Checkversions);
                    client.DownloadStringAsync(uri);
                }
                catch
                {
                    MessageBox.Show("Check yor internet connection");
                }
            }
        }

        public static int i = 0;
        public static String[] parsed = new String[2];
        public static Int32[] LastVersions = new Int32[2];
        public Int32 LatestVersionFound;
        
        private void Checkversions(Object sender, DownloadStringCompletedEventArgs e)
        {
            parsed[i] = e.Result;
            i++;
            
            char[] r = { '"' };
            string[] arr = parsed[i-1].Split(r);
            LastVersions[i-1] = int.Parse(arr[23].Replace(".", ""));
            //We execute comparison here.
            if (i == 2)
            {
                CompareLastVersion();
            }
        }

        public void CompareLastVersion()
        {
            LatestVersionFound = Math.Max(LastVersions[0], LastVersions[1]);
            //MessageBox.Show(LatestVersionFound.ToString());
            Compare();
        }
        //Comparison Between User/Github Version.
        public void Compare()
        {
            if (Check.UserVersion == LatestVersionFound)
            {
                this.NoUpdateLabel.Visible = true;
                this.pictureBox2.Visible = true;
                this.Update();
            }
            else
            {
                this.UpdateFound.Visible = true;
                this.pictureBox1.Visible = true;
                this.UpdateButton.Enabled = true;
                this.Update();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ProcessFinder.FindProcess("MadCow2011") == true)
            {
                ProcessFinder.KillProcess("MadCow2011");
            }
            Helper.DeleteTempFolder();
            Helper.DeleteZipFile();
            backgroundWorker2.RunWorkerAsync();
            this.UpdateButton.Enabled = false;
        }

        //Downloading Latest Version
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            String LastCommiter = "";
            if (LastVersions[0] > LastVersions[1])
            {
                LastCommiter = "Wesko";
            }
            else
                LastCommiter = "wetwlly";

            Uri url = new Uri("https://github.com/" + LastCommiter + "/MadCow/zipball/master");
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
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri("https://github.com/" +LastCommiter+ "/MadCow/zipball/master")))
                {
                    // We write those files into the file system.
                    using (Stream streamLocal = new FileStream(Path.GetTempPath() + "/MadCow.zip", FileMode.Create, FileAccess.Write, FileShare.None))
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
                            backgroundWorker2.ReportProgress(iProgressPercentage);
                        }

                        // Clean up the file stream
                        streamLocal.Close();
                    }

                    // Close the connection to the remote server
                    streamRemote.Close();
                } client.Dispose();
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdaterProcedures.RunWholeProcedure();
            this.UpdateComplete.Visible = true;
            this.UpdateButton.Visible = false;
            this.Update();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //TODO: Fucking process hangs over task manager after exit I dunno why, must be checked!.
            backgroundWorker1.CancelAsync();
            backgroundWorker2.CancelAsync();
            backgroundWorker1.Dispose();
            backgroundWorker2.Dispose();
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
