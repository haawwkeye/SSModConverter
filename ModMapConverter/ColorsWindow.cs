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
    public partial class ColorsWindow : Form
    {
        public SettingsWindow settingsWindow { get; internal set; }

        public ColorsWindow()
        {
            InitializeComponent();
            FormClosing += Settings_FormClosing; // Event for when the form starts to close
        }

        public void LoadSettings()
        {
            var manager = Properties.Resources.ResourceManager;
            var settings = Properties.Settings.Default;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Hide();
            settingsWindow.runningColors = false; // set to false since the settings are done
        }

        private void Save_Click(object sender, EventArgs e)
        {
            bool settingsChanged = false;
            //Properties.Settings.Default.Upgrade(); // update in case there is any missing settings

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
            settingsWindow.colorsWindow = new ColorsWindow(); // since the form is closing we need to provide a new form for settings
            settingsWindow.runningColors = false; // set to false since the settings are done
        }
    }
}
