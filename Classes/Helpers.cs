using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Nini.Config;
using System.Text.RegularExpressions;

namespace MadCow
{
    class Helper
    {
        public static void Helpers()
        {
        if (File.Exists(Program.programPath + "\\Tools\\" + "madcow.ini"))
            {
                IConfigSource source = new IniConfigSource(Program.programPath + @"\Tools\madcow.ini");
                String BalloonTips = source.Configs["Balloons"].Get("ShowBalloons");

                Form1.GlobalAccess.label21.ResetText();

                if (BalloonTips.Contains("1"))
                {
                    Form1.GlobalAccess.label21.Text = "Enabled";
                    Form1.GlobalAccess.label21.ForeColor = Color.SeaGreen;
                }
                else
                {
                    Form1.GlobalAccess.label21.Text = "Disabled";
                    Form1.GlobalAccess.label21.ForeColor = Color.DimGray;
                }

                String Shorcut = source.Configs["ShortCut"].Get("Shortcut");
                Form1.GlobalAccess.label9.ResetText();

                if (Shorcut.Contains("1"))
                {
                    Form1.GlobalAccess.label9.Text = "Enabled";
                    Form1.GlobalAccess.label9.ForeColor = Color.SeaGreen;
                }
                else
                {
                    Form1.GlobalAccess.label9.Text = "Disabled";
                    Form1.GlobalAccess.label9.ForeColor = Color.DimGray;
                }

                String LastRepo = source.Configs["LastPlay"].Get("Enabled");
                Form1.GlobalAccess.label23.ResetText();

                if (LastRepo.Contains("1"))
                {
                    Form1.GlobalAccess.label23.Text = "Enabled";
                    Form1.GlobalAccess.label23.ForeColor = Color.SeaGreen;
                    Form1.GlobalAccess.label25.Visible = true;
                }
                else
                {
                    Form1.GlobalAccess.label23.Text = "Disabled";
                    Form1.GlobalAccess.label23.ForeColor = Color.DimGray;
                    Form1.GlobalAccess.label25.Visible = false;
                }
            }
        }
    }
}
