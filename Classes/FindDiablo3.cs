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


namespace MadCow
{
    public class FindDiablo3
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
                        //Console.WriteLine("Found Diablo III Install Path");
                        return regKey.GetValue("InstallLocation").ToString();
                    }
                }
                catch {}
            }
            Console.WriteLine("Couldn't Find Diablo 3 Installation."
                +"\nPlease install Diablo III and try running MadCow again.");
            Console.ReadKey();
            Environment.Exit(0);
            return "";
        }
    }
}
