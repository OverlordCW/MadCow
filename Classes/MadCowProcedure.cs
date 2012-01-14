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
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Nini.Config;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace MadCow
{
    class MadCowProcedure
    {
        public static void RunWholeProcedure() //This is actually the whole process MadCow uses after Downloading source.
        {
            Compile.currentMooegeExePath = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            Compile.currentMooegeDebugFolderPath = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\";
            Compile.mooegeINI = Program.programPath + @"\" + @"Repositories\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini";
            ZipFile zip = null;
            var events = new FastZipEvents();

            if (ProcessFinder.FindProcess("Mooege") == true)
            {
                ProcessFinder.KillProcess("Mooege");
            }

            FastZip z = new FastZip(events);
            Console.WriteLine("Uncompressing zip file...");
            var stream = new FileStream(Program.programPath + @"\Repositories\" + @"\Mooege.zip", FileMode.Open, FileAccess.Read);
            zip = new ZipFile(stream);
            zip.IsStreamOwner = true; //Closes parent stream when ZipFile.Close is called
            zip.Close();

            var t1 = Task.Factory.StartNew(() => z.ExtractZip(Program.programPath + @"\Repositories\" + @"\Mooege.zip", Program.programPath + @"\" + @"Repositories\", null))
                .ContinueWith(delegate
            {
                //Comenting the lines below because I haven't tested this new way over XP VM or even normal XP.
                //RefreshDesktop.RefreshDesktopPlease(); //Sends a refresh call to desktop, probably this is working for Windows Explorer too, so i'll leave it there for now -wesko
                //Thread.Sleep(2000); //<-This and ^this is needed for madcow to work on VM XP, you need to wait for Windows Explorer to refresh folders or compiling wont find the new mooege folder just uncompressed.
                Console.WriteLine("Uncompress Complete.");
                if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini")){
                    IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                    String Src = source.Configs["Balloons"].Get("ShowBalloons");
                    if (Src.Contains("1")){Form1.GlobalAccess.notifyIcon1.ShowBalloonTip(1000, "MadCow", "Uncompress Complete!", ToolTipIcon.Info);}}
                Form1.GlobalAccess.Invoke((MethodInvoker)delegate { Form1.GlobalAccess.generalProgressBar.PerformStep(); });
                Compile.compileSource(); //Compile solution projects.
                Form1.GlobalAccess.Invoke((MethodInvoker)delegate { Form1.GlobalAccess.generalProgressBar.PerformStep(); });
                Compile.ModifyMooegeINI(); //Add MadCow MPQ folder Path to Mooege.
                Form1.GlobalAccess.Invoke((MethodInvoker)delegate { Form1.GlobalAccess.generalProgressBar.PerformStep(); });
                Console.WriteLine("[Process Complete!]");
                if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini"))
                {
                    IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                    String Src = source.Configs["Balloons"].Get("ShowBalloons");

                    if (Src.Contains("1"))
                    {
                        Form1.GlobalAccess.notifyIcon1.ShowBalloonTip(1000, "MadCow", "Process Complete!", ToolTipIcon.Info);
                    }
                }
            });
        }
    }
}
