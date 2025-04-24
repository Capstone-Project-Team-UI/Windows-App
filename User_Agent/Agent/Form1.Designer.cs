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
        private GradientPanel mainPanel;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void SetButtonHoverEffects(Button btn, Color baseColor, Color hoverColor)
        {
            btn.BackColor = baseColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;

            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = baseColor;
        }

        private void InitializeComponent()
        {
            mainPanel = new GradientPanel();
            button1 = new Button();
            txtSerial = new TextBox();
            txtIP = new TextBox();
            btnFetchInfo = new Button();
            btnSendProvisioningTask = new Button();
            btnProvisionDevice = new Button();
            txtCommandOutput = new TextBox();
            pictureBox1 = new PictureBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.AutoScroll = true;
            mainPanel.BackColor = Color.MidnightBlue;
            mainPanel.BottomColor = Color.FromArgb(0, 0, 0);
            mainPanel.Controls.Add(pictureBox1);
            mainPanel.Controls.Add(button1);
            mainPanel.Controls.Add(txtSerial);
            mainPanel.Controls.Add(txtIP);
            mainPanel.Controls.Add(btnFetchInfo);
            mainPanel.Controls.Add(btnSendProvisioningTask);
            mainPanel.Controls.Add(btnProvisionDevice);
            mainPanel.Controls.Add(txtCommandOutput);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1246, 1026);
            mainPanel.TabIndex = 0;
            mainPanel.TopColor = Color.FromArgb(25, 25, 112);
            mainPanel.Paint += mainPanel_Paint;
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(1182, 29);
            button1.Name = "button1";
            button1.Size = new Size(21, 22);
            button1.TabIndex = 6;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // txtSerial
            // 
            txtSerial.Anchor = AnchorStyles.None;
            txtSerial.BackColor = SystemColors.ButtonFace;
            txtSerial.BorderStyle = BorderStyle.FixedSingle;
            txtSerial.ForeColor = Color.Black;
            txtSerial.Location = new Point(205, 211);
            txtSerial.Name = "txtSerial";
            txtSerial.PlaceholderText = "Serial Number";
            txtSerial.Size = new Size(280, 23);
            txtSerial.TabIndex = 0;
            // 
            // txtIP
            // 
            txtIP.Anchor = AnchorStyles.None;
            txtIP.BackColor = SystemColors.ButtonFace;
            txtIP.BorderStyle = BorderStyle.FixedSingle;
            txtIP.ForeColor = Color.Black;
            txtIP.Location = new Point(505, 211);
            txtIP.Name = "txtIP";
            txtIP.PlaceholderText = "IP Address";
            txtIP.Size = new Size(280, 23);
            txtIP.TabIndex = 1;
            // 
            // btnFetchInfo
            // 
            btnFetchInfo.Anchor = AnchorStyles.None;
            btnFetchInfo.FlatStyle = FlatStyle.Popup;
            btnFetchInfo.ForeColor = SystemColors.HighlightText;
            btnFetchInfo.Location = new Point(805, 209);
            btnFetchInfo.Name = "btnFetchInfo";
            btnFetchInfo.Size = new Size(230, 30);
            btnFetchInfo.TabIndex = 2;
            btnFetchInfo.Text = "Fetch Info";
            btnFetchInfo.UseVisualStyleBackColor = false;
            btnFetchInfo.Click += btnFetchInfo_Click;
            // 
            // btnSendProvisioningTask
            // 
            btnSendProvisioningTask.Anchor = AnchorStyles.None;
            btnSendProvisioningTask.FlatStyle = FlatStyle.Popup;
            btnSendProvisioningTask.ForeColor = SystemColors.HighlightText;
            btnSendProvisioningTask.Location = new Point(205, 256);
            btnSendProvisioningTask.Name = "btnSendProvisioningTask";
            btnSendProvisioningTask.Size = new Size(410, 35);
            btnSendProvisioningTask.TabIndex = 3;
            btnSendProvisioningTask.Text = "Register/Request Provisioning";
            btnSendProvisioningTask.UseVisualStyleBackColor = false;
            btnSendProvisioningTask.Click += btnSendProvisioningTask_Click;
            // 
            // btnProvisionDevice
            // 
            btnProvisionDevice.Anchor = AnchorStyles.None;
            btnProvisionDevice.FlatStyle = FlatStyle.Popup;
            btnProvisionDevice.ForeColor = SystemColors.HighlightText;
            btnProvisionDevice.Location = new Point(625, 256);
            btnProvisionDevice.Name = "btnProvisionDevice";
            btnProvisionDevice.Size = new Size(410, 35);
            btnProvisionDevice.TabIndex = 4;
            btnProvisionDevice.Text = "Provision Device";
            btnProvisionDevice.UseVisualStyleBackColor = false;
            btnProvisionDevice.Click += btnFetchProvisioning_Click;
            // 
            // txtCommandOutput
            // 
            txtCommandOutput.Anchor = AnchorStyles.None;
            txtCommandOutput.BackColor = Color.FromArgb(30, 20, 91);
            txtCommandOutput.BorderStyle = BorderStyle.None;
            txtCommandOutput.Font = new Font("Consolas", 9F);
            txtCommandOutput.ForeColor = Color.FromArgb(220, 220, 235);
            txtCommandOutput.Location = new Point(205, 306);
            txtCommandOutput.Multiline = true;
            txtCommandOutput.Name = "txtCommandOutput";
            txtCommandOutput.ReadOnly = true;
            txtCommandOutput.Size = new Size(830, 643);
            txtCommandOutput.TabIndex = 5;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = null;
            pictureBox1.Image = Agent.Properties.Resources._1200px_HP_logo_2012_svg;
            pictureBox1.Location = new Point(567, 49);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1243, 1023);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1243, 1023);
            Name = "Form1";
            Text = "User Provisioning Tool";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private Button button1;
        private PictureBox pictureBox1;
    }
}
