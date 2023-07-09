using System;

namespace USB_Camera_Stream
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resolutionDropdown = new MetroFramework.Controls.MetroComboBox();
            this.brightnessSlider = new MetroFramework.Controls.MetroTrackBar();
            this.contrastSlider = new MetroFramework.Controls.MetroTrackBar();
            this.brightnessLabel = new MetroFramework.Controls.MetroLabel();
            this.contrastLabel = new MetroFramework.Controls.MetroLabel();
            this.resolutionLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(813, 536);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // resolutionDropdown
            // 
            this.resolutionDropdown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resolutionDropdown.FormattingEnabled = true;
            this.resolutionDropdown.ItemHeight = 23;
            this.resolutionDropdown.Location = new System.Drawing.Point(663, 578);
            this.resolutionDropdown.Name = "resolutionDropdown";
            this.resolutionDropdown.Size = new System.Drawing.Size(123, 29);
            this.resolutionDropdown.TabIndex = 1;
            this.resolutionDropdown.UseSelectable = true;
            this.resolutionDropdown.SelectedIndexChanged += new System.EventHandler(this.resolutionDropdown_SelectedIndexChanged);
            // 
            // brightnessSlider
            // 
            this.brightnessSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.brightnessSlider.BackColor = System.Drawing.Color.Transparent;
            this.brightnessSlider.Location = new System.Drawing.Point(31, 574);
            this.brightnessSlider.Name = "brightnessSlider";
            this.brightnessSlider.Size = new System.Drawing.Size(156, 39);
            this.brightnessSlider.TabIndex = 2;
            this.brightnessSlider.Text = "metroTrackBar1";
            this.brightnessSlider.ValueChanged += new System.EventHandler(this.brightnessSlider_ValueChanged);
            // 
            // contrastSlider
            // 
            this.contrastSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.contrastSlider.BackColor = System.Drawing.Color.Transparent;
            this.contrastSlider.Location = new System.Drawing.Point(311, 581);
            this.contrastSlider.Name = "contrastSlider";
            this.contrastSlider.Size = new System.Drawing.Size(145, 32);
            this.contrastSlider.TabIndex = 3;
            this.contrastSlider.Text = "metroTrackBar1";
            this.contrastSlider.ValueChanged += new System.EventHandler(this.contrastSlider_ValueChanged);
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(193, 581);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(71, 19);
            this.brightnessLabel.TabIndex = 4;
            this.brightnessLabel.Text = "Brightness:";
            // 
            // contrastLabel
            // 
            this.contrastLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.contrastLabel.AutoSize = true;
            this.contrastLabel.Location = new System.Drawing.Point(462, 584);
            this.contrastLabel.Name = "contrastLabel";
            this.contrastLabel.Size = new System.Drawing.Size(65, 19);
            this.contrastLabel.TabIndex = 5;
            this.contrastLabel.Text = "Contrast: ";
            // 
            // resolutionLabel
            // 
            this.resolutionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resolutionLabel.AutoSize = true;
            this.resolutionLabel.Location = new System.Drawing.Point(585, 584);
            this.resolutionLabel.Name = "resolutionLabel";
            this.resolutionLabel.Size = new System.Drawing.Size(72, 19);
            this.resolutionLabel.TabIndex = 6;
            this.resolutionLabel.Text = "Resolution:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 623);
            this.Controls.Add(this.resolutionLabel);
            this.Controls.Add(this.contrastLabel);
            this.Controls.Add(this.brightnessLabel);
            this.Controls.Add(this.contrastSlider);
            this.Controls.Add(this.brightnessSlider);
            this.Controls.Add(this.resolutionDropdown);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroComboBox resolutionDropdown;
        private MetroFramework.Controls.MetroTrackBar brightnessSlider;
        private MetroFramework.Controls.MetroTrackBar contrastSlider;
        private MetroFramework.Controls.MetroLabel brightnessLabel;
        private MetroFramework.Controls.MetroLabel contrastLabel;
        private MetroFramework.Controls.MetroLabel resolutionLabel;
    }
}

