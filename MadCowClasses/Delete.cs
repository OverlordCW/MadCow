// Copyright (C) 2011 MadCow Project
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
using System.Windows.Forms;
using System.Linq;
using Nini.Config;

namespace MadCow
{
    class DeleteHelper
    {
        public static void Delete(int selection)
        {
            switch (selection)
            {
                case 0:
                    // Delelete/Recreate Logs Folder
                    if (System.IO.Directory.Exists(Program.programPath + @"/logs"))
                    {
                        try
                        {
                            System.IO.Directory.Delete(Program.programPath + @"/logs", true);
                            Console.WriteLine("Deleted Logs Folder");
                            Directory.CreateDirectory(Program.programPath + @"/logs");
                        }
                        catch
                        {
                            Console.WriteLine("[ERROR] Could not delete Logs folder. (Delete.cs)" + "\nPlease report this error in the forum.");
                            break;
                        }
                    }
                    break;

                case 1: //Delete \Repositories\ subfolders.
                    if (System.IO.Directory.Exists(Program.programPath + @"/Repositories"))
                    {
                        try
                        {
                            string[] FoldersArray = Directory.GetDirectories(Program.programPath + @"\" + @"Repositories\");
                            if (FoldersArray.Length > 0)
                            {
                                Console.WriteLine("Deleting Saved Repositories...");
                            }
                            foreach (string folder in FoldersArray)
                            {
                                DirectoryInfo info = new DirectoryInfo(folder);
                                Directory.Delete(info.FullName, true);
                                Console.WriteLine("Deleted: {0}", info.Name);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("[ERROR] Could not delete repositories. (Delete.cs)" + "\nPlease report this error in the forum.");
                            break;
                        }
                    }

                    break;

                case 2: //Delete corrupted file thrown by ErrorFinder class.
                    if (File.Exists(Program.madcowINI))
                    {
                        try
                        {
                            IConfigSource source = new IniConfigSource(Program.madcowINI);
                            string MPQpath = source.Configs["DiabloPath"].Get("MPQpath");
                            if (MPQpath.Length > 0) //This is to avoid deleting a non existant file if user its only using standalone Mooege server without D3 client.
                            {
                                Console.WriteLine("Deleting file " + ErrorFinder.errorFileName);
                                File.Delete(MPQpath + @"\base\" + ErrorFinder.errorFileName);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("[ERROR] Could not delete corrupted file. (Delete.cs)" +"\nPlease report this error in the forum.");
                            break; 
                        }
                    }
                        //If madcow.ini aint found.
                    else
                    {
                        Console.WriteLine("[ERROR] Could not find MadCow config file. (Delete.cs)" + "\nPlease report this error in the forum.");
                    }
                    break;                                   
            }
        }

        public static void DeleteOldRepoVersion(string developerName)
        {
            if (Directory.Exists(Program.programPath + @"\Repositories\"))
            {
                try
                {
                    Console.WriteLine("Looking for [{0}] existing repository.", developerName);
                    String directoryString = Program.programPath + @"\Repositories\";
                    Int32 i = directoryString.LastIndexOf('\\');
                    directoryString = directoryString.Remove(i, directoryString.Length - i);
                    string[] directories = Directory.GetDirectories(directoryString);
                    string[] folderName = new string[2];
                    Int32 j = 0;

                    foreach (string directory in directories)
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(directory);
                        if (directory.Contains(developerName) && dinfo.Name.StartsWith(developerName)) //We avoid deleting all folder that contains Mooege when we are just trying to get rid of Master branch.
                        {
                            folderName[j] = dinfo.Name;
                            folderName[j + 1] = directory;
                            Directory.Delete(folderName[1], true);
                            Console.WriteLine("Deleted Old Version of : {0} repository.", folderName[0]);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("[ERROR] Could not delete Repositories. (Delete.cs)" + "\nPlease report this error in the forum.");
                }
            }
            else
            {
                Console.WriteLine("[ERROR] Could not find Repositories folder. (Delete.cs)" + "\nPlease report this error in the forum.");
            }
        }
    }
}