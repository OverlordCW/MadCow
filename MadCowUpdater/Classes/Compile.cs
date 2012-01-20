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
using Microsoft.Build.Evaluation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MadCowUpdater
{
    class Compile
    {
        public static void compileSource()
        {
            var madcowPath = Path.GetTempPath() + @"\MadCow\NewMadCow\MadCow.csproj";

            var compilemadcowTask = Task<bool>.Factory.StartNew(() => CompileMadcow(madcowPath));
            Task.WaitAll(compilemadcowTask);
        }

        private static bool CompileMadcow(string madcowPath)
        {
            var madcowProject = new Project(madcowPath);
            return madcowProject.Build(new Microsoft.Build.Logging.FileLogger());
        }
    }
}
