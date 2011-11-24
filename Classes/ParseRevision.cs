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
using System.Net;


namespace MadCow
{
    class ParseRevision
    {
        public static String GetRevision()
        {
            try
            {
                WebClient client = new WebClient();
                String commitFile = client.DownloadString("https://github.com/mooege/mooege/commits/master.atom");
                Int32 pos1 = commitFile.IndexOf("Commit/");
                String revision = commitFile.Substring(pos1 + 7, 7);
                return revision;
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.ToString());
                if (webEx.Status == WebExceptionStatus.ConnectFailure)
                {
                    Console.WriteLine("Error: Couldn't retrieve latest source");
                }
                return "Fatal Exception";
            }
        }
    }
}
