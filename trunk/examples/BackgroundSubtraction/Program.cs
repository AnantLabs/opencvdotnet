using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenCVDotNet.Examples
{
    static class BackgroundSubtraction
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Options());
        }
    }
}