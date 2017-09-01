using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using P1_AnsyncDownloading.Model;
using P1_AnsyncDownloading.Controller;

namespace P1_AnsyncDownloading
{
    public partial class AnsyncDownloadingForm : Form
    {
        public AnsyncDownloadingForm()
        {
            InitializeComponent();
        }

        private void AnsyncDownloadingForm_Load(object sender, EventArgs e)
        {

        }

        private void urlTextBox_Validating(object sender, CancelEventArgs e)
        {   
            
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {   
            TargetFile file = new TargetFile(this.urlTextBox.Text);
            if (!file.ValidURL)
            {
                MessageBox.Show("Invalid URL! Please correct your URL!");
            }
            else
            {
                DownloadingController.Download(file);
            }
            
        }
    }
}
