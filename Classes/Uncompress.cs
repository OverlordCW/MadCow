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
using System.IO.Compression;

namespace MadCow
{
    class Uncompress
    {
        public static void UncompressFiles()
        {
            try
            {
                using (ZipStorer zip = ZipStorer.Open(Program.programPath + "/Mooege.zip", FileAccess.Read))
                {
                    List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

                    Console.WriteLine("Uncompressing Mooege Source...");
                    foreach (ZipStorer.ZipFileEntry entry in dir)
                    {
                        zip.ExtractFile(entry, Program.programPath + "/" + entry);
                    }
                    Console.WriteLine("Uncompressing Mooege Source Complete");
                }
            }
            catch
            {
                Console.WriteLine("Unable to uncompress Mooege source");
            }
        }
    }
}
