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

            int[,] template = 
                {
                    { -1,  0, -1,  0,  0, -1,  0,  0, -1,  0, -1 },
                    { -1,  0, -1,  0,  0,  0,  0,  0, -1,  0, -1 },
                    {  0, -1,  0,  0,  0, +1,  0,  0,  0, -1,  0 },
                    { -1,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1 },
                    { -1,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1 },
                    {  0,  0,  0, +1,  0, +1,  0, +1,  0,  0,  0 },
                    { -1,  0,  0,  0,  0, +1,  0,  0,  0,  0, -1 },
                    {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
                    { -1,  0,  0,  0,  0, +1,  0,  0,  0,  0, -1 },
                    {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
                    { -1, -1,  0,  0,  0,  0,  0,  0,  0, -1, -1 },
                    { -1, -1,  0,  0,  0,  0,  0,  0,  0, -1, -1 },
                    { -1, -1,  0,  0, +1,  0, +1,  0,  0, -1, -1 },
                    { -1, -1,  0,  0,  0,  0,  0,  0,  0, -1, -1 },
                    { -1, -1,  0,  0,  0,  0,  0,  0,  0, -1, -1 },
                };

            List<PointF> fg = new List<PointF>();
            List<PointF> bg = new List<PointF>();

            int rows = template.GetLength(0);
            int cols = template.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    float x = (float)col / ((float)cols - 1);
                    float y = (float)row / ((float)rows - 1);

                    PointF pt = new PointF(x, y);

                    switch (template[row, col])
                    {
                        case 1: fg.Add(pt); break;
                        case -1: bg.Add(pt); break;
                        default: break;
                    }
                }
            }

            // update cross markers.
            meanShift.ClearMarkers();
            foreach (PointF pt in fg) meanShift.AddCrossMarker(pt, Color.Blue);
            foreach (PointF pt in bg) meanShift.AddCrossMarker(pt, Color.Red);
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
            CalculateMaskedHistogram();
        }

        private void CalculateMaskedHistogram()
        {
            if (videoPlayer.LastFrame == null) return;

            using (CVImage frame = videoPlayer.LastFrame.CopyRegion(meanShift.SelectionRect))
            {
                // create a list of real fg and bg markers.
                List<Point> realFg = new List<Point>(meanShift.GetMarkerLocations(Color.Blue));
                List<Point> realBg = new List<Point>(meanShift.GetMarkerLocations(Color.Red));

                // create the histogram mask.
                using (CVImage mask = MaskedHistogram.PrepareMask(frame, realFg.ToArray(), realBg.ToArray(), includeNeautral.Checked, ffThresh.Value))
                {
                    maskPicture.Image = mask.ToBitmap();

                    // show mask histogram to user interface.
                    maskHistogram.BinsPerChannel = Bins;
                    maskHistogram.ShowHistogram(frame, mask);

                    // calculate new histogram (dispose old one if exist).
                    if (hist != null) hist.Dispose();
                    hist = frame.CalcHistogram(Bins, mask);

                    // apply mask to overlay (just for ui).
                    using (CVImage masked = MaskedHistogram.ApplyMask(frame, mask))
                    {
                        maskOverlay.Image = masked.ToBitmap();
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

        private const double MIN_AREA = 0;
        private const double MAX_AREA = 1200000;

        private void NextFrame(bool track)
        {
            meanShiftNoMask.Image = videoPlayer.LastFrame.ToBitmap();

            if (!track) vot.Reset();

            if (hist == null) return;
            using (CVImage backProj = videoPlayer.LastFrame.CalcBackProject(hist))
            {
                CVConnectedComp cc = backProj.MeanShift(meanShift.SelectionRect, 1);
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
                CVConnectedComp cc = backProjNoMask.MeanShift(meanShiftNoMask.SelectionRect, 1);
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
            CalculateMaskedHistogram();
        }

        private void ffThresh_Scroll(object sender, EventArgs e)
        {
            thresholdLabel.Text = ffThresh.Value.ToString();
            CalculateMaskedHistogram();
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
            CalculateMaskedHistogram();
        }

        private void resetStatsButton_Click(object sender, EventArgs e)
        {
            NextFrame(false);
        }
    }
}