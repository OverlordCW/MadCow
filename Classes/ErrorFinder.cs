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
using Nini.Config;

namespace MadCow
{
    class ErrorFinder
    {
        //TODO: Add other errors, they are similar. and all require downloading all MPQs(my guess).
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
                                //This one is for Parsing Errors
                                if (System.Text.RegularExpressions.Regex.IsMatch(oldline, "Applying file:"))
                                {
                                var pattern = "Applying file: (?<filename>.*?).mpq";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(oldline);
                                errorFileName = match.Groups["filename"].Value;
                                return true;  
                                }
                                //This one is for Missing CoreData // ClientData
                                if (System.Text.RegularExpressions.Regex.IsMatch(oldline, "Cannot find base MPQ file:"))
                                {
                                var pattern = "Cannot find base MPQ file: (?<filename>.*?).mpq";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(oldline);
                                errorFileName = match.Groups["filename"].Value;
                                return true;
                                }
                                //Missing a base file/folder
                                if (System.Text.RegularExpressions.Regex.IsMatch(oldline, "Required patch-chain version"))
                                {
                                var pattern = "Required patch-chain version (?<Version>\\d+)";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(oldline);
                                errorFileName = "d3-update-base-" + match.Groups["Version"].Value;
                                return true;
                                }
                                //Need to pretty much redownload all MPQs
                                if (System.Text.RegularExpressions.Regex.IsMatch(line, "Mooege.Core.GS.Items.ItemGenerator"))
                                {
                                    var pattern = "Mooege.Core.GS.Items.ItemGenerator";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    errorFileName = "MajorFailure";
                                    return true;
                                }
                                //Need to pretty much redownload all MPQs
                                if (System.Text.RegularExpressions.Regex.IsMatch(line, "Mooege.Common.MPQ.MPQStorage"))
                                {
                                    var pattern = "Mooege.Common.MPQ.MPQStorage";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    errorFileName = "MajorFailure";
                                    return true;
                                }
                                else
                                {
                                    errorFileName = line;
                                    return true;
                                }

                            }
                            oldline = line;
                        }
                    }
                    return false;
                }
            }
        }

       public static Boolean hasMpqs()
        {
           IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.iniii");
           String Destination = Path.Combine(source.Configs["DiabloPath"].Get("MPQDest"),"base");
           string[] files2 = Directory.GetFiles(Destination, "*.mpq", SearchOption.TopDirectoryOnly);
           Console.WriteLine(Destination);
           Console.WriteLine(TestMPQ.mpqList.Count);
           Console.WriteLine(files2.Length);
           
            if (files2.Length < TestMPQ.mpqList.Count - 1) //-1 For people using previous supported version.
            {
                return false;
            }
            else
                return true;
        }
    }
}
