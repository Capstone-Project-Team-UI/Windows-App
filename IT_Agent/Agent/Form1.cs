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







namespace Agent
{
    public partial class Form1 : Form

    {

        // Save device info externally to neglect them once they are zipped

        private readonly string provisionedDevicesPath = @"C:\ProgramData\ITAgent\provisioned.txt";
        private readonly HashSet<string> provisionedHashes = new HashSet<string>();

        private readonly HashSet<string> seenDeviceIDs = new HashSet<string>();


        private System.Windows.Forms.Timer pollTimer;

        // Right now the keys are hardcoded, NOT RECOMMENDED TO DO THIS, you can use environmental variables later with your own bucket


        private static readonly string accessKey = "AKIA2MNVL7EDOIG3UO7I"; //Access Key 
        private static readonly string secretKey = "CJTexi2dkjqIHOwy4MIwRGkWyrZHHadN25YNVtoI"; //Secret Key
        private static readonly RegionEndpoint region = RegionEndpoint.USWest2; // bucket region

        private static readonly string bucketName = "testbucketawss333";
        private static readonly string objectKey = "AMD Provisioning Console.zip"; // S3 file key

        private static IAmazonS3 s3Client = new AmazonS3Client(accessKey, secretKey, region);
        private const string getTasksEndpoint = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/get-tasks";
        private const string hardcodedProvisioningPath = @"C:\ProvisioningTemplate";
        private string downloadedFolderPath;

        // To save pulled folder location for later use
        private readonly string cacheFilePath = @"C:\ProgramData\ITAgent\lastUsedPath.txt";
        private string cachedFolderPath = "";

        public Form1()
        {
            InitializeComponent();
            pollTimer = new System.Windows.Forms.Timer();
            pollTimer.Interval = 10000; // 10 seconds
            pollTimer.Tick += PollTimer_Tick;
            pollTimer.Start();

            if (File.Exists(provisionedDevicesPath))
            {
                foreach (var line in File.ReadAllLines(provisionedDevicesPath))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        provisionedHashes.Add(line.Trim());
                }
            }


            LoadCachedPath();



        }

        private async Task RefreshPendingListAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string usersApi = "http://localhost:8090/users";
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
                                                !provisionedHashes.Contains(user.uniqueID))

                                    {
                                        seenDeviceIDs.Add(deviceID);
                                        if (!lstPending.Items.Contains(entry))
                                            lstPending.Items.Add(entry);
                                    }
                                }
                                else if (taskType == "Provisioned")
                                {
                                    if (!lstProvisioned.Items.Contains(entry))
                                        lstProvisioned.Items.Add(entry);
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

        public class UserModel
        {
            public string userID { get; set; }
            public string organization { get; set; }
            public string serialNumber { get; set; }
            public string uniqueID { get; set; }
            public string emailAddress { get; set; }
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
                    BucketName = bucketName,
                    Key = $"users/{zipFileName}",
                    FilePath = zipPath,
                    ContentType = "application/zip"
                };

                using (var s3 = new AmazonS3Client(accessKey, secretKey, region))
                {
                    await s3.PutObjectAsync(putRequest);
                }

                MessageBox.Show($"✅ Zip uploaded to S3:\nusers/{zipFileName}", "Upload Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Remove from Pending list after provisioning
                lstPending.Items.Remove(selected);
                
                if (!provisionedHashes.Contains(serialHash))
                {
                    provisionedHashes.Add(serialHash);
                    File.AppendAllLines(provisionedDevicesPath, new[] { serialHash });
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
                            BucketName = bucketName,
                            Key = objectKey
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
            string defPackagePath = Path.Combine(packagesPath, "defPackage");
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
    }
}
