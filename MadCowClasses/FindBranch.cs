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
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace MadCow
{
    class FindBranch
    {
        public static void findBrach(String Url)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            Uri uri = new Uri(Url + "/branches");
            WebClient client = new WebClient();
            if (Proxy.proxyStatus)
                client.Proxy = proxy;
            client.DownloadFile(new Uri(Url + "/branches"), Environment.CurrentDirectory + @"\RuntimeDownloads\Branch.txt");
            searchBranch();
        }
        public static void searchBranch()
        {
            Form1.GlobalAccess.BranchComboBox.Items.Clear();
            Form1.GlobalAccess.BranchComboBox.Items.Add("master");
            using (FileStream fileStream = new FileStream(Environment.CurrentDirectory + @"\RuntimeDownloads\Branch.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(line, "/tree/"))
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(line, "/tree/"))
                                {
                                    String pattern = @"<A\shref=""(?<FilePath>[^""]*)"">(?<File>[^<]*)";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    Form1.GlobalAccess.BranchComboBox.Items.Add(match.Groups["File"].Value);
                                }
                            }
                        }
                    }
                    reader.Close();
                }
                fileStream.Close();
            }
            Form1.GlobalAccess.BranchComboBox.SelectedIndex = Form1.GlobalAccess.BranchComboBox.FindStringExact("master");
        }

    }
}

