using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    internal struct __CvPoint { public int x, y; public __CvPoint(Point pt) { x = pt.X; y = pt.Y; } }
    internal struct __CvScalar { public double v0, v1, v2, v3; public __CvScalar(double[] val) { v0 = val[0]; v1 = val[1]; v2 = val[2]; v3 = val[3]; } public __CvScalar(CVScalar scalar) { v0 = scalar.val[0]; v1 = scalar.val[1]; v2 = scalar.val[2]; v3 = scalar.val[3]; } }
    internal struct __CvMemStoragePtr { public IntPtr ptr; public __CvMemStoragePtr(IntPtr ptr) { this.ptr = ptr; } }
    internal struct __CvSeqPtr { public IntPtr ptr; public __CvSeqPtr(IntPtr ptr) { this.ptr = ptr; } }
    internal struct __CvArrPtr { public IntPtr ptr; public __CvArrPtr(CVArr arr) { 
        ptr = (arr != null? arr.Ptr : IntPtr.Zero); } 
    }
    internal struct __CvImagePtr { public IntPtr ptr; public __CvImagePtr(CVImage img) { ptr = img.Ptr; } }
    internal struct __CvMatPtr { public IntPtr ptr; public __CvMatPtr(CVMat mat) { ptr = mat.Ptr; } }
    internal struct __CvPoint2D32f { }
    internal struct __CvPoint2D32fPtr { public IntPtr ptr; }
    internal struct __IplConvKernelPtr { public IntPtr ptr; }
    internal struct __CvMomentsPtr { public IntPtr ptr; }
    internal struct __CvHuMomentsPtr { public IntPtr ptr; }
    
    
    internal sealed class PInvoke
    {
        [DllImport("cv100.dll")]
        internal static extern void cvCopyMakeBorder(__CvArrPtr src, __CvArrPtr dst, __CvPoint offset, int bordertype, __CvScalar value);

        [DllImport("cv100.dll")]
        internal static extern void cvSmooth(__CvArrPtr src, __CvArrPtr dst, int smoothtype, int param1, int param2, double param3, double param4);

        [DllImport("cv100.dll")]
        internal static extern void cvFilter2D(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr kernel, __CvPoint anchor);

        [DllImport("cv100.dll")]
        internal static extern void cvIntegral(__CvImagePtr image, __CvArrPtr sum, __CvArrPtr sqsum, __CvArrPtr tilted_sum);

        [DllImport("cv100.dll")]
        internal static extern void cvPyrDown(__CvImagePtr src, __CvImagePtr dst, int filter);

        [DllImport("cv100.dll")]
        internal static extern void cvPyrUp(__CvImagePtr src, __CvImagePtr dst, int filter);

        [DllImport("cv100.dll")]
        internal static extern void cvCalcPyramid(__CvImagePtr src, __CvArrPtr container, IntPtr levels, int level_count, int filter);

        [DllImport("cv100.dll")]
        internal static extern void cvPyrSegmentation(__CvImagePtr src, __CvImagePtr dst, __CvMemStoragePtr storage, __CvSeqPtr comp, int level, double threshold1, double threshold2);

        [DllImport("cv100.dll")]
        internal static extern void cvPyrMeanShiftFiltering(__CvArrPtr src, __CvArrPtr dst, double sp, double sr, int max_level /* CV_DEFAULT(1) */, CVTermCriteria termcrit /* CV_DEFAULT(cvTermCriteria(CV_TERMCRIT_ITER+CV_TERMCRIT_EPS,5,1)) */);

        [DllImport("cv100.dll")]
        internal static extern void cvWatershed(__CvArrPtr image, __CvArrPtr markers);

        [DllImport("cv100.dll")]
        internal static extern void cvInpaint(__CvArrPtr src, __CvArrPtr inpaint_mask, __CvArrPtr dst, double inpaintRange, int flags);

        [DllImport("cv100.dll")]
        internal static extern void cvSobel(__CvArrPtr src, __CvArrPtr dst, int xorder, int yorder, int aperture_size /* CV_DEFAULT(3) */);

        [DllImport("cv100.dll")]
        internal static extern void cvLaplace(__CvArrPtr src, __CvArrPtr dst, int aperture_size /* CV_DEFAULT(3) */);

        [DllImport("cv100.dll")]
        internal static extern void cvCvtColor(__CvArrPtr src, __CvArrPtr dst, int code);

        [DllImport("cv100.dll")]
        internal static extern void cvResize(__CvArrPtr src, __CvArrPtr dst, int interpolation /* CV_DEFAULT( CV_INTER_LINEAR ) */);

        [DllImport("cv100.dll")]
        internal static extern void cvWarpAffine(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr map_matrix, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport("cv100.dll")]
        internal static extern __CvMatPtr cvGetAffineTransform(__CvPoint2D32fPtr src, __CvPoint2D32fPtr dst, __CvMatPtr map_matrix);

        [DllImport("cv100.dll")]
        internal static extern __CvMatPtr cv2DRotationMatrix(__CvPoint2D32f center, double angle, double scale, __CvMatPtr map_matrix);

        [DllImport("cv100.dll")]
        internal static extern void cvWarpPerspective(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr map_matrix, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport("cv100.dll")]
        internal static extern __CvMatPtr cvGetPerspectiveTransform(__CvPoint2D32fPtr src, __CvPoint2D32fPtr dst, __CvMatPtr map_matrix);

        [DllImport("cv100.dll")]
        internal static extern void cvRemap(__CvArrPtr src, __CvArrPtr dst, __CvArrPtr mapx, __CvArrPtr mapy, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport("cv100.dll")]
        internal static extern void cvLogPolar(__CvArrPtr src, __CvArrPtr dst, __CvPoint2D32f center, double M, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */);

        [DllImport("cv100.dll")]
        internal static extern __IplConvKernelPtr cvCreateStructuringElementEx(int cols, int rows, int anchor_x, int anchor_y, int shape, IntPtr values /* CV_DEFAULT(NULL) */);

        [DllImport("cv100.dll")]
        internal static extern void cvReleaseStructuringElement(__IplConvKernelPtr element);

        [DllImport("cv100.dll")]
        internal static extern void cvErode(__CvArrPtr src, __CvArrPtr dst, __IplConvKernelPtr element /* CV_DEFAULT(NULL), int iterations CV_DEFAULT(1) */);

        [DllImport("cv100.dll")]
        internal static extern void cvDilate(__CvArrPtr src, __CvArrPtr dst, __IplConvKernelPtr element /* CV_DEFAULT(NULL) */, int iterations /* CV_DEFAULT(1) */);

        [DllImport("cv100.dll")]
        internal static extern void cvMorphologyEx(__CvArrPtr src, __CvArrPtr dst, __CvArrPtr temp, __IplConvKernelPtr element, int operation, int iterations /* CV_DEFAULT(1) */);

        [DllImport("cv100.dll")]
        internal static extern void cvMoments(__CvArrPtr arr, __CvMomentsPtr moments, int binary /* CV_DEFAULT(0) */);

        [DllImport("cv100.dll")]
        internal static extern void cvGetHuMoments(__CvMomentsPtr moments, __CvHuMomentsPtr hu_moments);

        [DllImport("cv100.dll")]
        internal static extern void cvMatchTemplate(__CvArrPtr image, __CvArrPtr templ, __CvArrPtr result, int method);
        
        [DllImport("cv100.dll")]
        internal static extern void cvCanny(__CvArrPtr image, __CvArrPtr dst, double threshold1, double threshold2, int apertureSize);
        
        [DllImport("cxcore100.dll")]
        internal static extern void cvMinMaxLoc(__CvArrPtr arr, ref double min_val, ref double max_val, ref __CvPoint min_loc, ref __CvPoint max_loc, __CvArrPtr mask);

    }
}
