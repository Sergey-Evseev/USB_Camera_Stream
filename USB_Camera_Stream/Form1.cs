using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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
        private Bitmap currentFrame; // Store the current frame in a separate variable
        private bool isProcessingFrame; // Track if a frame is currently being processed
        //variables for dragging events
        private bool isDraggingForm = false;
        private Point startPoint;
        
        public Form1()
        {
            InitializeComponent();
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Default;

            //dragging events
            // Wire up the mouse drag event handlers to the form
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;

            // Add event handlers for MouseEnter and MouseLeave
            pictureBox1.MouseEnter += PictureBox1_MouseEnter;
            pictureBox1.MouseLeave += PictureBox1_MouseLeave;

            //Attach event handler to the MouseDoubleClick event of the form
            pictureBox1.MouseDoubleClick += PictureBox1_MouseDoubleClick;
            
            // Set KeyPreview property to true (ESC click event)
            this.KeyPreview = true;
            // Add ESC KeyDown event handler
            this.KeyDown += Form1_KeyDown;

            //Set the initial visibility of the controls to false
            resolutionDropdown.Visible = false;
            brightnessSlider.Visible = false;
            contrastSlider.Visible = false;
            brightnessLabel.Visible = false;
            contrastLabel.Visible = false;
            resolutionLabel.Visible = false;            
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

            // Set default values for brightness and contrast sliders
            brightnessSlider.Value = 100;
            contrastSlider.Value = 0;
            brightnessLabel.Text = $"Brightness: {brightnessSlider.Value}";
            contrastLabel.Text = $"Contrast: {contrastSlider.Value}";
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
        //Mouse drag handlers ///////////////        
        
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if dragging the form is in progress
            if (isDraggingForm)
            {
                // Calculate the new position of the form
                int deltaX = e.X - startPoint.X;
                int deltaY = e.Y - startPoint.Y;
                int newX = this.Left + deltaX;
                int newY = this.Top + deltaY;

                // Update the form position
                this.Location = new Point(newX, newY);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if dragging is in progress
            if (isDraggingForm)
            {
                // Calculate the new position of the form
                int deltaX = e.X - startPoint.X;
                int deltaY = e.Y - startPoint.Y;
                int newX = this.Left + deltaX;
                int newY = this.Top + deltaY;

                // Update the form position
                this.Location = new Point(newX, newY);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Check if the left mouse button is released
            if (e.Button == MouseButtons.Left)
            {
                // Reset the dragging flag for the form
                isDraggingForm = false;
            }
        }
        //End of region Mouse drag handlers//////////////

        //Mouse enter handlers
        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            // Show the controls when mouse enters the form
            resolutionDropdown.Visible = true;
            brightnessSlider.Visible = true;
            contrastSlider.Visible = true;
            brightnessLabel.Visible = true;
            contrastLabel.Visible = true;
            resolutionLabel.Visible = true;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            // Check if the mouse is not over any of the controls
            if (!resolutionDropdown.Bounds.Contains(PointToClient(MousePosition))
                && !brightnessSlider.Bounds.Contains(PointToClient(MousePosition))
                && !contrastSlider.Bounds.Contains(PointToClient(MousePosition))
                && !brightnessLabel.Bounds.Contains(PointToClient(MousePosition))
                && !contrastLabel.Bounds.Contains(PointToClient(MousePosition)))
            {
                // Set the visibility of the controls to false when the mouse leaves the picture box
                resolutionDropdown.Visible = false;
                brightnessSlider.Visible = false;
                contrastSlider.Visible = false;
                brightnessLabel.Visible = false;
                contrastLabel.Visible = false;
                resolutionLabel.Visible = false;
            }
        }
        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Toogle between normal and maximized state
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;

            }
        }
        // Event handler for KeyDown event
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Esc key is pressed
            if (e.KeyCode == Keys.Escape)
            {
                // Set the form's WindowState to Normal
                WindowState = FormWindowState.Normal;
            }
        }


        // NewFrame event of the VideoCaptureDevice to capture new frames from the camera
        // and display them in the PictureBox control.
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!isProcessingFrame)
            {
                isProcessingFrame = true;

                // Create a new Bitmap instance to hold the captured frame
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

                // Perform image processing tasks in a separate thread using Task.Run
                Task.Run(() =>
                {
                    // Apply manual low light adjustment to the frame
                    int brightnessValue = brightnessSlider.Value; // Get the brightness value from a slider control
                    int contrastValue = contrastSlider.Value; // Get the contrast value from a slider control

                    AdjustBrightness(frame, brightnessValue);
                    AdjustContrast(frame, contrastValue);

                    // Store the processed frame in the currentFrame variable
                    currentFrame = frame;

                    // Invoke the display method on the UI thread to update the PictureBox
                    Invoke((MethodInvoker)DisplayCurrentFrame);

                    isProcessingFrame = false;
                });
            }
        }

        private void DisplayCurrentFrame()
        {
            // Display the current frame in the PictureBox
            pictureBox1.Image = currentFrame;
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
                // Calculate the desired width for the PictureBox
                int desiredWidth = (int)(ClientSize.Width * 1.2+65); // Adjust the factor as needed (e.g., 0.9 for 90% width)

                // Reset the PictureBox size and location
                pictureBox1.Size = new Size(desiredWidth, (int)(desiredWidth * 9 / 16)); // Assuming 16:9 aspect ratio                
                pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2,
                                                 (ClientSize.Height - pictureBox1.Height) / 2 + 4);
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
            var device = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString); //get device
            var resolution = device.VideoCapabilities.ElementAtOrDefault(resolutionIndex); //get device resolution

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

            // Lock the bitmap bits for faster pixel manipulation
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr ptr = bitmapData.Scan0;

            // Calculate the stride (number of bytes per scanline) of the bitmap
            int stride = bitmapData.Stride;

            // Calculate the total number of bytes in the bitmap
            int bytes = Math.Abs(stride) * image.Height;

            // Create a byte array to copy the bitmap data
            byte[] rgbValues = new byte[bytes];

            // Copy the bitmap data to the byte array
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Apply brightness adjustment to each pixel in the byte array
            for (int i = 0; i < rgbValues.Length; i += 3)
            {
                int red = (int)(rgbValues[i] * brightnessFactor);
                int green = (int)(rgbValues[i + 1] * brightnessFactor);
                int blue = (int)(rgbValues[i + 2] * brightnessFactor);

                // Clamp the values to the valid range (0-255)
                red = Math.Max(0, Math.Min(red, 255));
                green = Math.Max(0, Math.Min(green, 255));
                blue = Math.Max(0, Math.Min(blue, 255));

                rgbValues[i] = (byte)red;
                rgbValues[i + 1] = (byte)green;
                rgbValues[i + 2] = (byte)blue;
            }

            // Copy the modified byte array back to the bitmap data
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bitmap bits
            image.UnlockBits(bitmapData);
        }

        private void AdjustContrast(Bitmap image, int contrastValue)
        {
            float contrastFactor = (contrastValue + 100) / 100f;

            if (contrastFactor < 0)
                contrastFactor = 0;
            else if (contrastFactor > 2)
                contrastFactor = 2;

            // Lock the bitmap bits for faster pixel manipulation
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr ptr = bitmapData.Scan0;

            // Calculate the stride (number of bytes per scanline) of the bitmap
            int stride = bitmapData.Stride;

            // Calculate the total number of bytes in the bitmap
            int bytes = Math.Abs(stride) * image.Height;

            // Create a byte array to copy the bitmap data
            byte[] rgbValues = new byte[bytes];

            // Copy the bitmap data to the byte array
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Apply contrast adjustment to each pixel in the byte array
            for (int i = 0; i < rgbValues.Length; i += 3)
            {
                float red = (rgbValues[i] / 255f - 0.5f) * contrastFactor + 0.5f;
                float green = (rgbValues[i + 1] / 255f - 0.5f) * contrastFactor + 0.5f;
                float blue = (rgbValues[i + 2] / 255f - 0.5f) * contrastFactor + 0.5f;

                // Clamp the values to the valid range (0-255)
                red = Math.Max(0, Math.Min(red, 1));
                green = Math.Max(0, Math.Min(green, 1));
                blue = Math.Max(0, Math.Min(blue, 1));

                rgbValues[i] = (byte)(red * 255);
                rgbValues[i + 1] = (byte)(green * 255);
                rgbValues[i + 2] = (byte)(blue * 255);
            }

            // Copy the modified byte array back to the bitmap data
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bitmap bits
            image.UnlockBits(bitmapData);
        }


        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}
