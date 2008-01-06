using System;
using System.Drawing;
using System.Text;

namespace OpenCVDotNet
{
    /**
    * (C) 2007 Elad Ben-Israel & Yoav HaCohen
    * This code is licenced under the GPL.
    */

    public class CVRgbPixel
    {
        private Byte r, b, g;

        public CVRgbPixel(Byte r, Byte g, Byte b)
        {
            this.r = r;
            this.b = b;
            this.g = g;
        }

        public CVRgbPixel(Color col)
        {
            this.r = col.R;
            this.b = col.B;
            this.g = col.G;
        }

        public byte R { get { return r; } }
        public byte G { get { return g; } }
        public byte B { get { return b; } }

        public byte BwValue
        {
            get
            {
                return this.GrayLevel;
            }
        }

        public byte GrayLevel
        {
            get
            {
                byte bw_value = (byte)
                    ((double)b * 0.114 +
                     (double)g * 0.587 +
                     (double)r * 0.299);
                return bw_value;
            }
        }
        public override String ToString()
        {
            return String.Format("({0},{1},{2})", r, g, b);
        }

        public Color ToColor()
        {
            return Color.FromArgb(this.R, this.G, this.B);
        }
    }

}