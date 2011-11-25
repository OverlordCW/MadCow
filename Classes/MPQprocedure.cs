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
    class MPQprocedure
    {
        //\base\ Folder MD5's.
        public static String[] MD5ValidPool = {"39765d908accf4f37a4c2dfa99b8cd52"//7170
                                           ,"7148ee45696c84796f3ca16729b9aadc"   //7200
                                           ,"7ee326516f3da2c8f8b80eba6199deef"   //7318
                                           ,"68c43ae976872a1fa7f5a929b7f21b58"   //7338
                                           ,"751b8bf5c84220688048c192ab23f380"   //7447
                                           ,"d5eba8a2324cdc815b2cd5b92104b7fa"   //7728
                                           ,"5eb4983d4530e3b8bab0d6415d8251fa"   //7841
                                           ,"3faf4efa2a96d501c9c47746cba5a7ad"}; //7841

        public static void ValidateMD5()
        {
            String src = FindDiablo3.FindDiabloLocation() + "\\Data_D3\\PC\\MPQs\\base";
            String[] filePaths = Directory.GetFiles(src, "*.*", SearchOption.TopDirectoryOnly);
            int fileCount = Directory.GetFiles(src, "*.*", SearchOption.TopDirectoryOnly).Length;
            int trueCounter = 0;

            foreach (string dir in filePaths)
            {
                string md5Filecheck = Md5Validate.GetMD5HashFromFile(dir);

                for (int i = 0; i < MD5ValidPool.Length; i++)
                {
                    if (md5Filecheck.Contains(MD5ValidPool[i]) == true)
                    {
                        trueCounter += 1;
                    }
                }
            }

            if (fileCount == trueCounter)
            {
                Console.WriteLine("Validating MPQ's MD5 Hash Complete");
            }
            else
            {
                Console.WriteLine("Validating MPQ's MD5 Hash FAILED!"
                    + "\n Please reinstall your Diablo III client or"
                    + "\n try using D3 Launcher to fix them.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static void MpqTransfer()
        {
            Console.WriteLine("Copying MPQ files to MadCow Folders...");
            String Src = FindDiablo3.FindDiabloLocation() + "\\Data_D3\\PC\\MPQs";
            String Dst = Program.programPath +"\\MPQ";
            copyDirectory(Src, Dst);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Process has been completed successfully");
            Console.WriteLine("Check your desktop for Mooege shortcut.");
            Console.ForegroundColor = ConsoleColor.White;
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
                // Sub directories

                //Filter for non needed MPQ's
                if (Directory.Exists(Element) && Element.Contains("enUS") || Element.Contains("Cache") 
                    || Element.Contains("Win") || Element.Contains("enUS_Audio") 
                    || Element.Contains("enUS_Cutscene") || Element.Contains("enUS_Text") 
                    || Element.Contains("Sound") || Element.Contains("Texture")
                    || Element.Contains("HLSLShaders"))
                {
                    Console.WriteLine("Skipped: " + Path.GetFileName(Element));
                }

                //If not Filtered
                else if (Directory.Exists(Element))
                {
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                    Console.WriteLine("Created Directory: " + Path.GetFileName(Element));
                }
                //Copy the files from not filtered folders
                else
                {
                    Console.WriteLine("Copying: " + Path.GetFileName(Element) + "...");
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
                    Console.WriteLine("Copying: " + Path.GetFileName(Element) + " Complete");
                }

            }   Console.WriteLine("Copying MPQ files to MadCow Folders has completed.");
        }
    }
}