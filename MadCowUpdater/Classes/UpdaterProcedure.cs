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
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Logging;

namespace MadCowUpdater
{
    class UpdaterProcedures
    {
        public static void Uncompress() //This is actually the whole process MadCow uses after Downloading source.
        {
            var events = new FastZipEvents();

            var z = new FastZip(events);
            var stream = new FileStream(Path.GetTempPath() + @"\MadCow.zip", FileMode.Open, FileAccess.Read);
            var zip = new ZipFile(stream);
            zip.IsStreamOwner = true; //Closes parent stream when ZipFile.Close is called
            zip.Close();

            Task task = Task.Factory.StartNew(() => z.ExtractZip(Path.GetTempPath() + @"\MadCow.zip", Path.GetTempPath() + @"\" + @"MadCow\",null));
            task.Wait();

            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.UncompressSuccessDot.Visible = true;
                Form1.GlobalAccess.UncompressingLabel.ForeColor = Color.Green;
            }));
        }

        public static void CopyFiles()
        {
            var madCowPath = Directory.GetParent(Program.path);
            var task = Task<bool>.Factory.StartNew(() => Helper.CopyDirectory(Path.GetTempPath() + @"\MadCow\NewMadCow\bin\MadCowDebug\", madCowPath.ToString(), true));
            var result = task.Result;
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
                    Form1.GlobalAccess.CopyingLabel.ForeColor = Color.Green;
                }));
            }
        }

        public static void CompileSource()
        {
            var madcowPath = Path.GetTempPath() + @"\MadCow\NewMadCow\MadCow.csproj";

            var task = Task<bool>.Factory.StartNew(() => CompileMadcow(madcowPath));
            var result = task.Result;
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
                    Form1.GlobalAccess.CompilingLabel.ForeColor = Color.Green;
                }));
            }
        }

        private static bool CompileMadcow(string madcowPath)
        {
            var madcowProject = new Project(madcowPath);
            madcowProject.SetProperty("Configuration", "Release");
            madcowProject.SetProperty("Platform", "AnyCPU");
            return madcowProject.Build(new FileLogger());
        }
    }
}
