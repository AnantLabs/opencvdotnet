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
    public partial class RegionDrawing : Form
    {
        private CVHistogram selectionHistogram;
        private double[,] bgAccum;
        private CVImage bgFrame;


        public RegionDrawing()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;

            videoPlayer.Open(ofd.FileName);
            video_SelectionChanged(this, null);
        }

        private void videoPlayer_NextFrame(VideoPlayer sender, NextFrameEventArgs args)
        {
            ProcessRegionGrowing();
        }

        private void ProcessRegionGrowing()
        {
            if (selectionHistogram == null) return;

            using (CVImage lastFrameClone = videoPlayer.LastFrame.Clone())
            {
                using (CVImage bp = videoPlayer.LastFrame.CalcBackProject(selectionHistogram))
                {
                    CVConnectedComp cc = bp.MeanShift(video.SelectionRect, 100);
                    video.SelectionRect = cc.Rect;

                    Point midPoint = new Point(
                        video.SelectionRect.Left + video.SelectionRect.Width / 2,
                        video.SelectionRect.Top + video.SelectionRect.Height / 2);

                    using (CVImage rg = videoPlayer.Capture.CreateCompatibleImage())
                    {
                        rg.Zero();
                        OpenCVDotNet.Algorithms.RegionGrowing.Fill(midPoint, rg, new OpenCVDotNet.Algorithms.RegionGrowingCriteria(MyRGCriteria), videoPlayer.LastFrame);
                        growingFrame.Image = rg.ToBitmap();

                        if (overlay.Checked)
                        {
                            // go over regions in rg and overlay on output image.
                            for (int row = 0; row < rg.Height; row++)
                            {
                                for (int col = 0; col < rg.Width; ++col)
                                {
                                    if (rg[row, col].GrayLevel != 0)
                                    {
                                        videoPlayer.LastFrame[row, col] = new CVRgbPixel(0, 100, 0);
                                    }
                                }
                            }
                        }

                        video.Image = videoPlayer.LastFrame.ToBitmap();
                    }
                }
            }
        }

        private bool MyRGCriteria(Point startPt, Point candidatePt, object frameCookie)
        {
            CVImage frame = frameCookie as CVImage;

            int tolerance;

            if (!int.TryParse(grayscaleTolerance.Text, out tolerance))
                tolerance = 1;

            return Math.Abs(frame[startPt].GrayLevel - frame[candidatePt].GrayLevel) <= tolerance;
        }

        private void video_SelectionChanged(object sender, EventArgs f)
        {
            if (videoPlayer.LastFrame == null) return;

            using (CVImage region = videoPlayer.LastFrame.CopyRegion(video.SelectionRect))
            {
                selectionHistogram = region.CalcHistogram(30);
            }

            ProcessRegionGrowing();
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

        private void grayscaleTolerance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ProcessRegionGrowing();
        }
    }

}

