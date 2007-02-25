using System;
using System.Drawing;

namespace OpenCVDotNet.Algorithms
{
    public sealed class RegionGrowing
    {
        private static Point[] connections = 
        { 
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(-1, 1),
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1) 
        };

        /// <summary>
        /// Implements an 8-connected region growing algorithm based on histogram similarity (using back projection).
        /// Starts with the starting pixel and checks in the back projection all the four pixel around it.
        /// If the pixels pass some threshold, they are selected and the region is grown to contain them as well.
        /// </summary>
        public static void Fill(Point startPoint, CVImage outputImage, RegionGrowingCriteria criteria, object criteriaCookie)
        {
            Fill(startPoint, outputImage, criteria, criteriaCookie, Color.Red);
        }

        /// <summary>
        /// Implements an 8-connected region growing algorithm based on histogram similarity (using back projection).
        /// Starts with the starting pixel and checks in the back projection all the four pixel around it.
        /// If the pixels pass some threshold, they are selected and the region is grown to contain them as well.
        /// </summary>
        public static void Fill(Point startPoint, CVImage outputImage, RegionGrowingCriteria criteria, object criteriaCookie, Color c)
        {
            Fill(startPoint, outputImage, criteria, criteriaCookie, DefaultLabeling, c);
        }

        /// <summary>
        /// Implements an 8-connected region growing algorithm based on histogram similarity (using back projection).
        /// Starts with the starting pixel and checks in the back projection all the four pixel around it.
        /// If the pixels pass some threshold, they are selected and the region is grown to contain them as well.
        /// </summary>
        public static void Fill(Point startPoint, CVImage outputImage, 
            RegionGrowingCriteria criteria, object criteriaCookie,
            LabelingFunc labelFunc, object labelingCookie)
        {
            // first, include the starting pixel in the region.
            labelFunc(outputImage, startPoint, labelingCookie);

            // go over all the connection points.
            foreach (Point connection in connections)
            {
                Point connectionPoint = new Point(
                        startPoint.X + (int)connection.X,
                        startPoint.Y + (int)connection.Y);

                if (!outputImage.Contains(connectionPoint)) 
                    continue;

                // consider this point only if it's not already in the region.
                if (outputImage[connectionPoint].GrayLevel != 0) continue;

                // only if growing criteria is met, recurse away.
                if (criteria(startPoint, connectionPoint, criteriaCookie))
                {
                    // recurse into the new connection point.
                    Fill(connectionPoint, outputImage, criteria, criteriaCookie, labelFunc, labelingCookie);
                }
            }
        }

        /// <summary>
        /// Can be used in the call to Fill() as the default labeling function.
        /// This function merely colors the pixel in the color specified under labelCookie
        /// which should be a Color object.
        /// </summary>
        public static void DefaultLabeling(CVImage outputImage, Point pt, object labelCookie)
        {
            if (!outputImage.Contains(pt)) return;
            if (outputImage[pt].GrayLevel != 0) return;
            Color c = (Color)labelCookie;
            outputImage[pt] = new CVRgbPixel(c);
        }

        /// <summary>
        /// Label 8-connected points from each point with color.
        /// </summary>
        public static void EightConnectedLabeling(CVImage outputImage, Point pt, object colorCookie)
        {
            foreach (Point conn in connections)
            {
                Point connectPt = new Point(pt.X + conn.X, pt.Y + conn.Y);
                DefaultLabeling(outputImage, connectPt, colorCookie);
            }
        }

        /// <summary>
        /// Can be used to implement flood fill.
        /// </summary>
        public static bool FloodFillCondition(CVImage frame, Point startPt, Point candidatePt, int tolerange)
        {
            if (!frame.Contains(candidatePt) || !frame.Contains(startPt)) return false;
            return Math.Abs(frame[startPt].GrayLevel - frame[candidatePt].GrayLevel) <= tolerange;
        }
    }

    public delegate bool RegionGrowingCriteria(Point startPt, Point candidatePt, object cookie);
    public delegate void LabelingFunc(CVImage outputImage, Point pt, object labelCookie);
}
