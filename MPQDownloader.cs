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
    //This should get an Array from the CheckedListBox, create the links necessary to download one/multiple MPQs.
    //If possible, grab all the options that the User wants, send the array/download links back over to Form1,
    //this way we stick to one form, and subforms for options. (dealer's choice really). 

    public partial class MPQDownloader : Form
    {
        public MPQDownloader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //This gets all selected checkedlistbox items.
            string[] MPQarray = new string[checkedListBox1.SelectedItems.Count];

            for (int i = 0; i < checkedListBox1.SelectedItems.Count; i++)
            {

                object s = checkedListBox1.Items[i];
                MPQarray[i] = s.ToString();
            }
        }
    }
}
