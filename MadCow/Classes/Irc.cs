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

using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace MadCow
{
    public class Client : IrcClient
    {
#if DEBUG
        private const string Channel = "#mooege.irctest";
#else
        private const string Channel = "#mooege.chat";
#endif
        private const string Server = "downtown.tx.us.synirc.net";
        //private const int Port = 6667;

        internal void Run()
        {
            SendDelay = 200;
            AutoRetry = true;
            ChannelSyncing = true;
            base.OnQueryMessage += OnQueryMessage;
            base.OnChannelMessage += OnChannelMessage;
            base.OnJoin += OnJoin;
            base.OnNameReply += OnNameReply;
            base.OnNickChange += OnNickChange;
            base.OnDisconnected += OnDisconnected;

            var task = Task<bool>.Factory.StartNew(() => Connect(Server, 6667));
            task.Wait();
            var result = task.Result;

            if (result)
            {
                try
                {
                    Login(Configuration.MadCow.IrcNickname, "MadCow Live Help Client");
                    Join(Channel);
                    Connected();
                    Listen();
                }
                catch (ArgumentOutOfRangeException)
                {
                    //ignore
                }
                catch (Exception ircex)
                {
                    Console.WriteLine("[ERROR] Urc.cs - Line 55\n" + ircex);
                }
            }
            else
            {
                Console.WriteLine("[ERROR] Irc Client could not connect.");
            }
        }

        public new void OnDisconnected()
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
                Form1.GlobalAccess.ircIntroLabel.Visible = true;
                Form1.GlobalAccess.ConnectButton.Visible = true;
                Form1.GlobalAccess.ircNicknameTextBox.Visible = true;
                Form1.GlobalAccess.ircNicknameLabel.Visible = true;
                Form1.GlobalAccess.ircRulesLabel.Visible = true;
                //Kill Thread.
                //Form1.ircThread.Abort();
            }));
        }

        public new void OnNickChange(string oldnickname, string newnickname, Data ircdata)
        {
            //Todo: Update User List on ChatUsersBox OnNickChange.
        }

        //Todo: Fix userlist loading.
        public new void OnNameReply(string channel, string[] userlist, Data ircdata)
        {
            Array.Sort(userlist);
            foreach (var user in userlist.Where(user => user.Length > 0))
            {
                Form1.GlobalAccess.Invoke(
                    new Action(() => Form1.GlobalAccess.ChatUsersBox.Text += user + Environment.NewLine));
            }
        }

        public new void OnJoin(string x, string y, Data ircdata)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += ("Joined:" + ircdata.Channel + Environment.NewLine);
                Form1.GlobalAccess.DisconnectButton.Visible = true;
                Form1.GlobalAccess.ChatMessageBox.Visible = true;
                Form1.GlobalAccess.TypeHereLabel.Visible = true;
                Form1.GlobalAccess.statusStripStatusLabel.Text = "Ready";
            }));
        }

        public new void OnQueryMessage(Data ircdata)
        {
            switch (ircdata.MessageEx[0])
            {
                case "die":
                    Disconnect();
                    break;
            }
        }

        public new void OnChannelMessage(Data ircdata)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += ("<" + ircdata.Nick + "> " + ircdata.Message + Environment.NewLine);
                Form1.GlobalAccess.ChatDisplayBox.SelectionStart = Form1.GlobalAccess.ChatDisplayBox.Text.Length;
                Form1.GlobalAccess.ChatDisplayBox.ScrollToCaret();
            }));
        }

        public new void Connected()
        {
            Form1.GlobalAccess.Invoke(new Action(() => { Form1.GlobalAccess.ChatDisplayBox.Text += "Connected" + Environment.NewLine; }));
        }

        //This function sends the message to the irc channel, string message come from Form1.
        public void SendMessage(string message)
        {
            Form1.GlobalAccess.Invoke(new Action(() =>
            {
                Form1.GlobalAccess.ChatDisplayBox.Text += "<" + Configuration.MadCow.IrcNickname + "> " + message + Environment.NewLine;
                WriteLine(Rfc2812.Privmsg(Channel, message), Priority.Critical);
            }));
        }
    }
}
