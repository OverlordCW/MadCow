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
using System.IO;
using System.Linq;

namespace MadCow
{
    internal class RevisionParser
    {
        internal RevisionParser(Uri url)
        {
            RevisionUrl = url;
        }

        #region Properties
        internal Uri RevisionUrl { get; set; }

        internal string DeveloperName { get { return RevisionUrl.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0]; } }

        internal string ForkName { get { return RevisionUrl.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1]; } }

        internal string LastRevision
        {
            get
            {
                return string.IsNullOrEmpty(CommitFile)
                           ? null
                           : CommitFile.Substring(CommitFile.IndexOf("Commit/", StringComparison.Ordinal) + 7, 7);
            }
        }

        internal string CommitFile { get; set; }
        #endregion

        internal string GetPath()
        {
            return string.IsNullOrEmpty(LastRevision)
                       ? null
                       : string.Format("{0}-{1}-{2}", DeveloperName, ForkName, LastRevision);
        }
    }
}

