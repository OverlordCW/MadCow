using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace MadCow
{
    internal class Repository
    {
        private readonly RevisionParser _revisionParser;

        internal Repository(string url, string branch = "master")
        {
            Url = url;
            Branch = branch;
            _revisionParser = new RevisionParser(url);
        }

        internal string RepositoryPath { get; set; }

        internal string Url { get; private set; }

        internal string Branch { get; private set; }

        internal string Revision { get; set; }

        internal DateTime Date { get { return Directory.GetLastWriteTime(RepositoryPath); } }

        internal bool IsSelected { get; set; }

        internal bool IsDownloaded { get { return !string.IsNullOrEmpty(RepositoryPath); } }

        internal void Delete()
        {
            if (!string.IsNullOrEmpty(RepositoryPath))
            {
                Directory.Delete(RepositoryPath, true);
            }
            RepositoryPath = string.Empty;
        }

        internal bool Download()
        {
            
            //We set or "reset" progressbar value to zero.
            Form1.GlobalAccess.statusStripStatusLabel.Text = "Updating...";
            Form1.GlobalAccess.statusStripProgressBar.Value = 0;
            var repPath = Path.Combine(new[]
                                           {
                                               Program.programPath,
                                               "Repositories",
                                               _revisionParser.GetPath()
                                           });
            if (Directory.Exists(repPath))
            {
                Console.WriteLine("You have latest [{0}] revision: {1}",
                                  _revisionParser.DeveloperName,
                                  _revisionParser.LastRevision);

                Tray.ShowBalloonTip(string.Format("You have latest [{0}] revision: {1}",
                                                            _revisionParser.DeveloperName,
                                                            _revisionParser.LastRevision));

            }

            else if (Directory.Exists(Path.Combine(Program.programPath, "MPQ"))) //Checks for MPQ Folder
            {
                Console.WriteLine("Found default MadCow MPQ folder");
                DeleteHelper.DeleteOldRepoVersion(_revisionParser.DeveloperName); //We delete old repo version.
                //UpdateMooegeButton.Enabled = false;
                Console.WriteLine("Downloading...");

                Tray.ShowBalloonTip("Downloading...");


                //DownloadRepository.RunWorkerAsync();
            }

            else
            {
                DeleteHelper.DeleteOldRepoVersion(_revisionParser.DeveloperName); //We delete old repo version.
                Console.WriteLine("Downloading...");

                Tray.ShowBalloonTip("Downloading...");


                Directory.CreateDirectory(Path.Combine(Program.programPath, "MPQ"));
                //UpdateMooegeButton.Enabled = false;
                //DownloadRepository.RunWorkerAsync();
            }
            return false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }
            //We get the selected branch first.
            //BranchComboBox.Invoke(new Action(() => { SelectedBranch = BranchComboBox.SelectedItem.ToString(); }));
            var url = new Uri(Url + "/zipball/" + Branch);
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
                using (var streamRemote = client.OpenRead(new Uri(Url + "/zipball/" + Branch)))
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
                            //DownloadRepository.ReportProgress(iProgressPercentage);
                        }

                        // Clean up the file stream
                        streamLocal.Close();
                    }

                    // Close the connection to the remote server
                    streamRemote.Close();
                }
            }

        }

        internal bool Compile()
        {
            return false;
        }
    }
}
