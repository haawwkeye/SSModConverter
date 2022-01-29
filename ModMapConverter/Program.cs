using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using Generator3;
using System.Collections;

namespace ModMapConverter
{
    internal class httpHandler
    {
        public readonly CookieContainer cJar = new CookieContainer();

        public readonly string UserAgent = @"Mozilla/5.0 (Windows; Windows NT 6.1) AppleWebKit/534.23 (KHTML, like Gecko) Chrome/11.0.686.3 Safari/534.23";

        public string HttpGet(string url)
        {
            HttpWebResponse response;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cJar;
                request.UserAgent = UserAgent;
                request.KeepAlive = false;
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "bruh");
                return "Error";
            }
        }
    }

    static class Program
    {
        public static httpHandler httpHandler { get; private set; } = new httpHandler();
        public static MainWindow mainWindow { get; private set; }
        public static SettingsWindow settingsWindow { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InternetCheckApp.StartInternetCheck(); // check internet status

            Console.Write("\n");

            if (!InternetCheckApp.disableinternet)
            {
                string json = httpHandler.HttpGet("https://api.github.com/repos/haawwkeye/SSModConverter/releases/latest");

                try
                {
                    JObject GitReturn = JObject.Parse(json);
                    float version = Properties.Settings.Default.Version;
                    string tag = GitReturn["tag_name"].ToString().Substring(1);

                    bool s1 = float.TryParse(tag, out float tagVerF);
                    bool s2 = int.TryParse(tag, out int tagVerI);
                    bool s3 = double.TryParse(tag, out double tagVerD);

                    Console.WriteLine(tag);

                    if ((s1 & tagVerF > version) || (s2 & tagVerI > version) || (s3 & tagVerD > version))
                    {
                        var result = MessageBox.Show("New version found would you like to update from v" + version.ToString() + " to " + tag, "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            var downloadLink = "https://github.com/haawwkeye/SSModConverter/releases/download/" + tag + "/ModMapConverter.zip";
                            DownloadHandler.UpdateHandler.StartDownload(downloadLink);
                            return; // oop we dont want to load the windows
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //return;
            }

            Properties.Settings.Default.SettingsSaving += PropSettings_SettingsSaving;
            Properties.Settings.Default.SettingChanging += PropSettings_SettingChanging;
            Properties.Settings.Default.SettingsLoaded += PropSettings_SettingsLoaded;

            Properties.Settings.Default.Upgrade(); // Update settings just in case

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainWindow = new MainWindow();
            settingsWindow = new SettingsWindow();

            mainWindow.settingsWindow = settingsWindow;
            settingsWindow.mainWindow = mainWindow;

            Application.ApplicationExit += Application_ApplicationExit;

            Application.Run(mainWindow);

            CoroutineHandler.StartUpdate();
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory(); // delete songs on close to save space

            if (Directory.Exists(path + "\\songs"))
            {
                Directory.Delete(path + "\\songs", true);
            }
        }

        private static void PropSettings_SettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            Console.WriteLine("Settings Loaded");
        }

        private static void PropSettings_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            Console.WriteLine("Settings Changing");
        }

        private static void PropSettings_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Settings Saving");
        }
    }
}


namespace DownloadHandler
{
    class UpdateHandler
    {
        public static string path { get; private set; } = Directory.GetCurrentDirectory();
        private readonly static WebClient wc = new WebClient();

        public static void StartDownload(string url)
        {
            DownloadFileInBackground(new Uri(url), "ModMapConverter");
        }

        static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rDownloading {0} % complete...", e.ProgressPercentage);
        }

        static void DownloadFileInBackground(Uri address, string v)
        {

            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

            wc.DownloadFileAsync(address, v + ".zip");
        }

        static void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            Console.Write("\n");
            Console.WriteLine("downloaded, extracting\n");

            string file = "ModMapConverter.zip";

            if (!Directory.Exists(path + "\\update"))
            {
                Directory.CreateDirectory(path + "\\update");
            }
            else
            {
                Directory.Delete(path + "\\update", true);
                Directory.CreateDirectory(path + "\\update");
            }

            ZipFile.ExtractToDirectory(file, path + "\\update");

            File.WriteAllText(path + "\\finishUpdate.bat", "@echo off\ntitle Finishing update...\nmove /Y .\\update\\* .\\\nRD /S /Q .\\update\nStart .\\ModMapConverter.exe\nDEL \"%~f0\" & EXIT");

            File.Delete(file);

            if (e.Error != null)
            {
                Console.WriteLine(e.Error.ToString());
                MessageBox.Show(e.Error.ToString(), "Error");
            }
            else
            {
                Process.Start(path + "\\finishUpdate.bat");
                Application.Exit();
            }
        }
    }

    class BSHandler
    {
        public static string name { get; private set; } = "";
        public static string path { get; private set; } = Directory.GetCurrentDirectory();
        private readonly static WebClient wc = new WebClient();

        static IEnumerator Init(string key)
        {
            bool exists = Directory.Exists(path + "\\songs\\" + key);

            while (!exists)
            {
                yield return Sched.Instance.StartCoroutine(Sched.WaitAboutSeconds(1));
            }

            yield break;
        }

        public static string StartDownload(string url, string key)
        {
            if (name != "")
            {
                return "Already running";
            }

            name = key;

            DownloadFileInBackground(new Uri(url), key);

            Sched.Coroutine coroutine = Sched.Instance.StartCoroutine(Init(key));

            return path + "\\songs\\" + key;
        }

        static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rDownloading {0} % complete...", e.ProgressPercentage);
        }

        static void DownloadFileInBackground(Uri address, string v)
        {
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

            wc.DownloadFileAsync(address, v + ".zip");
        }
        static void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            Console.Write("\n");
            Console.WriteLine("downloaded, extracting\n");

            string file = name + ".zip";

            if (!Directory.Exists(path + "\\songs"))
            {
                Directory.CreateDirectory(path + "\\songs");
            }

            if (!Directory.Exists(path + "\\songs\\" + name))
            {
                Directory.CreateDirectory(path + "\\songs\\" + name);
            }
            else
            {
                Directory.Delete(path + "\\songs\\" + name);
                Directory.CreateDirectory(path + "\\songs\\" + name);
            }

            ZipFile.ExtractToDirectory(file, path + "\\songs\\" + name);

            File.Delete(file);

            name = "";

            if (e.Error != null)
            {
                Console.WriteLine(e.Error.ToString());
                MessageBox.Show(e.Error.ToString(), "Error");
            }
        }
    }
}