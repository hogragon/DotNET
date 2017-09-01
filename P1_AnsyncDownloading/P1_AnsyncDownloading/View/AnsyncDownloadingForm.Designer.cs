namespace P1_AnsyncDownloading
{
    partial class AnsyncDownloadingForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.downloadButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 126);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(533, 23);
            this.progressBar.TabIndex = 0;
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(388, 47);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(133, 40);
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.AccessibleName = "";
            this.urlTextBox.Location = new System.Drawing.Point(12, 47);
            this.urlTextBox.MaximumSize = new System.Drawing.Size(4, 40);
            this.urlTextBox.MinimumSize = new System.Drawing.Size(352, 40);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(352, 40);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.urlTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // AnsyncDownloadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 149);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.progressBar);
            this.Name = "AnsyncDownloadingForm";
            this.Text = "AnsyncDownloading";
            this.Load += new System.EventHandler(this.AnsyncDownloadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button downloadButton;
        public System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label1;
    }
}

