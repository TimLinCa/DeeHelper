using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeeHelper
{
    public partial class AddBusinessTierPathForm : Form
    {
        public string ProjectName = string.Empty;
        public string BusinessTierFolderPath = string.Empty;
        public AddBusinessTierPathForm()
        {
            InitializeComponent();
        }

        private void SelectFolder_button_Click(object sender, EventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    FolderPathTB.Text = fbd.SelectedPath;
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if(ProjectNameTB.Text == string.Empty || FolderPathTB.Text == string.Empty)
            {
                MessageBox.Show("Project name or BusinessTier folder path cannot be empty");
            }
            else
            {
                ProjectName = ProjectNameTB.Text;
                BusinessTierFolderPath = FolderPathTB.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
