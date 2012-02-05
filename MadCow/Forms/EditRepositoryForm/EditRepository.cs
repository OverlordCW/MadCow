using System;
using System.Windows.Forms;

namespace MadCow
{
    internal partial class EditRepository : Form
    {
        private readonly Repository _suppliedRepository;

        internal EditRepository(Repository repository = null)
        {
            _suppliedRepository = repository;
            InitializeComponent();
            ApplyRepository();
        }

        internal Repository SelectedRepository { get; private set; }

        private void ApplyRepository()
        {
            if(_suppliedRepository != null)
            {
                repositoryTextBox.Text = _suppliedRepository.Url.AbsoluteUri;
                var branches = FindBranch.FindBrach(_suppliedRepository.Url.AbsoluteUri);
                if(branches != null)
                {
                    branchComboBox.Items.AddRange(branches);
                    branchComboBox.SelectedIndex = 0;
                    okButton.Enabled = !string.IsNullOrEmpty(branchComboBox.SelectedItem.ToString());
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedRepository = new Repository(new Uri(repositoryTextBox.Text), branchComboBox.SelectedItem.ToString());
            Close();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            branchComboBox.Items.Clear();
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
