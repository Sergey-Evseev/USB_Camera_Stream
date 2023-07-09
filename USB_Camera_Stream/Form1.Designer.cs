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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(960, 540);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // resolutionDropdown
            // 
            this.resolutionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resolutionDropdown.FormattingEnabled = true;
            this.resolutionDropdown.ItemHeight = 23;
            this.resolutionDropdown.Location = new System.Drawing.Point(918, 523);
            this.resolutionDropdown.Name = "resolutionDropdown";
            this.resolutionDropdown.Size = new System.Drawing.Size(154, 29);
            this.resolutionDropdown.TabIndex = 1;
            this.resolutionDropdown.UseSelectable = true;
            this.resolutionDropdown.SelectedIndexChanged += new System.EventHandler(this.resolutionDropdown_SelectedIndexChanged);
            // 
            // brightnessSlider
            // 
            this.brightnessSlider.BackColor = System.Drawing.Color.Transparent;
            this.brightnessSlider.Location = new System.Drawing.Point(92, 607);
            this.brightnessSlider.Name = "brightnessSlider";
            this.brightnessSlider.Size = new System.Drawing.Size(169, 42);
            this.brightnessSlider.TabIndex = 2;
            this.brightnessSlider.Text = "metroTrackBar1";
            this.brightnessSlider.ValueChanged += new System.EventHandler(this.brightnessSlider_ValueChanged);
            // 
            // contrastSlider
            // 
            this.contrastSlider.BackColor = System.Drawing.Color.Transparent;
            this.contrastSlider.Location = new System.Drawing.Point(522, 607);
            this.contrastSlider.Name = "contrastSlider";
            this.contrastSlider.Size = new System.Drawing.Size(166, 32);
            this.contrastSlider.TabIndex = 3;
            this.contrastSlider.Text = "metroTrackBar1";
            this.contrastSlider.ValueChanged += new System.EventHandler(this.contrastSlider_ValueChanged);
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(283, 620);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(0, 0);
            this.brightnessLabel.TabIndex = 4;
            // 
            // contrastLabel
            // 
            this.contrastLabel.AutoSize = true;
            this.contrastLabel.Location = new System.Drawing.Point(744, 620);
            this.contrastLabel.Name = "contrastLabel";
            this.contrastLabel.Size = new System.Drawing.Size(0, 0);
            this.contrastLabel.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 672);
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
    }
}

