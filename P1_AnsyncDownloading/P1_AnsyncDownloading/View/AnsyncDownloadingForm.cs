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
using System.Threading;

namespace P1_AnsyncDownloading
{
    public partial class AnsyncDownloadingForm : Form
    {
        private int counter;
        private bool running;

        public AnsyncDownloadingForm()
        {
            InitializeComponent();
        }

        private void AnsyncDownloadingForm_Load(object sender, EventArgs e)
        {
            //Temporary data
            this.urlTextBox.Text = "https://drive.google.com/open?id=0B5fyPnNYo-UYTUdqV2toQlF3UUU";
        }

        private void urlTextBox_Validating(object sender, CancelEventArgs e)
        {   
            
        }

        //If we use await inside the method, the method signature must has async. Otherwise
        private async void downloadButton_Click(object sender, EventArgs e)
        {   
            TargetFile file = new TargetFile(this.urlTextBox.Text);
            if (!file.ValidURL)
            {
                MessageBox.Show("Invalid URL! Please correct your URL!");
            }
            else
            {
                //Start background thread to update progress bar
                //Note: can not access UI from other thread, will try another way.
                running = true;
                Thread updateCounterThread = new Thread(UpdateCounter);
                updateCounterThread.Start();
                
                //Start downloading content
                int contentLength = await DownloadingController.Instance.DownloadAsync(file);
                MessageBox.Show(String.Format("Content length {0}",contentLength));
                running = false;
            }
            
        }

        private void UpdateCounter()
        {
            while (running) {
                counter++;
                counter = Math.Max(counter, this.progressBar.Maximum);
                this.progressBar.Value = counter;
            }
        }
    }
}
