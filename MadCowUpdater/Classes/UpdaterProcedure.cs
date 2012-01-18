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
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;

namespace MadCowUpdater
{
    class UpdaterProcedures
    {
        public static void RunWholeProcedure() //This is actually the whole process MadCow uses after Downloading source.
        {
            ZipFile zip = null;
            var events = new FastZipEvents();

            FastZip z = new FastZip(events);
            Console.WriteLine("Uncompressing zip file...");
            var stream = new FileStream(Path.GetTempPath() + @"\MadCow.zip", FileMode.Open, FileAccess.Read);
            zip = new ZipFile(stream);
            zip.IsStreamOwner = true; //Closes parent stream when ZipFile.Close is called
            zip.Close();

            var t1 = Task.Factory.StartNew(() => z.ExtractZip(Path.GetTempPath() + @"\MadCow.zip", Path.GetTempPath() + @"\" + @"MadCow\", null))
                .ContinueWith(delegate
            {
                Continue();
            });
        }

        private static void Continue()
        {
            var MadCowPath = Directory.GetParent(Program.path);
            Helper.ModifyFolderName();
            Compile.compileSource(); //Compile solution projects.
            Helper.DeleteUpdaterFiles();
            bool copy2 = Helper.CopyDirectory(Path.GetTempPath() + @"\MadCow\NewMadCow\bin\Debug\", MadCowPath.ToString(), true);
            Finish();
        }

        private static void Finish()
        {
            var MadCowPath = Directory.GetParent(Program.path);
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = MadCowPath + @"\MadCow.exe";
            firstProc.Start();
            //Form1.GlobalAccess.timer1.Enabled = true;
        }
    }
}
