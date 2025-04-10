namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstPending;
        private System.Windows.Forms.Button btnFetchTasks;
        private System.Windows.Forms.Button btnCreateProvisioning;
        private System.Windows.Forms.Button btnDownloadTemplate;
        private System.Windows.Forms.Button btnDuplicatePackage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstPending = new System.Windows.Forms.ListBox();
            this.btnFetchTasks = new System.Windows.Forms.Button();
            this.btnCreateProvisioning = new System.Windows.Forms.Button();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.btnDuplicatePackage = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 🔹 Fetch Tasks Button
            this.btnFetchTasks.Location = new System.Drawing.Point(20, 20);
            this.btnFetchTasks.Size = new System.Drawing.Size(180, 35);
            this.btnFetchTasks.Text = "Fetch Tasks";
            this.btnFetchTasks.Click += new System.EventHandler(this.btnFetchTasks_Click);

            // 🔹 Task ListBox
            this.lstPending.FormattingEnabled = true;
            this.lstPending.ItemHeight = 20;
            this.lstPending.Location = new System.Drawing.Point(20, 70);
            this.lstPending.Size = new System.Drawing.Size(400, 160);
            this.lstPending.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lstPending.SelectedIndexChanged += new System.EventHandler(this.lstPending_SelectedIndexChanged);

            // 🔹 Create Provisioning Button
            this.btnCreateProvisioning.Location = new System.Drawing.Point(20, 240);
            this.btnCreateProvisioning.Size = new System.Drawing.Size(400, 35);
            this.btnCreateProvisioning.Text = "Create Provisioning Zip";
            this.btnCreateProvisioning.Enabled = false; // will enable when user selected
            this.btnCreateProvisioning.Click += new System.EventHandler(this.btnCreateProvisioning_Click);

            // 🔹 Download Default Provisioning Folder
            this.btnDownloadTemplate.Location = new System.Drawing.Point(20, 290);
            this.btnDownloadTemplate.Size = new System.Drawing.Size(400, 35);
            this.btnDownloadTemplate.Text = "Get Default Folder from S3";
            this.btnDownloadTemplate.Click += new System.EventHandler(this.btnDownloadTemplate_Click);

            // 🔹 Duplicate defPackage Folder
            this.btnDuplicatePackage.Location = new System.Drawing.Point(20, 340);
            this.btnDuplicatePackage.Size = new System.Drawing.Size(400, 35);
            this.btnDuplicatePackage.Text = "Duplicate Package Folder";
            this.btnDuplicatePackage.Click += new System.EventHandler(this.btnDuplicatePackage_Click);

            // 🔹 Add Controls
            this.Controls.Add(this.btnFetchTasks);
            this.Controls.Add(this.lstPending);
            this.Controls.Add(this.btnCreateProvisioning);
            this.Controls.Add(this.btnDownloadTemplate);
            this.Controls.Add(this.btnDuplicatePackage);

            // 🔹 Form Setup
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Name = "Form1";
            this.Text = "IT Agent Dashboard";
            this.ResumeLayout(false);
        }
    }
}
