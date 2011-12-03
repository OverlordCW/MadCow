using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MadCow
{
    class SimpleFileDelete
    {
        public static void Delete(int folder)
        {
            //Deletes Mooege Folder
            if (folder == 1)
            {
           // Delete a directory and all subdirectories with Directory static method...
            if (System.IO.Directory.Exists(Program.programPath + @"/" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
                {
                    try
                    {
                        Console.WriteLine("Deleting " + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision);
                        System.IO.Directory.Delete(Program.programPath + @"/" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision, true);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            //MPQs - d3-update-base-7841.MPQ
            if (folder == 0)
            {
                if (System.IO.Directory.Exists(Program.programPath + @"/MPQ"))
                {
                        Console.WriteLine("Deleting d3-update-base-7841.MPQ");
                        System.IO.File.Delete(Program.programPath + @"/MPQ/base/d3-update-base-7841.MPQ");
                }
            }
         }

    }
}