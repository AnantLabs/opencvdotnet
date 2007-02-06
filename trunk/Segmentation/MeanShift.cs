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
    public partial class MeanShift : Form
    {
        private CVImage image = null;
        private CVCapture cap = new CVCapture();
        private CVHistogram initialHistogram = null;
        private int updateHistoCounter = 0;

        public MeanShift()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Calculate the histogram for the region of interest and saves it under 
        /// initialHistogram.
        /// </summary>
        private void UpdateHistogram()
        {
            // release the previous histogram if any.
            if (initialHistogram != null) initialHistogram.Release();

            // if there's no image, just return.
            if (image == null) return;

            // we use symmetric bins and entire range.
            int[] bpBins = { NumberOfBins, NumberOfBins, NumberOfBins };
            CVPair[] bpRanges = { new CVPair(0, 255), new CVPair(0, 255), new CVPair(0, 255) };

            // calculate histogram for region of interest and display it in panel.
            image.RegionOfInterest = originalImage.SelectionRect;
            this.initialHistogram = image.CalcHistogram(bpBins, bpRanges);
            initHisto.ShowHistogram(image);
            image.ResetROI();

            RefreshImages();
        }

        private void Track()
        {
            if (image == null) return;

            // calculate the back projection of the current image on top of the initial histogram.
            using (CVImage bp = image.CalcBackProject(this.initialHistogram))
            {
                // show the back projection.
                backProjectImage.Image = bp.ToBitmap();

                // calculate mean shift with user parameters and region of interest.
                CVConnectedComp conn = bp.MeanShift(originalImage.SelectionRect, Iterations);

                // update region of interest according to mean shift result.
                originalImage.SelectionRect = conn.Rect;
            }

            // rotate update counter only if rate != 0.
            if (UpdateHistogramRate != 0)
            {
                updateHistoCounter = (updateHistoCounter + 1) % UpdateHistogramRate;

                // update histogram is counter reached 0.
                if (updateHistoCounter == 0)
                {
                    UpdateHistogram();
                }
            }

            // refresh images.
            RefreshImages();
        }

        /// <summary>
        /// Refresh all images.
        /// </summary>
        private void RefreshImages()
        {
            image.RegionOfInterest = originalImage.SelectionRect;
            regionOfInterest.Image = image.ToBitmap();
            roiHisto.ShowHistogram(image);
            image.ResetROI();
            originalImage.Image = image.ToBitmap();

            // update rectangle values into status bar.
            statusBar.Text = originalImage.SelectionRect.ToString() + " Update Rate: " + 
                (UpdateHistogramRate == 0 ? "Never" : UpdateHistogramRate.ToString());

        }

        #region User Parameters

        private int NumberOfBins
        {
            get
            {
                int numberOfBins;
                if (!int.TryParse(binSize.Text, out numberOfBins) || numberOfBins <= 0)
                {
                    statusBar.Text = string.Format("Number of bins '{0}' is not an integer > 0.", binSize.Text);
                    return 50;
                }
                else
                {
                    return numberOfBins;
                }
            }
        }

        private int UpdateHistogramRate
        {
            get
            {
                int rate;
                if (int.TryParse(updateHistoRate.Text, out rate))
                {
                    return rate;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Iterations
        {
            get
            {
                int iterations;
                if (!int.TryParse(iterationsCount.Text, out iterations))
                    return 1000;
                else
                    return iterations;
            }
        }

        #endregion

        #region UI Events
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Image File";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            image = new CVImage(openFile.FileName);
            Track();
        }

        private void Histograms_SizeChanged(object sender, EventArgs e)
        {
            Track();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void binSize_TextChanged(object sender, EventArgs e)
        {
            Track();
        }

        private void redCheck_CheckedChanged(object sender, EventArgs e)
        {
            Track();
        }

        private void blueCheck_CheckedChanged(object sender, EventArgs e)
        {
            Track();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Track();

        }

        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Video File";
            openFile.Filter = "AVI Files|*.avi";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            cap.Open(openFile.FileName);
            image = cap.QueryFrame();

            UpdateHistogram();
            Track();
        }

        private void videoTimer_Tick(object sender, EventArgs e)
        {
            image = cap.QueryFrame();
            
            if (image == null)
            {
                cap.Restart();
            }

            Track();            
        }


        private void originalImage_SelectionChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
            Track();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            videoTimer.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            videoTimer.Enabled = false; 
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            cap.Restart();
            UpdateHistogram();

        }

        #endregion
    }
}