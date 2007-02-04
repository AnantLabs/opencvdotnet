using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet.Examples
{
    /// <summary>
    /// This class implements moving average.
    /// </summary>
    class MovingAverage
    {
        double[,] averages;
        double alpha;

        public MovingAverage(int width, int height, double alpha)
        {
            if (height == 0 || width == 0) height = width = 400;

            this.alpha = alpha;
            averages = new double[height, width];

            for (int row = 0; row < height; ++row)
            {
                for (int col = 0; col < width; ++col)
                {
                    averages[row, col] = -1.0;
                }
            }
        }

        public void Accumulate(int row, int col, double value)
        {
            if (averages[row, col] == -1.0) averages[row, col] = value;
            else averages[row, col] = alpha * value + (1 - alpha) * averages[row, col];
        }

        public double this[int row, int col]
        {
            get
            {
                return averages[row, col];
            }
        }

        public double Alpha
        {
            get
            {
                return this.alpha;
            }
            set
            {
                this.alpha = value;
            }
        }

    }

}