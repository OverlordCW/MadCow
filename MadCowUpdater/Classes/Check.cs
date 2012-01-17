using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace MadCowUpdater
{
    class Check
    {
        public static Int32 UserVersion;
        public static void GetCurrentUserVersion()
        {
            var MadCowPath = Directory.GetParent(Program.path);
            FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(MadCowPath + @"\MadCow2011.exe");
            String StringVersion = myFI.ProductVersion;
            UserVersion = int.Parse(StringVersion.Replace(".", ""));
        }
    }
}
