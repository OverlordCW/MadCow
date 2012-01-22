using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace MadCow
{
    class FindBranch
    {
        public static void findBrach(String Url)
        {
            var proxy = new WebProxy();
            if (Proxy.proxyStatus)
            {
                proxy.Address = new Uri(Proxy.proxyUrl);
                proxy.Credentials = new NetworkCredential(Proxy.username, Proxy.password);
            }

            Uri uri = new Uri(Url + "/branches");
            WebClient client = new WebClient();
            if (Proxy.proxyStatus)
                client.Proxy = proxy;
            client.DownloadFile(new Uri(Url + "/branches"), @"Branch.txt");
            searchBranch();
        }
        public static void searchBranch()
        {
            Form1.GlobalAccess.BranchComboBox.Items.Clear();
            Form1.GlobalAccess.BranchComboBox.Items.Add("master");
            using (FileStream fileStream = new FileStream(Program.programPath + @"\Branch.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader reader = new StreamReader(fileStream))
                {
                    string oldline = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != oldline)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(line, "/tree/"))
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(line, "/tree/"))
                                {
                                    String pattern = @"<A\shref=""(?<FilePath>[^""]*)"">(?<File>[^<]*)";
                                    var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var match = regex.Match(line);
                                    Form1.GlobalAccess.BranchComboBox.Items.Add(match.Groups["File"].Value);
                                }
                            }
                        }
                    }
                    reader.Close();
                }
                fileStream.Close();
            }
            Form1.GlobalAccess.BranchComboBox.SelectedIndex = Form1.GlobalAccess.BranchComboBox.FindStringExact("master");
        }

    }
}

