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



namespace Agent
{
    public partial class Form1 : Form
    {
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
            LoadCachedPath();
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

        private async void btnFetchTasks_Click(object sender, EventArgs e)
        {
            lstPending.Items.Clear();

            try
            {
                string dummyUniqueID = "admin-check"; // Temporary if API requires something
                var body = new { unique_id = dummyUniqueID };

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(getTasksEndpoint, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"API Error: {json}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var parsed = JObject.Parse(json);

                    if (parsed["tasks"] != null && parsed["tasks"].HasValues)
                    {
                        foreach (var task in parsed["tasks"])
                        {
                            string bodyContent = task["Body"]?.ToString();
                            lstPending.Items.Add(bodyContent); // expected to be something like userID::hash
                        }
                    }
                    else
                    {
                        MessageBox.Show("📭 No tasks found in the SQS queue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching tasks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Upload to S3 under users/
                var putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = "your-bucket-name", // Replace this
                    Key = $"users/{zipFileName}",
                    FilePath = zipPath,
                    ContentType = "application/zip"
                };

                var response = await s3Client.PutObjectAsync(putRequest);
                MessageBox.Show($"✅ Zip uploaded to S3:\nusers/{zipFileName}", "Upload Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            string packagesPath = Path.Combine(cachedFolderPath, "Packages");
            string defPackagePath = Path.Combine(packagesPath, "defPackage");

            if (!Directory.Exists(defPackagePath))
            {
                MessageBox.Show("❌ 'defPackage' folder not found in: " + packagesPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string newFolderName = "PF5EVLV3"; // TODO: Replace with actual user hash
                string newPackagePath = Path.Combine(packagesPath, newFolderName);

                if (Directory.Exists(newPackagePath))
                {
                    MessageBox.Show($"⚠ A package named '{newFolderName}' already exists.", "User Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CopyDirectory(defPackagePath, newPackagePath);
                MessageBox.Show($"✅ defPackage duplicated as: {newFolderName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error duplicating folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
