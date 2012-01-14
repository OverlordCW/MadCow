using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Nini.Config;
using System.Text.RegularExpressions;

namespace MadCow
{
    public partial class MPQDownloader : Form
    {
        public MPQDownloader()
        {
            InitializeComponent();
            AddAvailableFiles();
        }

        public void AddAvailableFiles() //Adds available repos to the list.
        {
            foreach (string element in TestMPQ.mpqList)
            {
                int pointer = element.LastIndexOf("-");
                //We get the MPQ version ID only e.g(8081).
                string name = element.Substring(pointer+1, 4);
                //Add the name to our MPQ Downloader ListBox.
                this.checkedListBox1.Items.Add(name);
            }
            this.checkedListBox1.Items.Add("CoreData");
            this.checkedListBox1.Items.Add("ClientData");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mpqSelection.Count > 0)
            {
                Console.WriteLine("Starting download...");
                Form1.GlobalAccess.Invoke(new Action(() =>
                {
                    Form1.GlobalAccess.backgroundWorker3.RunWorkerAsync();
                }));
                this.Close();
            }
            else
            {
                MessageBox.Show("No selected file was found.","MadCow Downloader",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public static List<String> mpqSelection = new List<String>(); //We use this to save selected items.
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex) == true) //Add
            {
                if (checkedListBox1.SelectedItem.ToString().Contains("CoreData") || checkedListBox1.SelectedItem.ToString().Contains("ClientData"))
                {
                    mpqSelection.Add("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + checkedListBox1.SelectedItem.ToString() + ".mpq");
                    //Console.WriteLine("File Added: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
                else //base MPQ files.
                {
                    mpqSelection.Add("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-" + checkedListBox1.SelectedItem.ToString() + ".MPQ");
                    //Console.WriteLine("File Added: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
            }
            else //Remove
            {
                if (checkedListBox1.SelectedItem.ToString().Contains("CoreData") || checkedListBox1.SelectedItem.ToString().Contains("ClientData"))
                {
                    mpqSelection.Remove("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + checkedListBox1.SelectedItem.ToString() + ".mpq");
                    //Console.WriteLine("File Removed: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
                else //base MPQ files.
                {
                    mpqSelection.Remove("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-" + checkedListBox1.SelectedItem.ToString() + ".MPQ");
                    //Console.WriteLine("File Removed: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
            }
        }
    }
}
