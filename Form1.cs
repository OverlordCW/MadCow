using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //Timing
        //private int tik;

        public Form1()
        {
            InitializeComponent();
        }

//-------------------------//
// Unused Items in Form //
//-------------------------//
        private void Form1_Load(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }

//-------------------------//
// Update Mooege //
//-------------------------//
        private void button1_Click(object sender, EventArgs e)
        {
            //Update Mooege - does not start Diablo
            Commands.RunUpdate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Need to have this load when MadCow loads, or wont work until you change the repo
            ParseRevision.revisionUrl = textBox1.Text;
            //ParseRevision.GetRevision();
            ParseRevision.GetRevision();
            ParseRevision.getDeveloperName();
            ParseRevision.getBranchName();
            //Testing purposes
            Console.WriteLine(ParseRevision.revisionUrl);
            Console.WriteLine(ParseRevision.developerName);
            Console.WriteLine("Branch name: " + ParseRevision.branchName);
            Console.WriteLine("Last Revision: " + ParseRevision.lastRevision);
            //Console.WriteLine(ParseRevision.branchName);
            //ParseRevision.revisionUrl = textBox1.Text;
            //Console.WriteLine(ParseRevision.revisionUrl);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Delete Mooege Folder
            SimpleFileDelete.Delete();
        }

//-------------------------//
// First Run Through Button //
//-------------------------//
        private void button5_Click(object sender, EventArgs e)
        {
            //creates folders needed, copies over MPQs
            //MadCowRunProcedure.RunMadCow(1);
        }

//-------------------------//
// Play Diablo //
//-------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            //Starts Mooege
            //Run Diablo - Local Host
        }

//-------------------------//
// Update MPQS //
//-------------------------//
        private void button2_Click(object sender, EventArgs e)
        {
            //Update MPQs if necessary
            //Commands.RunUpdateMPQ(1);
        }

//-------------------------//
// Remote Server Settings //
//-------------------------//
        private void button7_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Remote Server Host IP
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Remote Server Host Port
        }

//-------------------------//
// Server Control Settings //
//-------------------------//
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //Bnet Server IP
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //Bnet Server Port
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //Game Server IP
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //Game Server Port
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //Public Server IP
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //enable or disable NAT
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //restores default settings
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //only launch mooege (mostly for servers)
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Updates Mooege does not check for Diablo Client
            //MadCowRunProcedure.RunMadCow(0);
        }

//-------------------------//
// Timer Stuff //
//-------------------------//
/*
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked == true)
            {
                tik = (int)this.numericUpDown1.Value * 60;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                label5.Text = " ";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tik--;
            if (tik == 0)
            {
                label5.Text = "Checking..";
                Commands.RunUpdate();
                timer1.Stop();
            }
            else
                label5.Text = "Check in " + tik.ToString();
        }
*/
//-------------------------//
// Diablo 3 Path Stuff //
//-------------------------//
        private void button9_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            OpenFileDialog d3folder = new OpenFileDialog();
            d3folder.Title = "Diablo 3.exe";
            d3folder.InitialDirectory = @"C:\Program Files x86\Diablo III Beta\";
            if (d3folder.ShowDialog() == DialogResult.OK) // Test result.
            {
                // Get the directory name.
                string dirName = System.IO.Path.GetDirectoryName(d3folder.FileName);
                // Output Name
                textBox4.Text = d3folder.FileName;
            }
        }
            
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //Diablo Path
        }
    }
}
