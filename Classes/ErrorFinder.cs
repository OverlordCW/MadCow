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
using System.IO;
using System.Text.RegularExpressions;

// When Testing error finder, by using Play Diablo. It is only allowed to be used with Wetwlly's Mooege Fork, 
//as I've made the changes to mooege to be able to read the logs while accessed by mooege.

namespace MadCow
{
    class ErrorFinder
    {
        public static void SearchLogs(String searchText)
        {
            using (FileStream fileStream = new FileStream(Program.programPath + @"\logs\mooege.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    //stream reader needs to read by lines? or be able to read previous line.

                    string fileContents = reader.ReadToEnd();
                    if (System.Text.RegularExpressions.Regex.IsMatch(fileContents, searchText))
                    {
                        //This should be a message box.
                        Console.WriteLine("You in trouble!");
                    }
                    else
                    {
                        //Show that it was not a match
                        Console.WriteLine("SO FAR GOOD");
                    }
                }
            }
        }

    }
}
