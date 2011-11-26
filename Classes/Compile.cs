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
        public static String currentMooegeExePath = Program.programPath + @"\mooege-mooege-" + Program.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
        public static String currentMooegeDebudFolderPath = Program.programPath + @"\mooege-mooege-" + Program.lastRevision + @"\src\Mooege\bin\Debug\";
        public static String mooegeINI = Program.programPath + @"\mooege-mooege-" + Program.lastRevision + @"\src\Mooege\bin\Debug\config.ini";
        public static String compileArgs = Program.programPath + @"\mooege-mooege-" + Program.lastRevision + @"\build\Mooege-VS2010.sln";
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
                Console.WriteLine("Compiling newest Mooege source...");
                proc.Start();
                proc.WaitForExit();
                Console.WriteLine("Compiling newest Mooege source Complete");
            }
            catch (Exception objException)
            {
                Console.WriteLine(objException);
            }
        }

        public static void ModifyMooegeINI()
        {
            try
            {
                IConfigSource source = new IniConfigSource(Compile.mooegeINI);
                string fileName = source.Configs["Storage"].Get("MPQRoot");
                if (fileName.Contains("${Root}"))
                {
                    Console.WriteLine("Modifying Mooege MPQ storage folder...");
                    IConfig config = source.Configs["Storage"];
                    config.Set("MPQRoot", Program.programPath + @"\MPQ");
                    source.Save();
                    Console.WriteLine("Modifying Mooege MPQ storage folder Complete");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR");
            }
        }

        static public void WriteVbsPath()
        {
            String vbsPath = (Program.programPath + "\\Tools\\ShortcutCreator.vbs");
            StreamReader reader = new StreamReader(vbsPath);
            string content = reader.ReadToEnd();
            reader.Close();
 
            content = Regex.Replace(content, "MODIFY", Compile.currentMooegeExePath);
            content = Regex.Replace(content, "WESKO", Compile.currentMooegeDebudFolderPath);
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
