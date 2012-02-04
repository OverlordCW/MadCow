// Copyright (C) 2011 MadCow Project
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
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace MadCow
{
    class Diablo
    {
        public static void Play(Repository repository)
        {
            try
            {
                var src = Configuration.MadCow.DiabloPath;
                Configuration.UpdateMooegeIni(repository);
                if (ProcessFinder.FindProcess("Mooege") == false)
                {
                    if (File.Exists(Paths.GetMooegeExePath(repository)))
                    {

                        Console.WriteLine("Starting Mooege..");
                        var mooege = new Process();
                        mooege.StartInfo = new ProcessStartInfo(Paths.GetMooegeExePath(repository));
                        mooege.Start();
                        Thread.Sleep(3000); //We sleep so our ErrorFinder has time to parse Mooege logs.
                        if (ErrorFinder.SearchLogs("Fatal"))
                        {
                            Console.WriteLine("Closing Mooege due Fatal Exception");
                            ProcessFinder.KillProcess("Mooege");
                        }
                        else
                        {
                            Console.WriteLine("Starting Diablo..");
                            var Diablo3 = new Process();
                            Diablo3.StartInfo = new ProcessStartInfo(src);
                            Diablo3.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                            Diablo3.Start();
                            //We save this repository for LastPlayed function.
                            Configuration.MadCow.LastRepository = Paths.GetMooegeExePath(repository);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[Error] Couldn't find selected repository binaries."
                        + "\nTry updating the repository again.");
                    }
                }
                else //If Mooege is running we kill it and start it again.
                {
                    Console.WriteLine("Killing Mooege Process..");
                    ProcessFinder.KillProcess("Mooege");
                    Console.WriteLine("Starting Mooege..");
                    var mooege = new Process();
                    mooege.StartInfo = new ProcessStartInfo(Paths.GetMooegeExePath(repository));
                    mooege.Start();
                    Thread.Sleep(3000);
                    if (ErrorFinder.SearchLogs("Fatal"))
                    {
                        Console.WriteLine("Closing Mooege due Fatal Exception");
                        ProcessFinder.KillProcess("Mooege");
                    }
                    else
                    {
                        Console.WriteLine("Starting Diablo..");
                        var Diablo3 = new Process();
                        Diablo3.StartInfo = new ProcessStartInfo(src);
                        Diablo3.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                        Diablo3.Start();
                    }
                }
            }
            catch
            {
                Console.WriteLine("[ERROR] Could not launch Diablo. (Diablo.cs)" +
                                  "\nPlease report this error in the forum.");
            }
        }


    }
}
