namespace Agent
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnFetchTasks;
        private TextBox txtUniqueId;
        private ListBox lstRegistered;
        private Button btnSelectProvisioning;
        private TextBox txtSelectedFolder;
        private Button btnUploadToBucket;
        private ListBox lstProvisioned;
        private TextBox txtLog;
        private FolderBrowserDialog folderDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderDialog = new FolderBrowserDialog();

            // Unique ID Label & TextBox
            Label lblUniqueId = new Label
            {
                Location = new Point(20, 20),
                Text = "User Unique ID:",
                AutoSize = true
            };
            this.txtUniqueId = new TextBox
            {
                Location = new Point(130, 16),
                Width = 300,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            this.btnFetchTasks = new Button
            {
                Location = new Point(440, 15),
                Text = "Fetch Tasks",
                Width = 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            this.btnFetchTasks.Click += new EventHandler(this.btnFetchTasks_Click);

            // Registered Devices List
            Label lblRegistered = new Label
            {
                Location = new Point(20, 60),
                Text = "📥 Registered Devices",
                AutoSize = true
            };
            this.lstRegistered = new ListBox
            {
                Location = new Point(20, 80),
                Width = 250,
                Height = 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            this.btnSelectProvisioning = new Button
            {
                Location = new Point(290, 80),
                Text = "Select Folder",
                Width = 120,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            this.btnSelectProvisioning.Click += new EventHandler(this.btnSelectProvisioning_Click);

            // Selected Folder Path
            Label lblFolder = new Label
            {
                Location = new Point(20, 190),
                Text = "Selected Folder:",
                AutoSize = true
            };
            this.txtSelectedFolder = new TextBox
            {
                Location = new Point(130, 185),
                Width = 310,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            this.btnUploadToBucket = new Button
            {
                Location = new Point(450, 183),
                Text = "Upload & Complete",
                Width = 120,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            this.btnUploadToBucket.Click += new EventHandler(this.btnUploadToBucket_Click);

            // Provisioned Devices
            Label lblProvisioned = new Label
            {
                Location = new Point(20, 230),
                Text = "✅ Provisioned Devices",
                AutoSize = true
            };
            this.lstProvisioned = new ListBox
            {
                Location = new Point(20, 250),
                Width = 250,
                Height = 100,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Logs
            Label lblLogs = new Label
            {
                Location = new Point(290, 230),
                Text = "📝 Logs",
                AutoSize = true
            };
            this.txtLog = new TextBox
            {
                Location = new Point(290, 250),
                Width = 280,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Add to Controls
            this.Controls.AddRange(new Control[] {
        lblUniqueId, txtUniqueId, btnFetchTasks,
        lblRegistered, lstRegistered, btnSelectProvisioning,
        lblFolder, txtSelectedFolder, btnUploadToBucket,
        lblProvisioned, lstProvisioned, lblLogs, txtLog
    });

            this.Text = "IT Agent App";
            this.MinimumSize = new Size(600, 420);
            this.ClientSize = new Size(600, 400);
        }


    }
}
