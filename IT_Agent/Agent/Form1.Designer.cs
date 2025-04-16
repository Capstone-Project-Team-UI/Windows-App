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

        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label lblProvisioned;
        private System.Windows.Forms.ListBox lstProvisioned;

        private void InitializeComponent()
        {
            this.lstPending = new System.Windows.Forms.ListBox();
            this.lstProvisioned = new System.Windows.Forms.ListBox();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblProvisioned = new System.Windows.Forms.Label();

            this.btnFetchTasks = new System.Windows.Forms.Button();
            this.btnCreateProvisioning = new System.Windows.Forms.Button();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.btnDuplicatePackage = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 🔹 Fetch Tasks Button
            this.btnFetchTasks.Location = new System.Drawing.Point(20, 20);
            this.btnFetchTasks.Size = new System.Drawing.Size(180, 30);
            this.btnFetchTasks.Text = "Fetch Tasks";
            this.btnFetchTasks.Click += new System.EventHandler(this.btnFetchTasks_Click);

            // 🔹 Pending Label
            this.lblPending.Location = new System.Drawing.Point(20, 60);
            this.lblPending.Size = new System.Drawing.Size(180, 20);
            this.lblPending.Text = "Pending";

            // 🔹 Provisioned Label
            this.lblProvisioned.Location = new System.Drawing.Point(460, 60);
            this.lblProvisioned.Size = new System.Drawing.Size(180, 20);
            this.lblProvisioned.Text = "Provisioned";

            // 🔹 Pending List
            this.lstPending.FormattingEnabled = true;
            this.lstPending.ItemHeight = 20;
            this.lstPending.Location = new System.Drawing.Point(20, 85);
            this.lstPending.Size = new System.Drawing.Size(400, 160);
            this.lstPending.SelectedIndexChanged += new System.EventHandler(this.lstPending_SelectedIndexChanged);

            // 🔹 Provisioned List
            this.lstProvisioned.FormattingEnabled = true;
            this.lstProvisioned.ItemHeight = 20;
            this.lstProvisioned.Location = new System.Drawing.Point(460, 85);
            this.lstProvisioned.Size = new System.Drawing.Size(400, 160);

            // 🔹 Create Provisioning Button
            this.btnCreateProvisioning.Location = new System.Drawing.Point(20, 260);
            this.btnCreateProvisioning.Size = new System.Drawing.Size(400, 35);
            this.btnCreateProvisioning.Text = "Create Provisioning Zip";
            this.btnCreateProvisioning.Enabled = false;
            this.btnCreateProvisioning.Click += new System.EventHandler(this.btnCreateProvisioning_Click);

            // 🔹 Download Default Folder
            this.btnDownloadTemplate.Location = new System.Drawing.Point(20, 310);
            this.btnDownloadTemplate.Size = new System.Drawing.Size(400, 35);
            this.btnDownloadTemplate.Text = "Get Default Folder from S3";
            this.btnDownloadTemplate.Click += new System.EventHandler(this.btnDownloadTemplate_Click);

            // 🔹 Duplicate defPackage
            this.btnDuplicatePackage.Location = new System.Drawing.Point(20, 360);
            this.btnDuplicatePackage.Size = new System.Drawing.Size(400, 35);
            this.btnDuplicatePackage.Text = "Duplicate Package Folder";
            this.btnDuplicatePackage.Click += new System.EventHandler(this.btnDuplicatePackage_Click);

            // 🔹 Add Controls
            this.Controls.Add(this.lblPending);
            this.Controls.Add(this.lblProvisioned);
            this.Controls.Add(this.lstPending);
            this.Controls.Add(this.lstProvisioned);
            this.Controls.Add(this.btnFetchTasks);
            this.Controls.Add(this.btnCreateProvisioning);
            this.Controls.Add(this.btnDownloadTemplate);
            this.Controls.Add(this.btnDuplicatePackage);

            // 🔹 Form Setup
            this.ClientSize = new System.Drawing.Size(900, 420);
            this.Name = "Form1";
            this.Text = "IT Agent Dashboard";
            this.ResumeLayout(false);
        }


    }
}
