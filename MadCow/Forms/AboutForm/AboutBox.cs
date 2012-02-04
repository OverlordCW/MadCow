using System;
using System.Reflection;
using System.Windows.Forms;

namespace MadCow
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = String.Format("About {0}", Application.ProductName);
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            labelProductName.Text = Application.ProductName;
            labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            labelCopyright.Text = AssemblyCopyright;
        }

        private string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0
                    ? ""
                    : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
    }
}
