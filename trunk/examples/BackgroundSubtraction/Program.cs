using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComputerVision
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