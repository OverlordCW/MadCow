using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MadCow
{
    class SimpleFileDelete
    {
        public static void Delete()
        {
           // Delete a directory and all subdirectories with Directory static method...
            if (System.IO.Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
                {
                    try
                    {
                        Console.WriteLine("Deleting Folder");
                        System.IO.Directory.Delete(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision, true);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
         }
    }
}