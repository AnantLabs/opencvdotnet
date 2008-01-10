using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    
    internal static partial class PInvoke
    {
        [DllImport("cxts001.dll", CallingConvention=CallingConvention.Cdecl)]
        internal static unsafe extern __CvTermCriteria cvTermCriteria(int type, int max_iter, double epsilon);

        internal static __CvRect cvRect(int x, int y, int width, int height)
        {
            __CvRect r;

            r.x = x;
            r.y = y;
            r.width = width;
            r.height = height;

            return r;
        }
    }
}
