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
    class SimpleFileDelete
    {
        public static void Delete(int folder)
        {
            //Deletes Mooege Folder
            if (folder == 1)
            {
           // Delete a directory and all subdirectories with Directory static method...
            if (System.IO.Directory.Exists(Program.programPath + @"/Repositories"))
                {
                    try
                    {
                        System.IO.Directory.Delete(Program.programPath + @"/Repositories", true);
                        Console.WriteLine("Deleted Repositories Folder");
                        Directory.CreateDirectory(Program.programPath + @"/Repositories");
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            if (folder == 0)
            {
                // Delete a directory and all subdirectories with Directory static method...
                if (System.IO.Directory.Exists(Program.programPath + @"/logs"))
                {
                    try
                    {
                        System.IO.Directory.Delete(Program.programPath + @"/logs", true);
                        Console.WriteLine("Deleted Logs Folder");
                        Directory.CreateDirectory(Program.programPath + @"/logs");
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
         }

        public static void HideFile()//We hide the default profile, we dont want nubs deleting this cause MadCow will cry about it.
        {
            //string filePath = Program.programPath + @"\ServerProfiles\Default.mdc";
            //File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
        }

        public static void DeleteCorruptedMpq()
        {
            IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
            string MPQpath = source.Configs["DiabloPath"].Get("MPQpath");
            var result1 = MPQprocedure.fileToDelete.Where(item => !string.IsNullOrEmpty(item));
            foreach (string value in result1)
            {
                Console.WriteLine("Deleting file " + value);
                File.Delete(MPQpath + @"\base\" + value);
            }
        }

    }
}