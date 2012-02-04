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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MadCow
{
    internal static class RetrieveMpqList
    {
        //This function will ask and retrieve latest file list from Blizzard server.
        internal static void GetfileList()
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            //WebRequest request = WebRequest.Create("http://enus.patch.battle.net:1119/patch"); //Up to 8101.
            var request = WebRequest.Create("http://public-test.patch.battle.net:1119");
            if (Proxy.proxyStatus)
                request.Proxy = proxy;

            ((HttpWebRequest)request).UserAgent = "Blizzard Web Client";
            request.Method = "POST";
            request.ContentType = "text/html";

            //var postData = "<version program=\"D3\"><record program=\"Bnet\" component=\"Win\" version=\"1\" /><record program=\"D3\" component=\"enUS\" version=\"1\" /></version>";//Up to 8101
            const string postData = "<version program=\"D3B\"><record program=\"Bnet\" component=\"Win\" version=\"1\" /><record program=\"D3B\" component=\"enUS\" version=\"3\"/></version>";
            var byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();

            var receiveStream = response.GetResponseStream();
            
            var readStream = new StreamReader(receiveStream, Encoding.GetEncoding("utf-8"));         
            
            var xml = new XmlTextReader(readStream);
            var D3Data = new string[0];

            while (xml.Read())
            {
                switch (xml.Name)
                {
                    case "record":
                        xml.MoveToAttribute("program");
                        if (xml.Value == "D3B")
                        {
                            xml.Read();
                            D3Data = xml.Value.Trim().Split(';');
                        }
                        break;
                }
            }
            xml.Close();
            readStream.Close();
            response.Close();

            var wc = new WebClient();
            if (Proxy.proxyStatus)
                wc.Proxy = proxy;
            //We put up the .mfil path which contains the fileList.
            var mfil = "d3b-" + D3Data[3] + "-" + D3Data[2] + ".mfil";

            var config = wc.DownloadString(D3Data[0]);
            var rdr = XmlReader.Create(new StringReader(config));
            while (rdr.Read())
            {
                switch (rdr.Name)
                {
                    case "server":
                        rdr.MoveToAttribute("url");
                        mfil = rdr.Value + mfil;
                        break;
                }
            }
            rdr.Close();

            Directory.CreateDirectory(Environment.CurrentDirectory + "\\RuntimeDownloads\\");
            File.WriteAllText(Environment.CurrentDirectory + "\\RuntimeDownloads\\Diablo III.mfil", wc.DownloadString(mfil));
            ParseFiles();
        }

        internal static List<String> MpqList = new List<String>();

        internal static void ParseFiles()
        {
            using (var fileStream = new FileStream(Environment.CurrentDirectory + @"\RuntimeDownloads\Diablo III.mfil", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    var i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (Regex.IsMatch(line, "name"))
                            {
                                const string pattern = @"=(?<name>.*)";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(line);

                                if (match.Groups["name"].Value.Contains("d3-update-base-"))
                                {
                                    MpqList.Add(match.Groups["name"].Value);
                                    i++;
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
