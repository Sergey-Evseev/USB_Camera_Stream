﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using MetroFramework;
using MetroFramework.Forms;

namespace USB_Camera_Stream
{
    public partial class Form1 : MetroForm
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public Form1()
        {
            InitializeComponent();
            this.Style = MetroFramework.MetroColorStyle.Magenta;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Enumerate available video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No video devices found.");
                return;
            }
            // Populate the resolution dropdown list with the available resolutions of the first device
            PopulateResolutionList(0);

            // Select the first device
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            SetResolution(0, 0); // Set the default resolution of the first device
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the video source
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        // NewFrame event of the VideoCaptureDevice to capture new frames from the camera
        // and display them in the PictureBox control.
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            // Apply manual low light adjustment to the frame
            int brightnessValue = brightnessSlider.Value; // Get the brightness value from a slider control
            int contrastValue = contrastSlider.Value; // Get the contrast value from a slider control

            AdjustBrightness(frame, brightnessValue);
            AdjustContrast(frame, contrastValue);

            pictureBox1.Image = frame;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Check if the form is in full-screen mode
            if (WindowState == FormWindowState.Maximized)
            {
                // Expand the PictureBox to fill the entire form
                pictureBox1.Size = new Size(ClientSize.Width, ClientSize.Height);
                pictureBox1.Location = new Point(0, 0);
            }
            else
            {
                // Reset the PictureBox size and location
                pictureBox1.Size = new Size(960, 540); // or whatever size you choose
                pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2,
                                                 (ClientSize.Height - pictureBox1.Height) / 2);
            }
        }

        private void PopulateResolutionList(int deviceIndex)
        {
            // Get the available resolutions of the selected device
            VideoCaptureDevice device = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            List<string> resolutions = new List<string>();
            foreach (var capability in device.VideoCapabilities)
            {
                string resolution = string.Format("{0}x{1}", capability.FrameSize.Width, capability.FrameSize.Height);
                if (!resolutions.Contains(resolution))
                {
                    resolutions.Add(resolution);
                }
            }

            // Populate the resolution dropdown list with the available resolutions
            resolutionDropdown.DataSource = resolutions;
        }

        private void SetResolution(int deviceIndex, int resolutionIndex)
        {
            // Set the resolution of the selected device
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }

            // Get the selected resolution
            var device = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            var resolution = device.VideoCapabilities.ElementAtOrDefault(resolutionIndex);

            if (resolution != null)
            {
                device.VideoResolution = resolution;
                videoSource.Start();
            }
        }

        private void resolutionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the resolution of the selected device
            SetResolution(0, resolutionDropdown.SelectedIndex);
        }

        private void brightnessSlider_ValueChanged(object sender, EventArgs e)
        {
            // Update the brightness value
            brightnessLabel.Text = $"Brightness: {brightnessSlider.Value}";
        }

        private void contrastSlider_ValueChanged(object sender, EventArgs e)
        {
            // Update the contrast value
            contrastLabel.Text = $"Contrast: {contrastSlider.Value}";
        }

        private void AdjustBrightness(Bitmap image, int brightnessValue)
        {
            float brightnessFactor = brightnessValue / 100f;

            if (brightnessFactor < 0)
                brightnessFactor = 0;
            else if (brightnessFactor > 2)
                brightnessFactor = 2;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixelColor = image.GetPixel(i, j);
                    int red = (int)(pixelColor.R * brightnessFactor);
                    int green = (int)(pixelColor.G * brightnessFactor);
                    int blue = (int)(pixelColor.B * brightnessFactor);
                    image.SetPixel(i, j, Color.FromArgb(pixelColor.A, red, green, blue));
                }
            }
        }

        private void AdjustContrast(Bitmap image, int contrastValue)
        {
            float contrastFactor = (contrastValue + 100) / 100f;

            if (contrastFactor < 0)
                contrastFactor = 0;
            else if (contrastFactor > 2)
                contrastFactor = 2;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixelColor = image.GetPixel(i, j);
                    int red = (int)(((pixelColor.R / 255f - 0.5f) * contrastFactor + 0.5f) * 255f);
                    int green = (int)(((pixelColor.G / 255f - 0.5f) * contrastFactor + 0.5f) * 255f);
                    int blue = (int)(((pixelColor.B / 255f - 0.5f) * contrastFactor + 0.5f) * 255f);
                    image.SetPixel(i, j, Color.FromArgb(pixelColor.A, Clamp(red, 0, 255), Clamp(green, 0, 255), Clamp(blue, 0, 255)));
                }
            }
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}
