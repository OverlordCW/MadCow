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
// Using API : http://www.meebey.net/projects/smartirc4net/

using Meebey.SmartIrc4net;
using Meebey.SmartIrc4net.Delegates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace MadCow
{
    public class Client
    {
        public static IrcClient irc = new IrcClient();
        public static String fixedNickname;
        public static String channel = "#mooege.chat";
        public static String server = "downtown.tx.us.synirc.net";

        public static void Run()
        {
            nickname();
            irc.SendDelay = 200;
            irc.AutoRetry = true;
            irc.ChannelSyncing = true;
            irc.OnQueryMessage += new MessageEventHandler(OnQueryMessage);
            irc.OnChannelMessage += new MessageEventHandler(OnChannelMessage);
            irc.OnJoin += new JoinEventHandler(OnJoin);
            irc.OnNameReply += new NameReplyEventHandler(OnNames);
            irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
            irc.OnDisconnected += new SimpleEventHandler(OnDisconnected);

            string[] serverlist;
            serverlist = new string[] { "downtown.tx.us.synirc.net" };

            int port = 6667;
            if (irc.Connect(serverlist, port) == true)
            {
                irc.Login(fixedNickname, "MadCow Live Help Client");
                irc.Join(channel);
                Connected();
                irc.Listen();
            }
            else
            {
                Console.WriteLine("[ERROR] Irc Client could not connect.");
            }
        }

        public static void OnDisconnected()
        {
            Form1.GlobalAccess.Invoke(new Action(() => 
            {
                //Hide Chat Window
                Form1.GlobalAccess.ChatDisplayBox.Clear();
                Form1.GlobalAccess.ChatUsersBox.Clear();
                Form1.GlobalAccess.ChatDisplayBox.Visible = false;
                Form1.GlobalAccess.ChatUsersBox.Visible = false;
                Form1.GlobalAccess.ChatMessageBox.Visible = false;
                Form1.GlobalAccess.TypeHereLabel.Visible = false;
                Form1.GlobalAccess.DisconnectButton.Visible = false;
                //Show Default help tab.
                Form1.GlobalAccess.Rules.Visible = true;
                Form1.GlobalAccess.label1.Visible = true;
                Form1.GlobalAccess.label2.Visible = true;
                Form1.GlobalAccess.label3.Visible = true;
                Form1.GlobalAccess.label4.Visible = true;
                Form1.GlobalAccess.BotonAlerta.Visible = true;
                Form1.GlobalAccess.Advertencia.Visible = true;
                Form1.GlobalAccess.ConnectButton.Visible = true;
                //Kill Thread.
                Form1.ircThread.Abort();
            }));
        }

        public static void OnNickChange(string oldnickname, string newnickname, Data ircdata)
        {
            //Todo: Update User List on ChatUsersBox.
        }

        public static void OnNames(string channel, string[] userlist, Meebey.SmartIrc4net.Data ircdata)
        {
            Array.Sort(userlist);
            foreach (string user in userlist)
            {
                Form1.GlobalAccess.Invoke(new Action(() =>
                {
                    if (user.Length > 0)
                    {
                        Form1.GlobalAccess.ChatUsersBox.Text += user + Environment.NewLine;
                    }
                }));
            }
        }

        public static void OnJoin(string x, string y, Data ircdata)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += ("Joined:" + ircdata.Channel + Environment.NewLine);
                Form1.GlobalAccess.DisconnectButton.Visible = true;
                Form1.GlobalAccess.ChatMessageBox.Visible = true;
                Form1.GlobalAccess.TypeHereLabel.Visible = true;
                Form1.GlobalAccess.PleaseWaitLabel.Visible = false;
            }));
        }

        public static void OnQueryMessage(Data ircdata)
        {
            switch (ircdata.MessageEx[0])
            {
                case "die":
                    irc.Disconnect();
                    break;
            }
        }

        public static void OnChannelMessage(Data ircdata)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += ("<" + ircdata.Nick + "> " + ircdata.Message + Environment.NewLine);
                Form1.GlobalAccess.ChatDisplayBox.SelectionStart = Form1.GlobalAccess.ChatDisplayBox.Text.Length;
                Form1.GlobalAccess.ChatDisplayBox.ScrollToCaret();
            }));
        }

        public static void Connected()
        {
            Form1.GlobalAccess.Invoke(new Action(() => { Form1.GlobalAccess.ChatDisplayBox.Text += "Connected" + Environment.NewLine; }));
        }

        public static void nickname()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Int32 hash = r.Next(0, 999);
            var nickname = "madcow" + hash.ToString();
            fixedNickname = nickname;
        }

        //This function sends the message to the irc channel, string message come from Form1.
        public static void SendMessage(string message)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += "<" + fixedNickname + "> " + message + Environment.NewLine;
                irc.WriteLine(Rfc2812.Privmsg(channel, message), Priority.Critical);
            }));
        }
    }
}
