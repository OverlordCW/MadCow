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
using System.Windows.Forms;
//using System.Runtime.InteropServices;

namespace MadCow
{

    class Program
    {
/*      //Things to Hide Console
      [DllImport("user32.dll")]
      public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
      [DllImport("user32.dll")]
      static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
*/

        //Global used variables.
        public static String programPath = System.IO.Directory.GetCurrentDirectory();
        public static String lastRevision = ParseRevision.GetRevision();
        public static String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        [STAThread]
        static void Main()
        {
            // hide the console window                   
            //setConsoleWindowVisibility(false, Console.Title); 

            //Windows Form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
/*        public static void setConsoleWindowVisibility(bool visible, string title)
        {
            // below is Brandon's code           
            //Sometimes System.Windows.Forms.Application.ExecutablePath works for the caption depending on the system you are running under.          
            IntPtr hWnd = FindWindow(null, title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                   
                    ShowWindow(hWnd, 0); // 0 = SW_HIDE               
                else
                    //Show window again                   
                    ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA          
            }
        }
 */

    }
}

//Console Only Application
/* using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MadCow
{
    class Program
    {
        //Global used variables.
        public static String programPath = System.IO.Directory.GetCurrentDirectory();
        public static String lastRevision = ParseRevision.GetRevision();
        public static String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


        static void Main()
        {
            Console.Title = "MadCow Wrapper/Compiler for Mooege (By Wesko)";
            Console.ForegroundColor = ConsoleColor.White;

            if (Directory.Exists(programPath+"/mooege-mooege-" + lastRevision)) 
            {
                Console.WriteLine("You have latest Mooege revision: " + lastRevision);
            }
                else
                {
                    PreRequeriments.FirstRunConfiguration();
                }

            Commands.CommandReader(); //Program loops here./
        }
    }
}
*/
