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
using System.Linq;
using System.Text;
using System.IO;

namespace MadCow
{
    class Commands
    {
        public static void RunUpdateMPQ(int RunUpdateMPQ1)
        {
            if (RunUpdateMPQ1 == 1)
            {
                    if (Directory.Exists(Program.programPath + "/MPQ"))
                    {
                        //does not delete directory
                        Directory.Delete(Program.programPath + "/MPQ", true);
                        Console.WriteLine("Deleted current MPQ MadCow folder succeedeed");
                        Directory.CreateDirectory(Program.programPath + "/MPQ");
                        Console.WriteLine("Creating new MPQ MadCow folder succeedeed");
                        MPQprocedure.MpqTransfer();
                    }
                    else
                    {
                        Directory.CreateDirectory(Program.programPath + "/MPQ");
                        MPQprocedure.MpqTransfer();
                    }
            }
        }
        public static void RunUpdate()
        {
            Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            Compile.currentMooegeDebugFolderPath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\";
            Compile.mooegeINI = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini";
            Compile.compileArgs = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\build\Mooege-VS2010.sln";

            Uncompress.UncompressFiles();
            Compile.ExecuteCommandSync(Compile.msbuildPath + " " + Compile.compileArgs);
            Compile.ModifyMooegeINI();
            Compile.WriteVbsPath();
         

            if (File.Exists(Program.desktopPath + "\\Mooege.lnk"))
            {
                File.Delete(Program.desktopPath + "\\Mooege.lnk");
                System.Diagnostics.Process.Start(Program.programPath + "\\Tools\\ShortcutCreator.vbs");
            }
            else
            {
            System.Diagnostics.Process.Start(Program.programPath + "\\Tools\\ShortcutCreator.vbs");
            }     
        }

        public static void AutoUpdate(int AutoUpdate1)
        {
            if (AutoUpdate1 == 1)
            {
                    //  TODO: Implement a timer which will check for Mooege updates.
                    //  !autoupdate <minutes>
                    Console.WriteLine("Not implemented yet");
                }
        }

        public static void Help(int Help1)
        {
                if (Help1 == 1)
                {
                    Console.WriteLine("You're So Silly");
                }
        }

        }
}
