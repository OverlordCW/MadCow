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
using System.IO;
using System.Threading;

namespace MadCow
{
    class MpqProcedure
    {
        public static void StartCopyProcedure()
        {
            var copyThread = new Thread(MpqTransfer);
            copyThread.Start();
        }

        public static void MpqTransfer()
        {
            //Takes Diablo Path from Ini, which gets it from finding diablo3.exe 
            if (File.Exists(Paths.MadcowIni))
            {
                var src = Configuration.MadCow.MpqDiablo;
                var dst = Configuration.MadCow.MpqServer;

                if (ProcessFinder.FindProcess("Diablo"))
                {
                    ProcessFinder.KillProcess("Diablo");
                    Console.WriteLine("Killed Diablo3 Process");
                }
                if (!Directory.Exists(dst))
                {
                    Console.WriteLine("Creating MPQ folder...");
                    Directory.CreateDirectory(dst);
                    Console.WriteLine("Created MPQ folder over:" + dst);
                }
                //Proceeds to copy data
                Console.WriteLine("Copying MPQ files to MadCow Folders...");
                CopyDirectory(src, dst);
                //When all the files has been copied then:
                Console.WriteLine("Copying MPQ files to MadCow Folders has completed.");
                Form1.GlobalAccess.Invoke(new Action(() =>
                {
                    Form1.GlobalAccess.CopyMPQButton.Enabled = true;
                    Form1.GlobalAccess.PlayDiabloButton.Enabled = true; //We enable Play D3 button again.
                }));
            }
            else
                Console.WriteLine("MadCow could not find your Diablo III Mpq Folder");
        }

        private static void CopyDirectory(String src, String dst)
        {
            try
            {
                if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                    dst += Path.DirectorySeparatorChar;
                if (!Directory.Exists(dst)) Directory.CreateDirectory(dst);
                var files = Directory.GetFileSystemEntries(src);
                foreach (var element in files)
                {
                    //Filter for non needed MPQ's
                    if (Directory.Exists(element) && element.Contains("enUS") || element.Contains("Cache")
                        || element.Contains("Win") || element.Contains("enUS_Audio")
                        || element.Contains("enUS_Cutscene") || element.Contains("enUS_Text")
                        || element.Contains("Sound") || element.Contains("Texture")
                        || element.Contains("HLSLShaders") || element.Contains("lock"))
                    {
                        Console.WriteLine("Skipped: " + Path.GetFileName(element));
                    }

                    //If not Filtered
                    else if (Directory.Exists(element))
                    {
                        CopyDirectory(element, dst + Path.GetFileName(element));
                    }
                    //Copy the files from not filtered folders
                    else
                    {
                        File.Copy(element, dst + Path.GetFileName(element), true);
                        Console.WriteLine("Copied: " + Path.GetFileName(element));
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("[ERROR] Unable to copy MPQ files. (MPQprocedures.cs)");
            }
        }
    }
}