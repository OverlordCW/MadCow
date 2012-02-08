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

namespace MadCow
{
    internal static class ErrorFinder
    {
        //TODO: Add other errors, they are similar. and all require downloading all MPQs(my guess).
        internal static string ErrorFileName = "";
        //change searchText to FATAL
        internal static Boolean SearchLogs(string searchText)
        {
            using (var fileStream = new FileStream(Path.Combine(Environment.CurrentDirectory, "logs", "mooege.log"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (Regex.IsMatch(line, searchText))
                            {
                                //This one is for Parsing Errors
                                if (Regex.IsMatch(oldline, "Applying file:"))
                                {
                                    const string pattern = "Applying file: (?<filename>.*?).mpq";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(oldline);
                                    ErrorFileName = match.Groups["filename"].Value;
                                    return true;
                                }
                                //This one is for Missing CoreData // ClientData
                                if (Regex.IsMatch(oldline, "Cannot find base MPQ file:"))
                                {
                                    const string pattern = "Cannot find base MPQ file: (?<filename>.*?).mpq";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(oldline);
                                    ErrorFileName = match.Groups["filename"].Value;
                                    return true;
                                }
                                //Missing a base file/folder
                                if (Regex.IsMatch(oldline, "Required patch-chain version"))
                                {
                                    const string pattern = "Required patch-chain version (?<Version>\\d+)";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(oldline);
                                    ErrorFileName = "d3-update-base-" + match.Groups["Version"].Value;
                                    return true;
                                }
                                //Need to pretty much redownload all MPQs
                                if (Regex.IsMatch(line, "Mooege.Core.GS.Items.ItemGenerator"))
                                {
                                    const string pattern = "Mooege.Core.GS.Items.ItemGenerator";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    ErrorFileName = "MajorFailure";
                                    return true;
                                }
                                //Need to pretty much redownload all MPQs
                                if (Regex.IsMatch(line, "Mooege.Common.MPQ.MPQStorage"))
                                {
                                    const string pattern = "Mooege.Common.MPQ.MPQStorage";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    ErrorFileName = "MajorFailure";
                                    return true;
                                }
                                ErrorFileName = line;
                                return true;
                            }
                            oldline = line;
                        }
                    }
                    return false;
                }
            }
        }

        public static bool HasMpqs()
        {
            //Here we compare onle base mpqs from Diablo Client & our destinations MPQ Path.
            try
            {
                var destination = Path.Combine(Configuration.MadCow.MpqServer, "base");
                var files2 = Directory.GetFiles(destination, "*.mpq", SearchOption.TopDirectoryOnly);

                //-1 For people using previous supported version.
                return files2.Length >= RetrieveMpqList.MpqList.Count - 1;
            }
            catch
            {
                Console.WriteLine("[ERROR] Could not get mpq count. (ErrorFinder.cs)" +
                                  "\nPlease report this error in the forum.");
                return false;
            }
        }
    }
}
