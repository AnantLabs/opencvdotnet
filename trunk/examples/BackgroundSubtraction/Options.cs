using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OpenCVDotNet;

namespace OpenCVDotNet.Examples
{
    public partial class Options : Form
    {
        const double THRESHOLD = 0.1;
        const double ALPHA = 0.2;
        const int THRESHOLD_RESOLUTION = 10;
        const int ALPHA_RESOLUTION = 100;

        CVRgbPixel backgroundColor = new CVRgbPixel(0, 0, 0);
        CVRgbPixel foregroundColor = new CVRgbPixel(255, 255, 255);

        private string videoFile;
        private CVVideoWriter vw = null;
        private MovingAverage avg = null;
        private CVCapture cap = null;
        private Thread worker;

        public Options()
        {
            InitializeComponent();

            thresholdScroll.SetRange(-THRESHOLD_RESOLUTION, THRESHOLD_RESOLUTION);
            alphaScroll.SetRange(0, ALPHA_RESOLUTION);

            Alpha = ALPHA;
            Threshold = THRESHOLD;
        }

        public double Alpha
        {
            get
            {
                return ((double)alphaScroll.Value) / ALPHA_RESOLUTION;
            }

            set
            {
                alphaScroll.Value = (int) (value * ALPHA_RESOLUTION);
                if (avg != null) avg.Alpha = value;
                alphaValue.Text = value.ToString();
            }
        }

        private double threshold;
        public double Threshold
        {
            get
            {
                return threshold;
            }

            set
            {
                threshold = value;
                thresholdScroll.Value = (int)(value * THRESHOLD_RESOLUTION);
                thresholdValue.Text = value.ToString();
            }
        }
        
        private void Options_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Input Video File (Cancel for Camera)";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                videoFile = null;
            }
            else
            {
                videoFile = ofd.FileName;
            }

            ReopenCapture();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select Output Video File (Cancel for no output)";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.vw = new CVVideoWriter(sfd.FileName, cap.Width, cap.Height);
            }

            this.avg = new MovingAverage(cap.Width, cap.Height, Alpha);
            this.worker = new Thread(new ThreadStart(Work));
            this.worker.Start();
        }

        private void ReopenCapture()
        {
            if (cap != null) cap.Release();

            if (videoFile == null) cap = new CVCapture();
            else cap = new CVCapture(videoFile);
        }


        private void Work()
        {
            CVImage frame;

            try
            {
                while (true)
                {
                    ReopenCapture();

                    while ((frame = cap.QueryFrame()) != null)
                    {
                        for (int row = 0; row < frame.Height; ++row)
                        {
                            for (int col = 0; col < frame.Width; ++col)
                            {
                                CVRgbPixel pixel = frame[row, col];

                                // get black and white value for this pixel.
                                byte bwValue = pixel.BwValue;

                                // accumulate the new value to the moving average.
                                double val = (double)bwValue / 255.0;
                                avg.Accumulate(row, col, val);

                                // calculate the diff between the frame and the average up until now.
                                double delta = val - avg[row, col];

                                // if delta is smaller than the threshold, eliminate the background.
                                if (delta < Threshold)
                                {
                                    // set the color of the background frame to black.
                                    frame[row, col] = backgroundColor;
                                }
                                else
                                {
                                    // if monochrome is checked, set pixel to foreground color.
                                    if (monochrome.Checked)
                                    {
                                        frame[row, col] = foregroundColor;
                                    }
                                }
                            }
                        }

                        // display the updated frame on the window.
                        cvPanel.Image = frame.ToBitmap();

                        if (vw != null) vw.WriteFrame(frame);

                        // delay for 20ms.
                        Thread.Sleep(20);

                        frame.Release();
                    }
                }
            }
            catch (CVException e1)
            {
                MessageBox.Show(e1.Message);
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Aborting thread...");
            }
            finally
            {
                if (cap != null) cap.Release();
                if (vw != null) vw.Release();
            }
        }

        private void alphaScroll_Scroll(object sender, EventArgs e)
        {
            Alpha = ((double)alphaScroll.Value) / ALPHA_RESOLUTION;
        }

        private void thresholdScroll_Scroll(object sender, EventArgs e)
        {
            Threshold = (double)thresholdScroll.Value / THRESHOLD_RESOLUTION;
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.worker != null)
            {
                this.worker.Abort();
                this.worker.Join();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}