using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CVUtils.ErrorsToExceptions();
            CVImage image = new CVImage(@"C:\Users\Yoav HaCohen\Pictures\temp\hair_res.bmp");
            CVHistogram h = image.CalcHistogram(255);
            MessageBox.Show("Done");
        }
    }
}
