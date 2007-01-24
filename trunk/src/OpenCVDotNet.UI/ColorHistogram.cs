using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDotNet.UI
{
    public partial class ColorHistogram : UserControl
    {
        private int binsPerChannel = 100;

        public ColorHistogram()
        {
            InitializeComponent();
        }

        int BinsPerChannel
        {
            get { return binsPerChannel; }
            set { binsPerChannel = value; }
        }

        public void ShowHistogram(CVImage image)
        {
            if (image == null) return;

            // split image into channels (b,g,r)
            CVImage[] planes = image.Split();

            // we will create a 1D histogram for every channel. each histogram will have
            // 'numberOfBins' bins for its single dimension (ranged from 0 to 255).
            int[] bins = { BinsPerChannel };
            CVPair[] ranges = { new CVPair(0, 255) };

            // calculate histogram for red, green and blue channels (seperately).
            CVHistogram histoRed = planes[0].CalcHistogram(bins, ranges);
            CVHistogram histoBlue = planes[1].CalcHistogram(bins, ranges);
            CVHistogram histoGreen = planes[2].CalcHistogram(bins, ranges);

            // dispose of plane images.
            foreach (CVImage img in planes)
                img.Release();

            // draw the three histograms.
            DrawHistogram(bluePanel, histoBlue, 1);
            DrawHistogram(greenPanel, histoGreen, 2);
            DrawHistogram(redPanel, histoRed, 0);

            histoBlue.Release();
            histoGreen.Release();
            histoRed.Release();
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
                color[channelIdx] = (byte)(((double)bin / (double)bins) * 255);

                byte[] markerColor = new byte[3];
                markerColor[channelIdx] = level;

                outputImage.DrawRectangle(bin * binWidth, imageHeight, bin * binWidth + binWidth - 1, imageHeight - binHeight, color);
                outputImage.DrawRectangle(bin * binWidth, imageHeight - binHeight, bin * binWidth + binWidth - 1, imageHeight - binHeight, markerColor);
                outputImage.DrawRectangle(bin * binWidth, imageHeight, bin * binWidth, imageHeight - binHeight, markerColor);
            }

            window.Image = outputImage.ToBitmap();
            outputImage.Release();
        }
    }
}
