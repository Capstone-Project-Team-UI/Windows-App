namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedListBox lstPending;
        private RoundedListBox lstProvisioned;
        private System.Windows.Forms.Button btnFetchTasks;
        private System.Windows.Forms.Button btnCreateProvisioning;
        private System.Windows.Forms.Button btnClearProvisioned;
        private System.Windows.Forms.Button btnFetchAllProvisioned;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label lblProvisioned;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel mainPanel;

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
            btnClearProvisioned = new Button();
            btnFetchAllProvisioned = new Button();
            lblPending = new Label();
            lblProvisioned = new Label();
            toolTip1 = new ToolTip(components);
            button2 = new Button();
            button1 = new Button();
            mainPanel = new Panel();
            txtCommandOutput = new TextBox();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lstPending
            // 
            lstPending.Anchor = AnchorStyles.None;
            lstPending.BackColor = Color.FromArgb(192, 255, 255);
            lstPending.BorderRadius = 20;
            lstPending.DrawMode = DrawMode.OwnerDrawFixed;
            lstPending.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lstPending.ItemHeight = 18;
            lstPending.Location = new Point(77, 73);
            lstPending.Name = "lstPending";
            lstPending.Size = new Size(400, 400);
            lstPending.TabIndex = 2;
            lstPending.SelectedIndexChanged += lstPending_SelectedIndexChanged;
            // 
            // lstProvisioned
            // 
            lstProvisioned.Anchor = AnchorStyles.None;
            lstProvisioned.BackColor = Color.FromArgb(192, 255, 255);
            lstProvisioned.BorderRadius = 20;
            lstProvisioned.DrawMode = DrawMode.OwnerDrawFixed;
            lstProvisioned.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lstProvisioned.HorizontalScrollbar = true;
            lstProvisioned.ItemHeight = 18;
            lstProvisioned.Location = new Point(517, 73);
            lstProvisioned.Name = "lstProvisioned";
            lstProvisioned.ScrollAlwaysVisible = true;
            lstProvisioned.Size = new Size(400, 598);
            lstProvisioned.TabIndex = 3;
            // 
            // btnFetchTasks
            // 
            btnFetchTasks.Anchor = AnchorStyles.None;
            btnFetchTasks.BackColor = Color.LightCyan;
            btnFetchTasks.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnFetchTasks.Location = new Point(77, 514);
            btnFetchTasks.Name = "btnFetchTasks";
            btnFetchTasks.Size = new Size(400, 35);
            btnFetchTasks.TabIndex = 4;
            btnFetchTasks.Text = "Fetch Tasks";
            btnFetchTasks.UseVisualStyleBackColor = false;
            btnFetchTasks.Click += btnFetchTasks_Click;
            // 
            // btnCreateProvisioning
            // 
            btnCreateProvisioning.Anchor = AnchorStyles.None;
            btnCreateProvisioning.BackColor = Color.LightCyan;
            btnCreateProvisioning.Enabled = false;
            btnCreateProvisioning.Font = new Font("SimSun", 9F, FontStyle.Bold);
            btnCreateProvisioning.Location = new Point(77, 568);
            btnCreateProvisioning.Name = "btnCreateProvisioning";
            btnCreateProvisioning.Size = new Size(400, 35);
            btnCreateProvisioning.TabIndex = 5;
            btnCreateProvisioning.Text = "Create Provisioning Zip";
            btnCreateProvisioning.UseVisualStyleBackColor = false;
            btnCreateProvisioning.Click += btnCreateProvisioning_Click;
            // 
            // 
            // btnClearProvisioned
            // 
            btnClearProvisioned.Anchor = AnchorStyles.None;
            btnClearProvisioned.BackColor = Color.LightCyan;
            btnClearProvisioned.Location = new Point(680, 683);
            btnClearProvisioned.Name = "btnClearProvisioned";
            btnClearProvisioned.Size = new Size(30, 28);
            btnClearProvisioned.TabIndex = 8;
            btnClearProvisioned.Text = "\U0001f9f9";
            toolTip1.SetToolTip(btnClearProvisioned, "Clear visual provisioned list");
            btnClearProvisioned.UseVisualStyleBackColor = false;
            btnClearProvisioned.Click += btnClearProvisioned_Click;
            // 
            // btnFetchAllProvisioned
            // 
            btnFetchAllProvisioned.Anchor = AnchorStyles.None;
            btnFetchAllProvisioned.BackColor = Color.LightCyan;
            btnFetchAllProvisioned.Location = new Point(725, 683);
            btnFetchAllProvisioned.Name = "btnFetchAllProvisioned";
            btnFetchAllProvisioned.Size = new Size(30, 28);
            btnFetchAllProvisioned.TabIndex = 9;
            btnFetchAllProvisioned.Text = "📂";
            toolTip1.SetToolTip(btnFetchAllProvisioned, "Load saved provisioned list");
            btnFetchAllProvisioned.UseVisualStyleBackColor = false;
            btnFetchAllProvisioned.Click += btnFetchAllProvisioned_Click;
            // 
            // lblPending
            // 
            lblPending.Anchor = AnchorStyles.None;
            lblPending.AutoSize = true;
            lblPending.BackColor = Color.DarkSeaGreen;
            lblPending.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lblPending.Location = new Point(239, 39);
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(54, 12);
            lblPending.TabIndex = 0;
            lblPending.Text = "Pending";
            // 
            // lblProvisioned
            // 
            lblProvisioned.Anchor = AnchorStyles.None;
            lblProvisioned.AutoSize = true;
            lblProvisioned.BackColor = Color.DarkSeaGreen;
            lblProvisioned.Font = new Font("SimSun", 9F, FontStyle.Bold);
            lblProvisioned.Location = new Point(680, 39);
            lblProvisioned.Name = "lblProvisioned";
            lblProvisioned.Size = new Size(82, 12);
            lblProvisioned.TabIndex = 1;
            lblProvisioned.Text = "Provisioned";

            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.BackColor = Color.Brown;
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(956, 19);
            button2.Name = "button2";
            button2.Size = new Size(30, 28);
            button2.TabIndex = 11;
            button2.Text = "X";
            toolTip1.SetToolTip(button2, "Load saved provisioned list");
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.FromArgb(20, 30, 40);
            mainPanel.Controls.Add(button2);
            mainPanel.Controls.Add(lblPending);
            mainPanel.Controls.Add(lblProvisioned);
            mainPanel.Controls.Add(lstPending);
            mainPanel.Controls.Add(lstProvisioned);
            mainPanel.Controls.Add(btnFetchTasks);
            mainPanel.Controls.Add(btnCreateProvisioning);
            mainPanel.Controls.Add(btnClearProvisioned);
            mainPanel.Controls.Add(btnFetchAllProvisioned);
            mainPanel.Controls.Add(txtCommandOutput);
            mainPanel.Location = new Point(0, -7);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(999, 908);
            mainPanel.TabIndex = 0;
            // 
            // txtCommandOutput
            // 
            txtCommandOutput.Anchor = AnchorStyles.None;
            txtCommandOutput.BackColor = Color.FromArgb(192, 255, 255);
            txtCommandOutput.Font = new Font("Consolas", 9F);
            txtCommandOutput.Location = new Point(77, 742);
            txtCommandOutput.Multiline = true;
            txtCommandOutput.Name = "txtCommandOutput";
            txtCommandOutput.ReadOnly = true;
            txtCommandOutput.Size = new Size(842, 117);
            txtCommandOutput.TabIndex = 10;
            txtCommandOutput.TextChanged += txtCommandOutput_TextChanged;
            // 
            // Form1
            // 
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(998, 890);
            Controls.Add(mainPanel);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "IT Agent Dashboard";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        private Button button2;
        private TextBox txtCommandOutput;
    }
}
