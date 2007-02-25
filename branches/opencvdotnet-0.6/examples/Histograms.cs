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
    public partial class Histograms : Form
    {
        private CVImage image = null;

        public Histograms()
        {
            InitializeComponent();
        }


        private void UpdateHistogram()
        {
            int numberOfBins;
            if (!int.TryParse(binSize.Text, out numberOfBins))
            {
                statusBar.Text = string.Format("Number of bins '{0}' is not an integer.", binSize.Text);
                return;
            }

            if (image == null) return;

            //image.RegionOfInterest = originalImage.SelectionRect;

            // split image into channels (b,g,r)
            CVImage[] planes = image.Split();

            // we will create a 1D histogram for every channel. each histogram will have
            // 'numberOfBins' bins for its single dimension (ranged from 0 to 255).
            int[] bins = { numberOfBins };
            CVPair[] ranges = { new CVPair(0, 255) };

            // calculate histogram for red, green and blue channels (seperately).
            CVHistogram histoRed = planes[0].CalcHistogram(bins, ranges);
            CVHistogram histoBlue = planes[1].CalcHistogram(bins, ranges);
            CVHistogram histoGreen = planes[2].CalcHistogram(bins, ranges);

            // draw the three histograms.
            DrawHistogram(bluePanel, histoBlue, 1);
            DrawHistogram(greenPanel, histoGreen, 2);
            DrawHistogram(redPanel, histoRed, 0);

            // resize & put original image onto form.
            CVImage output = new CVImage(image.Width, image.Height, CVDepth.Depth8U, 3);
            CVImage emptyPlane = new CVImage(image.Width, image.Height, CVDepth.Depth8U, 1);
            emptyPlane.Zero();

            CVImage[] images = new CVImage[3];
            images[0] = images[1] = images[2] = emptyPlane;

            if (blueCheck.Checked) images[0] = planes[0];
            if (greenCheck.Checked) images[1] = planes[1];
            if (redCheck.Checked) images[2] = planes[2];

            output.Merge(images);
            originalImage.Image = output.ToBitmap();

            // dispose of plane images.
            foreach (CVImage img in planes)
                img.Dispose();

            statusBar.Text = "Ready";
        }

        void DrawHistogram(PictureBox window, CVHistogram histo, int channelIdx)
        {
            int imageWidth = window.Width;
            int imageHeight = window.Height;

            int bins = histo.BinSizes[0];
            int binWidth = imageWidth / bins;
            if (binWidth <= 0) binWidth = 1;
            
            CVPair minMax = histo.MinMaxValue;
            CVImage outputImage = new CVImage(imageWidth, imageHeight, CVDepth.Depth8U, 3);
            outputImage.Zero();

            for (int bin = 0; bin < bins; bin++)
            {
                double binValue = histo[bin];
                byte level = (byte)CVUtils.Round(binValue * 255 / minMax.Second);
                byte binHeight = (byte)CVUtils.Round(binValue * imageHeight / minMax.Second);

                byte[] color = new byte[3];
                color[channelIdx] = (byte) (((double) bin / (double) bins) * 255);

                byte[] markerColor = new byte[3];
                markerColor[channelIdx] = level;

                Color colColor = Color.FromArgb(color[2], color[1], color[0]);
                Color colMarker = Color.FromArgb(markerColor[2], markerColor[1], markerColor[0]);


                outputImage.DrawRectangle(new Rectangle(bin * binWidth, imageHeight - binHeight, binWidth - 1, binHeight), colColor);
                outputImage.DrawRectangle(new Rectangle(bin * binWidth, imageHeight - binHeight, binWidth - 1, binHeight), colMarker);
                outputImage.DrawRectangle(new Rectangle(bin * binWidth, imageHeight - binHeight, 1, binHeight), colMarker);
            }

            window.Image = outputImage.ToBitmap();
            outputImage.Release();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Image File";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            image = new CVImage(openFile.FileName);
            UpdateHistogram();
        }

        private void Histograms_SizeChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void binSize_TextChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void redCheck_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void blueCheck_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHistogram();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHistogram();

        }

        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Video File";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            CVCapture cap = new CVCapture(openFile.FileName);
            image = cap.QueryFrame();
            cap.Dispose();

            UpdateHistogram();
        }

        private void originalImage_Click(object sender, EventArgs e)
        {

        }
    }
}