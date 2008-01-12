using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{

    internal static partial class PInvoke
    {

        private const int CV_CN_MAX = 64;
        private const int CV_CN_SHIFT = 3;
        private const int CV_DEPTH_MAX = (1 << CV_CN_SHIFT);

        internal static __CvTermCriteria cvTermCriteria(int type, int max_iter, double epsilon)
        {
            __CvTermCriteria t = new __CvTermCriteria();

            t.type = type;
            t.max_iter = max_iter;
            t.epsilon = (float)epsilon;

            return t;
        }

        internal static __CvRect cvRect(int x, int y, int width, int height)
        {
            __CvRect r;

            r.x = x;
            r.y = y;
            r.width = width;
            r.height = height;

            return r;
        }

        internal static bool CV_IS_IMAGE_HDR(__IplImagePtr img)
        {
            unsafe
            {
                return ((img.ptr != IntPtr.Zero) && img.ToPointer()->nSize == sizeof(__IplImage));
            }
        }

        internal static bool CV_IS_IMAGE(__IplImagePtr img)
        {
            unsafe
            {
                return (CV_IS_IMAGE_HDR(img) && img.ToPointer()->imageData != null);
            }
        }

        internal static int CV_MAKETYPE(int depth, int cn) { return ((depth) + (((cn) - 1) << CV_CN_SHIFT)); }
    }
}
