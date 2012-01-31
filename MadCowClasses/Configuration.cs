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

            internal static bool RememberLastRepository
            {
                get { return Convert.ToBoolean(MadCowConfig.Get("RememberLastRepository", "True")); }
                set { MadCowConfig.Set("RememberLastRepository", value); }
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
                get { return MadCowConfig.Get("MpqServer", Path.Combine(Program.programPath, "MPQ")); }
                set { MadCowConfig.Set("MpqServer", value); }
            }

            internal static ServerProfile CurrentProfile
            {
                get { return new ServerProfile(MadCowConfig.Get("Profile", Path.Combine(Program.programPath, "ServerProfiles", "Default.mdc"))); }
                set { MadCowConfig.Set("Profile", value); }
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
        public static void Save()
        {
            Source.Save();
        }
        #endregion
    }
}
