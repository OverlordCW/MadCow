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
using System.Threading.Tasks;
using System.Threading;

namespace MadCowUpdater
{
    public partial class Form1 : Form
    {
        public static Form1 GlobalAccess;

        public Form1()
        {
            GlobalAccess = this;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SearchLabel.Visible = true;
            Check.GetCurrentUserVersion();
            backgroundWorker1.RunWorkerAsync();
        }

        //We download both AssemblyInfo.cs from Wesko and Wetwlly for further comparison. Hardcoded? Kinda :P.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String[] commiters = { "Wesko", "wetwlly" };
            foreach (string commiter in commiters)
            {
                try
                {
                    WebClient client = new WebClient();
                    Uri uri = new Uri("https://raw.github.com/" + commiter + "/MadCow/master/Properties/AssemblyInfo.cs");
                    parsed.Add(client.DownloadString(uri));
                    client.Dispose();
                }
                catch
                {
                    MessageBox.Show("Check yor internet connection");
                }
            }
            ParseVersions();
        }

        public List<String> parsed = new List<string>();
        public List<Int32> LastVersion = new List<Int32>();
        //We add each version found on each branch to our LastVersion List.
        public void ParseVersions()
        {
            int i = 0;
            foreach (string element in parsed)
            {
                char[] r = { '"' };
                string[] arr = element.Split(r);
                LastVersion.Add(int.Parse(arr[23].Replace(".", "")));
                i++;
            }
            CompareLastVersion();
        }

        public Int32 LatestVersionFound = 0;
        public String LastCommiter = "";
        //We take the max version out of the List and assign the correct developer for URI use.
        public void CompareLastVersion()
        {
            LatestVersionFound = Math.Max(LastVersion[0], LastVersion[1]);

            if (LastVersion[0] > LastVersion[1])
            {
                LastCommiter = "Wesko";
            }
            else
            {
                LastCommiter = "wetwlly";
            }
            Compare();
        }
        //Comparison Between User/Github Version.      
        public void Compare()
        {
            this.SearchLabel.Invoke((MethodInvoker)delegate { this.SearchLabel.Visible = false; });
            if (Check.UserVersion == LatestVersionFound)
            {
                this.NoUpdateLabel.Invoke((MethodInvoker)delegate { this.NoUpdateLabel.Visible = true; });
                this.NoUpdateLabel.Invoke((MethodInvoker)delegate { this.NoUpdateCross.Visible = true; });
                Form1.GlobalAccess.Invoke((MethodInvoker)delegate { this.timer1.Enabled = true; });
            }
            else
            {
                this.UpdateFound.Invoke((MethodInvoker)delegate { this.UpdateFound.Visible = true; });
                this.UpdateFoundTick.Invoke((MethodInvoker)delegate { this.UpdateFoundTick.Visible = true; });
                this.UpdateButton.Invoke((MethodInvoker)delegate { this.UpdateButton.Enabled = true; });
            }
        }
        //Update Button Clic Event.
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ProcessFinder.FindProcess("MadCow") == true)
            {
                ProcessFinder.KillProcess("MadCow");
            }
            Helper.DeleteTempFolder();
            Helper.DeleteZipFile();

            //Procedure Descriptors.
            this.DownloadingLabel.Visible = true;
            this.UncompressingLabel.Visible = true;
            this.CopyingLabel.Visible = true;
            this.CompilingLabel.Visible = true;
            this.UpdatingLabel.Visible = true;

            //Hide previous images and disable the Update Button.
            this.UpdateFoundTick.Visible = false;
            this.UpdateFound.Visible = false;
            this.UpdateButton.Enabled = false;
            this.UpdateFound.Visible = false;
            backgroundWorker2.RunWorkerAsync();
        }

        //Downloading Latest Version from assigned developer.
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
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
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri("https://github.com/" + LastCommiter + "/MadCow/zipball/master")))
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
            this.DownloadSuccessDot.Visible = true;
            this.DownloadingLabel.ForeColor = Color.Green;
            //When the download is complete, we execute the uncompress/delete/copy procedures.
            UpdaterProcedures.Uncompress();
            Helper.ModifyFolderName();
            //We delete the MadCowUpdater files in the new folder.
            Helper.DeleteMadCowUpdaterFiles();
            //Compile
            UpdaterProcedures.CompileSource();
            //Copy Files
            UpdaterProcedures.CopyFiles();
            //Clean downloaded/generated files.         
            Helper.DeleteTempFolder();
            Helper.DeleteZipFile();
            Form1.GlobalAccess.Invoke(new Action(() => 
            {
            UpdateComplete.Visible = true;
            UpdatingLabel.Visible = false;
            UpdateButton.Visible = false;
            }));

            //We launch MadCow.
            Task.WaitAll();
            this.timer1.Interval = 3000;
            this.timer1.Enabled = true;
        }

        //Timer tick event, if no update found just to close the updater app.
        private void timer1_Tick(object sender, EventArgs e)
        {
            var MadCowPath = Directory.GetParent(Program.path);
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = MadCowPath + @"\MadCow.exe";
            firstProc.Start();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
