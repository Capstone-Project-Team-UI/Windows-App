namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedListBox lstPending;
        private RoundedListBox lstProvisioned;
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
            lstPending = new RoundedListBox();
            lstProvisioned = new RoundedListBox();
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
            button1 = new Button();
            SuspendLayout();
            // 
            // lstPending
            // 
            lstPending.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstPending.BackColor = Color.FromArgb(192, 255, 255);
            lstPending.BorderRadius = 20;
            lstPending.DrawMode = DrawMode.OwnerDrawFixed;
            lstPending.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lstPending.FormattingEnabled = true;
            lstPending.ItemHeight = 18;
            lstPending.Location = new Point(395, 70);
            lstPending.Margin = new Padding(0);
            lstPending.MaximumSize = new Size(400, 400);
            lstPending.MinimumSize = new Size(400, 76);
            lstPending.Name = "lstPending";
            lstPending.Size = new Size(400, 400);
            lstPending.TabIndex = 2;
            lstPending.SelectedIndexChanged += lstPending_SelectedIndexChanged;
            // 
            // lstProvisioned
            // 
            lstProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstProvisioned.BackColor = Color.FromArgb(192, 255, 255);
            lstProvisioned.BorderRadius = 20;
            lstProvisioned.DrawMode = DrawMode.OwnerDrawFixed;
            lstProvisioned.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lstProvisioned.FormattingEnabled = true;
            lstProvisioned.ItemHeight = 18;
            lstProvisioned.Location = new Point(837, 70);
            lstProvisioned.Margin = new Padding(0);
            lstProvisioned.MaximumSize = new Size(400, 598);
            lstProvisioned.MinimumSize = new Size(400, 598);
            lstProvisioned.Name = "lstProvisioned";
            lstProvisioned.Size = new Size(400, 598);
            lstProvisioned.TabIndex = 3;
            // 
            // btnFetchTasks
            // 
            btnFetchTasks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnFetchTasks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnFetchTasks.BackColor = Color.LightCyan;
            btnFetchTasks.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnFetchTasks.Location = new Point(395, 511);
            btnFetchTasks.MaximumSize = new Size(400, 35);
            btnFetchTasks.MinimumSize = new Size(400, 35);
            btnFetchTasks.Name = "btnFetchTasks";
            btnFetchTasks.Size = new Size(400, 35);
            btnFetchTasks.TabIndex = 6;
            btnFetchTasks.Text = "Fetch Tasks";
            btnFetchTasks.UseVisualStyleBackColor = false;
            btnFetchTasks.Click += btnFetchTasks_Click;
            // 
            // btnCreateProvisioning
            // 
            btnCreateProvisioning.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCreateProvisioning.BackColor = Color.LightCyan;
            btnCreateProvisioning.Enabled = false;
            btnCreateProvisioning.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnCreateProvisioning.Location = new Point(395, 565);
            btnCreateProvisioning.MaximumSize = new Size(400, 35);
            btnCreateProvisioning.MinimumSize = new Size(400, 35);
            btnCreateProvisioning.Name = "btnCreateProvisioning";
            btnCreateProvisioning.Size = new Size(400, 35);
            btnCreateProvisioning.TabIndex = 7;
            btnCreateProvisioning.Text = "Create Provisioning Zip";
            btnCreateProvisioning.UseVisualStyleBackColor = false;
            btnCreateProvisioning.Click += btnCreateProvisioning_Click;
            // 
            // btnDownloadTemplate
            // 
            btnDownloadTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDownloadTemplate.BackColor = Color.LightCyan;
            btnDownloadTemplate.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnDownloadTemplate.Location = new Point(395, 619);
            btnDownloadTemplate.MaximumSize = new Size(400, 35);
            btnDownloadTemplate.MinimumSize = new Size(400, 35);
            btnDownloadTemplate.Name = "btnDownloadTemplate";
            btnDownloadTemplate.Size = new Size(400, 35);
            btnDownloadTemplate.TabIndex = 8;
            btnDownloadTemplate.Text = "Select Provisioning Folder";
            btnDownloadTemplate.UseVisualStyleBackColor = false;
            btnDownloadTemplate.Click += btnDownloadTemplate_Click;
            // 
            // btnDuplicatePackage
            // 
            btnDuplicatePackage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDuplicatePackage.BackColor = Color.LightCyan;
            btnDuplicatePackage.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnDuplicatePackage.Location = new Point(395, 676);
            btnDuplicatePackage.MaximumSize = new Size(400, 35);
            btnDuplicatePackage.MinimumSize = new Size(400, 35);
            btnDuplicatePackage.Name = "btnDuplicatePackage";
            btnDuplicatePackage.Size = new Size(400, 35);
            btnDuplicatePackage.TabIndex = 9;
            btnDuplicatePackage.Text = "Duplicate Package Folder";
            btnDuplicatePackage.UseVisualStyleBackColor = false;
            btnDuplicatePackage.Click += btnDuplicatePackage_Click;
            // 
            // btnClearProvisioned
            // 
            btnClearProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClearProvisioned.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnClearProvisioned.BackColor = Color.LightCyan;
            btnClearProvisioned.Location = new Point(1004, 677);
            btnClearProvisioned.MaximumSize = new Size(30, 28);
            btnClearProvisioned.MinimumSize = new Size(30, 28);
            btnClearProvisioned.Name = "btnClearProvisioned";
            btnClearProvisioned.Size = new Size(30, 28);
            btnClearProvisioned.TabIndex = 4;
            btnClearProvisioned.Text = "\U0001f9f9";
            toolTip1.SetToolTip(btnClearProvisioned, "Clear visual provisioned list");
            btnClearProvisioned.UseVisualStyleBackColor = false;
            btnClearProvisioned.Click += btnClearProvisioned_Click;
            // 
            // btnFetchAllProvisioned
            // 
            btnFetchAllProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnFetchAllProvisioned.BackColor = Color.LightCyan;
            btnFetchAllProvisioned.Location = new Point(1049, 677);
            btnFetchAllProvisioned.MaximumSize = new Size(30, 28);
            btnFetchAllProvisioned.MinimumSize = new Size(30, 28);
            btnFetchAllProvisioned.Name = "btnFetchAllProvisioned";
            btnFetchAllProvisioned.Size = new Size(30, 28);
            btnFetchAllProvisioned.TabIndex = 5;
            btnFetchAllProvisioned.Text = "📂";
            toolTip1.SetToolTip(btnFetchAllProvisioned, "Load saved provisioned list");
            btnFetchAllProvisioned.UseVisualStyleBackColor = false;
            btnFetchAllProvisioned.Click += btnFetchAllProvisioned_Click;
            // 
            // lblPending
            // 
            lblPending.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPending.AutoSize = true;
            lblPending.BackColor = Color.DarkSeaGreen;
            lblPending.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lblPending.Location = new Point(547, 35);
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(78, 18);
            lblPending.TabIndex = 0;
            lblPending.Text = "Pending";
            // 
            // lblProvisioned
            // 
            lblProvisioned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblProvisioned.AutoSize = true;
            lblProvisioned.BackColor = Color.DarkSeaGreen;
            lblProvisioned.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lblProvisioned.Location = new Point(947, 35);
            lblProvisioned.MaximumSize = new Size(118, 18);
            lblProvisioned.MinimumSize = new Size(118, 18);
            lblProvisioned.Name = "lblProvisioned";
            lblProvisioned.Size = new Size(118, 18);
            lblProvisioned.TabIndex = 1;
            lblProvisioned.Text = "Provisioned";
            // 
            // txtCommandOutput
            // 
            txtCommandOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCommandOutput.BackColor = Color.FromArgb(192, 255, 255);
            txtCommandOutput.Font = new Font("Consolas", 9F);
            txtCommandOutput.Location = new Point(395, 739);
            txtCommandOutput.Margin = new Padding(0);
            txtCommandOutput.MaximumSize = new Size(842, 117);
            txtCommandOutput.MinimumSize = new Size(842, 117);
            txtCommandOutput.Multiline = true;
            txtCommandOutput.Name = "txtCommandOutput";
            txtCommandOutput.ReadOnly = true;
            txtCommandOutput.Size = new Size(842, 117);
            txtCommandOutput.TabIndex = 10;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Maroon;
            button1.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(1588, 6);
            button1.Margin = new Padding(0);
            button1.MaximumSize = new Size(25, 25);
            button1.MinimumSize = new Size(25, 25);
            button1.Name = "button1";
            button1.Size = new Size(25, 25);
            button1.TabIndex = 11;
            button1.Text = "x";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(1633, 890);
            Controls.Add(button1);
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
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1633, 890);
            Name = "Form1";
            Text = "IT Agent Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }
        private Button button1;
    }
}
