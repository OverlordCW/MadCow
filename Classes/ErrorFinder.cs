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
        //change searchText to FATAL
        public static void SearchLogs(String searchText)
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
                                var FileName = match.Groups["filename"].Value;
                                //FileName should now contain the filename of the patch minus the extension
                                var ErrorAnswer = MessageBox.Show(@"Seems your MPQ: " + FileName + @" is corrupted." ,"Would you like to download a new MPQ?", MessageBoxButtons.YesNo);
                                if (ErrorAnswer == DialogResult.Yes)
                                {
                                    //http://ak.worldofwarcraft.com.edgesuite.net/d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/ + FileName + @".MPQ"
                                    //Download using the help of outputError, as in having the download link ready and add outputError to it.
                                }
                                else
                                {
                                    //Nothing!
                                }
                            }
                            oldline = line;
                        }
                    }
                }
            }
        }

    }
}
