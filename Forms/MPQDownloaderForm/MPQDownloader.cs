using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            foreach (var name in from element in RetrieveMpqList.MpqList
                                 let pointer = element.LastIndexOf("-")
                                 select element.Substring(pointer + 1, 4))
            {
                //Add the name to our MPQ Downloader ListBox.
                checkedListBox1.Items.Add(name);
            }
            checkedListBox1.Items.Add("CoreData");
            checkedListBox1.Items.Add("ClientData");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MpqSelection.Count > 0)
            {
                Console.WriteLine("Starting download...");
                Form1.GlobalAccess.Invoke(new Action(() => Form1.GlobalAccess.DownloadSelectedMpqs.RunWorkerAsync()));
                Close();
            }
            else
            {
                MessageBox.Show("No selected file was found.", "MadCow Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static List<String> MpqSelection = new List<String>(); //We use this to save selected items.
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex)) //Add
            {
                if (checkedListBox1.SelectedItem.ToString().Contains("CoreData") || checkedListBox1.SelectedItem.ToString().Contains("ClientData"))
                {
                    MpqSelection.Add("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + checkedListBox1.SelectedItem + ".mpq");
                    //Console.WriteLine("File Added: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
                else //base MPQ files.
                {
                    MpqSelection.Add("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-" + checkedListBox1.SelectedItem + ".MPQ");
                    //Console.WriteLine("File Added: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
            }
            else //Remove
            {
                if (checkedListBox1.SelectedItem.ToString().Contains("CoreData") || checkedListBox1.SelectedItem.ToString().Contains("ClientData"))
                {
                    MpqSelection.Remove("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/" + checkedListBox1.SelectedItem + ".mpq");
                    //Console.WriteLine("File Removed: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
                else //base MPQ files.
                {
                    MpqSelection.Remove("http://ak.worldofwarcraft.com.edgesuite.net//d3-pod/20FB5BE9/NA/7162.direct/Data_D3/PC/MPQs/base/d3-update-base-" + checkedListBox1.SelectedItem + ".MPQ");
                    //Console.WriteLine("File Removed: {0}", checkedListBox1.SelectedItem.ToString());
                    //Console.WriteLine("Download Array Length: {0}", mpqSelection.Count);
                }
            }
        }
    }
}
