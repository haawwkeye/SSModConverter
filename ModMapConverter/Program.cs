﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading;

namespace ModMapConverter
{
    internal class httpHandler
    {
        public readonly CookieContainer cJar = new CookieContainer();

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
                    string tag = GitReturn["tag_name"].ToString();
                    //tag.Remove(0);

                    float.TryParse(tag, out float tagVerF);
                    int.TryParse(tag, out int tagVerI);
                    double.TryParse(tag, out double tagVerD);

                    if ((tagVerF != null & tagVerF > version) || (tagVerI != null & tagVerI > version) || (tagVerD != null & tagVerD > version))
                    {
                        var result = MessageBox.Show("New version found would you like to update from v" + version.ToString() + " to " + tag, "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            var downloadLink = "https://github.com/haawwkeye/SSModConverter/releases/download/" + tag + "/ModMapConverter.zip";
                            DownloadHandler.Handler.StartDownload(downloadLink);
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

            Application.Run(mainWindow);
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
    class Handler
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
}