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
using System.Xml;
using System.Text.RegularExpressions;

namespace MadCow
{
    class TestMPQ
    {
        //This function will ask and retrieve latest file list from Blizzard server.
        public static void getfileList()
        {
            WebRequest request = WebRequest.Create("http://enus.patch.battle.net:1119/patch");

            ((HttpWebRequest)request).UserAgent = "Blizzard Web Client";
            request.Method = "POST";
            request.ContentType = "text/html";

            var postData = "<version program=\"D3\"><record program=\"Bnet\" component=\"Win\" version=\"1\" /><record program=\"D3\" component=\"enUS\" version=\"1\" /></version>";
            var byteArray = ASCIIEncoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            Stream ReceiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(ReceiveStream, Encoding.GetEncoding("utf-8"));

            var xml = new XmlTextReader(readStream);
            string[] D3Data = new string[0];

            while (xml.Read())
            {
                switch (xml.Name)
                {
                    case "record":
                        xml.MoveToAttribute("program");
                        if (xml.Value == "D3")
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
            //We put up the .mfil path which contains the fileList.
            var mfil = "d3-" + D3Data[3] + "-" + D3Data[2] + ".mfil";

            var config = wc.DownloadString(D3Data[0]);
            var rdr = System.Xml.XmlReader.Create(new StringReader(config));
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

            System.IO.File.WriteAllText(Program.programPath + "\\Diablo III.mfil", wc.DownloadString(mfil));
            parseFiles();
        }

        public static List<String> mpqList = new List<String>();
        public static void parseFiles()
        {
            using (FileStream fileStream = new FileStream(Program.programPath + @"\Diablo III.mfil", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    Int16 i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(line, "name"))
                            {
                                var pattern = @"=(?<name>.*)";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(line);

                                if (match.Groups["name"].Value.ToString().Contains("d3-update-base-"))
                                {
                                    mpqList.Add(match.Groups["name"].Value);
                                    i++;
                                }
                            }

                            /*if (System.Text.RegularExpressions.Regex.IsMatch(line, "size"))
                            {
                                var pattern = "size=(?<size>\\d+)";
                                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var match = regex.Match(line);
                                Console.WriteLine("Size: " + match.Groups["size"].Value);
                            }*/
                            oldline = line;
                        }
                    }
                }
            }
        }
    }
}
