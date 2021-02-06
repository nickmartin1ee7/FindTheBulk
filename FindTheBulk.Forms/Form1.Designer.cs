
namespace FindTheBulk.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.searchButton = new System.Windows.Forms.Button();
            this.rootDirectoryLabel = new System.Windows.Forms.Label();
            this.directoryTextBox = new System.Windows.Forms.TextBox();
            this.recurseCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchButton.Location = new System.Drawing.Point(0, 54);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(554, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // rootDirectoryLabel
            // 
            this.rootDirectoryLabel.AutoSize = true;
            this.rootDirectoryLabel.Location = new System.Drawing.Point(12, 9);
            this.rootDirectoryLabel.Name = "rootDirectoryLabel";
            this.rootDirectoryLabel.Size = new System.Drawing.Size(75, 13);
            this.rootDirectoryLabel.TabIndex = 1;
            this.rootDirectoryLabel.Text = "Root Directory";
            // 
            // directoryTextBox
            // 
            this.directoryTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.directoryTextBox.Location = new System.Drawing.Point(0, 34);
            this.directoryTextBox.Name = "directoryTextBox";
            this.directoryTextBox.Size = new System.Drawing.Size(554, 20);
            this.directoryTextBox.TabIndex = 2;
            this.directoryTextBox.TextChanged += new System.EventHandler(this.directoryTextBox_TextChanged);
            // 
            // recurseCheckBox
            // 
            this.recurseCheckBox.AutoSize = true;
            this.recurseCheckBox.Checked = true;
            this.recurseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recurseCheckBox.Location = new System.Drawing.Point(476, 8);
            this.recurseCheckBox.Name = "recurseCheckBox";
            this.recurseCheckBox.Size = new System.Drawing.Size(66, 17);
            this.recurseCheckBox.TabIndex = 3;
            this.recurseCheckBox.Text = "Recurse";
            this.recurseCheckBox.UseVisualStyleBackColor = true;
            this.recurseCheckBox.CheckedChanged += new System.EventHandler(this.recurseCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 77);
            this.Controls.Add(this.recurseCheckBox);
            this.Controls.Add(this.directoryTextBox);
            this.Controls.Add(this.rootDirectoryLabel);
            this.Controls.Add(this.searchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FindTheBulk - Large file finder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label rootDirectoryLabel;
        private System.Windows.Forms.TextBox directoryTextBox;
        private System.Windows.Forms.CheckBox recurseCheckBox;
    }
}

