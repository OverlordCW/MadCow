// Copyright (C) 2011 MadCow Project
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

// Used code from http://www.dreamincode.net/code/snippet1568.htm.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Nini.Config;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace MadCow
{
    class Helper
    {
        //For MadCow configuration loading settings.
        public static void Helpers()
        {
            //if (File.Exists(Program.madcowINI))
            //{
            //    IConfigSource source = new IniConfigSource(Program.madcowINI);
            //    String BalloonTips = source.Configs["Balloons"].Get("ShowBalloons");

            //    Form1.GlobalAccess.TrayNotificationsStatusLabel.ResetText();

            //    if (BalloonTips.Contains("1"))
            //    {
            //        Form1.GlobalAccess.TrayNotificationsStatusLabel.Text = "Enabled";
            //        Form1.GlobalAccess.TrayNotificationsStatusLabel.ForeColor = Color.SeaGreen;
            //    }
            //    else
            //    {
            //        Form1.GlobalAccess.TrayNotificationsStatusLabel.Text = "Disabled";
            //        Form1.GlobalAccess.TrayNotificationsStatusLabel.ForeColor = Color.DimGray;
            //    }

            //    String Shorcut = source.Configs["ShortCut"].Get("Shortcut");
            //    Form1.GlobalAccess.SrtCutStatusLabel.ResetText();

            //    if (Shorcut.Contains("1"))
            //    {
            //        Form1.GlobalAccess.SrtCutStatusLabel.Text = "Enabled";
            //        Form1.GlobalAccess.SrtCutStatusLabel.ForeColor = Color.SeaGreen;
            //    }
            //    else
            //    {
            //        Form1.GlobalAccess.SrtCutStatusLabel.Text = "Disabled";
            //        Form1.GlobalAccess.SrtCutStatusLabel.ForeColor = Color.DimGray;
            //    }

            //    String LastRepo = source.Configs["LastPlay"].Get("Enabled");
            //    Form1.GlobalAccess.RememberLastRepoStatusLabel.ResetText();

            //    if (LastRepo.Contains("1"))
            //    {
            //        Form1.GlobalAccess.RememberLastRepoStatusLabel.Text = "Enabled";
            //        Form1.GlobalAccess.RememberLastRepoStatusLabel.ForeColor = Color.SeaGreen;
            //        Form1.GlobalAccess.LastPlayedRepoReminderLabel.Visible = true;
            //    }
            //    else
            //    {
            //        Form1.GlobalAccess.RememberLastRepoStatusLabel.Text = "Disabled";
            //        Form1.GlobalAccess.RememberLastRepoStatusLabel.ForeColor = Color.DimGray;
            //        Form1.GlobalAccess.LastPlayedRepoReminderLabel.Visible = false;
            //    }

            //    String TrayIcon = source.Configs["Tray"].Get("Enabled");
            //    Form1.GlobalAccess.MinimizeTrayStatusLabel.ResetText();

            //    if (TrayIcon.Contains("1"))
            //    {
            //        Form1.GlobalAccess.MinimizeTrayStatusLabel.Text = "Enabled";
            //        Form1.GlobalAccess.MinimizeTrayStatusLabel.ForeColor = Color.SeaGreen;
            //        Form1.GlobalAccess.loadTrayMenu();//Loading the contextMenu for trayIcon    
            //    }
            //    else
            //    {
            //        Form1.GlobalAccess.MinimizeTrayStatusLabel.Text = "Disabled";
            //        Form1.GlobalAccess.MinimizeTrayStatusLabel.ForeColor = Color.DimGray;
            //    }
            //}
        }

        //MadCowUpdater process killer.
        public static void KillUpdater()
        {
            if (ProcessFinder.FindProcess("MadCowUpdater") == true)
            {
                ProcessFinder.KillProcess("MadCowUpdater");
            }
        }

        //Check for internet Connection.
        public static bool isConnectionAvailable()
        {
            bool _success;
            //We use google... ff google is down, probably we got hit by a meteor.
            string[] sitesList = { "www.google.com" };
            Ping ping = new Ping();
            PingReply reply;
            int notReturned = 0;

            try
            {
                reply = ping.Send(sitesList[0], 10);
                if (reply.Status != IPStatus.Success)
                {
                    notReturned += 1;
                }
                if (notReturned == sitesList.Length)
                {
                    _success = false;
                }
                else
                {
                    _success = true;
                }
            }
            catch
            {
                _success = false;
            }
            return _success;
        }

        //Check for internet Connection.
        public static void CheckForInternet()
        {
            //If we were unable to ping the server, we warn the user about repercutions!.
            if (!isConnectionAvailable())
            {
                MessageBox.Show("There is no Internet connection, MadCow will NOT be able to perform correctly."
                    + "\n\nBe aware: Fatal errors might happen:"
                    + "\nMadCow has an strong Internet dependency.",
                    "Warning - No Internet Connection was found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Create default need folders.
        public static void DefaultFolderCreator()
        {
            var programPath = Environment.CurrentDirectory;
            if (Directory.Exists(programPath + @"\MPQ\base") == false)
            {
                Directory.CreateDirectory(programPath + @"\MPQ\base");
            }
            if (Directory.Exists(programPath + @"\Repositories") == false)
            {
                Directory.CreateDirectory(programPath + @"\Repositories");
            }
            if (Directory.Exists(programPath + @"\RuntimeDownloads") == false)
            {
                Directory.CreateDirectory(programPath + @"\RuntimeDownloads");
            }
            //Apart from creating ServerProfiles folder, we write the Defualt profile.
            if (Directory.Exists(programPath + @"\ServerProfiles") == false)
            {
                Directory.CreateDirectory(programPath + @"\ServerProfiles");
                TextWriter tw = new StreamWriter(programPath + @"\ServerProfiles\Default.mdc");
                //tw.WriteLine("Bnet Server Ip");
                tw.WriteLine("0.0.0.0");
                //tw.WriteLine("Game Server Ip");
                tw.WriteLine("0.0.0.0");
                //tw.WriteLine("Public Server Ip");
                tw.WriteLine("0.0.0.0");
                //tw.WriteLine("Bnet Server Port");
                tw.WriteLine("1345");
                //tw.WriteLine("Game Server Port");
                tw.WriteLine("1999");
                //tw.WriteLine("MOTD");
                tw.WriteLine("Welcome to mooege development server!");
                //tw.WriteLine("NAT");
                tw.WriteLine("False");
                tw.Close();
            }
        }
    }
}
