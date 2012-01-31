using System;
using System.IO;
using System.Linq;

namespace MadCow
{
    internal class Repository
    {
        internal Repository(string url, string branch = "master")
        {
            Url = url;
            Branch = branch;
        }

        internal string RepositoryPath { get; set; }

        internal string Url { get; private set; }

        internal string Branch { get; private set; }

        internal string Revision { get; set; }

        internal DateTime Date { get { return Directory.GetLastWriteTime(RepositoryPath); } }

        internal bool IsSelected { get; set; }

        internal void Delete()
        {
            if(File.Exists(Path.Combine("Tools", "RepoList.txt")))
            {
                //TODO
                var repos = File.ReadAllLines(Path.Combine("Tools", "RepoList.txt"));
                File.WriteAllLines(Path.Combine("Tools", "RepoList.txt"), repos);
            }

            if(!string.IsNullOrEmpty(RepositoryPath))
            {
                Directory.Delete(RepositoryPath, true);
            }
            Compile.Repositories.Remove(this);
        }
    }
}
