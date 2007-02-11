using System;
using System.Drawing;

namespace OpenCVDotNet.Algorithms
{
    public sealed class MaskedHistogram
    {
        private const int FLOOD_FILL_TOLERANCE = 3;
        private static Color FG_COLOR = Color.White;
        private static Color NEAUTRAL_COLOR = Color.Gray;
        private static Color TEMP_BG_COLOR = Color.Green;
        private static Color BG_COLOR = Color.Black;

        /// <summary>
        /// Creates a new image that will be used as a mask for the histogram calculation.
        /// All forground points will be colored blue and all background points will be colored red.
        /// </summary>
        public static CVImage PrepareMask(CVImage image, Point[] forgroundPoints,Point[] backgroundPoints, bool includeNeautral, int floodFillThreshold)
        {
            CVImage outputImage = image.Clone();
            outputImage.Zero();

            Rectangle region = image.RegionOfInterest;

            FloodFillParams ffp = new FloodFillParams();
            ffp.Frame = image;
            ffp.Threshold = floodFillThreshold;

            // fill all forground points in FG_COLOR
            foreach (Point pt in forgroundPoints)
            {
                RegionGrowing.Fill(pt, outputImage, new RegionGrowingCriteria(FloodFill), ffp, new LabelingFunc(RegionGrowing.DefaultLabeling), FG_COLOR);
            }

            // fill all background points in BG_COLOR
            foreach (Point pt in backgroundPoints)
            {
                RegionGrowing.Fill(pt, outputImage, new RegionGrowingCriteria(FloodFill), ffp, new LabelingFunc(RegionGrowing.DefaultLabeling), TEMP_BG_COLOR);
            }

            // now colorize to actual colors.
            for (int row = 0; row < outputImage.Height; ++row)
            {
                for (int col = 0; col < outputImage.Width; ++col)
                {
                    int argb = outputImage[row, col].ToColor().ToArgb();

                    if (includeNeautral)
                    {
                        if (argb == Color.Black.ToArgb())
                        {
                            outputImage[row, col] = new CVRgbPixel(NEAUTRAL_COLOR);
                        }
                    }

                    if (argb == TEMP_BG_COLOR.ToArgb())
                        outputImage[row, col] = new CVRgbPixel(BG_COLOR);
                }
            }

            return outputImage;
        }

        /// <summary>
        /// This function applies the mask to image in the following way:
        ///  - All pixels in 'mask' that are blue are copied to the output image in maximum intensity.
        ///  - All pixels in 'mask' that are black are copied to the output image in half intensity.
        ///  - All pixels in 'mask' that are red are not copied at all to output image.
        /// </summary>
        public static CVImage ApplyMask(CVImage image, CVImage mask)
        {
            CVImage outputImage = image.Clone();
            outputImage.Zero();

            for (int row = 0; row < image.Height; ++row)
            {
                for (int col = 0; col < image.Width; ++col)
                {
                    // copy everything that's not background.
                    if (mask[row, col].ToColor().ToArgb() != BG_COLOR.ToArgb())
                    {
                        outputImage[row, col] = image[row, col];
                    }
                    else
                    {
                        outputImage[row, col] = new CVRgbPixel(TEMP_BG_COLOR);
                    }
                }
            }

            return outputImage;
        }

        /// <summary>
        /// This callback is used in the call to RegionGrowing.Fill to perform
        /// flood fill.
        /// </summary>
        private static bool FloodFill(Point pt1, Point pt2, object cookie)
        {
            FloodFillParams ffp = cookie as FloodFillParams;
            return RegionGrowing.FloodFillCondition(ffp.Frame, pt1, pt2, ffp.Threshold);
        }
    }

    class FloodFillParams
    {
        public CVImage Frame;
        public int Threshold;
    }
}
