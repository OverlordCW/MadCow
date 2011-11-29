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
using Nini.Config;
using System.Text.RegularExpressions;

namespace MadCow
{
    class Compile
    {
        //This paths may change depending on which repository ur trying to retrieve, they are set over ParseRevision.cs
        public static String currentMooegeExePath = "";
        public static String currentMooegeDebugFolderPath = "";
        public static String mooegeINI = "";
        public static String compileArgs = "";
        //This paths dont change.
        public static String madcowINI = Program.programPath + @"\Tools\\Settings.ini";
        public static String msbuildPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.System) + @"\..\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";

        public static void ExecuteCommandSync(String command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInformation =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c" + command);
            
                procStartInformation.RedirectStandardOutput = false;
                procStartInformation.UseShellExecute = true;
                procStartInformation.CreateNoWindow = true;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInformation;
                Console.WriteLine("Compiling newest [" + ParseRevision.developerName + "] Mooege source...");
                proc.Start();
                proc.WaitForExit();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compiling newest [" + ParseRevision.developerName + "] Mooege source Complete");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.Red;
                //The problem is, while passing args to ExecuteCommandSync(String command)
                //If the argument its too long due to the current mooege folder path (Program.programPath)
                //msbuild.exe won't be able to recieve the complete arguments and compiling will fail.
                Console.WriteLine("\nLONGPATHERROR: Couldn't compile Mooege Source,"
                                  + "\nplease use a shorter folder path by moving"
                                  + "\nMadCow files into (e.g C:/MadCow/)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static void ModifyMooegeINI()
        {
            try
            {
                //First we modify the Mooege INI storage path.
                IConfigSource source = new IniConfigSource(Compile.mooegeINI);
                string fileName = source.Configs["Storage"].Get("MPQRoot");
                if (fileName.Contains("${Root}"))
                {
                    Console.WriteLine("Modifying Mooege MPQ storage folder...");
                    IConfig config = source.Configs["Storage"];
                    config.Set("MPQRoot", Program.programPath + "\\MPQ");
                    source.Save();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Modifying Mooege MPQ storage folder Complete");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //Then we create the MPQ folder in MadCow Root Folder.
                if (Directory.Exists(Program.programPath + "\\MPQ"))
                {
                    Directory.Delete(Program.programPath + "\\MPQ", true);
                    Console.WriteLine("Deleted current MPQ MadCow folder succeedeed");
                    Directory.CreateDirectory(Program.programPath + "\\MPQ");
                    Console.WriteLine("Creating new MPQ MadCow folder succeedeed");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not modify Mooege INI FILE");
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static public void WriteVbsPath()
        {
            String vbsPath = (Program.programPath + "\\Tools\\ShortcutCreator.vbs");
            StreamReader reader = new StreamReader(vbsPath);
            string content = reader.ReadToEnd();
            reader.Close();
 
            content = Regex.Replace(content, "MODIFY", Compile.currentMooegeExePath);
            content = Regex.Replace(content, "WESKO", Compile.currentMooegeDebugFolderPath);
            StreamWriter writer = new StreamWriter(vbsPath);
            writer.Write(content);
            writer.Close();

            //Creates shortcut
            if (File.Exists(Program.desktopPath + "\\Mooege.lnk"))
            {
                File.Delete(Program.desktopPath + "\\Mooege.lnk");
                System.Diagnostics.Process.Start(Program.programPath + "\\Tools\\ShortcutCreator.vbs");
            }
            else
                System.Diagnostics.Process.Start(Program.programPath + "\\Tools\\ShortcutCreator.vbs");
        }
    }
}
