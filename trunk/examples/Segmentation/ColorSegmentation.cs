using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;

namespace OpenCVDotNet.Examples
{
    public partial class ColorSegmentation : Form
    {
        private CVHistogram selectionHistogram;

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

        private void videoPlayer_NextFrame(SharedUI.NextFrameEventArgs args)
        {
            ProcessFrame();
        }

        private void ProcessFrame()
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

                    CVImage colorBp = new CVImage(bp.Width, bp.Height, bp.Depth, 3);
                    colorBp.Merge(new CVImage[] { bp, bp, bp });

                    colorBp.DrawPixel(midPoint, Color.Red, 5);
                    colorBp.DrawRectangle(video.SelectionRect, Color.Blue);

                    backProjection.Image = colorBp.ToBitmap();
                }
            }
        }

        private void video_SelectionChanged(object sender, EventArgs e)
        {
            if (videoPlayer.LastFrame == null) return;

            using (CVImage region = videoPlayer.LastFrame.CopyRegion(video.SelectionRect))
            {
                selectionHistogram = region.CalcHistogram(30);
                histo.ShowHistogram(region);
            }

            ProcessFrame();
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
    }
}