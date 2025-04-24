using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DeviceInfoApp
{
    public partial class Form1 : Form

    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnFetchInfo_Click(object sender, EventArgs e)
        {
            txtSerial.Text = await Task.Run(() => GetSerialNumber());
            txtIP.Text = await Task.Run(() => GetLocalIPAddress());

            txtCommandOutput.AppendText("🔹 Device Info Fetched.\r\n");
        }

        // Registers and Provisions
        private async void btnSendProvisioningTask_Click(object sender, EventArgs e)
        {
            txtCommandOutput.AppendText("🔧 Registering and requesting provisioning...\r\n");

            string serialNumber = txtSerial.Text;
            string deviceID = serialNumber;
            string uniqueID = ApiHelper.GenerateSHA256Hash(serialNumber);
            string userID = $"{Environment.UserName}@{Environment.MachineName}";
            string organization = "Company A";
            string email = "support@companya.com";

            bool proceedToProvisioning = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var payload = new
                    {
                        userID = userID,
                        organization = organization,
                        serialNumber = serialNumber,
                        uniqueID = uniqueID,
                        emailAddress = email
                    };

                    string json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    string registerUrl = "http://localhost:8090/users"; // Replace with config if needed
                    HttpResponseMessage response = await client.PostAsync(registerUrl, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    txtCommandOutput.AppendText($"{DateTime.Now}: 📤 Registering user: {json}\r\n");

                    if (response.IsSuccessStatusCode)
                    {
                        txtCommandOutput.AppendText("✅ Device Registered! Proceeding to provisioning.\r\n");
                        proceedToProvisioning = true;
                    }
                    else
                    {
                        txtCommandOutput.AppendText($"❌ Registration failed: {responseBody}\r\n");

                        if (responseBody.Contains("already exists") || responseBody.Contains("already registered"))
                        {
                            txtCommandOutput.AppendText("ℹ Device is already registered. Proceeding to provisioning.\r\n");
                            proceedToProvisioning = true;
                        }
                        else
                        {
                            txtCommandOutput.AppendText("❌ Device not registered. Cannot proceed to provisioning.\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText($"❌ Registration error: {ex.Message}\r\n");
            }

            // 🧠 Only send provisioning task if registration was successful or already registered
            if (!proceedToProvisioning)
                return;

            // 🔁 Send provisioning request
            var taskPayload = new TaskModel
            {
                Task = "Request Provisioning",
                Priority = "high",
                S3BucketUrl = "N/A",
                DeviceId = deviceID,
                UniqueId = uniqueID
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(taskPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    string apiUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/"; // Replace with config if needed
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    txtCommandOutput.AppendText($"📨 Sent Payload: {json}\r\n");

                    if (response.IsSuccessStatusCode)
                    {
                        txtCommandOutput.AppendText("📤 Provisioning task sent successfully.\r\n");
                    }
                    else
                    {
                        txtCommandOutput.AppendText($"❌ Failed to send task: {responseBody}\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText($"⚠ Error sending task: {ex.Message}\r\n");
            }
        }

        // 🔹 Get System Serial Number
        private string GetSerialNumber()
        {
            try
            {
                return "SG56YUI"; // Replace with actual serial retrieval logic
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching serial number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Unknown";
            }

            // Actual serial logic

            /*
             try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS");
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["SerialNumber"]?.ToString() ?? "Unknown";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching serial number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "Unknown"; */

        }

        // 🔹 Get Local IP Address
        private string GetLocalIPAddress()
        {
            /*
            try
            {   
                return "192.168.1.100"; // Replace with actual IP retrieval logic
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching IP Address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Unknown";
            }
            */

            // Actual ip fetcher
            try
               {
                   string localIP = "Unknown";
                   var host = Dns.GetHostEntry(Dns.GetHostName());

                   foreach (var ip in host.AddressList)
                   {
                       if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                       {
                           localIP = ip.ToString();
                           break;
                       }
                   }

                   return localIP;
               }
               catch (Exception ex)
               {
                   MessageBox.Show($"Error fetching IP address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return "Unknown";

               }   

        }

        private void RunCommandAsAdmin(string workingDirectory, string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Verb = "runas"
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText("❌ Error running command: " + ex.Message + "\r\n");
            }
        }

        private bool RunAimtProvisioning(string selectedDirectory)
        {
            txtCommandOutput.AppendText("⚙ Starting AIM-T provisioning...\r\n");

            string provisioningAppPath = Path.Combine(selectedDirectory, "AIM-TProvisioningApp.exe");
            string logFilePath = Path.Combine(selectedDirectory, "provision_log.txt");

            if (!Directory.Exists(selectedDirectory) || !File.Exists(provisioningAppPath))
            {
                txtCommandOutput.AppendText("❌ AIM-T folder or executable not found.\r\n");
                return false;
            }

            // ✅ Ensure service is running
            try
            {
                ServiceController sc = new ServiceController("AIMTManageabilityService");
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    txtCommandOutput.AppendText("❌ AIMTManageabilityService is not running. Enable it in BIOS.\r\n");
                    return false;
                }
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText($"⚠ Error checking AIMT service: {ex.Message}\r\n");
                return false;
            }

            // ✅ Create log file and set permissions
            string createLog = $"echo. > \"{logFilePath}\" && icacls \"{logFilePath}\" /grant Everyone:F";
            RunCommandAsAdmin(selectedDirectory, createLog);

            // ✅ Step 1: Run _oMt
            string workingDir = $"\"{selectedDirectory}\"";
            string run_oMt = $"cd /d {workingDir} && \"{provisioningAppPath}\" -i AIM-T-CRYPTO_ACMS_oMt >> \"{logFilePath}\" 2>&1";
            RunCommandAsAdmin(selectedDirectory, run_oMt);

            if (!File.Exists(logFilePath))
            {
                txtCommandOutput.AppendText("❌ Log file not found. Command may have failed.\r\n");
                return false;
            }

            // ✅ Read logs
            bool alreadyProvisioned = false;
            string[] lines = File.ReadAllLines(logFilePath);
            txtCommandOutput.AppendText("🔍 Log Output:\r\n" + string.Join("\r\n", lines) + "\r\n");

            foreach (string line in lines)
            {
                if (line.Contains("System owned error"))
                {
                    alreadyProvisioned = true;
                    break;
                }
            }

            // ✅ Step 2: If already provisioned, run _M
            if (alreadyProvisioned)
            {
                txtCommandOutput.AppendText("⚠ Already provisioned. Running re-provision (_M)...\r\n");
                string run_M = $"cd /d {workingDir} && \"{provisioningAppPath}\" -i AIM-T_CRYPTO_ACMS_M >> \"{logFilePath}\" 2>&1";
                RunCommandAsAdmin(selectedDirectory, run_M);
                txtCommandOutput.AppendText("✅ Re-provisioning complete. Restart required.\r\n");
                return true;
            }

            txtCommandOutput.AppendText("✅ Successfully Provisioned. Restart required.\r\n");
            return true;
        }


        private async void btnFetchProvisioning_Click(object sender, EventArgs e)
        {
            txtCommandOutput.AppendText("🔍 Checking for provisioning package on S3...\r\n");

            try
            {
                string serial = txtSerial.Text;
                string hash = ApiHelper.GenerateSHA256Hash(serial);
                string fileName = $"{hash}_ProvisioningFiles.zip";
                string s3Key = $"users/{fileName}";
                string localZipPath = Path.Combine(@"C:\ProgramData\Provisioning", fileName);
                string extractPath = Path.Combine(@"C:\ProgramData\Provisioning", hash);
                string aimtPath = Path.Combine(extractPath, "AIM-T");

                Directory.CreateDirectory(Path.GetDirectoryName(localZipPath)!);

                var s3Client = new Amazon.S3.AmazonS3Client("AKIA2MNVL7EDOIG3UO7I", "CJTexi2dkjqIHOwy4MIwRGkWyrZHHadN25YNVtoI", Amazon.RegionEndpoint.USWest2);
                var request = new Amazon.S3.Model.GetObjectRequest
                {
                    BucketName = "testbucketawss333",
                    Key = s3Key
                };

                using (var response = await s3Client.GetObjectAsync(request))
                using (var stream = response.ResponseStream)
                using (var outputFile = new FileStream(localZipPath, FileMode.Create))
                {
                    await stream.CopyToAsync(outputFile);
                }

                txtCommandOutput.AppendText($"📥 Downloaded ZIP to: {localZipPath}\r\n");

                System.IO.Compression.ZipFile.ExtractToDirectory(localZipPath, extractPath, true);
                txtCommandOutput.AppendText($"📂 Extracted to: {extractPath}\r\n");

                // ✅ Begin AIM-T provisioning
                bool success = RunAimtProvisioning(aimtPath);

                if (!success)
                {
                    txtCommandOutput.AppendText("⚠ Provisioning failed. Task not sent.\r\n");
                    return;
                }

                // ✅ Send "Provisioned" task to IT Agent
                var taskPayload = new TaskModel
                {
                    Task = "Provisioned",
                    Priority = "normal",
                    S3BucketUrl = $"s3://testbucketawss333/{s3Key}",
                    DeviceId = serial,
                    UniqueId = hash
                };


                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(taskPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    string apiUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/";
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        txtCommandOutput.AppendText("✅ Sent 'Provisioned' task to IT Agent.\r\n");
                    }
                    else
                    {
                        txtCommandOutput.AppendText($"❌ Failed to send 'Provisioned' task: {responseBody}\r\n");
                    }
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3Ex)
            {
                txtCommandOutput.AppendText($"⚠ S3 Error: {s3Ex.Message}\r\n");
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText($"⚠ General Error: {ex.Message}\r\n");
            }
        }



        public class TaskModel
        {
            [JsonProperty("task")]
            public string Task { get; set; }

            [JsonProperty("priority")]
            public string Priority { get; set; }

            [JsonProperty("s3-bucket-url")]
            public string S3BucketUrl { get; set; }

            [JsonProperty("device-id")]
            public string DeviceId { get; set; }

            [JsonProperty("unique-id")]
            public string UniqueId { get; set; }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
