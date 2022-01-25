
using System.Drawing;
using System.Windows.Forms;

namespace ModMapConverter
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.desc = new System.Windows.Forms.Label();
            this.convertBtn = new System.Windows.Forms.Button();
            this.input = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.Settings = new System.Windows.Forms.Button();
            this.Version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(12, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(240, 48);
            this.title.TabIndex = 0;
            this.title.Text = "SS to Modded JSON Converter";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // desc
            // 
            this.desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desc.Location = new System.Drawing.Point(12, 56);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(240, 32);
            this.desc.TabIndex = 0;
            this.desc.Text = "This tool will convert any original Sound Space map into a JSON for use in mod ch" +
    "arts.";
            this.desc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // convertBtn
            // 
            this.convertBtn.Location = new System.Drawing.Point(138, 205);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(114, 24);
            this.convertBtn.TabIndex = 3;
            this.convertBtn.Text = "Convert";
            this.convertBtn.UseVisualStyleBackColor = true;
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(16, 91);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(116, 113);
            this.input.TabIndex = 4;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(136, 91);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(116, 113);
            this.output.TabIndex = 5;
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(16, 205);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(116, 24);
            this.Settings.TabIndex = 6;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.BackColor = System.Drawing.Color.Transparent;
            this.Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.ForeColor = Color.FromArgb(255, 220, 220, 220);
            this.Version.Location = new System.Drawing.Point(0, 0);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(22, 13);
            this.Version.TabIndex = 7;
            this.Version.Text = "V" + Properties.Settings.Default.Version.ToString();
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 241);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.output);
            this.Controls.Add(this.input);
            this.Controls.Add(this.convertBtn);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod Map Converter by Kermeet";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Label title;

        private Label desc;

        private Button convertBtn;

        private TextBox input;

        private TextBox output;

        private Button Settings;

        private Label Version;
    }
}
