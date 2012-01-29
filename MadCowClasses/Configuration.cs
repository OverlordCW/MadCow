using System;
using System.IO;

using Nini.Config;

namespace MadCow
{
    public static class Configuration
    {
        #region Fields
        private static readonly IniConfigSource Source;
        private static readonly IConfig MadCowConfig;
        private static readonly IConfig MooegeConfig;
        #endregion

        static Configuration()
        {
            if (!File.Exists(Program.madcowINI))
            {
                File.Create(Program.madcowINI);
            }
            Source = new IniConfigSource(Program.madcowINI) { AutoSave = true };
            MadCowConfig = Source.Configs["MadCow"];
            MooegeConfig = Source.Configs["Mooege"];
        }

        #region Properties
        /// <summary>
        /// MadCow specific settings.
        /// </summary>
        public struct MadCow
        {
            public static bool TrayEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("Tray", "True")); }
                set { MadCowConfig.Set("Tray", value); }
            }

            public static bool TrayNotificationsEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("TrayNotifications", "True")); }
                set { MadCowConfig.Set("TrayNotifications", value); }
            }

            public static bool ShortcutEnabled
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("Shortcut", "True")); }
                set { MadCowConfig.Set("Shortcut", value); }
            }

            public static bool BackupAccountDatabase
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("BackupAccountDatabase", "True")); }
                set { MadCowConfig.Set("BackupAccountDatabase", value); }
            }

            public static bool RememberLastRepository
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("RememberLastRepository", "True")); }
                set { MadCowConfig.Set("RememberLastRepository", value); }
            }

            public static string LastRepository
            {
                get { return MadCowConfig.Get("LastRepository"); }
                set { MadCowConfig.Set("LastRepository", value); }
            }

            public static string DiabloPath
            {
                get { return MadCowConfig.Get("DiabloPath"); }
                set { MadCowConfig.Set("DiabloPath", value); }
            }

            public static string MpqDiablo
            {
                get { return MadCowConfig.Get("MpqDiablo"); }
                set { MadCowConfig.Set("MpqDiablo", value); }
            }

            public static string MpqServer
            {
                get { return MadCowConfig.Get("MpqServer", Path.Combine(Program.programPath, "MPQ")); }
                set { MadCowConfig.Set("MpqServer", value); }
            }

            public static string CurrentProfile
            {
                get { return MadCowConfig.Get("Profile", Path.Combine(Program.programPath, "ServerProfiles", "Default.mdc")); }
                set { MadCowConfig.Set("Profile", value); }
            }
        }

        /// <summary>
        /// Mooege specific settings.
        /// </summary>
        public struct Mooege
        {
            public static bool FileLogging
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("FileLogging", "True")); }
                set { MooegeConfig.Set("FileLogging", value); }
            }

            public static bool PacketLogging
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("PacketLogging", "False")); }
                set { MooegeConfig.Set("PacketLogging", value); }
            }

            public static bool Tasks
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("Tasks", "True")); }
                set { MooegeConfig.Set("Tasks", value); }
            }

            public static bool LazyLoading
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("LazyLoading", "True")); }
                set { MooegeConfig.Set("LazyLoading", value); }
            }

            public static bool PasswordCheck
            {
                get { return Convert.ToBoolean(MooegeConfig.Get("PasswordCheck", "True")); }
                set { MooegeConfig.Set("PasswordCheck", value); }
            }
        }
        #endregion

        #region Methods
        public static void Save()
        {
            Source.Save();
        }
        #endregion
    }
}
