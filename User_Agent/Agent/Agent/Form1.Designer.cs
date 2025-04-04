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

        private Button btnDownloadProvisioningFolder;
        private Button btnFetchProvisioning;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
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

            this.SuspendLayout();

            // 🔹 Temporary Button to Fetch from SQS
            this.btnFetchProvisioning = new Button();
            this.btnFetchProvisioning.Location = new System.Drawing.Point(280, 100);
            this.btnFetchProvisioning.Size = new System.Drawing.Size(150, 30);
            this.btnFetchProvisioning.Text = "Fetch Provisioning";
            this.btnFetchProvisioning.Click += new System.EventHandler(this.btnFetchProvisioning_Click);
            this.Controls.Add(this.btnFetchProvisioning);

            // 🔹 Serial Number Label
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(20, 20);
            this.lblSerial.Text = "Serial Number:";

            // 🔹 Serial Number TextBox
            this.txtSerial.Location = new System.Drawing.Point(150, 20);
            this.txtSerial.Size = new System.Drawing.Size(250, 27);
            this.txtSerial.ReadOnly = true;

            // 🔹 IP Address Label
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(20, 60);
            this.lblIP.Text = "IP Address:";

            // 🔹 IP Address TextBox
            this.txtIP.Location = new System.Drawing.Point(150, 60);
            this.txtIP.Size = new System.Drawing.Size(250, 27);
            this.txtIP.ReadOnly = true;

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

            // 🔹 Provisioning Button (Disabled initially)
            this.btnProvisioning.Location = new System.Drawing.Point(20, 140);
            this.btnProvisioning.Size = new System.Drawing.Size(250, 30);
            this.btnProvisioning.Text = "Proceed to Provisioning";
            this.btnProvisioning.Enabled = false;
            this.btnProvisioning.Click += new System.EventHandler(this.btnProvisioning_Click);

            // 🔹 Command Output Box
            this.txtCommandOutput.Location = new System.Drawing.Point(20, 180);
            this.txtCommandOutput.Size = new System.Drawing.Size(380, 100);
            this.txtCommandOutput.Multiline = true;
            this.txtCommandOutput.ReadOnly = true;

            // 🔹 Add Controls to Form
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnFetchInfo);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnProvisioning);
            this.Controls.Add(this.txtCommandOutput);

            // 🔹 Form Settings
            this.ClientSize = new System.Drawing.Size(420, 320);
            this.Name = "Form1";
            this.Text = "Device Registration";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
