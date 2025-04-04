using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace WindowsProvisioningApp
{
    public partial class Form1 : Form
    {
        private const string bucketName = "testbucketawss333"; // 🔹 Change to your S3 bucket
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1; // 🔹 Change to your AWS region
        private static IAmazonS3 s3Client;
        private string selectedFileKey = ""; // Stores the selected file name

        public Form1()
        {
            InitializeComponent();
            s3Client = new AmazonS3Client(
                "AKIA2MNVL7EDGL2XAFUU",  // 🔹 Replace with your AWS Access Key
                "CrWNfN6AfAWAUrguba3vqHnAoJ4r2SJ+0DSgntGr",  // 🔹 Replace with your AWS Secret Key
                bucketRegion
            );
        }

        // 🔹 List all files in S3
        private async void btnListFiles_Click(object sender, EventArgs e)
        {
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketName
                };

                ListObjectsV2Response response = await s3Client.ListObjectsV2Async(request);

                if (response.S3Objects.Count == 0)
                {
                    MessageBox.Show("No files found in the bucket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Show file selection dialog
                string fileList = "Select a file:\n\n";
                foreach (var obj in response.S3Objects)
                {
                    fileList += $"{obj.Key}\n";
                }

                string selectedFile = Microsoft.VisualBasic.Interaction.InputBox(fileList, "Select File", response.S3Objects[0].Key);

                if (!string.IsNullOrEmpty(selectedFile))
                {
                    selectedFileKey = selectedFile;
                    lblSelectedFile.Text = $"Selected: {selectedFileKey}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error listing files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Download the selected file
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFileKey))
            {
                MessageBox.Show("No file selected. Please list and select a file first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath = Path.Combine(@"C:\ProgramData\Provisioning", Path.GetFileName(selectedFileKey));
                Directory.CreateDirectory(@"C:\ProgramData\Provisioning"); // Ensure directory exists

                lblStatus.Text = "Downloading...";
                await DownloadFile(bucketName, selectedFileKey, filePath);
                lblStatus.Text = "Download Complete!";
                MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Download file from S3
        private static async Task DownloadFile(string bucket, string key, string filePath)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucket,
                Key = key
            };

            using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await response.ResponseStream.CopyToAsync(fileStream);
            }
        }
    }
}
