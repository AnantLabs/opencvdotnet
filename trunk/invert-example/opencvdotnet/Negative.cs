using System;
using System.Windows.Forms;
using OpenCVDotNet;

namespace NegativeSample
{
    public partial class Negative : Form
    {
        private CVVideoWriter writer;

        public Negative()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the button is clicked to open the video/image.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // show file dialog, and continue only if OK was chosen.
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            // open and play the file (errors are not handled nicely here).
            videoPlayer1.Open(dialog.FileName);

            // open the video writer (close previously opened writer).
            if (writer != null) writer.Release();
            writer = videoPlayer1.CreateVideoWriter("c:\\myoutput.avi");
            
            // start the video.
            videoPlayer1.Play();
        }

        /// <summary>
        /// Called by the video player to handle the next video frame.
        /// </summary>
        private void videoPlayer1_NextFrame(OpenCVDotNet.UI.VideoPlayer sender, OpenCVDotNet.UI.NextFrameEventArgs args)
        {
            // args.Frame contains the frame to be handled.
            CVImage frame = args.Frame;

            // go over all the pixels (rows, cols)
            for (int y = 0; y < frame.Height; ++y)
            {
                for (int x = 0; x < frame.Width; ++x)
                {
                    CVRgbPixel pixel = frame[y, x];

                    // invert RGB colors.
                    frame[y, x] = new CVRgbPixel(
                        (byte)(255 - pixel.R),
                        (byte)(255 - pixel.G),
                        (byte)(255 - pixel.B));
                }
            }
            
            // assign resulting frame to picture box as a bitmap.
            pictureBox1.Image = frame.ToBitmap();
            writer.WriteFrame(frame);
        }

        /// <summary>
        /// Called when the form is perform cleanups.
        /// </summary>
        private void Negative_FormClosing(object sender, FormClosingEventArgs e)
        {
            writer.Release();
        }

        #region Program entry point
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Negative());
        }
        #endregion
    }
}