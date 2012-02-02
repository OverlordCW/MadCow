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
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;


namespace MadCow
{

    class Program
    {
        static Mutex s_mutex;

        [STAThread]
        static void Main()
        {
            bool instantiated;

            s_mutex = new Mutex(false, "{F9E6DEE0-70F5-46E4-97F8-962F5F829CC9}", out instantiated);

            if (instantiated)
            {
                //Forcing the culture to English, this way we can read exceptions better from foreign users.
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("MadCow is already open.");
            }
        }
    }
}