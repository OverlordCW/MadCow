using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

using ICSharpCode.SharpZipLib.Zip;

using Microsoft.Build.Evaluation;
using Microsoft.Build.Logging;

namespace MadCow
{
    internal class Repository
    {
        #region Fields
        private readonly RevisionParser _revisionParser;

        private readonly BackgroundWorker _updateBackgroundWorker;

        internal static readonly ObservableCollection<Repository> Repositories = GetRepositories();
        #endregion

        #region Constructor
        internal Repository(Uri url, string branch = "master")
        {
            Url = url;
            Branch = branch;
            _revisionParser = new RevisionParser(url);
            _updateBackgroundWorker = new BackgroundWorker();
            _updateBackgroundWorker.DoWork += DownloadBackgroundWorker_DoWork;
            _updateBackgroundWorker.ProgressChanged += UpdateBackgroundWorker_ProgressChanged;
            //_updateBackgroundWorker.RunWorkerCompleted += UpdateBackgroundWorker_RunWorkerCompleted;
            _updateBackgroundWorker.WorkerReportsProgress = true;

            foreach (var directory in Directory.GetDirectories(Paths.RepositoriesPath)
                .Where(directory => Path.GetFileName(directory).StartsWith(Developer, StringComparison.InvariantCultureIgnoreCase)))
            {
                LocalRevision = directory.Split('-')[2];
            }
        }
        #endregion

        #region Properties
        internal string Name { get { return String.Format("{0}-{1}-{2}", Developer, Fork, LocalRevision); } }

        internal Uri Url { get; private set; }

        internal string Developer { get { return _revisionParser.DeveloperName; } }

        internal string Fork { get { return _revisionParser.ForkName; } }

        internal string Branch { get; private set; }

        internal string LocalRevision { get; private set; }

        internal string LastRevision { get; private set; }

        internal DateTime Date { get { return Directory.GetCreationTime(Path.Combine(Paths.RepositoriesPath, Name)); } }

        internal bool IsDownloaded { get { return Directory.Exists(Path.Combine(Paths.RepositoriesPath, Name)); } }

        internal bool IsUpdating { get { return _updateBackgroundWorker.IsBusy; } }

        internal event RunWorkerCompletedEventHandler RunWorkerCompleted
        {
            add { _updateBackgroundWorker.RunWorkerCompleted += value; }
            remove { _updateBackgroundWorker.RunWorkerCompleted -= value; }
        }
        #endregion

        #region Methods
        internal void Delete()
        {
            if (!String.IsNullOrEmpty(Name))
            {
                Directory.Delete(Path.Combine(Paths.RepositoriesPath, Name), true);
                LocalRevision = null;
            }
        }

        internal void UpdateRevision()
        {
            //TODO
        }

        internal void Update()
        {
            _updateBackgroundWorker.RunWorkerAsync();
            Console.WriteLine("Downloading...");
            Tray.ShowBalloonTip("Downloading...");
            Form1.GlobalAccess.statusStripStatusLabel.Text = "Downloading...";
            Form1.GlobalAccess.statusStripProgressBar.Value = 0;
            Form1.GlobalAccess.statusStripProgressBar.Visible = true;
        }

        private void DownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!GetCommitFile())
            {
                MessageBox.Show("Either connection failed or repository is not available.");
            }

            var repPath = Path.Combine(new[]
                                           {
                                               Environment.CurrentDirectory,
                                               "Repositories",
                                               _revisionParser.GetPath()
                                           });
            if (Directory.Exists(repPath))
            {
                Console.WriteLine("You have latest [{0}] revision: {1}",
                                  _revisionParser.DeveloperName,
                                  _revisionParser.LastRevision);

                Tray.ShowBalloonTip(String.Format("You have latest [{0}] revision: {1}",
                                                            _revisionParser.DeveloperName,
                                                            _revisionParser.LastRevision));
                return;

            }

            DeleteHelper.DeleteOldRepoVersion(_revisionParser.DeveloperName); //We delete old repo version.

            _updateBackgroundWorker.ReportProgress(0);
            Console.WriteLine("Downloading zip file...");
            Tray.ShowBalloonTip("Downloading zip file...");
            Form1.GlobalAccess.Invoke(
                (MethodInvoker)(() => Form1.GlobalAccess.statusStripStatusLabel.Text = "Downloading zip file..."));
            Download();

            _updateBackgroundWorker.ReportProgress(0);
            Console.WriteLine("Uncompressing zip file...");
            Tray.ShowBalloonTip("Uncompressing zip file...");
            Form1.GlobalAccess.Invoke(
                (MethodInvoker)(() => Form1.GlobalAccess.statusStripStatusLabel.Text = "Uncompressing zip file..."));
            Unzip();

            _updateBackgroundWorker.ReportProgress(50);
            Console.WriteLine("Compiling Mooege...");
            Tray.ShowBalloonTip("Compiling Mooege...");
            Form1.GlobalAccess.Invoke(
                (MethodInvoker)(() => Form1.GlobalAccess.statusStripStatusLabel.Text = "Compiling Mooege..."));
            if (!Compile())
            {
                Console.WriteLine("Error while compiling");
                Tray.ShowBalloonTip("Error while compiling");
                Form1.GlobalAccess.Invoke(
                    (MethodInvoker)(() => Form1.GlobalAccess.statusStripStatusLabel.Text = "Error while compiling"));
                MessageBox.Show(Form.ActiveForm,
                                String.Format(
                                    "An error occurred while compiling Mooege{0}Check the msbuild.log for further info.",
                                    Environment.NewLine),
                                "MadCow",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            _updateBackgroundWorker.ReportProgress(100);
        }

        private void UpdateBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Form1.GlobalAccess.statusStripProgressBar.Value = e.ProgressPercentage;
        }

        private void Download()
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }
            var url = new Uri(Url.AbsoluteUri + "/zipball/" + Branch);
            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
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
                using (var streamRemote = client.OpenRead(url))
                {
                    // We write those files into the file system.
                    using (Stream streamLocal = new FileStream(Paths.MooegeDownloadPath,
                                                               FileMode.Create,
                                                               FileAccess.Write,
                                                               FileShare.None))
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
                            _updateBackgroundWorker.ReportProgress(iProgressPercentage);
                        }

                        // Clean up the file stream
                        streamLocal.Close();
                    }

                    // Close the connection to the remote server
                    streamRemote.Close();
                }
            }
        }

        private void Unzip()
        {
            if (ProcessFinder.FindProcess("Mooege"))
            {
                ProcessFinder.KillProcess("Mooege");
            }

            //var stream = new FileStream(zipFileName, FileMode.Open, FileAccess.Read);
            //var zip = new ZipFile(stream) { IsStreamOwner = true };
            //Closes parent stream when ZipFile.Close is called
            //zip.Close();
            new FastZip(new FastZipEvents()).ExtractZip(Paths.MooegeDownloadPath, Paths.RepositoriesPath, null);
        }

        private bool Compile()
        {
            LocalRevision = LastRevision;
            var path = Path.Combine(Paths.RepositoriesPath, Name, "src", "Mooege", "Mooege-VS2010.csproj");
            var project = new Project(path);
            project.SetProperty("Configuration", Configuration.MadCow.CompileAsDebug ? "Debug" : "Release");
            project.SetProperty("Platform", "x86");
            //project.SetProperty("OutputPath", Path.Combine(Paths.RepositoriesPath, Name, "compiled"));
            //project.SetProperty("TargetDir", Path.Combine(Paths.RepositoriesPath, Name, "compiled"));
            project.SetProperty("OutDir", Path.Combine(Paths.RepositoriesPath, Name, "compiled"));
            var p = project.GetProperty("OutputPath");
            return project.Build(new FileLogger());
        }

        private bool GetCommitFile()
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
                try
                {
                    var uri = new Uri(Url + "/commits/master.atom");
                    //client.DownloadStringAsync(uri);
                    _revisionParser.CommitFile = client.DownloadString(uri);
                    LastRevision = _revisionParser.LastRevision;
                }
                catch (UriFormatException)
                {

                    return false;
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        //RevisionParser.CommitFile = "Incorrect repository entry";
                        return false;
                    }
                }
                else if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    //RevisionParser.CommitFile = "ConnectionFailure";
                    return false;
                }
                else
                {
                    //RevisionParser.CommitFile = "Incorrect repository entry";
                    return false;
                }
            }
            return true;
        }

        private static ObservableCollection<Repository> GetRepositories()
        {
            Directory.CreateDirectory(Paths.RepositoriesPath);
            var rep = new ObservableCollection<Repository>();
            if (File.Exists(Paths.RepositoriesListPath))
            {
                foreach (var url in File.ReadAllLines(Paths.RepositoriesListPath).Distinct().Select(s => new Uri(s)))
                {
                    rep.Add(new Repository(url));
                }
            }
            return rep;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
