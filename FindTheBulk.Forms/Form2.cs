using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FindTheBulk.ClassLibrary;

namespace FindTheBulk.Forms
{
    public partial class Form2 : Form
    {
        private List<FileInfo> _files;
        private FileScanner _scanner;

        public Form2()
        {
            InitializeComponent();
            _files = new List<FileInfo>();

            _scanner = new FileScanner(Form1.ViewModel.RootDirectory, Form1.ViewModel.RecurseDirectories);
            _scanner.FileFoundEventHandler += ScannerFileFound;
            Task.Run(() => _scanner.StartAsync());
        }

        private void ScannerFileFound(object sender, FileFoundEventArgs e)
        {
            _files.Add(e.File);

            Invoke(new MethodInvoker(delegate
            {
                itemListBox.Items.Add($"{e.File.Name} ({e.File.GetFileSizeString()})");
            }));
        }

        private void itemListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var targetFile = _files[itemListBox.SelectedIndex];
            var result = MessageBox.Show(targetFile.PrintProperties(),
                "File Details - Open directory containing file?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
                Process.Start(targetFile.DirectoryName);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Dispose();
        }
    }
}
