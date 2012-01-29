using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MadCowUpdater
{
    class Helper
    {
        public static void ModifyFolderName()
        {
            String Destination = Path.GetTempPath() + @"\MadCow";
            if (Directory.Exists(Destination))
            {
                string[] files2 = Directory.GetDirectories(Destination);
                foreach (string directory in files2)
                {
                    Directory.Move(directory, Path.GetTempPath() + @"\MadCow\NewMadCow");
                }
            }
        }

        public static void DeleteMadCowUpdaterFiles()
        {
            if (Directory.Exists(Path.GetTempPath() + @"\MadCow\NewMadCow\bin\MadCowDebug\MadCowUpdater"))
            {
                Directory.Delete(Path.GetTempPath() + @"\MadCow\NewMadCow\bin\MadCowDebug\MadCowUpdater", true);
            }
        }

        public static void DeleteZipFile()
        {
            if (File.Exists(Path.GetTempPath() + @"\MadCow.zip"))
            {
                File.Delete(Path.GetTempPath() + @"\MadCow.zip");
            }
        }

        public static void DeleteTempFolder()
        {
            if (Directory.Exists(Path.GetTempPath() + @"\MadCow\"))
            {
                Directory.Delete(Path.GetTempPath() + @"\MadCow\", true);
            }
        }

        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}
