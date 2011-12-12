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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Nini.Config;

namespace MadCow
{
    class Diablo
    {
        public static void Play()
        {
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            String Src = source.Configs["DiabloPath"].Get("D3Path");
            
            if (ProcessFinder.FindProcess("Mooege") == false)
            {
                Console.WriteLine("Starting Mooege..");
                Process Mooege = new Process();
                Mooege.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                Mooege.Start();
                Thread.Sleep(2000);
                if (ErrorFinder.SearchLogs("Fatal") == true)
                {
                    Console.WriteLine("Closing Mooege due Fatal Exception");
                    ProcessFinder.KillProcess("Mooege");
                }
                else
                {
                    Console.WriteLine("Starting Diablo..");
                    Process Diablo3 = new Process();
                    Diablo3.StartInfo = new ProcessStartInfo(Src);
                    Diablo3.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                    Diablo3.Start();
                }
            }
            else //If Mooege is running we kill it and start it again.
            {
                Console.WriteLine("Killing Mooege Process..");
                ProcessFinder.KillProcess("Mooege");
                Console.WriteLine("Starting Mooege..");
                Process Mooege = new Process();
                Mooege.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                Mooege.Start();
                Thread.Sleep(2000);
                if (ErrorFinder.SearchLogs("Fatal") == true)
                {
                    Console.WriteLine("Closing Mooege due Fatal Exception");
                    ProcessFinder.KillProcess("Mooege");
                }
                else
                {
                    Console.WriteLine("Starting Diablo..");
                    Process Diablo3 = new Process();
                    Diablo3.StartInfo = new ProcessStartInfo(Src);
                    Diablo3.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                    Diablo3.Start();
                }
            }
        }
    }
}
