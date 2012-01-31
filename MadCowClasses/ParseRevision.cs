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

namespace MadCow
{
    class ParseRevision
    {
        public static String RevisionUrl { get; set; }
        public static String DeveloperName
        {
            get
            {
                try
                {
                    var firstPointer = RevisionUrl.IndexOf(".com/", StringComparison.Ordinal);
                    var lastPointer = RevisionUrl.LastIndexOf("/", StringComparison.Ordinal);
                    var betweenPointers = lastPointer - firstPointer;
                    return RevisionUrl.Substring(firstPointer + 5, betweenPointers - 5);
                }
                catch (Exception)
                {
                    CommitFile = "Incorrect repository entry";
                }
                return null;
            }
        }

        public static String BranchName
        {
            get
            {
                var lastPointer = RevisionUrl.Length;
                var firstPointer = RevisionUrl.IndexOf(DeveloperName, StringComparison.Ordinal);
                var developerNameLength = DeveloperName.Length;
                var branchNameLength = lastPointer - (firstPointer + developerNameLength) - 1; //+1 or -1 are to get rid of "/".
                return RevisionUrl.Substring(firstPointer + developerNameLength + 1, branchNameLength);
            }
        }

        public static String LastRevision { get; set; }
        public static String CommitFile { get; set; }
    }
}

