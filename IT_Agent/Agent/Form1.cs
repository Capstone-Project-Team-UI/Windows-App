using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Amazon;
using Amazon.S3;
using System.Drawing;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Agent
{

    public partial class Form1 : Form

    {

        // Drag Windows without borderBar

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        private void EnableDrag(Control control)
        {
            control.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };
        }


        private AgentConfig config;
        private IAmazonS3 s3Client;

        // Tray icon
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        // Device tracking
        private readonly string provisionedDevicesPath = @"C:\ProgramData\ITAgent\provisioned.txt";
        private readonly string zippedDevicesPath = @"C:\ProgramData\ITAgent\zipped.txt";
        private readonly string cacheFilePath = @"C:\ProgramData\ITAgent\lastUsedPath.txt";

        private readonly HashSet<string> provisionedListHashes = new HashSet<string>();
        private readonly HashSet<string> zippedHashes = new HashSet<string>();
        private readonly HashSet<string> seenDeviceIDs = new HashSet<string>();

        private System.Windows.Forms.Timer pollTimer;
        private string cachedFolderPath = "";
        private string downloadedFolderPath = "";


        public Form1()
        {
            InitializeComponent();

            EnableDrag(this); // Makes the whole form draggable

            // OR just make a specific panel draggable:
            EnableDrag(mainPanel); // If you only want the panel to act as a title bar

            // this.Icon = Properties.Resources.your_icon; // (add icon to resouces.resx)


            // 🧠 Load config from file
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agent_config.json");

            if (!File.Exists(configPath))
            {
                MessageBox.Show("⚠ Configuration file missing! Make sure agent_config.json exists in ProgramData.", "Missing Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            string configJson = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<AgentConfig>(configJson);

            // ✅ Initialize AWS S3 client from config
            s3Client = new AmazonS3Client(config.aws_access_key, config.aws_secret_key, RegionEndpoint.GetBySystemName(config.aws_region));

            // 🔁 Start polling
            pollTimer = new System.Windows.Forms.Timer();
            pollTimer.Interval = 10000;
            pollTimer.Tick += PollTimer_Tick;
            pollTimer.Start();

            // 🧾 Load zipped + provisioned hashes
            if (File.Exists(zippedDevicesPath))
                foreach (var line in File.ReadAllLines(zippedDevicesPath))
                    if (!string.IsNullOrWhiteSpace(line)) zippedHashes.Add(line.Trim());

            if (File.Exists(provisionedDevicesPath))
                foreach (var line in File.ReadAllLines(provisionedDevicesPath))
                    if (!string.IsNullOrWhiteSpace(line)) provisionedListHashes.Add(line.Trim());

            // 🖥 Tray setup
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Exit", null, OnTrayExit);

            trayIcon = new NotifyIcon
            {
                Text = "IT Agent",
                Icon = SystemIcons.Application,
                // Icon = new Icon("Resources\\your_icon.ico"), // (After icon)
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            trayIcon.DoubleClick += (s, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };

            LoadCachedPath();
        }


        // Easier to change company database later
        private (string userID, string organization, string email) GetUserMetadata(UserModel user)
        {
            // Default logic based on API model
            string userID = user.userID;
            string organization = user.organization;
            string email = user.emailAddress;

            // Optional override via local file
            string configPath = "agent_config.json";
            if (File.Exists(configPath))
            {
                try
                {
                    var json = File.ReadAllText(configPath);
                    dynamic config = JsonConvert.DeserializeObject(json);

                    userID = config.userID ?? userID;
                    organization = config.organization ?? organization;
                    email = config.email ?? email;
                }
                catch (Exception ex)
                {
                    txtCommandOutput?.AppendText($"⚠ Failed to load agent_config.json: {ex.Message}\r\n");
                }
            }

            return (userID, organization, email);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancel the close
                this.Hide();     // Hide the window

                trayIcon.BalloonTipTitle = "IT Agent";
                trayIcon.BalloonTipText = "Minimized to taskbar";
                trayIcon.ShowBalloonTip(3000); // Show for 3 seconds
            }

            base.OnFormClosing(e);
        }

        private void OnTrayExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private async Task RefreshPendingListAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string usersApi = config.users_api;
                    ;
                    var usersResp = await client.GetAsync(usersApi);
                    string usersJson = await usersResp.Content.ReadAsStringAsync();
                    if (!usersResp.IsSuccessStatusCode) return;

                    var users = JsonConvert.DeserializeObject<List<UserModel>>(usersJson);

                    foreach (var user in users)
                    {
                        var payload = new Dictionary<string, string>
                {
                    { "unique-id", user.uniqueID }
                };

                        var json = JsonConvert.SerializeObject(payload);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var taskResp = await client.PostAsync("https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/get-tasks", content);
                        if (!taskResp.IsSuccessStatusCode) continue;

                        var outer = JObject.Parse(await taskResp.Content.ReadAsStringAsync());
                        string innerJson = outer["body"]?.ToString();
                        if (string.IsNullOrEmpty(innerJson)) continue;

                        JObject innerParsed;
                        try { innerParsed = JsonConvert.DeserializeObject<JObject>(innerJson); }
                        catch { continue; }

                        var tasks = innerParsed["tasks"] as JArray;
                        if (tasks == null) continue;

                        foreach (var task in tasks)
                        {
                            string bodyRaw = task["Body"]?.ToString();
                            if (string.IsNullOrEmpty(bodyRaw)) continue;

                            try
                            {
                                string unescaped = Regex.Unescape(bodyRaw.Trim('"'));
                                JObject taskData = JsonConvert.DeserializeObject<JObject>(unescaped);
                                string taskType = taskData["task"]?.ToString();
                                string deviceID = taskData["device-id"]?.ToString();
                                string entry = $"{user.userID}::{user.uniqueID}";

                                if (taskType == "Request Provisioning")
                                {
                                    // Skip if previously zipped

                                    if (!string.IsNullOrWhiteSpace(deviceID) &&
                                            !seenDeviceIDs.Contains(deviceID) &&
                                                !zippedHashes.Contains(user.uniqueID))

                                    {
                                        seenDeviceIDs.Add(deviceID);
                                        if (!lstPending.Items.Contains(entry))
                                            lstPending.Items.Add(entry);
                                    }
                                }
                                if (taskType == "Provisioned")
                                {

                                    if (!lstProvisioned.Items.Contains(entry))
                                    {
                                        lstProvisioned.Items.Add(entry);

                                        if (!provisionedListHashes.Contains(user.uniqueID))
                                        {
                                            provisionedListHashes.Add(user.uniqueID);

                                            // ✅ Save full entry (userID::hash) — not just the hash
                                            File.AppendAllLines(provisionedDevicesPath, new[] { entry });
                                        }
                                    }
                                }


                            }
                            catch { continue; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"📡 Refresh error: {ex.Message}");
            }
        }


        private async void PollTimer_Tick(object sender, EventArgs e)
        {
            await RefreshPendingListAsync();
        }

        private async void btnFetchTasks_Click(object sender, EventArgs e)
        {
            await RefreshPendingListAsync();
        }



        private void SaveCachedPath(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(cacheFilePath)!);
            File.WriteAllText(cacheFilePath, path);
            cachedFolderPath = path;
        }

        private void LoadCachedPath()
        {
            if (File.Exists(cacheFilePath))
            {
                cachedFolderPath = File.ReadAllText(cacheFilePath);
            }
        }

        private async void btnCreateProvisioning_Click(object sender, EventArgs e)
        {
            if (lstPending.SelectedItem == null)
            {
                MessageBox.Show("Please select a device from the pending list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selected = lstPending.SelectedItem.ToString(); // Format: userID::serialHash
            string[] parts = selected.Split(new[] { "::" }, StringSplitOptions.None);
            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid task format. Expected: userID::serialHash", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string serialHash = parts[1];
            string userPackagePath = Path.Combine(cachedFolderPath, "Packages", serialHash);

            if (!Directory.Exists(userPackagePath))
            {
                MessageBox.Show($"❌ Folder '{serialHash}' does not exist under Packages. Please duplicate defPackage first.", "Missing Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string zipFileName = $"{serialHash}_ProvisioningFiles.zip";
            string zipPath = Path.Combine(cachedFolderPath, "Zipped", zipFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(zipPath)!);

            try
            {
                if (File.Exists(zipPath)) File.Delete(zipPath);
                ZipFile.CreateFromDirectory(userPackagePath, zipPath);

                MessageBox.Show($"✅ Zip created: {zipPath}", "Zipped Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var putRequest = new PutObjectRequest
                {
                    BucketName = config.s3_bucket,
                    Key = $"users/{zipFileName}",
                    FilePath = zipPath,
                    ContentType = "application/zip"
                };

                using (var s3 = new AmazonS3Client(config.aws_access_key, config.aws_secret_key, RegionEndpoint.GetBySystemName(config.aws_region)))
                {
                    await s3.PutObjectAsync(putRequest);
                }

                MessageBox.Show($"✅ Zip uploaded to S3:\nusers/{zipFileName}", "Upload Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Remove from Pending list after provisioning
                lstPending.Items.Remove(selected);

                if (!zippedHashes.Contains(serialHash))
                {
                    zippedHashes.Add(serialHash);
                    File.AppendAllLines(zippedDevicesPath, new[] { serialHash });
                }

            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"❌ AWS S3 Error:\n{s3Ex.Message}", "S3 Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ General Error:\n{ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Create zip should be greyed out until user selected
        private void lstPending_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCreateProvisioning.Enabled = lstPending.SelectedItem != null;
        }



        // 🔹  Download Default Provisioning Folder from S3
        private async void btnDownloadTemplate_Click(object sender, EventArgs e)
        {


            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Choose a folder to extract the provisioning base to:";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string downloadPath = Path.Combine(dialog.SelectedPath, "ProvisioningBase.zip");

                    try
                    {
                        // Download the file from S3
                        var request = new Amazon.S3.Model.GetObjectRequest
                        {
                            BucketName = config.s3_bucket,
                            Key = config.object_key
                        };

                        using (var response = await s3Client.GetObjectAsync(request))
                        using (var responseStream = response.ResponseStream)
                        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
                        {
                            await responseStream.CopyToAsync(fileStream);
                        }

                        // Extract the zip
                        string extractPath = Path.Combine(dialog.SelectedPath, "AMD Provisioning Folder");
                        if (Directory.Exists(extractPath)) Directory.Delete(extractPath, true);
                        ZipFile.ExtractToDirectory(downloadPath, extractPath);

                        // Save the extracted path to the cache
                        SaveCachedPath(extractPath);

                        MessageBox.Show("✅ Provisioning folder downloaded and extracted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Error downloading from S3:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        // 🔹 Duplicate defPackage inside Packages/
        private void btnDuplicatePackage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cachedFolderPath) || !Directory.Exists(cachedFolderPath))
            {
                MessageBox.Show("⚠ No cached folder found. Please download the provisioning folder first.", "Missing Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstPending.SelectedItem == null)
            {
                MessageBox.Show("⚠ Please select a user from the pending list first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selected = lstPending.SelectedItem.ToString(); // Format: userID::serialHash
            string[] parts = selected.Split(new[] { "::" }, StringSplitOptions.None);
            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid format for selected user. Expected 'userID::serialHash'.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userHash = parts[1];

            string packagesPath = Path.Combine(cachedFolderPath, "Packages");
            string defPackagePath = Path.Combine(packagesPath, "ACMS");
            string newPackagePath = Path.Combine(packagesPath, userHash);

            if (!Directory.Exists(defPackagePath))
            {
                MessageBox.Show("❌ 'defPackage' folder not found inside Packages directory.", "Missing defPackage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Directory.Exists(newPackagePath))
            {
                MessageBox.Show($"⚠ A package named '{userHash}' already exists.", "Package Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CopyDirectory(defPackagePath, newPackagePath);
                MessageBox.Show($"✅ defPackage duplicated as: {userHash}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error duplicating folder:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string targetFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, targetFile, true);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string newSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, newSubDir);
            }
        }

        private void btnClearProvisioned_Click(object sender, EventArgs e)
        {
            lstProvisioned.Items.Clear();
            txtCommandOutput?.AppendText("🧹 Cleared visual list of provisioned devices.\r\n");
        }

        private void btnFetchAllProvisioned_Click(object sender, EventArgs e)
        {
            if (File.Exists(provisionedDevicesPath))
            {
                var lines = File.ReadAllLines(provisionedDevicesPath);
                lstProvisioned.Items.Clear();
                foreach (var line in lines.Distinct())
                {
                    string trimmed = line.Trim();

                    if (!string.IsNullOrWhiteSpace(trimmed) && !lstProvisioned.Items.Contains(trimmed))
                    {
                        lstProvisioned.Items.Add(trimmed);

                        // ✅ Safe parsing of hash
                        var parts = trimmed.Split(new[] { "::" }, StringSplitOptions.None);
                        if (parts.Length == 2)
                        {
                            provisionedListHashes.Add(parts[1]); // only the hash part
                        }
                    }
                }


                txtCommandOutput?.AppendText("📂 Reloaded saved provisioned devices.\r\n");
            }
            else
            {
                txtCommandOutput?.AppendText("⚠ No saved file found to fetch provisioned users.\r\n");
            }
        }

        // API Model
        public class UserModel
        {
            public string userID { get; set; }
            public string organization { get; set; }
            public string serialNumber { get; set; }
            public string uniqueID { get; set; }
            public string emailAddress { get; set; }
        }

        // config class
        public class AgentConfig
        {
            public string api_url { get; set; }
            public string users_api { get; set; }
            public string s3_bucket { get; set; }
            public string aws_access_key { get; set; }
            public string aws_secret_key { get; set; }
            public string aws_region { get; set; }
            public string object_key { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCommandOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
