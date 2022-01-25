using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModMapConverter
{
    public partial class SettingsWindow : Form
    {
        public MainWindow mainWindow { get; internal set; }

        public SettingsWindow()
        {
            InitializeComponent();
            FormClosing += Settings_FormClosing; // Event for when the form starts to close
        }

        public void LoadSettings()
        {
            var manager = Properties.Resources.ResourceManager;
            var settings = Properties.Settings.Default;

            Notes_change_fog_text.Text = manager.GetString("Notes_change_fog");
            Notes_change_fog.Checked = settings.Notes_change_fog;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Hide();
            mainWindow.Show();
            mainWindow.runningSettings = false; // set to false since the settings are done
        }

        private void Save_Click(object sender, EventArgs e)
        {
            bool settingsChanged = false;
            //Properties.Settings.Default.Upgrade(); // update in case there is any missing settings

            if (Properties.Settings.Default.Notes_change_fog != Notes_change_fog.Checked)
            {
                settingsChanged = true;
                Properties.Settings.Default.Notes_change_fog = Notes_change_fog.Checked; // set to new setting
            }

            if (settingsChanged == true)
            {
                Properties.Settings.Default.Save(); // save settings
                Close_Click(sender, e); // close after save
            }
            else
            {
                MessageBox.Show("No change detected", "bruh", MessageBoxButtons.OK);
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainWindow.Show(); // Show the MainWindow
            mainWindow.settingsWindow = new SettingsWindow(); // since the form is closing we need to provide a new form for settings
            mainWindow.runningSettings = false; // set to false since the settings are done
        }
    }
}
