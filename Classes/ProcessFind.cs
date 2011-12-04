using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MadCow
{
    class ProcessFind
    {
        //Is Process Running Function
        public static bool FindProcess(string AppName)
        {
            bool bRtn = false;
            foreach (Process clsProcess in Process.GetProcesses())
            {
                try
                {
                    if (clsProcess.ProcessName.StartsWith(AppName))
                    {
                        bRtn = true;
                    }
                }
                catch { }
            }
            return bRtn;
        }

        public static void KillProcess(string AppKillName)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                    if (clsProcess.ProcessName.Contains(AppKillName))
                    {
                        // Kill Kill Kill
                        clsProcess.Kill();
                    }
            }
        }

    }
}
