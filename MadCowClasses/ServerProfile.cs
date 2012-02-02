
using System;
using System.IO;

using Nini.Config;

namespace MadCow
{
    internal class ServerProfile
    {
        #region Fields
        private readonly IniConfigSource _source;
        private readonly string _name; 
        #endregion

        internal ServerProfile(string name = "default")
        {
            if (!name.EndsWith(".mdc")) name += ".mdc";
            _name = name;

            var path = Path.Combine("ServerProfiles", name);
            if(!File.Exists(path))
            {
                File.WriteAllLines(path, new[]
                                             {
                                                 "[MooNet-Server]",
                                                 "[Game-Server]",
                                                 "[NAT]"
                                             });
            }

            _source = new IniConfigSource(path);
        }

        #region Properties
        internal string MooNetServerIp
        {
            get { return _source.Configs["MooNet-Server"].Get("BindIP", "0.0.0.0"); }
            set { _source.Configs["MooNet-Server"].Set("BindIP", value); }
        }

        internal string MooNetServerPort
        {
            get { return _source.Configs["MooNet-Server"].Get("Port", "1345"); }
            set { _source.Configs["MooNet-Server"].Set("Port", value); }
        }

        internal string MooNetServerMotd
        {
            get { return _source.Configs["MooNet-Server"].Get("MOTD", "Welcome to mooege development server!"); }
            set { _source.Configs["MooNet-Server"].Set("MOTD", value); }
        }

        internal string GameServerIp
        {
            get { return _source.Configs["Game-Server"].Get("BindIP", "0.0.0.0"); }
            set { _source.Configs["Game-Server"].Set("BindIP", value); }
        }

        internal string GameServerPort
        {
            get { return _source.Configs["Game-Server"].Get("Port", "1999"); }
            set { _source.Configs["Game-Server"].Set("Port", value); }
        }

        internal string NatIp
        {
            get { return _source.Configs["NAT"].Get("PublicIP", "0.0.0.0"); }
            set { _source.Configs["NAT"].Set("PublicIP", value); }
        }

        internal bool NatEnabled
        {
            get { return Convert.ToBoolean(_source.Configs["NAT"].Get("Enabled", "False")); }
            set { _source.Configs["NAT"].Set("Enabled", value); }
        } 
        #endregion

        internal void Save()
        {
            _source.Save();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(_name);
        }
    }
}
