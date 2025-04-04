namespace WindowsProvisioningApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnListFiles;
        private Button btnDownload;
        private Label lblStatus;
        private Label lblSelectedFile;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // List Files Button
            this.btnListFiles = new Button();
            this.btnListFiles.Text = "List S3 Files";
            this.btnListFiles.Location = new System.Drawing.Point(50, 50);
            this.btnListFiles.Click += new System.EventHandler(this.btnListFiles_Click);
            this.Controls.Add(this.btnListFiles);

            // Selected File Label
            this.lblSelectedFile = new Label();
            this.lblSelectedFile.Text = "Selected: None";
            this.lblSelectedFile.Location = new System.Drawing.Point(50, 100);
            this.Controls.Add(this.lblSelectedFile);

            // Download Button
            this.btnDownload = new Button();
            this.btnDownload.Text = "Download Selected File";
            this.btnDownload.Location = new System.Drawing.Point(50, 150);
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            this.Controls.Add(this.btnDownload);

            // Status Label
            this.lblStatus = new Label();
            this.lblStatus.Text = "Status: Waiting...";
            this.lblStatus.Location = new System.Drawing.Point(50, 200);
            this.Controls.Add(this.lblStatus);

            // Form Settings
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "S3 File Downloader";
            this.ResumeLayout(false);
        }
    }
}
