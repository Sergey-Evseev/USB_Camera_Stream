using System;
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

namespace USB_Camera_Stream
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Enumerate available video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No video devices found.");
                return;
            }


            //Select the first device
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop the video source
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;  
            }
        }


        //NewFrame event of the VideoCaptureDevice to capture new frames from the camera
        //and display them in the PictureBox control.
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
        }

        
    }//end of public partial class Form1

} //end of namespace USB_Camera_Stream
