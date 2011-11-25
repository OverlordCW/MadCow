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
        public static void CommandReader()
        {
            String command = "";
            do
            {
                Console.Write("Type command: ");
                command = Console.ReadLine();

                if (command == "!updatempq")
                {
                    if (Directory.Exists(Program.programPath + "/MPQ"))
                    {
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

                if (command == "!update")
                {
                    if (Directory.Exists(Program.programPath + "/mooege-mooege-" + Program.lastRevision))
                    {
                        Console.WriteLine("You have latest Mooege revision: " + Program.lastRevision);
                    }
                    else
                    {
                        DownloadRevision.DownloadLatest();
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
                            System.Diagnostics.Process.Start(Program.programPath + "\\Tools\\ShortcutCreator.vbs");
                    }
                }

                if (command == "!autoupdate")
                {
                    //  TODO: Implement a timer which will check for Mooege updates.
                    //  !autoupdate <minutes>
                    Console.WriteLine("Not implemented yet");
                }

                if (command == "!help")
                {
                    Console.WriteLine("Availavable commands:"
                        +"\n!update - Download latest Mooege revision"
                        +"\n!updatempq - Will refresh MadCow MPQ folder"
                        +"\n!autoupdate <minutes> - Timer cycle"
                        );
                }

                if (command != "!updatempq" && command != "!update" && command != "!help" && command != "!autoupdate")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (command != "!exit");
        }
    }
}
