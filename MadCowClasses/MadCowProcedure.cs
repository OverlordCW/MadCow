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
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace MadCow
{
    class MadCowProcedure
    {
        //This is actually the whole process MadCow uses after Downloading source.
        #region RunProcedure
        public static void RunWholeProcedure()
        {
            Compile.CurrentMooegeExePath = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            Compile.CurrentMooegeDebugFolderPath = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\";
            Compile.MooegeINI = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini";

            var events = new FastZipEvents();

            if (ProcessFinder.FindProcess("Mooege"))
            {
                ProcessFinder.KillProcess("Mooege");
            }

            var z = new FastZip(events);
            Console.WriteLine("Uncompressing zip file...");
            var stream = new FileStream(Path.Combine(Program.programPath, "Repositories", "Mooege.zip"), FileMode.Open, FileAccess.Read);
            var zip = new ZipFile(stream);
            zip.IsStreamOwner = true; //Closes parent stream when ZipFile.Close is called
            zip.Close();

            var t1 = Task.Factory.StartNew(() => z.ExtractZip(Path.Combine(Program.programPath, "Repositories", "Mooege.zip"), Program.programPath + @"\" + @"Repositories\", null))
                .ContinueWith(delegate
            {
                //Comenting the lines below because I haven't tested this new way over XP VM or even normal XP.
                //RefreshDesktop.RefreshDesktopPlease(); //Sends a refresh call to desktop, probably this is working for Windows Explorer too, so i'll leave it there for now -wesko
                //Thread.Sleep(2000); //<-This and ^this is needed for madcow to work on VM XP, you need to wait for Windows Explorer to refresh folders or compiling wont find the new mooege folder just uncompressed.
                Console.WriteLine("Uncompress Complete.");

                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Uncompress Complete!", ToolTipIcon.Info);
                }
                Form1.GlobalAccess.Invoke((MethodInvoker)(() => Form1.GlobalAccess.generalProgressBar.PerformStep()));
                Compile.CompileSource(); //Compile solution projects.
                Form1.GlobalAccess.Invoke((MethodInvoker)(() => Form1.GlobalAccess.generalProgressBar.PerformStep()));
                Form1.GlobalAccess.Invoke((MethodInvoker)(() => Form1.GlobalAccess.generalProgressBar.PerformStep()));
                Console.WriteLine("[Process Complete!]");

                if (Configuration.MadCow.TrayNotificationsEnabled)
                {
                    Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(1000, "MadCow", "Process Complete!", ToolTipIcon.Info);
                }
            });
        }
        #endregion
    }
}
