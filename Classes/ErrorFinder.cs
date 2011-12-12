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
using System.Windows.Forms;

// When Testing error finder, by using Play Diablo. It is only allowed to be used with Wetwlly's Mooege Fork, 
//as I've made the changes to mooege to be able to read the logs while accessed by mooege.

namespace MadCow
{
    class ErrorFinder
    {
        public static String errorFileName = "";
        //change searchText to FATAL
        public static Boolean SearchLogs(String searchText)
        {
            using (FileStream fileStream = new FileStream(Program.programPath + @"\logs\mooege.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(line, searchText))
                            {
                                var pattern = "Applying file: (?<filename>.*?).mpq";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(oldline);
                                errorFileName = match.Groups["filename"].Value;
                                return true;                                
                            }
                            oldline = line;
                        }
                    }
                    return false;
                }
            }
        }

    }
}
