using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModMapConverter
{
    public partial class MainWindow : Form
    {
		public SettingsWindow settingsWindow { get; internal set; }
		public bool runningSettings { get; internal set; } = false;
		public bool Running { get; private set; } = false;

		private readonly CookieContainer cJar = new CookieContainer();

		public readonly string UserAgent = @"Mozilla/5.0 (Windows; Windows NT 6.1) AppleWebKit/534.23 (KHTML, like Gecko) Chrome/11.0.686.3 Safari/534.23";

		public string HttpGet(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.CookieContainer = cJar;
			request.UserAgent = UserAgent;
			request.KeepAlive = false;
			request.Method = "GET";
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "bruh");
				return "Error";
			}
			StreamReader sr = new StreamReader(response.GetResponseStream());
			return sr.ReadToEnd();
		}
		public MainWindow()
        {
			InitializeComponent();
			convertBtn.MouseClick += Convert;
		}

		public void Convert(object sender, MouseEventArgs e)
		{
			Running = true;
			string[] array;
			string text = input.Text;
			if (text.Length == 0)
			{
				MessageBox.Show("You cannot convert a nonexistent map.", "bruh");
				return;
			}
			else
			{
				if (text.StartsWith("https://raw.githubusercontent.com") || text.StartsWith("https://gist.githubusercontent.com") || text.StartsWith("https://pastebin.com/raw"))
				{
					string txt = HttpGet(text);
					if (txt == "Error")
					{
						return;
					}
					text = txt;
				}
				else if (text.StartsWith("https://") || text.StartsWith("https://"))
				{
					var dialogResult = MessageBox.Show("Are you sure you want to send a WebRequest to '" + text + "'", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (dialogResult == DialogResult.Yes)
					{
						string txt = HttpGet(text);
						if (txt == "Error")
						{
							return;
						}
						text = txt;
					}
					else
					{
						return;
					}
				}
			}
			JsonObject jsonObject = new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>());

			array = text.Split(new char[]{','});

			jsonObject.Add("audio", "rbxassetid://" + array[0]);
			jsonObject.Add("noteDistance", 70);
			jsonObject.Add("colors", new JsonArray(new JsonValue[]
			{
				new JsonArray(new JsonValue[]{255, 0, 0}),
				new JsonArray(new JsonValue[]{0, 255, 255})
			}));
			JsonArray jsonArray = new JsonArray(Array.Empty<JsonValue>());
			string[] array2 = array;
			try
			{
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array3 = array2[i].Split(new char[]
					{
						'|'
					});
					if (array3.Length > 1)
					{
						jsonArray.Add(new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>())
						{
							{
								"type",
								0
							},
							{
								"time",
								int.Parse(array3[2]) / 1000.0
							},
							{
								"length",
								1
							},
							{
								"position",
								new JsonArray(new JsonValue[]
								{
									-(2.0 * (double.Parse(array3[0]) - 1.0)),
									(2.0 * (double.Parse(array3[1]) - 1.0))
								})
							},
							{
								"color",
								0
							},
							{
								"transparency",
								0
							},
							{
								"fog",
								Properties.Settings.Default.Notes_change_fog
							},
							{
								"animation",
								new JsonArray(Array.Empty<JsonValue>())
							}
						});
					}
				}
				jsonObject.Add("objects", jsonArray);
				jsonObject.Add("events", new JsonArray(Array.Empty<JsonValue>()));
				jsonObject.Add("tracks", new JsonArray(Array.Empty<JsonValue>()));
				Running = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Running = false;
				return;
			}
			output.Text = jsonObject.ToString();
		}

		private void Settings_Click(object sender, EventArgs e)
        {
			if (Running)
				return;

			if (!runningSettings)
			{
				runningSettings = true;
				Hide();
				settingsWindow.LoadSettings();
				settingsWindow.mainWindow = this; // just in case
				settingsWindow.Show();
			}
			else
			{
				MessageBox.Show("Settings already running...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
