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
                Process proc0 = new Process();
                proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
                proc0.Start();
                Thread.Sleep(1000);
                Process proc1 = new Process();
                proc1.StartInfo = new ProcessStartInfo(Src);
                proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
                proc1.Start();
                Console.WriteLine("Starting Diablo..");
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
