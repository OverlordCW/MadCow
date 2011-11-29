using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //Timing
        private int tik;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //No Coding Here
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Update Mooege - does not start Diablo
            Commands.RunUpdate(1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tik = (int)this.numericUpDown1.Value;
            timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Change Repository
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Delete Mooege Folder
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void label12_Click(object sender, EventArgs e)
        {
            //No Coding Necessary Here
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //creates folders needed, copies over MPQs
            MadCowRunProcedure.RunMadCow(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Starts Mooege
            //Run Diablo - Local Host
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Update MPQs if necessary
            Commands.RunUpdateMPQ(1);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //No Coding Necessary
        }

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
            MadCowRunProcedure.RunMadCow(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tik--;
            if (tik == 0)
            {
                label5.Text = "Checking..";
                Commands.RunUpdate(1);
                timer1.Stop();
            }
            else
                label5.Text = "Check in " + tik.ToString();

        }
    }
}
