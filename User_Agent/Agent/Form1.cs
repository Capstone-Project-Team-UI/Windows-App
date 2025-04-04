using System;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Http;
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
            btnProvisioning.Enabled = false; // 🚫 Disable provisioning until registered
        }

        private async void btnFetchInfo_Click(object sender, EventArgs e)
        {
            txtSerial.Text = await Task.Run(() => GetSerialNumber());
            txtIP.Text = await Task.Run(() => GetLocalIPAddress());

            txtCommandOutput.AppendText("🔹 Device Info Fetched.\r\n");
        }


        private async void btnRegister_Click(object sender, EventArgs e)
        {
            txtCommandOutput.AppendText("🔹 Registering device...\r\n");

            string serialNumber = txtSerial.Text;
            string ipAddress = txtIP.Text;
            string userID = "test"; // Dynamic later
            string organization = "Company A";
            string email = "support@companya.com";
            string uniqueID = ApiHelper.GenerateSHA256Hash(serialNumber);

            bool registrationSuccess = await ApiHelper.RegisterUser(userID, organization, serialNumber, uniqueID, email, txtCommandOutput);

            if (registrationSuccess)
            {
                txtCommandOutput.AppendText("✅ Registration Successful! Sending provisioning request to IT...\r\n");

                // 🔸 Send provisioning task to SQS API
                string taskApiUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/";
                var taskPayload = new
                {
                    task = "Request Provisioning",
                    priority = "high",
                    s3_bucket_url = "N/A", // Placeholder, to be filled by IT
                    device_id = serialNumber,
                    unique_id = uniqueID
                };

                using (HttpClient httpClient = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(taskPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(taskApiUrl, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        txtCommandOutput.AppendText("📤 Task sent to IT Agent successfully.\r\n");
                    }
                    else
                    {
                        txtCommandOutput.AppendText($"❌ Failed to send task: {responseBody}\r\n");
                    }
                }

                btnProvisioning.Enabled = true;
            }
            else
            {
                txtCommandOutput.AppendText("❌ Registration Failed! Check logs for details.\r\n");
            }
        }



        private void btnProvisioning_Click(object sender, EventArgs e)
        {
            Form2 provisioningForm = new Form2();
            provisioningForm.Show();
            this.Hide(); // Hide the first form to ensure proper order
        }

        // 🔹 Get System Serial Number
        private string GetSerialNumber()
        {   
            try
            {
                return "Device123"; // Replace with actual serial retrieval logic
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
            try
            {
                return "192.168.1.100"; // Replace with actual IP retrieval logic
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching IP Address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Unknown";
            }

         // Actual ip fetcher
         /* try
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

            }   */

        }

        // 🔹 Temporary: AWS S3 Download After AGENT POSTS task (Later automate it after Running Provision button and display loading while the AGENT uploads the stuffs)
        private async void btnFetchProvisioning_Click(object sender, EventArgs e)
        {
            txtCommandOutput.AppendText("🔍 Fetching provisioning task from SQS...\r\n");

            try
            {
                string uniqueID = ApiHelper.GenerateSHA256Hash(txtSerial.Text);
                string apiUrl = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/get-tasks";

                var body = new
                {
                    unique_id = uniqueID
                };

                using (HttpClient client = new HttpClient())
                {
                    string jsonBody = JsonConvert.SerializeObject(body);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        txtCommandOutput.AppendText($"❌ API Error: {responseBody}\r\n");
                        return;
                    }

                    dynamic result = JsonConvert.DeserializeObject(responseBody);

                    if (result.tasks == null || result.tasks.Count == 0)
                    {
                        txtCommandOutput.AppendText("⚠ No tasks found in the queue for this device.\r\n");
                        return;
                    }

                    string s3Url = result.tasks[0].Body;


                    txtCommandOutput.AppendText($"✅ S3 URL received: {s3Url}\r\n");

                    // Download ZIP to folder
                    string fileName = $"{uniqueID}_ProvisioningFiles.zip";
                    string downloadPath = Path.Combine(@"C:\ProgramData\Provisioning", fileName);
                    Directory.CreateDirectory(@"C:\ProgramData\Provisioning");

                    using (HttpClient httpClient = new HttpClient())
                    using (var stream = await httpClient.GetStreamAsync(s3Url))
                    using (var fileStream = new FileStream(downloadPath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    txtCommandOutput.AppendText($"📥 File downloaded to: {downloadPath}\r\n");

                    // Extract it
                    string extractPath = Path.Combine(@"C:\ProgramData\Provisioning", uniqueID);
                    System.IO.Compression.ZipFile.ExtractToDirectory(downloadPath, extractPath);
                    txtCommandOutput.AppendText($"📂 Extracted to: {extractPath}\r\n");
                }
            }
            catch (Exception ex)
            {
                txtCommandOutput.AppendText($"⚠ Error: {ex.Message}\r\n");
            }
        }


    }
}
