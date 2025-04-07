namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstPending;
        private System.Windows.Forms.Button btnFetchTasks;
        private System.Windows.Forms.Button btnCreateProvisioning;

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

            this.SuspendLayout();

            // 🔹 ListBox
            this.lstPending.FormattingEnabled = true;
            this.lstPending.ItemHeight = 20;
            this.lstPending.Location = new System.Drawing.Point(20, 20);
            this.lstPending.Size = new System.Drawing.Size(400, 160);

            // 🔹 Fetch Tasks Button
            this.btnFetchTasks.Location = new System.Drawing.Point(20, 200);
            this.btnFetchTasks.Size = new System.Drawing.Size(150, 30);
            this.btnFetchTasks.Text = "Fetch Tasks";
            this.btnFetchTasks.Click += new System.EventHandler(this.btnFetchTasks_Click);

            // 🔹 Create Provisioning Button
            this.btnCreateProvisioning.Location = new System.Drawing.Point(200, 200);
            this.btnCreateProvisioning.Size = new System.Drawing.Size(220, 30);
            this.btnCreateProvisioning.Text = "Create Provisioning Zip";
            this.btnCreateProvisioning.Click += new System.EventHandler(this.btnCreateProvisioning_Click);

            // 🔹 Form Setup
            this.ClientSize = new System.Drawing.Size(450, 260);
            this.Controls.Add(this.lstPending);
            this.Controls.Add(this.btnFetchTasks);
            this.Controls.Add(this.btnCreateProvisioning);
            this.Text = "IT Agent Dashboard";
            this.ResumeLayout(false);
        }
    }
}
