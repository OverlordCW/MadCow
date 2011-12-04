using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nini.Config;

namespace MadCow
{
    class Announcer
    {
        public static void Say(string Name)
        {
            Form1 form1 = new Form1();
            form1.label2.Text = Name;
            form1.Show();

        }
    }
}
