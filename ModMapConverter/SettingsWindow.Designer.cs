
using ModMapConverter.Properties;
using System.Drawing;
using System.Windows.Forms;


namespace ModMapConverter
{
    partial class SettingsWindow
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
            this.CloseBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.Change_Colors = new System.Windows.Forms.Button();
            this.Change_Colors_Text = new System.Windows.Forms.Label();
            this.Notes_change_fog_text = new System.Windows.Forms.Label();
            this.Notes_change_fog = new System.Windows.Forms.CheckBox();
            this.desc = new System.Windows.Forms.Label();
            this.SettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(12, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(240, 48);
            this.title.TabIndex = 0;
            this.title.Text = "Settings";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Location = new System.Drawing.Point(138, 205);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(114, 24);
            this.CloseBtn.TabIndex = 3;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.Close_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.Location = new System.Drawing.Point(16, 205);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(116, 24);
            this.SaveBtn.TabIndex = 6;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.Save_Click);
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.AutoScroll = true;
            this.SettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SettingsPanel.Controls.Add(this.Change_Colors);
            this.SettingsPanel.Controls.Add(this.Change_Colors_Text);
            this.SettingsPanel.Controls.Add(this.Notes_change_fog_text);
            this.SettingsPanel.Controls.Add(this.Notes_change_fog);
            this.SettingsPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.SettingsPanel.Location = new System.Drawing.Point(16, 91);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(236, 113);
            this.SettingsPanel.TabIndex = 7;
            // 
            // Change_Colors
            // 
            this.Change_Colors.AutoSize = true;
            this.Change_Colors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Change_Colors.Location = new System.Drawing.Point(0, 72);
            this.Change_Colors.Name = "Change_Colors";
            this.Change_Colors.Size = new System.Drawing.Size(232, 23);
            this.Change_Colors.TabIndex = 4;
            this.Change_Colors.Text = "Change Colors";
            this.Change_Colors.UseVisualStyleBackColor = true;
            this.Change_Colors.Visible = false;
            // 
            // Change_Colors_Text
            // 
            this.Change_Colors_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Change_Colors_Text.Location = new System.Drawing.Point(0, 52);
            this.Change_Colors_Text.Name = "Change_Colors_Text";
            this.Change_Colors_Text.Size = new System.Drawing.Size(231, 17);
            this.Change_Colors_Text.TabIndex = 3;
            this.Change_Colors_Text.Text = "Change the colors from default";
            this.Change_Colors_Text.Visible = false;
            // 
            // Notes_change_fog_text
            // 
            this.Notes_change_fog_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Notes_change_fog_text.Location = new System.Drawing.Point(1, 0);
            this.Notes_change_fog_text.Name = "Notes_change_fog_text";
            this.Notes_change_fog_text.Size = new System.Drawing.Size(228, 26);
            this.Notes_change_fog_text.TabIndex = 1;
            this.Notes_change_fog_text.Text = "Automatically allow notes to change the fog";
            // 
            // Notes_change_fog
            // 
            this.Notes_change_fog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Notes_change_fog.Location = new System.Drawing.Point(1, 29);
            this.Notes_change_fog.Name = "Notes_change_fog";
            this.Notes_change_fog.Size = new System.Drawing.Size(228, 20);
            this.Notes_change_fog.TabIndex = 0;
            this.Notes_change_fog.Text = "Notes change fog";
            this.Notes_change_fog.UseVisualStyleBackColor = true;
            // 
            // desc
            // 
            this.desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desc.Location = new System.Drawing.Point(12, 56);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(240, 32);
            this.desc.TabIndex = 0;
            this.desc.Text = "i have no idea what to put here";
            this.desc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 241);
            this.Controls.Add(this.SettingsPanel);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod Map Converter by Kermeet";
            this.TopMost = true;
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label title;

        private Button CloseBtn;

        private Button SaveBtn;

        private Panel SettingsPanel;

        private Label desc;

        private CheckBox Notes_change_fog;

        private Label Notes_change_fog_text;
        private Button Change_Colors;
        private Label Change_Colors_Text;
    }
}
