namespace DeviceInfoApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnFetchInfo;
        private System.Windows.Forms.Button btnSendProvisioningTask;
        private System.Windows.Forms.Button btnProvisionDevice;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtCommandOutput;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtCommandOutput = new System.Windows.Forms.TextBox();
            this.btnFetchInfo = new System.Windows.Forms.Button();
            this.btnSendProvisioningTask = new System.Windows.Forms.Button();
            this.btnProvisionDevice = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 🔹 Serial Number Input
            this.txtSerial.Location = new System.Drawing.Point(20, 20);
            this.txtSerial.Size = new System.Drawing.Size(280, 27);
            this.txtSerial.PlaceholderText = "Serial Number";

            // 🔹 IP Address Input
            this.txtIP.Location = new System.Drawing.Point(320, 20);
            this.txtIP.Size = new System.Drawing.Size(280, 27);
            this.txtIP.PlaceholderText = "IP Address";

            // 🔹 Fetch Info Button
            this.btnFetchInfo.Location = new System.Drawing.Point(620, 18);
            this.btnFetchInfo.Size = new System.Drawing.Size(230, 30);
            this.btnFetchInfo.Text = "Fetch Info";
            this.btnFetchInfo.Click += new System.EventHandler(this.btnFetchInfo_Click);

            // 🔹 Request Provisioning Button
            this.btnSendProvisioningTask.Location = new System.Drawing.Point(20, 65);
            this.btnSendProvisioningTask.Size = new System.Drawing.Size(410, 35);
            this.btnSendProvisioningTask.Text = "Register/Request Provisioning";
            this.btnSendProvisioningTask.Click += new System.EventHandler(this.btnSendProvisioningTask_Click);

            // 🔹 Provision Device Button
            this.btnProvisionDevice.Location = new System.Drawing.Point(460, 65);
            this.btnProvisionDevice.Size = new System.Drawing.Size(390, 35);
            this.btnProvisionDevice.Text = "Provision Device";
            this.btnProvisionDevice.Click += new System.EventHandler(this.btnFetchProvisioning_Click);

            // 🔹 Command Output
            this.txtCommandOutput.Location = new System.Drawing.Point(20, 115);
            this.txtCommandOutput.Size = new System.Drawing.Size(830, 370);
            this.txtCommandOutput.Multiline = true;
            this.txtCommandOutput.ScrollBars = ScrollBars.Vertical;
            this.txtCommandOutput.ReadOnly = true;
            this.txtCommandOutput.Font = new System.Drawing.Font("Consolas", 9); // optional: monospace for logs

            // 🔹 Add Controls
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnFetchInfo);
            this.Controls.Add(this.btnSendProvisioningTask);
            this.Controls.Add(this.btnProvisionDevice);
            this.Controls.Add(this.txtCommandOutput);

            // 🔹 Form Setup
            this.ClientSize = new System.Drawing.Size(880, 510);
            this.Name = "Form1";
            this.Text = "User Provisioning Tool";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }
}
