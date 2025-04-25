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
            pictureBox1 = new PictureBox();
            button1 = new Button();
            txtSerial = new TextBox();
            txtIP = new TextBox();
            btnFetchInfo = new Button();
            btnSendProvisioningTask = new Button();
            btnProvisionDevice = new Button();
            txtCommandOutput = new TextBox();
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
            mainPanel.Margin = new Padding(4);
            mainPanel.MinimumSize = new Size(1850, 1100);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1850, 1164);
            mainPanel.TabIndex = 0;
            mainPanel.TopColor = Color.FromArgb(25, 25, 112);
            mainPanel.Paint += mainPanel_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = null;
            pictureBox1.Image = Agent.Properties.Resources._1200px_HP_logo_2012_svg;
            pictureBox1.Location = new Point(880, 71);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 150);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(1767, 30);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(32, 33);
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
            txtSerial.Location = new Point(312, 281);
            txtSerial.Margin = new Padding(4);
            txtSerial.Name = "txtSerial";
            txtSerial.PlaceholderText = "Serial Number";
            txtSerial.Size = new Size(419, 31);
            txtSerial.TabIndex = 0;
            // 
            // txtIP
            // 
            txtIP.Anchor = AnchorStyles.None;
            txtIP.BackColor = SystemColors.ButtonFace;
            txtIP.BorderStyle = BorderStyle.FixedSingle;
            txtIP.ForeColor = Color.Black;
            txtIP.Location = new Point(762, 281);
            txtIP.Margin = new Padding(4);
            txtIP.Name = "txtIP";
            txtIP.PlaceholderText = "IP Address";
            txtIP.Size = new Size(419, 31);
            txtIP.TabIndex = 1;
            // 
            // btnFetchInfo
            // 
            btnFetchInfo.Anchor = AnchorStyles.None;
            btnFetchInfo.FlatStyle = FlatStyle.Popup;
            btnFetchInfo.ForeColor = SystemColors.HighlightText;
            btnFetchInfo.Location = new Point(1212, 279);
            btnFetchInfo.Margin = new Padding(4);
            btnFetchInfo.Name = "btnFetchInfo";
            btnFetchInfo.Size = new Size(345, 45);
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
            btnSendProvisioningTask.Location = new Point(312, 349);
            btnSendProvisioningTask.Margin = new Padding(4);
            btnSendProvisioningTask.Name = "btnSendProvisioningTask";
            btnSendProvisioningTask.Size = new Size(615, 52);
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
            btnProvisionDevice.Location = new Point(942, 349);
            btnProvisionDevice.Margin = new Padding(4);
            btnProvisionDevice.Name = "btnProvisionDevice";
            btnProvisionDevice.Size = new Size(615, 52);
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
            txtCommandOutput.Location = new Point(310, 445);
            txtCommandOutput.Margin = new Padding(4);
            txtCommandOutput.Multiline = true;
            txtCommandOutput.Name = "txtCommandOutput";
            txtCommandOutput.ReadOnly = true;
            txtCommandOutput.Size = new Size(1245, 664);
            txtCommandOutput.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1844, 1152);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
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
