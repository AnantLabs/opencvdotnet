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
    public partial class ColorSegmentation : Form
    {
        private CVHistogram selectionHistogram;
        private double[,] bgAccum;
        private CVImage bgFrame;


        public ColorSegmentation()
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

                    Point midPoint = new Point(
                        video.SelectionRect.Left + video.SelectionRect.Width / 2,
                        video.SelectionRect.Top + video.SelectionRect.Height / 2);

                    /*
                    using (CVImage colorBp = new CVImage(bp.Width, bp.Height, bp.Depth, 3))
                    {
                        colorBp.Merge(new CVImage[] { bp, bp, bp });

                        colorBp.DrawPixel(midPoint, Color.Red, 5);
                        colorBp.DrawRectangle(video.SelectionRect, Color.Blue);

                        backProjection.Image = colorBp.ToBitmap();
                    }
                    */

                    backProjection.Image = bp.ToBitmap();

                    using (CVImage rg = videoPlayer.Capture.CreateCompatibleImage())
                    {
                        rg.Zero();
                        Rectangle rect = video.SelectionRect;
                        //rect.Inflate(50, 50);

                        for (int row = rect.Top; row < rect.Bottom; ++row)
                        {
                            for (int col = rect.Left; col < rect.Right; ++col)
                            {
                                Point pt = new Point(col, row);
                                RegionGrowing(bp, pt, rg, 1, rect);
                            }
                        }

                        growingFrame.Image = rg.ToBitmap();
                    }                    
                }
            }
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

        private void videoPlayer_Playing(object sender, CancelEventArgs e)
        {
            video.ShowSelection = false;
        }

        private void videoPlayer_Pausing(object sender, CancelEventArgs e)
        {
            video.ShowSelection = true;
        }

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

        private CVPair[] connections = 
        { 
            new CVPair(-1, -1),
            new CVPair(-1, 0),
            new CVPair(-1, 1),
            new CVPair(0, -1),
            new CVPair(0, 1),
            new CVPair(1, -1),
            new CVPair(1, 0),
            new CVPair(1, 1) 
        };

        /// <summary>
        /// Implements an 8-connected region growing algorithm based on histogram similarity (using back projection).
        /// Starts with the starting pixel and checks in the back projection all the four pixel around it.
        /// If the pixels pass some threshold, they are selected and the region is grown to contain them as well.
        /// </summary>
        private void RegionGrowing(CVImage backProjection, Point startPoint, CVImage outputImage, byte thresh, Rectangle boundingRect)
        {
            // accept only pixels in the bounding rectangle.
            if (!boundingRect.Contains(startPoint)) return;

            // continue only with pixels that are above the threshold.
            if (backProjection[startPoint].GrayLevel <= thresh)
            {
                // color it in green to mark it as an edge.
                //outputImage[startPoint] = new CVRgbPixel(0, 255, 0);
                return;
            }

            // first, include the starting pixel in the region.
            outputImage[startPoint] = new CVRgbPixel(255, 0, 0);

            // go over all the connection points.
            foreach (CVPair connection in connections)
            {
                Point connectionPoint = new Point(
                        startPoint.X + (int) connection.Second, 
                        startPoint.Y + (int) connection.First);

                // consider this point only if it's not already in the region.
                if (outputImage[connectionPoint].GrayLevel != 0) continue;

                // recurse into the new connection point.
                RegionGrowing(backProjection, connectionPoint, outputImage, thresh, boundingRect);
            }
        }
    }
}

