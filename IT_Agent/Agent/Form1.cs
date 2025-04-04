using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agent
{
    public partial class Form1 : Form
    {
        private const string SqsGetUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/get-tasks";
        private const string SqsPostUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/";

        public Form1()
        {
            InitializeComponent();
            txtLog.ReadOnly = true;
        }

        private async void btnFetchTasks_Click(object sender, EventArgs e)
        {
            lstRegistered.Items.Clear();
            string uniqueId = txtUniqueId.Text.Trim();

            if (string.IsNullOrWhiteSpace(uniqueId))
            {
                MessageBox.Show("Enter Unique ID first.");
                return;
            }

            using var client = new HttpClient();
            var json = JsonSerializer.Serialize(new { unique_id = uniqueId });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(SqsGetUrl, content);
                string result = await response.Content.ReadAsStringAsync();
                txtLog.AppendText("✅ Task Response:\n" + result + "\n");

                if (result.Contains("tasks"))
                {
                    lstRegistered.Items.Add(uniqueId);
                }
                else
                {
                    txtLog.AppendText("❌ No new tasks.\n");
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText("⚠ API Error: " + ex.Message + "\n");
            }
        }

        private void btnSelectProvisioning_Click(object sender, EventArgs e)
        {
            if (lstRegistered.SelectedItem == null)
            {
                MessageBox.Show("Select a registered device.");
                return;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtSelectedFolder.Text = folderDialog.SelectedPath;
            }
        }

        private async void btnUploadToBucket_Click(object sender, EventArgs e)
        {
            string selectedDevice = lstRegistered.SelectedItem?.ToString();
            string selectedFolder = txtSelectedFolder.Text;

            if (string.IsNullOrEmpty(selectedDevice) || !Directory.Exists(selectedFolder))
            {
                MessageBox.Show("Device or folder selection missing.");
                return;
            }

            string zipPath = Path.Combine(Path.GetTempPath(), $"{selectedDevice}ProvisioningFiles.zip");
            ZipFile.CreateFromDirectory(selectedFolder, zipPath);

            string s3Url = $"s3://bucket/path/{Path.GetFileName(zipPath)}"; // placeholder

            var payload = new
            {
                task = "UploadProvisioningFiles",
                priority = "high",
                s3_bucket_url = s3Url,
                device_id = selectedDevice,
                unique_id = selectedDevice
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(SqsPostUrl, content);
            string result = await response.Content.ReadAsStringAsync();

            txtLog.AppendText("📤 Upload result: " + result + "\n");
            lstProvisioned.Items.Add(selectedDevice);
            lstRegistered.Items.Remove(selectedDevice);
        }
    }
}
