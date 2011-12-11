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
            
            if (ProcessFind.FindProcess("Mooege") == false)
            {
                SimpleFileDelete.Delete(0);
                Process proc0 = new Process();
                proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                proc0.Start();
                Thread.Sleep(1000);
                Process proc1 = new Process();
                proc1.StartInfo = new ProcessStartInfo(Src);
                proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                proc1.Start();
                Console.WriteLine("Starting Diablo..");
                //After starting up processes, give the log a bit of time before trying to search for error.
                Thread.Sleep(3000);
                //Once Ready, change to SearchLogs();
                ErrorFinder.SearchLogs("Applying file: d3-update-base-7841.MPQ");
            }
            else
            {
                Process proc1 = new Process();
                proc1.StartInfo = new ProcessStartInfo(Src);
                proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                proc1.Start();
                Console.WriteLine("Starting Diablo..");
            }
        }
    }
}
