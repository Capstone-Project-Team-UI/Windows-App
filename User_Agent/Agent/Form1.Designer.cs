namespace DeviceInfoApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblSerial;
        private TextBox txtSerial;
        private Label lblIP;
        private TextBox txtIP;
        private Button btnFetchInfo;
        private Button btnRegister;
        private Button btnProvisioning;
        private TextBox txtCommandOutput;

        private Button btnSendProvisioningTask;
        private Button btnFetchProvisioning;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSerial = new Label();
            this.txtSerial = new TextBox();
            this.lblIP = new Label();
            this.txtIP = new TextBox();
            this.btnFetchInfo = new Button();
            this.btnRegister = new Button();
            this.btnProvisioning = new Button();
            this.txtCommandOutput = new TextBox();
            this.btnSendProvisioningTask = new Button();
            this.btnFetchProvisioning = new Button();

            this.SuspendLayout();

            // 🔹 Label for Serial Number
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(20, 20);
            this.lblSerial.Text = "Serial Number:";

            // 🔹 TextBox for Serial Number
            this.txtSerial.Location = new System.Drawing.Point(150, 20);
            this.txtSerial.Size = new System.Drawing.Size(250, 27);
            this.txtSerial.ReadOnly = true;
            this.txtSerial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // 🔹 Label for IP Address
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(20, 60);
            this.lblIP.Text = "IP Address:";

            // 🔹 TextBox for IP Address
            this.txtIP.Location = new System.Drawing.Point(150, 60);
            this.txtIP.Size = new System.Drawing.Size(250, 27);
            this.txtIP.ReadOnly = true;
            this.txtIP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // 🔹 Fetch Info Button
            this.btnFetchInfo.Location = new System.Drawing.Point(20, 100);
            this.btnFetchInfo.Size = new System.Drawing.Size(120, 30);
            this.btnFetchInfo.Text = "Fetch Info";
            this.btnFetchInfo.Click += new System.EventHandler(this.btnFetchInfo_Click);

            // 🔹 Register Button
            this.btnRegister.Location = new System.Drawing.Point(150, 100);
            this.btnRegister.Size = new System.Drawing.Size(120, 30);
            this.btnRegister.Text = "Register Device";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // 🔹 Send Provisioning Task Button
            this.btnSendProvisioningTask.Location = new System.Drawing.Point(280, 100);
            this.btnSendProvisioningTask.Size = new System.Drawing.Size(180, 30);
            this.btnSendProvisioningTask.Text = "Send Task to IT Agent";
            this.btnSendProvisioningTask.Click += new System.EventHandler(this.btnSendProvisioningTask_Click);

            // 🔹 Fetch Provisioning Button
            this.btnFetchProvisioning.Location = new System.Drawing.Point(20, 140);
            this.btnFetchProvisioning.Size = new System.Drawing.Size(250, 30);
            this.btnFetchProvisioning.Text = "Fetch Provisioning";
            this.btnFetchProvisioning.Click += new System.EventHandler(this.btnFetchProvisioning_Click);

            // 🔹 Proceed to Provisioning Button
            this.btnProvisioning.Location = new System.Drawing.Point(280, 140);
            this.btnProvisioning.Size = new System.Drawing.Size(180, 30);
            this.btnProvisioning.Text = "Proceed to Provisioning";
            this.btnProvisioning.Enabled = false;
            this.btnProvisioning.Click += new System.EventHandler(this.btnProvisioning_Click);

            // 🔹 TextBox for Output
            this.txtCommandOutput.Location = new System.Drawing.Point(20, 180);
            this.txtCommandOutput.Size = new System.Drawing.Size(440, 150);
            this.txtCommandOutput.Multiline = true;
            this.txtCommandOutput.ReadOnly = true;
            this.txtCommandOutput.ScrollBars = ScrollBars.Vertical;
            this.txtCommandOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 🔹 Add controls
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnFetchInfo);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnSendProvisioningTask);
            this.Controls.Add(this.btnFetchProvisioning);
            this.Controls.Add(this.btnProvisioning);
            this.Controls.Add(this.txtCommandOutput);

            // 🔹 Form Config
            this.ClientSize = new System.Drawing.Size(500, 360);
            this.Text = "Device Registration";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
