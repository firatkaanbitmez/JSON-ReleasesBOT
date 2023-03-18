using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SoundRecorderReleaseBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Replace this with your own GitHub repository URL
            var repoUrl = "https://api.github.com/repos/firatkaanbitmez/SoundRecorder/releases/latest";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "SoundRecorderReleaseBot");
            var response = await client.GetAsync(repoUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseContent);

                var tagName = json["tag_name"].ToString();
                var releaseNotes = json["body"].ToString();

                MessageBox.Show($"Latest release: {tagName}\n\nRelease notes:\n\n{releaseNotes}", "SoundRecorder Release Notes");
            }
            else
            {
                MessageBox.Show($"Error retrieving latest release: {response.StatusCode}", "SoundRecorder Release Notes");
            }
        }
    }
}
