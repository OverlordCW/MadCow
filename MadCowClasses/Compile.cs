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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Logging;

namespace MadCow
{
    internal static class Compile
    {
        //This paths may change depending on which repository ur trying to retrieve, they are set over ParseRevision.cs
        internal static readonly ObservableCollection<Repository> Repositories = GetRepositories();

        private static ObservableCollection<Repository> GetRepositories()
        {
            Directory.CreateDirectory("Repositories");
            var rep = new ObservableCollection<Repository>();
            if(File.Exists(Path.Combine("Tools", "RepoList.txt")))
            {
                foreach (var url in File.ReadAllLines(Path.Combine("Tools", "RepoList.txt")).Distinct())
                {
                    rep.Add(new Repository(url));
                }
            }
            //foreach (var directory in Directory.GetDirectories("Repositories")
            //    .SkipWhile(d => !File.Exists(Path.Combine(d, "info.txt"))))
            //{
            //    rep.Add(new Repository(File.ReadAllLines(Path.Combine(directory, "info.txt"))[0], "")
            //    {
            //        Revision = ParseRevision.LastRevision,
            //    });
            //}
            return rep;
        }

        internal static Repository SelectedRepository { get { return Repositories.FirstOrDefault(r => r.IsSelected); } }

        internal static string CurrentMooegeFolderPath
        {
            get
            {
                return Path.Combine(
                    SelectedRepository.RepositoryPath,
                    //string.Format("{0}-{1}-{2}",
                    //              ParseRevision.DeveloperName,
                    //              ParseRevision.BranchName,
                    //              ParseRevision.LastRevision),
                    "Compiled"
                    );
            }
        }

        internal static string CurrentMooegeExePath { get { return Path.Combine(CurrentMooegeFolderPath, "Mooege.exe"); } }

        internal static string MooegeINI { get { return Path.Combine(CurrentMooegeFolderPath, "config.ini"); } }

        internal static void CompileSource()
        {
            var libmoonetPath = Path.Combine(new[]
                                                 {
                                                     Program.programPath,
                                                     "Repositories",
                                                     ParseRevision.DeveloperName + "-" +
                                                     ParseRevision.BranchName + "-" +
                                                     ParseRevision.LastRevision,
                                                     "src",
                                                     "LibMooNet",
                                                     "LibMooNet.csproj"
                                                 });
            var mooegePath = Path.Combine(new[]
                                              {
                                                  Program.programPath,
                                                  "Repositories",
                                                  ParseRevision.DeveloperName + "-" +
                                                  ParseRevision.BranchName + "-" +
                                                  ParseRevision.LastRevision,
                                                  "src",
                                                  "Mooege",
                                                  "Mooege-VS2010.csproj"
                                              });

            var compileLibMooNetTask = Task<bool>.Factory.StartNew(() => CompileLibMooNet(libmoonetPath));
            var compileMooegeTask = compileLibMooNetTask.ContinueWith(x => CompileMooege(mooegePath, x.Result));

            Task.WaitAll(compileMooegeTask);

            if (compileMooegeTask.Result == false)
                Console.WriteLine("[Fatal] Failed to compile.");
            else
            {
                Console.WriteLine("Compiling Complete.");
                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Compiling Complete!", ToolTipIcon.Info);
                }
            }
        }

        private static bool CompileLibMooNet(string libmoonetPath)
        {
            //Console.WriteLine("Compiling LibMoonet...");
            //if (Configuration.MadCow.TrayNotificationsEnabled)
            //{
            //    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Compiling LibMoonet...", ToolTipIcon.Info);
            //}
            //var libmoonetProject = new Project(libmoonetPath);
            //libmoonetProject.SetProperty("Configuration", Configuration.MadCow.CompileAsDebug ? "Debug" : "Release");
            //libmoonetProject.SetProperty("Platform", "AnyCPU");

            //return libmoonetProject.Build(new FileLogger());
            return true;
        }

        private static bool CompileMooege(string mooegePath, bool libMooNetStatus)
        {
            if (libMooNetStatus)
            {
                Console.WriteLine("Compiling Mooege...");
                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Compiling Mooege......", ToolTipIcon.Info);
                }
                var mooegeProject = new Project(mooegePath);
                mooegeProject.SetProperty("Configuration", Configuration.MadCow.CompileAsDebug ? "Debug" : "Release");
                mooegeProject.SetProperty("Platform", "AnyCPU");
                mooegeProject.SetProperty("OutputPath", CurrentMooegeFolderPath);
                return mooegeProject.Build(new FileLogger());
            }
            return false;
        }
    }
}
