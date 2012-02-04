using System;
using System.Windows.Forms;

namespace MadCow
{
    public partial class EditRepository : Form
    {
        internal Repository SelectedRepository { get; private set; }

        public EditRepository()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedRepository = new Repository(new Uri(repositoryTextBox.Text), branchComboBox.SelectedItem.ToString());
            Close();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            var branches = FindBranch.FindBrach(repositoryTextBox.Text);
            if (branches != null)
            {
                branchComboBox.Items.AddRange(branches);
                branchComboBox.SelectedIndex = 0;
                okButton.Enabled = !string.IsNullOrEmpty(branchComboBox.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void branchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !string.IsNullOrEmpty(branchComboBox.SelectedItem.ToString());
        }
    }
}
