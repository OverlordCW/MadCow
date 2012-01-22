using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;

namespace MadCow
{
    //We use this to get the values when we need to use a proxy.
    class Proxy
    {
        public static string username
        {
            get 
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                return source.Configs["Proxy"].Get("Username");
            }
        }

        public static string password
        {
            get
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                return source.Configs["Proxy"].Get("Password");
            }
        }

        public static string proxyUrl
        {
            get
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                return source.Configs["Proxy"].Get("ProxyUrl");
            }
        }

        public static Boolean proxyStatus
        {
            get
            {
                IConfigSource source = new IniConfigSource(Program.madcowINI);
                var _status = source.Configs["Proxy"].Get("Enabled");
                if (_status == "1")
                    return true;
                else
                    return false;
            }
        }
    }
}
