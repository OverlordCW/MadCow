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
using System.Windows.Forms;
using System.Threading;

namespace MadCow
{
    class Commands
    {
        //
        //    TODO: This is next to work on. Validating MD5, Copying MPQ's- Wesko
        //
        /*public static void RunUpdateMPQ(int RunUpdateMPQ1)
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
        }*/

        public static void RunUpdate() //This is actually the whole process MadCow uses before Downloading source.
        {
            Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            Compile.currentMooegeDebugFolderPath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\";
            Compile.mooegeINI = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini";
            Compile.compileArgs = "\"" + Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\build\Mooege-VS2010.sln" + "\"";

            //Where does this delete Mooege Folders?
            //FindAndKillProcess("mooege");
            Uncompress.UncompressFiles();
            RefreshDesktop.RefreshDesktopPlease(); //Sends a refresh call to desktop, probably this is working for Windows Explorer too, so i'll leave it there for now -wesko
            Thread.Sleep(2000); //This shit is needed for madcow to work on VM XP, you need to wait for Windows Explorer to refresh folders or compiling wont find the new mooege folder just uncompressed.
            Form1.progressBar1.PerformStep(); //This sends an update to progress bar
            Compile.CreateBatchCompileFile();
            Compile.WriteCompileBatch();
            Compile.ExecuteCommandSync(Program.programPath + @"\Tools\CompileBatch");  //Compile command.         
            Form1.progressBar1.PerformStep();
            Compile.ModifyMooegeINI(); //Add MadCow MPQ folder Path to Mooege
            Form1.progressBar1.PerformStep();
            Form1.progressBar1.PerformStep();    
        }
    }
}
