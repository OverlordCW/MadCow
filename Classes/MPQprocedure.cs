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
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Nini.Config;

namespace MadCow
{
    class MPQprocedure
    {
        public static void StartCopyProcedure()
        {
            Thread copyThread = new Thread(MpqTransfer);
            copyThread.Start();
        }

        public static void MpqTransfer()
        {
            //Takes Diablo Path from Ini, which gets it from finding diablo3.exe 
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string Src = source.Configs["DiabloPath"].Get("MPQpath");
            String Dst = source.Configs["DiabloPath"].Get("MPQDest");

            if (ProcessFinder.FindProcess("Diablo") == true)
            {
                ProcessFinder.KillProcess("Diablo");
                Console.WriteLine("Killed Diablo3 Process");
            }
            Console.WriteLine("Creating MadCow MPQ folder...");
            Directory.CreateDirectory(Dst);
            Console.WriteLine("Creating MadCow MPQ folder Complete");
            //Proceeds to copy data
            Console.WriteLine("Copying MPQ files to MadCow Folders...");
            copyDirectory(Src, Dst);
            //When all the files has been copied then:
            Console.WriteLine("Copying MPQ files to MadCow Folders has completed.");
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.CopyMPQButton.Enabled = true;
                Form1.GlobalAccess.PlayDiabloButton.Enabled = true; //We enable Play D3 button again.
            }));
        }

        private static void copyDirectory(String Src, String Dst)
        {
            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (String Element in Files)
            {
                //Filter for non needed MPQ's
                if (Directory.Exists(Element) && Element.Contains("enUS") || Element.Contains("Cache")
                    || Element.Contains("Win") || Element.Contains("enUS_Audio")
                    || Element.Contains("enUS_Cutscene") || Element.Contains("enUS_Text")
                    || Element.Contains("Sound") || Element.Contains("Texture")
                    || Element.Contains("HLSLShaders") || Element.Contains("lock"))
                {
                    Console.WriteLine("Skipped: " + Path.GetFileName(Element));
                }

                //If not Filtered
                else if (Directory.Exists(Element))
                {
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                }
                //Copy the files from not filtered folders
                else
                {
                    Console.WriteLine("Copying: " + Path.GetFileName(Element) + "...");
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
                    Console.WriteLine("Copying: " + Path.GetFileName(Element) + " Complete");
                }

            }
        }
    }
}