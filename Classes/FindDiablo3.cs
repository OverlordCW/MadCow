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
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Net;


namespace MadCow
{
    public class Diablo3
    {
        public static String FindDiabloLocation()
        {
            RegistryKey d3Path = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\");
            String[] nameList = d3Path.GetSubKeyNames();
            for (int i = 0; i < nameList.Length; i++)
            {
                RegistryKey regKey = d3Path.OpenSubKey(nameList[i]);
                try
                {
                    if (regKey.GetValue("DisplayName").ToString() == "Diablo III Beta")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Found Diablo III Install Path");
                        Console.ForegroundColor = ConsoleColor.White;
                        VerifyVersion(regKey.GetValue("InstallLocation").ToString());
                        return regKey.GetValue("InstallLocation").ToString();
                    }
                }
                catch 
                {
                    //the ball?
                }
            }
            Console.WriteLine("Couldn't Find Diablo 3 Installation."
                +"\nPlease install Diablo III and try running MadCow again.");
            Console.ReadKey();
            Environment.Exit(0);
            return "";
        }

        public static void VerifyVersion(string currentPath)
        {
            FileVersionInfo.GetVersionInfo(Path.Combine(currentPath,"Diablo III.exe"));
            FileVersionInfo d3Version = FileVersionInfo.GetVersionInfo(currentPath + "\\Diablo III.exe");

            try
            {
                WebClient client = new WebClient();
                String commitFile = client.DownloadString("https://raw.github.com/mooege/mooege/master/src/Mooege/Common/Versions/VersionInfo.cs");
                Int32 ParsePointer = commitFile.IndexOf("RequiredPatchVersion = ");
                String revision = commitFile.Substring(ParsePointer+23, 4); //Gets required version by Mooege
                int CurrentD3Version = Convert.ToInt32(revision);
                int MooegeD3needs = d3Version.FilePrivatePart;

                if (MooegeD3needs == CurrentD3Version)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Found the correct Mooege supported version of Diablo III [" + CurrentD3Version + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mooege needs Diablo III version [" + revision + "]"
                        +"\nUpgrade or Downgrade to Diablo III version [" + MooegeD3needs + "]."
                        +"\nTry running MadCow again after getting the correct version");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.ToString());
                if (webEx.Status == WebExceptionStatus.ConnectFailure)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Make sure your internet connection is working");
                }
            }
        }
    }
}
