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
        //      Validate MD5s, if incorrect MessageBox (Yes or No), if you would like to autograb correct MPQs -> checks in Commands, Does the work in MPQprocedure
        //          Find MD5, if wrong, Delete Base file, Base-Win file, and Cache Folder
        //                              Delete.cs can be configured to do this since it is not in use for Reseting Last Commit(It can do both if we set it to)
        //          Start up Diablo Beta Launcher, timer check on mpqs? (do we have to wait for them to finish or just load new mpqs?)
        //          Repeat until canceled or correct MD5.
        //      Once correct MD5, proceed to MPQTransfer.
        //
        public static void RunUpdateMPQ()
        {
                    if (Directory.Exists(Program.programPath + @"/MPQ"))
                    {
                        //MD5 Check
                        //inside validateMD5, it will if/else to delete mpqs from diablo and validate new ones
                        MPQprocedure.ValidateMD5();

                        //Delete Folder if already exists
                        System.IO.Directory.Delete(Program.programPath + @"/MPQ", true);
                        Console.WriteLine("Deleted current MPQ MadCow folder succeedeed");

                        //Create new Folder
                        System.IO.Directory.CreateDirectory(Program.programPath + @"/MPQ");
                        Console.WriteLine("Creating new MPQ MadCow folder succeedeed");

                        //Transfer MPQs
                        MPQprocedure.MpqTransfer();
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Program.programPath + @"/MPQ");
                        MPQprocedure.MpqTransfer();
                    }
        }

        public static void RunUpdate() //This is actually the whole process MadCow uses before Downloading source.
        {
            Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            Compile.currentMooegeDebugFolderPath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\";
            Compile.mooegeINI = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\config.ini";
            Compile.compileArgs = "\"" + Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\build\Mooege-VS2010.sln" + "\"";

            Uncompress.UncompressFiles();
            RefreshDesktop.RefreshDesktopPlease(); //Sends a refresh call to desktop, probably this is working for Windows Explorer too, so i'll leave it there for now -wesko
            Thread.Sleep(2000); //This shit is needed for madcow to work on VM XP, you need to wait for Windows Explorer to refresh folders or compiling wont find the new mooege folder just uncompressed.
            //Form1.progressBar1.PerformStep(); //This sends an update to progress bar
            Compile.CreateBatchCompileFile();
            Compile.WriteCompileBatch();
            Compile.ExecuteCommandSync(Program.programPath + @"\Tools\CompileBatch");  //Compile command.         
            //Form1.progressBar1.PerformStep();
            Compile.ModifyMooegeINI(); //Add MadCow MPQ folder Path to Mooege
            //Form1.progressBar1.PerformStep();
            //Form1.progressBar1.PerformStep();    
        }
    }
}
