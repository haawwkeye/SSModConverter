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
		internal httpHandler httpHandler { get; private set; } = new httpHandler();
		public SettingsWindow settingsWindow { get; internal set; }
		public bool runningSettings { get; internal set; } = false;
		public bool isConvertingMap { get; private set; } = false;
		public bool isConvertingType { get; private set; } = false;

		public MainWindow()
        {
			InitializeComponent();

			Version.Text = "V" + Properties.Settings.Default.Version;

			convertBtn.MouseClick += Convert;
			convertType.MouseClick += ChangeType;

			songAR.GotFocus += arRemoveText;
			songAR.LostFocus += arAddText;

			BSSongId.GotFocus += bsRemoveText;
			BSSongId.GotFocus += bsAddText;
		}

		public void ChangeType(object sender, EventArgs e)
		{
			if (isConvertingType)
				return;

			isConvertingType = true;

			string type = convertType.Text.Substring(9);

			if (type == "SS")
			{
				convertType.Text = "Convert: BS";
				type = "BS";
			}
			else if (type == "BS")
			{
				convertType.Text = "Convert: SSJ";
				type = "SSJ";
			}
			else
			{
				convertType.Text = "Convert: SS";
				type = "SS";
			}

			if (type == "BS")
            {
				BSSongId.Visible = true;
            }
			else
            {
				BSSongId.Visible = false;
			}

			isConvertingType = false;
		}

		public void arRemoveText(object sender, EventArgs e)
		{
			if (songAR.Text == "Enter AR")
				songAR.Text = "";
		}

		public void arAddText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(songAR.Text))
				songAR.Text = "Enter AR";
		}

		public void bsRemoveText(object sender, EventArgs e)
		{
			if (BSSongId.Text == "Enter BS SongId")
				BSSongId.Text = "";
		}

		public void bsAddText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(BSSongId.Text))
				BSSongId.Text = "Enter BS SongId";
		}

		public void Convert(object sender, MouseEventArgs e)
		{
			bool fakeCursor = FakeCursor.Checked;
			bool osuNotes = OsuNote.Checked;
			bool sar = double.TryParse(songAR.Text, out double songar);
			double offset = 0;
			string type = convertType.Text.Substring(9);

			if (isConvertingMap)
            {
				MessageBox.Show("Already running", "Error");
				return;
            }

			if (type == "SSJ")
            {
				MessageBox.Show("Sound Space JSON files not supported yet.", "Error");
				return;
            }
			else if (type == "BS")
			{
				MessageBox.Show("Beat Saber JSON files not supported yet.", "Error");
				return;
			}

			string[] array;
			string text = input.Text;
			if (text.Length == 0)
			{
				MessageBox.Show("You cannot convert a nonexistent map.", "bruh");
				return;
			}
			else
			{
				isConvertingMap = true;
				if (text.StartsWith("https://raw.githubusercontent.com") || text.StartsWith("https://gist.githubusercontent.com") || text.StartsWith("https://pastebin.com/raw"))
				{
					string txt = httpHandler.HttpGet(text);
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
						string txt = httpHandler.HttpGet(text);
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

			double AR = 70;

			if (sar)
			{
				AR = songar;
			}

			jsonObject.Add("audio", "rbxassetid://" + array[0]);

			jsonObject.Add("noteDistance", AR);


			jsonObject.Add("colors", new JsonArray(new JsonValue[]
			{
				new JsonArray(new JsonValue[]{255, 0, 0}),
				new JsonArray(new JsonValue[]{0, 255, 255})
			}));
			JsonArray jsonArray = new JsonArray(Array.Empty<JsonValue>());
			string[] array2 = array;
			/**/
			try
			{
			/**/

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
							{"type", 0},
							{"time", int.Parse(array3[2]) / 1000.0},
							{"length", 1},
							{"position", new JsonArray(new JsonValue[] { -(2.0 * (double.Parse(array3[0]) - 1.0)), (2.0 * (double.Parse(array3[1]) - 1.0)) })},
							{"color", 0},
							{"transparency", 0},
							{"fog", Properties.Settings.Default.Notes_change_fog},
							{"animation", new JsonArray(Array.Empty<JsonValue>()) }
						});

						var note = jsonArray.Count-1;
						//Console.WriteLine(note);
						if (osuNotes)
						{
							var obj = jsonArray[note];

							obj["length"] = 0.8;

							obj["animation"] = new JsonArray(Array.Empty<JsonValue>()) {
								new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
									{ "time", 0 },
									{ "position", new JsonArray(new JsonValue[] { obj["position"][0], obj["position"][1], -AR }) },
									{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
									{ "transparency", 0.4 }
								},
								new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
									{ "time", 1 },
									{ "position", new JsonArray(new JsonValue[] { obj["position"][0], obj["position"][1], 0 }) },
									{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
									{ "transparency", 0 }
								}
							};

							jsonArray.Add(new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>())
							{
								{"type", 1},
								{"time", obj["time"] - 0.8},
								{"length", 0.8},
								{"position",  new JsonArray(new JsonValue[]{0, 0, 0})},
								{"rotation", new JsonArray(new JsonValue[]{0, 0, 0})},
								{"size", new JsonArray(new JsonValue[]{0, 0, 0})},
								{"transparency", 0},
								{"material", 0},
								{"appearance", 1},
								{"color", 0},
								{"animation", new JsonArray(Array.Empty<JsonValue>()) }
							});

							var effectobj = jsonArray[jsonArray.Count-1];

							effectobj["animation"] = new JsonArray(Array.Empty<JsonValue>())
							{
								new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
									{ "time", 0 },
									{ "position", new JsonArray(new JsonValue[] { obj["position"][0], obj["position"][1], -AR + -0.5 }) },
									{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
									{ "size", new JsonArray(new JsonValue[] { 5.5, 5.5, 0 }) },
									{ "transparency", 1 }
								},
								new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
									{ "time", 1 },
									{ "position", new JsonArray(new JsonValue[] { obj["position"][0], obj["position"][1], 0 }) },
									{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
									{ "size", new JsonArray(new JsonValue[] { 1.75, 1.75, 0 }) },
									{ "transparency", 0.4 }
								}
							};
						}
					}
				}

				if (fakeCursor)
				{
					jsonArray.Add(new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>())
					{
						{"type", 1},
						{"time", 0},
						{"length", 0},
						{"position",  new JsonArray(new JsonValue[]{0, 0, 0})},
						{"rotation", new JsonArray(new JsonValue[]{0, 0, 0})},
						{"size", new JsonArray(new JsonValue[]{0, 0, 0})},
						{"transparency", 0},
						{"material", 0},
						{"appearance", 1},
						{"color", 0},
						{"animation", new JsonArray(Array.Empty<JsonValue>()) {
							new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
								{ "time", 0 },
								{ "ease", 0 },
								{ "position", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
								{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
								{ "size", new JsonArray(new JsonValue[] { 0.525, 0.525, 0.05 }) },
								{ "transparency", 1 }
							}
						}}
					});

					var cursor = jsonArray.Count - 1;

                    JsonArray cursorAnimation = new JsonArray(Array.Empty<JsonValue>())
					{
						new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
							{ "time", 0 },
							{ "ease", 0 },
							{ "position", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
							{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
							{ "size", new JsonArray(new JsonValue[] { 0.525, 0.525, 0.05 }) },
							{ "transparency", 1 }
						}, new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
							{ "time", 1 },
							{ "ease", 0 },
							{ "position", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
							{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 540 }) },
							{ "size", new JsonArray(new JsonValue[] { 0.525, 0.525, 0.05 }) },
							{ "transparency", 1 }
						}
					};

                    for (int i = jsonArray.Count-1; i > 0; i--)
					{
						var cursorObj = jsonArray[cursor];
						var note = jsonArray[i];

						if (note["type"] != 0)
							continue;

						cursorObj["length"] = note["time"] + 2; // add 2 seconds since it just disappers after this
						break;
					}

				for (int i = 0; i < jsonArray.Count; i++)
					{
						var cursorObj = jsonArray[cursor];
						var note = jsonArray[i];

						if (note["type"] != 0)
							continue;

						double ct = (note["time"] + offset) / cursorObj["length"];
						double nt = (note["time"] + offset) / cursorObj["length"];
						double td = (nt - ct);
						double tt = ct + (td / 2);

						//Console.WriteLine(i + ", " + td + ", " + tt);

						cursorAnimation.AddRange(new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
							{ "time", ct },
							{ "ease", 0 },
							{ "position", new JsonArray(new JsonValue[] { note["position"][0], note["position"][1], 0 }) },
							{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
							{ "size", new JsonArray(new JsonValue[] { 0.525, 0.525, 0.05 }) },
							{ "transparency", 0.25 }
						}, new JsonObject(Array.Empty<KeyValuePair<string, JsonValue>>()) {
							{ "time", tt},
							{ "ease", 0 },
							{ "position", new JsonArray(new JsonValue[] { note["position"][0], note["position"][1], 0 }) },
							{ "rotation", new JsonArray(new JsonValue[] { 0, 0, 0 }) },
							{ "size", new JsonArray(new JsonValue[] { 0.525, 0.525, 0.05 }) }, //{ 0.775, 0.775, 0.05 }) },
							{ "transparency", 0.25 }
						});
					}

					jsonArray[cursor]["animation"] = cursorAnimation;
				}

				Console.WriteLine(jsonArray.Count);

				jsonObject.Add("objects", jsonArray);
				jsonObject.Add("events", new JsonArray(Array.Empty<JsonValue>()));
				jsonObject.Add("tracks", new JsonArray(Array.Empty<JsonValue>()));
				isConvertingMap = false;
			/**/
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				isConvertingMap = false;
				return;
			}
			/**/
			output.Text = jsonObject.ToString();
		}

		private void Settings_Click(object sender, EventArgs e)
		{
			if (isConvertingMap)
			{
				return;
			}

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
