using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;

namespace Test
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            CVUtils.ErrorsToExceptions();
            //CVImage image = new CVImage(@"C:\Users\Yoav HaCohen\Pictures\temp\hair_res.bmp");
            CVImage image = new CVImage(new System.Drawing.Bitmap(@"C:\Users\Yoav HaCohen\Pictures\temp\hair_res.bmp"));
            new BitmapViewer(image.ToBitmap()).ShowDialog();
            //CVHistogram h = image.CalcHistogram(255);
            //MessageBox.Show("Done");
            //MessageBox.Show("!!! " + h.MinMaxValue);
            
        }
    }
}
