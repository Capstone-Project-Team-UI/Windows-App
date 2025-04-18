namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox lstPending;
        private System.Windows.Forms.ListBox lstProvisioned;
        private System.Windows.Forms.Button btnFetchTasks;
        private System.Windows.Forms.Button btnCreateProvisioning;
        private System.Windows.Forms.Button btnDownloadTemplate;
        private System.Windows.Forms.Button btnDuplicatePackage;
        private System.Windows.Forms.Button btnClearProvisioned;
        private System.Windows.Forms.Button btnFetchAllProvisioned;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label lblProvisioned;
        private System.Windows.Forms.TextBox txtCommandOutput;
        private System.Windows.Forms.ToolTip toolTip1;

        private System.Windows.Forms.Button btnStartKVM;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstPending = new System.Windows.Forms.ListBox();
            this.lstProvisioned = new System.Windows.Forms.ListBox();
            this.btnFetchTasks = new System.Windows.Forms.Button();
            this.btnCreateProvisioning = new System.Windows.Forms.Button();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.btnDuplicatePackage = new System.Windows.Forms.Button();
            this.btnClearProvisioned = new System.Windows.Forms.Button();
            this.btnFetchAllProvisioned = new System.Windows.Forms.Button();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblProvisioned = new System.Windows.Forms.Label();
            this.txtCommandOutput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);



            this.SuspendLayout();

            // 🔹 Start KVM Button
            this.btnStartKVM = new System.Windows.Forms.Button();
            this.btnStartKVM.Location = new System.Drawing.Point(460, 210); // Adjust position as needed
            this.btnStartKVM.Size = new System.Drawing.Size(400, 35);
            this.btnStartKVM.Text = "Start KVM";
            this.btnStartKVM.Click += new System.EventHandler(this.btnStartKVM_Click);
            this.Controls.Add(this.btnStartKVM);

            // 🔹 Labels
            this.lblPending.AutoSize = true;
            this.lblPending.Location = new System.Drawing.Point(20, 15);
            this.lblPending.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblPending.Text = "Pending";

            this.lblProvisioned.AutoSize = true;
            this.lblProvisioned.Location = new System.Drawing.Point(460, 15);
            this.lblProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblProvisioned.Text = "Provisioned";

            // 🔹 Pending ListBox
            this.lstPending.FormattingEnabled = true;
            this.lstPending.ItemHeight = 20;
            this.lstPending.Location = new System.Drawing.Point(20, 40);
            this.lstPending.Size = new System.Drawing.Size(400, 160);
            this.lstPending.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lstPending.SelectedIndexChanged += new System.EventHandler(this.lstPending_SelectedIndexChanged);

            // 🔹 Provisioned ListBox
            this.lstProvisioned.FormattingEnabled = true;
            this.lstProvisioned.ItemHeight = 20;
            this.lstProvisioned.Location = new System.Drawing.Point(460, 40);
            this.lstProvisioned.Size = new System.Drawing.Size(400, 160);
            this.lstProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.lstProvisioned.SelectedIndexChanged += new System.EventHandler(this.lstProvisioned_SelectedIndexChanged);


            // 🔹 Clear Provisioned Button
            this.btnClearProvisioned.Location = new System.Drawing.Point(820, 10);
            this.btnClearProvisioned.Size = new System.Drawing.Size(30, 28);
            this.btnClearProvisioned.Text = "🧹";
            this.btnClearProvisioned.Click += new System.EventHandler(this.btnClearProvisioned_Click);
            this.btnClearProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.toolTip1.SetToolTip(this.btnClearProvisioned, "Clear visual provisioned list");

            // 🔹 Fetch All Provisioned Button
            this.btnFetchAllProvisioned.Location = new System.Drawing.Point(855, 10);
            this.btnFetchAllProvisioned.Size = new System.Drawing.Size(30, 28);
            this.btnFetchAllProvisioned.Text = "📂";
            this.btnFetchAllProvisioned.Click += new System.EventHandler(this.btnFetchAllProvisioned_Click);
            this.btnFetchAllProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.toolTip1.SetToolTip(this.btnFetchAllProvisioned, "Load saved provisioned list");

            // 🔹 Fetch Tasks Button
            this.btnFetchTasks.Location = new System.Drawing.Point(20, 210);
            this.btnFetchTasks.Size = new System.Drawing.Size(400, 35);
            this.btnFetchTasks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnFetchTasks.Text = "Fetch Tasks";
            this.btnFetchTasks.Click += new System.EventHandler(this.btnFetchTasks_Click);

            // 🔹 Create Provisioning Button
            this.btnCreateProvisioning.Location = new System.Drawing.Point(20, 260);
            this.btnCreateProvisioning.Size = new System.Drawing.Size(400, 35);
            this.btnCreateProvisioning.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnCreateProvisioning.Text = "Create Provisioning Zip";
            this.btnCreateProvisioning.Enabled = false;
            this.btnCreateProvisioning.Click += new System.EventHandler(this.btnCreateProvisioning_Click);

            // 🔹 Download Default Folder Button
            this.btnDownloadTemplate.Location = new System.Drawing.Point(20, 310);
            this.btnDownloadTemplate.Size = new System.Drawing.Size(400, 35);
            this.btnDownloadTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnDownloadTemplate.Text = "Get Default Folder from S3";
            this.btnDownloadTemplate.Click += new System.EventHandler(this.btnDownloadTemplate_Click);

            // 🔹 Duplicate Package Button
            this.btnDuplicatePackage.Location = new System.Drawing.Point(20, 360);
            this.btnDuplicatePackage.Size = new System.Drawing.Size(400, 35);
            this.btnDuplicatePackage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnDuplicatePackage.Text = "Duplicate Package Folder";
            this.btnDuplicatePackage.Click += new System.EventHandler(this.btnDuplicatePackage_Click);

            // 🔹 Command Output (Log Box)
            this.txtCommandOutput.Location = new System.Drawing.Point(20, 410);
            this.txtCommandOutput.Size = new System.Drawing.Size(865, 120);
            this.txtCommandOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtCommandOutput.Multiline = true;
            this.txtCommandOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandOutput.ReadOnly = true;
            this.txtCommandOutput.Font = new System.Drawing.Font("Consolas", 9);

            // 🔹 Add Controls
            this.Controls.Add(this.lblPending);
            this.Controls.Add(this.lblProvisioned);
            this.Controls.Add(this.lstPending);
            this.Controls.Add(this.lstProvisioned);
            this.Controls.Add(this.btnClearProvisioned);
            this.Controls.Add(this.btnFetchAllProvisioned);
            this.Controls.Add(this.btnFetchTasks);
            this.Controls.Add(this.btnCreateProvisioning);
            this.Controls.Add(this.btnDownloadTemplate);
            this.Controls.Add(this.btnDuplicatePackage);
            this.Controls.Add(this.txtCommandOutput);

            // 🔹 Form Setup
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "Form1";
            this.Text = "IT Agent Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
