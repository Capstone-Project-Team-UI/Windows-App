using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Agent
{
    public partial class Form1 : Form
    {
        private const string getTasksEndpoint = "https://1dz4oqtvri.execute-api.us-east-2.amazonaws.com/prod/get-tasks";
        private const string hardcodedProvisioningPath = @"C:\ProvisioningTemplate";

        public Form1()
        {
            InitializeComponent();
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

        private void btnCreateProvisioning_Click(object sender, EventArgs e)
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
            string zipPath = $@"C:\ProgramData\Provisioning\{serialHash}_ProvisioningFiles.zip";

            try
            {
                Directory.CreateDirectory(@"C:\ProgramData\Provisioning");
                if (File.Exists(zipPath)) File.Delete(zipPath);

                ZipFile.CreateFromDirectory(hardcodedProvisioningPath, zipPath);
                MessageBox.Show($"✅ Provisioning file created:\n{zipPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating zip: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
