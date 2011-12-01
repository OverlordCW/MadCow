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
using System.Threading;

namespace MadCow
{
    class ParseRevision
    {
        public static String revisionUrl = "";
        public static String developerName = "";
        public static String branchName = "";
        public static String lastRevision = "";
        public static String commitFile = "";
        public static String errorSender = "";

        public static void getDeveloperName()
        {
            try
            {
                Int32 FirstPointer = revisionUrl.IndexOf(".com/");
                Int32 LastPointer = revisionUrl.LastIndexOf("/");
                Int32 BetweenPointers = LastPointer - FirstPointer;
                developerName = revisionUrl.Substring(FirstPointer + 5, BetweenPointers - 5);
            }
            catch (Exception)
            {
                commitFile = "Incorrect repository entry";
                errorSender = "Incorrect repository entry.";
            }
        }

        public static void getBranchName() // /D3Sharp /Mooege /Mooege-1 , etc.
        {
            Int32 LastPointer = revisionUrl.Length;
            Int32 FirstPointer = revisionUrl.IndexOf(developerName);
            Int32 DeveloperNameLength = developerName.Length;
            Int32 BranchNameLength = LastPointer-(FirstPointer+DeveloperNameLength)-1; //+1 or -1 are to fix missing "/" while parsing.
            branchName = revisionUrl.Substring(FirstPointer + DeveloperNameLength + 1, BranchNameLength);
        }

        public static void GetRevision()
        {
            try
            {
                WebClient client = new WebClient();
                commitFile = client.DownloadString(revisionUrl + "/commits/master.atom");
                Int32 pos2 = commitFile.IndexOf("Commit/");
                String revision = commitFile.Substring(pos2 + 7, 7);
                lastRevision = commitFile.Substring(pos2 + 7, 7);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        commitFile = "Incorrect repository entry";
                        errorSender = "Incorrect repository entry.";
                    }
                }
                else if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    commitFile = "ConnectionFailure";
                    errorSender = "Check your internet connection.";
                }
                else
                    commitFile = "Incorrect repository entry";
                errorSender = "Incorrect repository entry.";
            }
        }
    }
}
