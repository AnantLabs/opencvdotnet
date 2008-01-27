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
            TestBitmapConversion();
            TestDrawContours();
        }

        private static void TestBitmapConversion()
        {
            CVImage image = new CVImage(new System.Drawing.Bitmap(@"C:\Users\Yoav HaCohen\Pictures\temp\hair_res.bmp"));
            new BitmapViewer(image.ToBitmap()).ShowDialog();
        }

        private static void TestDrawContours()
        {
            CVImage image = new CVImage(100, 100, CVDepth.Depth8U, 1);
            
            image.DrawRectangle(new System.Drawing.Rectangle(10, 10, 20, 30), System.Drawing.Color.Red, 3);
            new BitmapViewer(image.ToBitmap()).ShowDialog();
            CVImage res = image.DrawContours();
            new BitmapViewer(res.ToBitmap()).ShowDialog();
        }
    }
}
