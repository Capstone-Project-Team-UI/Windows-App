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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lstPending = new ListBox();
            lstProvisioned = new ListBox();
            btnFetchTasks = new Button();
            btnCreateProvisioning = new Button();
            btnDownloadTemplate = new Button();
            btnDuplicatePackage = new Button();
            btnClearProvisioned = new Button();
            btnFetchAllProvisioned = new Button();
            lblPending = new Label();
            lblProvisioned = new Label();
            txtCommandOutput = new TextBox();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // lstPending
            // 
            lstPending.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstPending.FormattingEnabled = true;
            lstPending.ItemHeight = 25;
            lstPending.Location = new Point(20, 40);
            lstPending.Name = "lstPending";
            lstPending.Size = new Size(378, 79);
            lstPending.TabIndex = 2;
            lstPending.SelectedIndexChanged += lstPending_SelectedIndexChanged;
            // 
            // lstProvisioned
            // 
            lstProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lstProvisioned.FormattingEnabled = true;
            lstProvisioned.ItemHeight = 25;
            lstProvisioned.Location = new Point(438, 40);
            lstProvisioned.Name = "lstProvisioned";
            lstProvisioned.Size = new Size(400, 404);
            lstProvisioned.TabIndex = 3;
            // 
            // btnFetchTasks
            // 
            btnFetchTasks.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnFetchTasks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnFetchTasks.Location = new Point(20, 166);
            btnFetchTasks.Name = "btnFetchTasks";
            btnFetchTasks.Size = new Size(378, 35);
            btnFetchTasks.TabIndex = 6;
            btnFetchTasks.Text = "Fetch Tasks";
            btnFetchTasks.Click += btnFetchTasks_Click;
            // 
            // btnCreateProvisioning
            // 
            btnCreateProvisioning.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCreateProvisioning.Enabled = false;
            btnCreateProvisioning.Location = new Point(20, 216);
            btnCreateProvisioning.Name = "btnCreateProvisioning";
            btnCreateProvisioning.Size = new Size(378, 35);
            btnCreateProvisioning.TabIndex = 7;
            btnCreateProvisioning.Text = "Create Provisioning Zip";
            btnCreateProvisioning.Click += btnCreateProvisioning_Click;
            // 
            // btnDownloadTemplate
            // 
            btnDownloadTemplate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDownloadTemplate.Location = new Point(20, 266);
            btnDownloadTemplate.Name = "btnDownloadTemplate";
            btnDownloadTemplate.Size = new Size(378, 35);
            btnDownloadTemplate.TabIndex = 8;
            btnDownloadTemplate.Text = "Get Default Folder from S3";
            btnDownloadTemplate.Click += btnDownloadTemplate_Click;
            // 
            // btnDuplicatePackage
            // 
            btnDuplicatePackage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDuplicatePackage.Location = new Point(20, 316);
            btnDuplicatePackage.Name = "btnDuplicatePackage";
            btnDuplicatePackage.Size = new Size(378, 35);
            btnDuplicatePackage.TabIndex = 9;
            btnDuplicatePackage.Text = "Duplicate Package Folder";
            btnDuplicatePackage.Click += btnDuplicatePackage_Click;
            // 
            // btnClearProvisioned
            // 
            btnClearProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearProvisioned.Location = new Point(798, 10);
            btnClearProvisioned.Name = "btnClearProvisioned";
            btnClearProvisioned.Size = new Size(30, 28);
            btnClearProvisioned.TabIndex = 4;
            btnClearProvisioned.Text = "\U0001f9f9";
            toolTip1.SetToolTip(btnClearProvisioned, "Clear visual provisioned list");
            btnClearProvisioned.Click += btnClearProvisioned_Click;
            // 
            // btnFetchAllProvisioned
            // 
            btnFetchAllProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFetchAllProvisioned.Location = new Point(833, 10);
            btnFetchAllProvisioned.Name = "btnFetchAllProvisioned";
            btnFetchAllProvisioned.Size = new Size(30, 28);
            btnFetchAllProvisioned.TabIndex = 5;
            btnFetchAllProvisioned.Text = "📂";
            toolTip1.SetToolTip(btnFetchAllProvisioned, "Load saved provisioned list");
            btnFetchAllProvisioned.Click += btnFetchAllProvisioned_Click;
            // 
            // lblPending
            // 
            lblPending.AutoSize = true;
            lblPending.Location = new Point(20, 15);
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(76, 25);
            lblPending.TabIndex = 0;
            lblPending.Text = "Pending";
            // 
            // lblProvisioned
            // 
            lblProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProvisioned.AutoSize = true;
            lblProvisioned.Location = new Point(438, 15);
            lblProvisioned.Name = "lblProvisioned";
            lblProvisioned.Size = new Size(105, 25);
            lblProvisioned.TabIndex = 1;
            lblProvisioned.Text = "Provisioned";
            // 
            // txtCommandOutput
            // 
            txtCommandOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCommandOutput.Font = new Font("Consolas", 9F);
            txtCommandOutput.Location = new Point(20, 354);
            txtCommandOutput.Multiline = true;
            txtCommandOutput.Name = "txtCommandOutput";
            txtCommandOutput.ReadOnly = true;
            txtCommandOutput.ScrollBars = ScrollBars.Vertical;
            txtCommandOutput.Size = new Size(843, 120);
            txtCommandOutput.TabIndex = 10;
            // 
            // Form1
            // 
            ClientSize = new Size(878, 504);
            Controls.Add(lblPending);
            Controls.Add(lblProvisioned);
            Controls.Add(lstPending);
            Controls.Add(lstProvisioned);
            Controls.Add(btnClearProvisioned);
            Controls.Add(btnFetchAllProvisioned);
            Controls.Add(btnFetchTasks);
            Controls.Add(btnCreateProvisioning);
            Controls.Add(btnDownloadTemplate);
            Controls.Add(btnDuplicatePackage);
            Controls.Add(txtCommandOutput);
            MinimumSize = new Size(900, 560);
            Name = "Form1";
            Text = "IT Agent Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
