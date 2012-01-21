using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Nini.Config;

namespace MadCow.ProxyForm
{
    public partial class ProxyForm : Form
    {
        public ProxyForm()
        {
            InitializeComponent();
        }

        private void ProxyForm_Load(object sender, EventArgs e)
        {
            this.IPTxtBox.Text = Proxy.currentProxyIp.ToString();
            this.PortTxtBox.Text = Proxy.currentProxyPort;
        }

        private void ProxyOkButton_Click(object sender, EventArgs e)
        {
            Proxy.currentProxyUrl = "http://" + this.IPTxtBox.Text + ":" + this.PortTxtBox.Text;
            Proxy.currentProxyIp =  IPAddress.Parse(this.IPTxtBox.Text);
            Proxy.currentProxyPort = this.PortTxtBox.Text;
            Proxy.username = this.UserNameTxtBox.Text;
            Proxy.password = this.PasswordTxtBox.Text;

            IConfigSource source = new IniConfigSource(Program.madcowINI);
            source.Configs["Proxy"].Set("Enabled", "1");
            source.Configs["Proxy"].Set("ProxyUrl", Proxy.currentProxyUrl);
            source.Configs["Proxy"].Set("Username", Proxy.username);
            source.Configs["Proxy"].Set("Password", Proxy.password);
            source.Save();
            this.Close();
        }
    }
}
