using System;
using System.IO;

using Nini.Config;

namespace MadCow
{
    internal static class Configuration
    {
        #region Fields
        private static readonly IniConfigSource Source;
        private static readonly IConfig MadCowConfig;
        private static readonly IConfig MooegeConfig;
        #endregion

        static Configuration()
        {
            if (!File.Exists(Paths.MadcowIni))
            {
                File.Create(Paths.MadcowIni);
            }
            Source = new IniConfigSource(Paths.MadcowIni) { AutoSave = true };
            MadCowConfig = Source.Configs["MadCow"];
            MooegeConfig = Source.Configs["Mooege"];

        }

        #region Properties
        /// <summary>
        /// MadCow specific settings.
        /// </summary>
        internal struct MadCow
        {
            internal static bool TrayEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("Tray", "True")); }
                set { MadCowConfig.Set("Tray", value); }
            }

            internal static bool TrayNotificationsEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("TrayNotifications", "True")); }
                set { MadCowConfig.Set("TrayNotifications", value); }
            }

            internal static bool ShortcutEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("Shortcut", "True")); }
                set { MadCowConfig.Set("Shortcut", value); }
            }

            internal static bool BackupAccountDatabase
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("BackupAccountDatabase", "True")); }
                set { MadCowConfig.Set("BackupAccountDatabase", value); }
            }

            internal static bool CompileAsDebug
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("CompileAsDebug", "False")); }
                set { MadCowConfig.Set("CompileAsDebug", value); }
            }

            internal static bool CheckMooegeUpdates
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("CheckMooegeUpdates", "True")); }
                set { MadCowConfig.Set("CheckMooegeUpdates", value); }
            }

            internal static string LastRepository
            {
                get { return MadCowConfig.Get("LastRepository"); }
                set { MadCowConfig.Set("LastRepository", value); }
            }

            internal static string DiabloPath
            {
                get { return MadCowConfig.Get("DiabloPath"); }
                set { MadCowConfig.Set("DiabloPath", value); }
            }

            internal static string MpqDiablo
            {
                get { return MadCowConfig.Get("MpqDiablo"); }
                set { MadCowConfig.Set("MpqDiablo", value); }
            }

            internal static string MpqServer
            {
                get { return MadCowConfig.Get("MpqServer", Path.Combine(Environment.CurrentDirectory, "MPQ")); }
                set { MadCowConfig.Set("MpqServer", value); }
            }

            internal static ServerProfile CurrentProfile
            {
                get { return new ServerProfile(MadCowConfig.Get("Profile", Path.Combine(Environment.CurrentDirectory, "ServerProfiles", "Default.mdc"))); }
                set { MadCowConfig.Set("Profile", value); }
            }

            internal static string IrcNickname
            {
                get { return MadCowConfig.Get("IrcNickname"); }
                set { MadCowConfig.Set("IrcNickname", value); }
            }
        }

        /// <summary>
        /// Mooege specific settings.
        /// </summary>
        internal struct Mooege
        {
            internal static bool FileLogging
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("FileLogging", "True")); }
                set { MooegeConfig.Set("FileLogging", value); }
            }

            internal static bool PacketLogging
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("PacketLogging", "False")); }
                set { MooegeConfig.Set("PacketLogging", value); }
            }

            internal static bool Tasks
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("Tasks", "True")); }
                set { MooegeConfig.Set("Tasks", value); }
            }

            internal static bool LazyLoading
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("LazyLoading", "True")); }
                set { MooegeConfig.Set("LazyLoading", value); }
            }

            internal static bool PasswordCheck
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("PasswordCheck", "True")); }
                set { MooegeConfig.Set("PasswordCheck", value); }
            }
        }
        #endregion

        #region Methods

        internal static void Save()
        {
            Source.Save();
        }

        internal static void UpdateMooegeIni(Repository repository)
        {
            var repoIniPath = new IniConfigSource(Paths.GetMooegeIniPath(repository));
            //For each selection we set the correct MPQ storage path & PacketLog|ServerLog settings on the config INI, this is the best way I could think to have the paths updated at everytime
            //We CANNOT call variable Compile.mooegeINI because that variable only saves latest compiled ini path for INSTANT writting after compiling a repository.
            //WE do not need to write different IPS / PORTS for this since its LOCAL function, We do that over RepositorySelectionSERVER.
            #region SetSettings
            repoIniPath.Configs["Storage"].Set("MPQRoot", MadCow.MpqServer);
            repoIniPath.Configs["ServerLog"].Set("Enabled", Mooege.FileLogging);
            repoIniPath.Configs["PacketLog"].Set("Enabled", Mooege.PacketLogging);
            repoIniPath.Configs["Storage"].Set("EnableTasks", Mooege.Tasks);
            repoIniPath.Configs["Storage"].Set("LazyLoading", Mooege.LazyLoading);
            repoIniPath.Configs["Authentication"].Set("DisablePasswordChecks", Mooege.PasswordCheck);
            //We set the server variables:
            //MooNet-Server IP
            repoIniPath.Configs["MooNet-Server"].Set("BindIP", MadCow.CurrentProfile.MooNetServerIp);
            //Game-Server IP
            repoIniPath.Configs["Game-Server"].Set("BindIP", MadCow.CurrentProfile.GameServerIp);
            //Public IP
            repoIniPath.Configs["NAT"].Set("PublicIP", MadCow.CurrentProfile.NatIp);
            //MooNet-Server Port
            repoIniPath.Configs["MooNet-Server"].Set("Port", MadCow.CurrentProfile.MooNetServerPort);
            //Game-Server Port
            repoIniPath.Configs["Game-Server"].Set("Port", MadCow.CurrentProfile.GameServerPort);
            //MOTD
            repoIniPath.Configs["MooNet-Server"].Set("MOTD", MadCow.CurrentProfile.MooNetServerMotd);
            //NAT
            repoIniPath.Configs["NAT"].Set("Enabled", MadCow.CurrentProfile.NatEnabled);
            repoIniPath.Save();
            #endregion
            Console.WriteLine("Current Profile: " + MadCow.CurrentProfile);
            Console.WriteLine("Set Mooege config.ini according to your profile " + MadCow.CurrentProfile);
            Console.WriteLine(repository + " is ready to go.");
        }

        #endregion
    }
}
