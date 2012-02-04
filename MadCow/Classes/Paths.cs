
using System;
using System.IO;

namespace MadCow
{
    internal class Paths
    {
        internal static readonly string RepositoriesPath = Path.Combine(Environment.CurrentDirectory, "Repositories");

        internal static readonly string ToolsPath = Path.Combine(Environment.CurrentDirectory, "Tools");

        internal static readonly string ServerProfilesPath = Path.Combine(Environment.CurrentDirectory, "ServerProfiles");

        internal static readonly string RuntimeDownloadsPath = Path.Combine(Environment.CurrentDirectory, "RuntimeDownloads");

        internal static readonly string RepositoriesListPath = Path.Combine(ToolsPath, "RepoList.txt");

        internal static readonly string MooegeDownloadPath = Path.Combine(RepositoriesPath, "Mooege.zip");

        internal static readonly string MadcowIni = Path.Combine(ToolsPath, "madcow.ini");



        internal static readonly string CommitsPath = Path.Combine(RuntimeDownloadsPath, "Commits.atom");

        internal static string GetMooegeFolderPath(Repository repository)
        {
            return Path.Combine(RepositoriesPath, repository.Name, "Compiled");
        }

        internal static string GetMooegeExePath(Repository repository)
        {
            return Path.Combine(GetMooegeFolderPath(repository), "Mooege.exe");
        }

        internal static string GetMooegeIniPath(Repository repository)
        {
            return Path.Combine(GetMooegeFolderPath(repository), "config.ini");
        }
    }
}
