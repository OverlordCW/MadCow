using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Nini.Config;

namespace MadCow
{
    class Proxy
    {
        //We will use this variables each time we call our proxy.
        public static String currentProxyUrl = "";
        public static IPAddress currentProxyIp;
        public static String currentProxyPort = "";
        public static String username = "";
        public static String password = "";
        public static Boolean proxyEnabled;

        public static Boolean ProxyDetect()
        {
            // Create a new request to the mentioned URL.				
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            IConfigSource source = new IniConfigSource(Program.madcowINI);
            // We get the proxy from the explorer settings. 
            IWebProxy proxy = myWebRequest.Proxy;
            // If we find a proxy, we parse the ip & port for further use.
            if (proxy != null)
            {
                string url = proxy.GetProxy(myWebRequest.RequestUri).ToString();
                System.Uri uri = new System.Uri(url);
                //We parse out IP & Port
                int port = uri.Port;
                string host = uri.Host;
                //Set the Global Variables
                currentProxyPort = port.ToString();
                currentProxyIp = IPAddress.Parse(host);
                currentProxyUrl = proxy.GetProxy(myWebRequest.RequestUri).ToString();
                proxyEnabled = true;
                return true;
            }
            //IF not proxy, return false.
            else
            {               
                source.Configs["Proxy"].Set("Enabled", "0");
                source.Save();
                return false;
            }
        }
    }
}
