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
using System.Threading.Tasks;
using System.Windows.Forms;
using Nini.Config;

namespace MadCow
{
    class MPQprocedure
    {

        //\base\ Folder known working MD5's.
        private static string validFound = "";
        private static String[] MD5ValidPool = {"39765d908accf4f37a4c2dfa99b8cd52"   //7170
                                                    ,"d3-update-base-7170.MPQ"
                                               ,"a88d550d389492605a6ff6cb44ab1d63"   //7170 3
                                                    ,"d3-update-base-7170.MPQ"
                                               ,"aa22c97a921216ad84b7712627ccf689"   //7200 6
                                                    ,"d3-update-base-7200.MPQ"
                                               ,"7148ee45696c84796f3ca16729b9aadc"   //7200
                                                    ,"d3-update-base-7200.MPQ"
                                               ,"ae804b7a6eb0454df1d73b17d1a3d8b4"   //7318 2
                                                    ,"d3-update-base-7318.MPQ"
                                               ,"7ee326516f3da2c8f8b80eba6199deef"   //7318 7
                                                    ,"d3-update-base-7318.MPQ"
                                               ,"4d1e10d80450f44038b6a2b75e57f46b"   //7338
                                                    ,"d3-update-base-7338.MPQ"
                                               ,"68c43ae976872a1fa7f5a929b7f21b58"   //7338
                                                    ,"d3-update-base-7338.MPQ"
                                               ,"b7f9057d018f8341c65f8c1220d578c7"   //7447
                                                    ,"d3-update-base-7447.MPQ"
                                               ,"751b8bf5c84220688048c192ab23f380"   //7447
                                                    ,"d3-update-base-7447.MPQ"
                                               ,"357d257eac6f5990096a75b03cc284df"   //7728
                                                    ,"d3-update-base-7728.MPQ"
                                               ,"d5eba8a2324cdc815b2cd5b92104b7fa"   //7728
                                                    ,"d3-update-base-7728.MPQ"
                                               ,"7e13f6184d66520ed3f6b799656a30ca"   //7728
                                                    ,"d3-update-base-7728.MPQ"
                                               ,"5eb4983d4530e3b8bab0d6415d8251fa"   //7841
                                                    ,"d3-update-base-7841.MPQ"
                                               ,"3faf4efa2a96d501c9c47746cba5a7ad"   //7841
                                                    ,"d3-update-base-7841.MPQ"
                                               ,"0a1e7ebcaa1199c5349db83946aa1b5d"   //7841
                                                    ,"d3-update-base-7841.MPQ"
                                               ,"777da16a46d4f1d231bae8c1e11cdeaf"   //7931
                                                    ,"d3-update-base-7931.MPQ"
                                               ,"3d92eee4ed83aeedd977274bdb8af1b7"   //7931
                                                    ,"d3-update-base-7931.MPQ"};
        
        private static int[] valid = {0,0,0,0,0,0,0,0}; //We set a 1 if we found correct hash, it will remain as 0 if incorrect so we will use that to know which files was corrupted.
        public static String[] fileToDownload = new String[8];//Filled with complete urls
        public static String[] fileToDelete = new String[8];//Filled with just the name
        
        public static Boolean ValidateMD5()
        {
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string MPQpath = source.Configs["DiabloPath"].Get("MPQpath");
            String[] filePaths = Directory.GetFiles(MPQpath + @"\base", "*.*", SearchOption.TopDirectoryOnly);
            int fileCount = Directory.GetFiles(MPQpath + @"\base", "*.*", SearchOption.TopDirectoryOnly).Length;
            int trueCounter = 0;
            int j = 0;
            validFound = "";

            foreach (string dir in filePaths)
            {
                string md5Filecheck = Md5Validate.GetMD5HashFromFile(dir);

                for (int i = 0; i < MD5ValidPool.Length; i++)
                {
                    if (md5Filecheck.Contains(MD5ValidPool[i]) == true)
                    {
                        validFound += "Correct hash found for: " + MD5ValidPool[i + 1] + "\n";
                        valid[j] = 1;
                        trueCounter += 1;
                        j++;
                    }
                }
            }

            if (fileCount == trueCounter)
            {
                Console.WriteLine("Validating MPQ's MD5 Hash Complete");
                Console.WriteLine(validFound);
                return true;
            }

            else if (fileCount != trueCounter)
            {
                for (int i = 0; i < valid.Length; i++)
                {
                    if (valid[i] == 0)
                    {
                        switch (i)
                        {
                            case 7:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7170.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7170.MPQ";
                                fileToDelete[i] = "d3-update-base-7170.MPQ";
                                break;
                            case 6:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7200.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7200.MPQ";
                                fileToDelete[i] = "d3-update-base-7200.MPQ";
                                break;
                            case 5:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7318.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7318.MPQ";
                                fileToDelete[i] = "d3-update-base-7318.MPQ";
                                break;
                            case 4:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7338.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7338.MPQ";
                                fileToDelete[i] = "d3-update-base-7338.MPQ";
                                break;
                            case 3:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7447.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7447.MPQ";
                                fileToDelete[i] = "d3-update-base-7447.MPQ";
                                break;
                            case 2:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7728.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7728.MPQ";
                                fileToDelete[i] = "d3-update-base-7728.MPQ";
                                break;
                            case 1:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7841.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7841.MPQ";
                                fileToDelete[i] = "d3-update-base-7841.MPQ";
                                break;
                            case 0:
                                Console.WriteLine("Incorrect hash found on -> d3-update-base-7931.MPQ");
                                fileToDownload[i] = "http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-7931.MPQ";
                                fileToDelete[i] = "d3-update-base-7931.MPQ";
                                break;
                        }
                    }
                }
            } return false;
        }

        public static void MpqTransfer()
        {
            //Takes Diablo Path from Ini, which gets it from finding diablo3.exe 
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string Src = source.Configs["DiabloPath"].Get("MPQpath");
            String Dst = Program.programPath + @"/MPQ";

            if (Directory.Exists(Program.programPath + @"/MPQ")) //Checks for MPQ Folder -WTF u did here wlly? :P
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
                Console.WriteLine(Src);
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
                    || Element.Contains("HLSLShaders") || Element.Contains("lock"))
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