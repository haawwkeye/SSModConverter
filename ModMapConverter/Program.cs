using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModMapConverter
{
    static class Program
    {
        public static MainWindow mainWindow { get; private set; }
        public static SettingsWindow settingsWindow { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
