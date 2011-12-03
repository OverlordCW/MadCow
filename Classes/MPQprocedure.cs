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
using System.Threading.Tasks;
using System.Windows.Forms;
using Nini.Config;

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
                                           ,"3faf4efa2a96d501c9c47746cba5a7ad"   //7841
                                           ,"777da16a46d4f1d231bae8c1e11cdeaf"}; //7931

        public static void ValidateMD5()
        {
            DateTime startTime = DateTime.Now;
            String baseFolderPath = Diablo3._d3loc + "\\Data_D3\\PC\\MPQs\\base";
            String[] filePaths = Directory.GetFiles(baseFolderPath, "*.*", SearchOption.TopDirectoryOnly);
            int fileCount = Directory.GetFiles(baseFolderPath, "*.*", SearchOption.TopDirectoryOnly).Length;
            int trueCounter = 0;

            Parallel.ForEach(filePaths, dir =>  
            {
                string md5Filecheck = Md5Validate.GetMD5HashFromFile(dir);
                
                for (int i = 0; i < MD5ValidPool.Length; i++)
                {
                    if (md5Filecheck.Contains(MD5ValidPool[i]) == true)
                    {
                        trueCounter += 1;
                    }
                }
            });

            if (fileCount == trueCounter)
            {
                DateTime stopTime = DateTime.Now;
                TimeSpan duration = stopTime - startTime;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Validating MPQ's MD5 Hash Complete in: {0}ms",duration.Milliseconds);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                MessageBox.Show("One of your MPQs may be an incorrect hash\nIf you receive a CoreToc.dat error\nGo to Help Tab on MadCow.");
            }
        }

        public static void MpqTransfer()
        {
            //Takes Diablo Path from Ini, which gets it from finding diablo3.exe 
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string fileName = source.Configs["DiabloPath"].Get("D3Path");
            String Src = fileName;
            String Dst = Program.programPath + @"/MPQ";

            if (Directory.Exists(Program.programPath + @"/MPQ/base") && File.Exists(Program.programPath + @"/MPQ/CoreData.mpq") && File.Exists(Program.programPath + @"/MPQ/ClientData.mpq")) //Checks for MPQ Folder
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Found default MadCow MPQ folder");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else //If not found, creates it and proceed with copying.
            {
                Console.WriteLine("Creating MadCow MPQ folder...");
                Directory.CreateDirectory(Program.programPath + @"/MPQ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Creating MadCow MPQ folder Complete");
                Console.ForegroundColor = ConsoleColor.White;
                //Proceeds to copy data
                Console.WriteLine("Copying MPQ files to MadCow Folders...");
                copyDirectory(Src, Dst);
                //When all the files has been copied then:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Copying MPQ files to MadCow Folders has completed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Skipped: " + Path.GetFileName(Element));
                    Console.ForegroundColor = ConsoleColor.White;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Copying: " + Path.GetFileName(Element) + " Complete");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
        }

    }
}