using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;
using OpenCVDotNet.Algorithms;

namespace OpenCVDotNet.Examples
{
    public partial class MaskedHistogramTracker : Form
    {
        private CVHistogram hist = null;
        private CVHistogram histNoMask = null;

        public MaskedHistogramTracker()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Video or Image File";
            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            videoPlayer.Open(ofd.FileName);
            vot.Reset();
        }

        private void output_SelectionChanged(object sender, EventArgs e)
        {
            CalculateHistograms();
        }

        private void CalculateHistograms()
        {
            if (videoPlayer.LastFrame == null) return;

            using (CVImage frame = videoPlayer.LastFrame.CopyRegion(meanShift.SelectionRect))
            {
                Rectangle region = frame.RegionOfInterest;

                int quarterHeight = region.Height / 4;
                int quarterWidth = region.Width / 4;

                // forground
                Point[] fg = 
                { 
                    new Point(region.Left + region.Width / 2, region.Top + quarterHeight),
                    new Point(region.Left + region.Width / 2, region.Top + 2 * quarterHeight),
                    new Point(region.Left + region.Width / 2, region.Top + 3 * quarterHeight),
                    new Point(region.Left + (quarterWidth / 4) * 8, region.Top + 6 * quarterHeight / 2),
                    new Point(region.Left + (quarterWidth / 4) * 9, region.Top + 6 * quarterHeight / 2),
                    new Point(region.Left + (quarterWidth / 4) * 6, region.Top + 3 * quarterHeight / 2),
                    new Point(region.Left + (quarterWidth / 4) * 10, region.Top + 3 * quarterHeight / 2),
                };

                // background
                Point[] bg = 
                {
                    new Point(region.Left + 2 * quarterWidth, region.Top),
                    new Point(region.Left + quarterWidth / 2, region.Top + quarterHeight / 2),
                    new Point(region.Left, region.Top + 4 * quarterHeight / 2),
                    new Point(region.Left, region.Top + 5 * quarterHeight / 2),
                    new Point(region.Left, region.Top + 6 * quarterHeight / 2),
                    new Point(region.Left, region.Top + 7 * quarterHeight / 2),
                    new Point(region.Left, region.Bottom),
                    new Point(region.Right - quarterWidth / 2, region.Top + quarterHeight / 2),
                    new Point(region.Right, region.Top + 4 * quarterHeight / 2),
                    new Point(region.Right, region.Top + 5 * quarterHeight / 2),
                    new Point(region.Right, region.Top + 6 * quarterHeight / 2),
                    new Point(region.Right, region.Top + 7 * quarterHeight / 2),
                    new Point(region.Right, region.Bottom),
                };

                // update cross markers.
                meanShift.ClearMarkers();
                foreach (Point pt in fg) meanShift.AddMarker(pt, Color.Blue);
                foreach (Point pt in bg) meanShift.AddMarker(pt, Color.Red);

                using (CVImage mask = MaskedHistogram.PrepareMask(frame, fg, bg, includeNeautral.Checked, ffThresh.Value))
                {
                    outputPic.Image = mask.ToBitmap();

                    using (CVImage gsMask = mask.ToGrayscale())
                    {
                        maskHistogram.BinsPerChannel = Bins;
                        maskHistogram.ShowHistogram(frame, gsMask);

                        if (hist != null) hist.Dispose();
                        hist = frame.CalcHistogram(Bins, gsMask);
                    }

                    using (CVImage masked = MaskedHistogram.ApplyMask(frame, mask))
                    {
                        afterMask.Image = masked.ToBitmap();
                    }
                }
            }

            NextFrame(false);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            videoPlayer.Play();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            videoPlayer.Pause();
        }

        private void videoPlayer_NextFrame(OpenCVDotNet.UI.VideoPlayer sender, OpenCVDotNet.UI.NextFrameEventArgs args)
        {
            NextFrame(true);
        }

        private const double MIN_AREA = 100000;
        private const double MAX_AREA = 1200000;

        private void NextFrame(bool track)
        {
            meanShiftNoMask.Image = videoPlayer.LastFrame.ToBitmap();

            if (!track) vot.Reset();

            if (hist == null) return;
            using (CVImage backProj = videoPlayer.LastFrame.CalcBackProject(hist))
            {
                CVConnectedComp cc = backProj.MeanShift(meanShift.SelectionRect, 200);
                vot.AddValue(cc.Area, Color.Blue);

                if (cc.Area > MIN_AREA && cc.Area < MAX_AREA)
                {
                    if (track) meanShift.SelectionRect = cc.Rect;
                }

                bpWithMask.Image = backProj.ToBitmap();
            }

            OpenCVDotNet.UI.Statistics blueStats = vot.GetStatistics(Color.Blue);
            if (blueStats != null)
            {
                avgWithMask.Text = ((int)blueStats.Average).ToString();
                lastWithMask.Text = ((int)blueStats.LastValue).ToString();
            }

            if (histNoMask == null) return;
            using (CVImage backProjNoMask = videoPlayer.LastFrame.CalcBackProject(histNoMask))
            {
                CVConnectedComp cc = backProjNoMask.MeanShift(meanShiftNoMask.SelectionRect, 200);
                vot.AddValue(cc.Area, Color.Red);

                if (cc.Area > MIN_AREA && cc.Area < MAX_AREA)
                {
                    if (track) meanShiftNoMask.SelectionRect = cc.Rect;
                }

                bpNoMask.Image = backProjNoMask.ToBitmap();
            }

            OpenCVDotNet.UI.Statistics redStats = vot.GetStatistics(Color.Red);
            if (redStats != null)
            {
                avgNoMask.Text = ((int)redStats.Average).ToString();
                lastNoMask.Text = ((int)redStats.LastValue).ToString();
            }
        }

        private void includeNeautral_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHistograms();
        }

        private void ffThresh_Scroll(object sender, EventArgs e)
        {
            thresholdLabel.Text = ffThresh.Value.ToString();
            CalculateHistograms();
        }

        private void SmartHistograms_Load(object sender, EventArgs e)
        {
            ffThresh_Scroll(null, null);
            histBins_Scroll(null, null);
        }

        private void meanShiftNoMask_SelectionChanged(object sender, EventArgs e)
        {
            using (CVImage frame = videoPlayer.LastFrame.CopyRegion(meanShiftNoMask.SelectionRect))
            {
                if (histNoMask != null) histNoMask.Dispose();
                histNoMask = frame.CalcHistogram(Bins);
                noMaskHist.BinsPerChannel = Bins;
                noMaskHist.ShowHistogram(frame);
            }

            NextFrame(false);
        }

        public int Bins
        {
            get
            {
                return histBins.Value;
            }
        }

        private void histBins_Scroll(object sender, EventArgs e)
        {
            binsLabel.Text = histBins.Value.ToString();
            CalculateHistograms();
        }

        private void resetStatsButton_Click(object sender, EventArgs e)
        {
            NextFrame(false);
        }
    }
}