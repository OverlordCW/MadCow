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
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MadCow
{
    internal static class FindBranch
    {
        public static string[] FindBrach(string url)
        {
            try
            {
                var proxy = new WebProxy();
                if (Proxy.proxyStatus)
                {
                    proxy.Address = new Uri(Proxy.proxyUrl);
                    proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
                }

                var client = new WebClient();
                if (Proxy.proxyStatus)
                    client.Proxy = proxy;
                client.DownloadFile(new Uri(url + "/branches"), Environment.CurrentDirectory + @"\RuntimeDownloads\Branch.txt");
                return SearchBranch();
            }
            catch (UriFormatException)
            {
                return null;
            }
            catch (WebException)
            {
                return null;
            }
        }

        private static string[] SearchBranch()
        {

            //using (var fileStream = new FileStream(Environment.CurrentDirectory + @"\RuntimeDownloads\Branch.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            //{
            //    using (TextReader reader = new StreamReader(fileStream))
            //    {
            //        string line;
            //        while ((line = reader.ReadLine()) != null)
            //        {
            var branches = new List<string> { "master" };
            foreach (var line in File.ReadAllLines(Environment.CurrentDirectory + @"\RuntimeDownloads\Branch.txt")
                .Where(line => Regex.IsMatch(line, "/tree/"))
                .Where(line => Regex.IsMatch(line, "/tree/")))
            {
                const string pattern = @"<A\shref=""(?<FilePath>[^""]*)"">(?<File>[^<]*)";
                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var match = regex.Match(line);
                branches.Add(match.Groups["File"].Value);
            }

            //        }
            //    }
            //}
            return branches.ToArray();
        }

    }
}

