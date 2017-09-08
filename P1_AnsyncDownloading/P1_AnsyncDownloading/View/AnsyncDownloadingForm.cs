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

        //Use for update UI elements on the form
        private delegate void UpdateProgressBar();
        private event UpdateProgressBar updateProgressBar;

        public AnsyncDownloadingForm()
        {
            InitializeComponent();
        }        

        private void OnAnsyncDownloadingFormLoad(object sender, EventArgs e)
        {
            //Temporary data
            this.urlTextBox.Text = "https://drive.google.com/open?id=0B5fyPnNYo-UYTUdqV2toQlF3UUU";
        }


        private void UpdateProgressBarValue()
        {
            progressBar.Value = Math.Min(Math.Max(counter, 0), progressBar.Maximum);
        }

        private void IncreaseCounter()
        {
            bool running = true;

            while (running) {
                counter+=1;
            }
            
        }

        private void DecreaseCounter()
        {
            bool running = true;

            while (running)
            {
                counter-=1;
            }

        }

        private void UpdateUI()
        {
            bool running = true;

            while (running)
            {
                //Note: can not set value for the progress bar from other thread directly
                //We use Invoke funtion to do that
                this.progressBar.Invoke(updateProgressBar);
            }

        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void OnButtonRunThreadClick(object sender, EventArgs e)
        {
            updateProgressBar = UpdateProgressBarValue;

            //Start background thread to update progress bar
            Thread increaseCounterThread = new Thread(IncreaseCounter);
            //auto close thread when application exit
            increaseCounterThread.IsBackground = true;
            increaseCounterThread.Start();

            Thread decreaseCounterThread = new Thread(DecreaseCounter);
            decreaseCounterThread.IsBackground = true;
            decreaseCounterThread.Start();

            //When we call join, the function OnButtonRunThreadClick will be stop and wait for 
            //the decreaseCounterThread complete its task, if it has an infinity loop the application will be frozen
            //decreaseCounterThread.Join();
            


            //If we let 2 thread invoke the progress bar, it will lock the form
            //So I create another thread only for update UI, the problem fades away
            Thread updateUIThread = new Thread(UpdateUI);
            updateUIThread.IsBackground = true;
            updateUIThread.Start();            
        }

        //If we use await inside the method, the method signature must has async. Otherwise
        private async void OnDownloadButtonClick(object sender, EventArgs e)
        {
            TargetFile file = new TargetFile(this.urlTextBox.Text);
            if (!file.ValidURL)
            {
                MessageBox.Show("Invalid URL! Please correct your URL!");
            }
            else
            {
                //Start downloading content
                int contentLength = await DownloadingController.Instance.DownloadAsync(file);
                MessageBox.Show(String.Format("Content length {0}", contentLength));
            }
        }
    }
}
