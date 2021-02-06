using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FindTheBulk.Forms
{
    public partial class Form1 : Form
    {
        public static Form1ViewModel ViewModel;

        public Form1()
        {
            InitializeComponent();
            ViewModel = new Form1ViewModel
            {
                RecurseDirectories = recurseCheckBox.Enabled,
            };
        }

        private void directoryTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateRootDirectory();
                directoryTextBox.ForeColor = ViewModel.RootDirectory.Exists ? Color.Black : Color.Red;
            }
            catch (Exception)
            {
                directoryTextBox.ForeColor = Color.Red;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!ViewModel.RootDirectory.Exists)
                throw new ArgumentException($"{ViewModel.RootDirectory.FullName} is not a valid directory!");

            var f2 = new Form2();
            f2.Show(this);
            Hide();
        }

        private void UpdateRootDirectory() => ViewModel.RootDirectory = new DirectoryInfo(directoryTextBox.Text);

        private void recurseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.RecurseDirectories = recurseCheckBox.Checked;
        }
    }
}
