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
using Microsoft.Build.Evaluation;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;

namespace MadCowUpdater
{
    class UpdaterProcedures
    {
        public static void Uncompress() //This is actually the whole process MadCow uses after Downloading source.
        {
            ZipFile zip = null;
            var events = new FastZipEvents();

            FastZip z = new FastZip(events);
            var stream = new FileStream(Path.GetTempPath() + @"\MadCow.zip", FileMode.Open, FileAccess.Read);
            zip = new ZipFile(stream);
            zip.IsStreamOwner = true; //Closes parent stream when ZipFile.Close is called
            zip.Close();

            Task task = Task.Factory.StartNew(() => z.ExtractZip(Path.GetTempPath() + @"\MadCow.zip", Path.GetTempPath() + @"\" + @"MadCow\",null));
            task.Wait();

            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.UncompressSuccessDot.Visible = true;
                Form1.GlobalAccess.UncompressingLabel.ForeColor = System.Drawing.Color.Green;
            }));
        }

        public static void CopyFiles()
        {
            var MadCowPath = Directory.GetParent(Program.path);
            Task<bool> task = Task<bool>.Factory.StartNew(() => Helper.CopyDirectory(Path.GetTempPath() + @"\MadCow\NewMadCow\bin\MadCowDebug\", MadCowPath.ToString(), true));
            bool result = task.Result;
            task.Wait();

            if (result == false)
            {
                MessageBox.Show("[Fatal] Failed to copy files.");
            }
            else
            {
                Form1.GlobalAccess.Invoke(new Action(() =>
                {
                    Form1.GlobalAccess.CopySuccessDot.Visible = true;
                    Form1.GlobalAccess.CopyingLabel.ForeColor = System.Drawing.Color.Green;
                }));
            }
        }

        public static void CompileSource()
        {
            var madcowPath = Path.GetTempPath() + @"\MadCow\NewMadCow\MadCow.csproj";

            Task<bool> task = Task<bool>.Factory.StartNew(() => CompileMadcow(madcowPath));
            bool result = task.Result;
            task.Wait();

            if (result == false)
            {
                MessageBox.Show("[Fatal] Failed to compile.");
            }
            else
            {
                Form1.GlobalAccess.Invoke(new Action(() =>
                {
                    Form1.GlobalAccess.CompilingSuccessDot.Visible = true;
                    Form1.GlobalAccess.CompilingLabel.ForeColor = System.Drawing.Color.Green;
                }));
            }
        }

        private static bool CompileMadcow(string madcowPath)
        {
            var madcowProject = new Project(madcowPath);
            return madcowProject.Build(new Microsoft.Build.Logging.FileLogger());
        }
    }
}
