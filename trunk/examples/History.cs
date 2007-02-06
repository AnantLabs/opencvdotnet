using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;
using OpenCVDotNet.UI;

namespace OpenCVDotNet.Examples
{
    public partial class History : Form
    {
        private CVHistogram selectionHistogram;
        private double[,] bgAccum;
        private CVImage bgFrame;


        public History()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;

            videoPlayer.Open(ofd.FileName);
        }

        private void videoPlayer_NextFrame(VideoPlayer sender, NextFrameEventArgs args)
        {
            video.Image = videoPlayer.LastFrame.ToBitmap();
            ProcessMeanShift();
            ProcessBackgroundSubtraction();
        }

        private void ProcessMeanShift()
        {
            if (selectionHistogram == null) return;

            using (CVImage lastFrameClone = videoPlayer.LastFrame.Clone())
            {
                using (CVImage bp = videoPlayer.LastFrame.CalcBackProject(selectionHistogram))
                {
                    CVConnectedComp cc = bp.MeanShift(video.SelectionRect, 100);
                    video.SelectionRect = cc.Rect;
                }
            }
        }

        private bool MyRGCriteria(Point startPt, Point candidatePt, object frameCookie)
        {
            CVImage frame = frameCookie as CVImage;

            return frame[startPt].GrayLevel - frame[candidatePt].GrayLevel == 0;
        }

        private void video_SelectionChanged(object sender, EventArgs e)
        {
            if (videoPlayer.LastFrame == null) return;

            using (CVImage region = videoPlayer.LastFrame.CopyRegion(video.SelectionRect))
            {
                selectionHistogram = region.CalcHistogram(30);
                //histo.ShowHistogram(region);
            }

            ProcessMeanShift();
        }

        #region Toolbar Button Events

        private void playButton_Click(object sender, EventArgs e)
        {
            videoPlayer.Play();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            videoPlayer.Pause();
        }

        #endregion

        private double alpha = 0.2;
        private double threshold = 0.1;
        private const int STEP_SIZE = 5;

        private void ProcessBackgroundSubtraction()
        {
            CVImage frame = videoPlayer.LastFrame;

            // access last frame from video player.
            for (int row = 0; row < frame.Height; ++row)
            {
                for (int col = 0; col < frame.Width; ++col)
                {
                    CVRgbPixel pixel = frame[row, col];

                    // get black and white value for this pixel.
                    int bwValue = pixel.BwValue;

                    // accumulate the new value to the moving average.
                    double val = (double)bwValue / 255.0;

                    if (bgAccum[row, col] == -1)
                    {
                        bgAccum[row, col] = val;
                    }
                    else
                    {
                        bgAccum[row, col] = alpha * val + (1 - alpha) * bgAccum[row, col];
                    }

                    // calculate the diff between the frame and the average up until now.
                    double delta = val - bgAccum[row, col];

                    byte currentBgValue = bgFrame[row, col].BwValue;

                    if (currentBgValue > 0)
                    {
                        currentBgValue -= STEP_SIZE;
                    }

                    // if delta is smaller than the threshold, eliminate the background.
                    if (delta < threshold)
                    {
                        // set the color of the background frame to black.
                        //bgFrame[row, col] = new CVRgbPixel(0, 0, 0);
                    }
                    else
                    {
                        currentBgValue = 255;
                    }

                    bgFrame[row, col] = new CVRgbPixel(currentBgValue, currentBgValue, currentBgValue);
                }
            }

            // display the updated frame on the window.
            bs.Image = bgFrame.ToBitmap();
        }

        private void videoPlayer_Opening(VideoPlayer sender, OpeningEventArgs args)
        {
            if (bgFrame != null) bgFrame.Dispose();

            // create accumulator image when a new video is opened.
            bgAccum = new double[args.NewCapture.Height, args.NewCapture.Width];
            for (int row = 0; row < args.NewCapture.Height; ++row)
                for (int col = 0; col < args.NewCapture.Width; ++col)
                    bgAccum[row, col] = -1.0;

            bgFrame = args.NewCapture.CreateCompatibleImage();
        }
    }
}

