using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    
    internal static partial class PInvoke
    {
        private const string CV100_DLL = @"cv100.dll";
        //private const string CV100_DLL = @"C:\dev\personal\OpenCV\opencv\bin\cv100d.dll";

        #region CV100_DLL
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCopyMakeBorder(__CvArrPtr src, __CvArrPtr dst, __CvPoint offset, int bordertype, __CvScalar value);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvSmooth(__CvArrPtr src, __CvArrPtr dst, int smoothtype, int param1, int param2, double param3, double param4);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvFilter2D(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr kernel, __CvPoint anchor);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvIntegral(__CvImagePtr image, __CvArrPtr sum, __CvArrPtr sqsum, __CvArrPtr tilted_sum);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvPyrDown(__CvImagePtr src, __CvImagePtr dst, int filter);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvPyrUp(__CvImagePtr src, __CvImagePtr dst, int filter);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCalcPyramid(__CvImagePtr src, __CvArrPtr container, IntPtr levels, int level_count, int filter);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvPyrSegmentation(__CvImagePtr src, __CvImagePtr dst, __CvMemStoragePtr storage, __CvSeqPtr comp, int level, double threshold1, double threshold2);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvPyrMeanShiftFiltering(__CvArrPtr src, __CvArrPtr dst, double sp, double sr, int max_level /* CV_DEFAULT(1) */, CVTermCriteria termcrit /* CV_DEFAULT(cvTermCriteria(CV_TERMCRIT_ITER+CV_TERMCRIT_EPS,5,1)) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvWatershed(__CvArrPtr image, __CvArrPtr markers);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvInpaint(__CvArrPtr src, __CvArrPtr inpaint_mask, __CvArrPtr dst, double inpaintRange, int flags);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvSobel(__CvArrPtr src, __CvArrPtr dst, int xorder, int yorder, int aperture_size /* CV_DEFAULT(3) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLaplace(__CvArrPtr src, __CvArrPtr dst, int aperture_size /* CV_DEFAULT(3) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCvtColor(__CvArrPtr src, __CvArrPtr dst, int code);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvResize(__CvArrPtr src, __CvArrPtr dst, int interpolation /* CV_DEFAULT( CV_INTER_LINEAR ) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvWarpAffine(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr map_matrix, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern __CvMatPtr cvGetAffineTransform(__CvPoint2D32fPtr src, __CvPoint2D32fPtr dst, __CvMatPtr map_matrix);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern __CvMatPtr cv2DRotationMatrix(__CvPoint2D32f center, double angle, double scale, __CvMatPtr map_matrix);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvWarpPerspective(__CvArrPtr src, __CvArrPtr dst, __CvMatPtr map_matrix, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern __CvMatPtr cvGetPerspectiveTransform(__CvPoint2D32fPtr src, __CvPoint2D32fPtr dst, __CvMatPtr map_matrix);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvRemap(__CvArrPtr src, __CvArrPtr dst, __CvArrPtr mapx, __CvArrPtr mapy, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */, __CvScalar fillval /* CV_DEFAULT(__CvScalarAll(0)) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLogPolar(__CvArrPtr src, __CvArrPtr dst, __CvPoint2D32f center, double M, int flags /* CV_DEFAULT(CV_INTER_LINEAR+CV_WARP_FILL_OUTLIERS) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern __IplConvKernelPtr cvCreateStructuringElementEx(int cols, int rows, int anchor_x, int anchor_y, int shape, IntPtr values /* CV_DEFAULT(NULL) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvReleaseStructuringElement(__IplConvKernelPtr element);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvErode(__CvArrPtr src, __CvArrPtr dst, __IplConvKernelPtr element /* CV_DEFAULT(NULL), int iterations CV_DEFAULT(1) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvDilate(__CvArrPtr src, __CvArrPtr dst, __IplConvKernelPtr element /* CV_DEFAULT(NULL) */, int iterations /* CV_DEFAULT(1) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvMorphologyEx(__CvArrPtr src, __CvArrPtr dst, __CvArrPtr temp, __IplConvKernelPtr element, int operation, int iterations /* CV_DEFAULT(1) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvMoments(__CvArrPtr arr, __CvMomentsPtr moments, int binary /* CV_DEFAULT(0) */);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvGetHuMoments(__CvMomentsPtr moments, __CvHuMomentsPtr hu_moments);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvMatchTemplate(__CvArrPtr image, __CvArrPtr templ, __CvArrPtr result, int method);
        
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCanny(__CvArrPtr image, __CvArrPtr dst, double threshold1, double threshold2, int apertureSize);

        #region cvPreCornerDetect
        /// <summary>
        /// Calculates feature map for corner detection
        /// The function cvPreCornerDetect calculates the function Dx2Dyy+Dy2Dxx - 2DxDyDxy where D? denotes one of the first image derivatives and D?? denotes a second image derivative. The corners can be found as local maximums of the function:
        /// <code>
        /// //assume that the image is floating-point
        /// IplImage* corners = cvCloneImage(image);
        /// IplImage* dilated_corners = cvCloneImage(image);
        /// IplImage* corner_mask = cvCreateImage( cvGetSize(image), 8, 1 );
        /// cvPreCornerDetect( image, corners, 3 );
        /// cvDilate( corners, dilated_corners, 0, 1 );
        /// cvSubS( corners, dilated_corners, corners );
        /// cvCmpS( corners, 0, corner_mask, CV_CMP_GE );
        /// cvReleaseImage( &corners );
        /// cvReleaseImage( &dilated_corners );
        /// </code>
        /// </summary>
        /// <param name="image">Input image</param>
        /// <param name="corners">Image to store the corner candidates</param>
        /// <param name="apertureSize">parameter for Sobel operator (see cvSobel). </param>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvPreCornerDetect(__CvArrPtr image, __CvArrPtr corners, int aperture_size);

        /// <summary>
        /// Calculates feature map for corner detection
        /// The function cvPreCornerDetect calculates the function Dx2Dyy+Dy2Dxx - 2DxDyDxy where D? denotes one of the first image derivatives and D?? denotes a second image derivative. The corners can be found as local maximums of the function:
        /// <code>
        /// //assume that the image is floating-point
        /// IplImage* corners = cvCloneImage(image);
        /// IplImage* dilated_corners = cvCloneImage(image);
        /// IplImage* corner_mask = cvCreateImage( cvGetSize(image), 8, 1 );
        /// cvPreCornerDetect( image, corners, 3 );
        /// cvDilate( corners, dilated_corners, 0, 1 );
        /// cvSubS( corners, dilated_corners, corners );
        /// cvCmpS( corners, 0, corner_mask, CV_CMP_GE );
        /// cvReleaseImage( &corners );
        /// cvReleaseImage( &dilated_corners );
        /// </code>
        /// </summary>
        /// <param name="image">Input image</param>
        /// <param name="corners">Image to store the corner candidates</param>       
        internal static void cvPreCornerDetect(__CvArrPtr image, __CvArrPtr corners) {
            cvPreCornerDetect(image, corners, 3);
        }
        #endregion

        #region Histogram

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCalcArrHist(__CvArrPtr[] image, IntPtr dst, int accumulate, __CvArrPtr mask);

        internal static void cvCalcArrHist(__CvArrPtr[] image, IntPtr dst, int accumulate)
        {
            cvCalcArrHist(image, dst, accumulate, IntPtr.Zero);
        }

        internal static unsafe void cvCalcHist(__IplImagePtr[] image, IntPtr hist, int accumulate, __CvArrPtr mask)
        {
            __CvArrPtr[] arrs = new __CvArrPtr[image.Length];
            for (int i = 0; i < image.Length; ++i) arrs[i] = image[i];
            cvCalcArrHist(arrs, hist, accumulate, mask);
        }

        internal static unsafe void cvCalcHist(__IplImagePtr[] image, IntPtr hist, int accumulate)
        {
            __CvArrPtr[] arrs = new __CvArrPtr[image.Length];
            for (int i = 0; i < image.Length; ++i) arrs[i] = image[i];
            cvCalcArrHist(arrs, hist, accumulate, IntPtr.Zero);
        }


        internal static unsafe void cvCalcHist(__IplImagePtr[] image, IntPtr hist)
        {
            __CvArrPtr[] arrs = new __CvArrPtr[image.Length];
            for (int i = 0; i < image.Length; ++i) arrs[i] = image[i];
            cvCalcArrHist(arrs, hist, 0, IntPtr.Zero);
        }

        /// <summary>
        /// Calculates back project
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dst"></param>
        /// <param name="hist"></param>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvCalcArrBackProject(__CvArrPtr[] image, __CvArrPtr dst, IntPtr hist);

        /// <summary>
        /// Calculates back project
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dst"></param>
        /// <param name="hist"></param>
        internal static unsafe void cvCalcBackProject(__CvArrPtr[] image, __CvArrPtr dst, IntPtr hist)
        {
            cvCalcArrBackProject(image, dst, hist);
        }

        internal static unsafe void cvCalcBackProject(__IplImagePtr[] image, __CvArrPtr dst, IntPtr hist)
        {
            __CvArrPtr[] arrs = new __CvArrPtr[image.Length];
            for (int i = 0; i < image.Length; ++i) arrs[i] = image[i];
            cvCalcArrBackProject(arrs, dst, hist);
        }

        /// <summary>
        /// Implements MeanShift algorithm - determines object position
        /// from the object histogram back project
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dst"></param>
        /// <param name="hist"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cvMeanShift(__CvArrPtr image, __CvRect window, __CvTermCriteria criteria, ref __CvConnectedComp comp);

        /// <summary>
        /// Implements CAMSHIFT algorithm - determines object position, size and orientation
        /// from the object histogram back project (extension of meanshift)
        /// </summary>
        /// <param name="prob"></param>
        /// <param name="window"></param>
        /// <param name="criteria"></param>
        /// <param name="comp"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cvCamShift(__CvArrPtr prob, __CvRect window, __CvTermCriteria criteria, ref __CvConnectedComp comp, /*__CvBox2D*/ IntPtr box);

        internal static int cvCamShift(__CvArrPtr prob, __CvRect window, __CvTermCriteria criteria, ref __CvConnectedComp comp)
        {
            return cvCamShift(prob, window, criteria, ref comp, IntPtr.Zero);
        }

        /// <summary>
        /// Releases histogram
        /// </summary>
        /// <param name="hist"></param>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvReleaseHist(ref IntPtr hist);

        ///// <summary>
        ///// Finds indices and values of minimum and maximum histogram bins
        ///// </summary>
        ///// <param name="hist"></param>
        ///// <param name="?"></param>
        ///// <param name="?"></param>
        ///// <param name="?"></param>
        //[DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        //internal static unsafe extern void cvGetMinMaxHistValue(IntPtr hist, float* min_value, float* max_value, int* min_ind);

        ///// <summary>
        ///// Finds indices and values of minimum and maximum histogram bins
        ///// </summary>
        ///// <param name="hist"></param>
        ///// <param name="?"></param>
        ///// <param name="?"></param>
        //[DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        //internal static unsafe extern void cvGetMinMaxHistValue(IntPtr hist, float* min_value, float* max_value);

        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint="cvGetMinMaxHistValue")]
        private static extern void __cvGetMinMaxHistValue(IntPtr hist, out float minVal, out float maxVal, IntPtr minIdx, IntPtr maxIdx);

        #region cvGetMinMaxHistValue defaults
        private static void cvGetMinMaxHistValue(IntPtr hist, out float minVal, out float maxVal, out int minIdx, out int maxIdx)
        {
            maxIdx = minIdx = 0;
            unsafe
            {
                fixed (int* minIdxPtr = &minIdx)
                {
                    fixed (int* maxIdxPtr = &maxIdx)
                    {
                        __cvGetMinMaxHistValue(hist, out minVal, out maxVal, new IntPtr(minIdxPtr), new IntPtr(maxIdxPtr));
                    }
                }
            }
        }

        internal static void cvGetMinMaxHistValue(IntPtr hist, ref float minVal, ref float maxVal, out int minIdx)
        {
            unsafe {
                fixed (int* minIdxPtr = &minIdx)
                {
                    __cvGetMinMaxHistValue(hist, out minVal, out maxVal, new IntPtr(minIdxPtr), IntPtr.Zero);
                }
            }
        }

        internal static void cvGetMinMaxHistValue(IntPtr hist, ref float minVal, ref float maxVal)
        {
            __cvGetMinMaxHistValue(hist, out minVal, out maxVal, IntPtr.Zero, IntPtr.Zero);
        }
        #endregion

        /// <summary>
        /// Create a Histogram.  The number of dimensions is inferred from the number
        /// of elements in the dims array.
        /// </summary>
        /// <param name="dims">Number of Bins Per Dimension</param>
        /// <param name="ranges">Ranges for each bin set</param>
        /// <param name="uniform">Uniform distribution of bin ranges?</param>	

        internal static unsafe IntPtr cvCreateHist(int[] dims, int type, float[][] ranges)
        {
            return cvCreateHist(dims, type, ranges, true);
        }

        internal static unsafe IntPtr cvCreateHistOld(int[] dims, int type, float[][] ranges, bool uniform)
        {
            IntPtr[] pts = new IntPtr[dims.Length];
            for (int i = 0; i < dims.Length; i++) pts[i] = IntPtr.Zero;
            IntPtr result;
            auxLoadAndCall(0, pts, dims, type, ranges, uniform, out result);
            return result;
        }

        

        internal static unsafe IntPtr cvCreateHist(int[] dims, int type, float[][] ranges, bool uniform)
        {
            IntPtr[] unmanagedRanges = UnmanagedArray.ToUnmanaged(ranges);
            IntPtr hist = cvCreateHist(unmanagedRanges.Length, dims, type, unmanagedRanges, (uniform ? 1 : 0));
            UnmanagedArray.FreeUnmanagedArray(unmanagedRanges);
            return hist;
        }

        private static unsafe void auxLoadAndCall(int recursiveDepth, IntPtr[] pts, int[] dims, int type, float[][] ranges, bool uniform, out IntPtr result)
        {
            // This Trick was taken from SharperCV:
            // Key problem:  we need to use "fixed" to pin every array in "ranges".  But "fixed"
            // is a syntax construct, so we'd have to write the impossible 
            //    fixed (...) { fixed (...) { fixed (...) ... <call the native routine> }}}
            // Instead we resort to recursion as a trick embed an arb number of fixed
            // constructs inside one another.

            // On each level of recursion here we fix one array so that the collector won't move it,
            // and we take its pointer and load it into the pts array.  When all elements have
            // been processed (i.e. when recursiveDepth == cDims), we are at the base case where we
            // can call the native routine and set up our histogram.

            if (recursiveDepth == pts.Length)
            {
                result = cvCreateHist(pts.Length, dims, type, pts, (uniform ? 1 : 0));
            }
            else
            {
                fixed (float* p = ranges[recursiveDepth])
                {
                    pts[recursiveDepth] = new IntPtr(p);
                    auxLoadAndCall(recursiveDepth + 1, pts, dims, type, ranges, uniform, out result);
                }
            }
        }

        // TODO: Copy array of fixed pointers to a new allocated array on the unmanaged memory. The user is responsible to realease it.
        //private static IntPtr[] CopyArrayToUnmanaged(IntPtr[][] array)
        //{
        //    IntPtr[] pointersToArrays = Marshal.AllocHGlobal(IntPtr.Size * array.Length);
        //    foreach (IntPtr p in array)
        //    return pointersToArrays;
        //}

        ////internal static unsafe extern IntPtr cvCreateHist(int dims, int* sizes, int type, [MarshalAs(UnmanagedType.LPArray)] float[][] ranges, int uniform);

        ///// <summary>
        ///// Creates new histogram
        ///// </summary>
        ///// <param name="dims"></param>
        ///// <param name="sizes"></param>
        ///// <param name="type"></param>
        ///// <param name="ranges"></param>
        ///// <returns></returns>
        //[DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        //internal static unsafe extern IntPtr cvCreateHist(int dims, int* sizes, int type, float** ranges);


        /// <summary>
        /// Creates new histogram
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="sizes"></param>
        /// <param name="type"></param>
        /// <param name="ranges"></param>
        /// <param name="uniform"></param>
        /// <returns></returns>
        [DllImport(CV100_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr cvCreateHist(int dims, int[] sizes, int type, IntPtr[] ranges, int uniform);

        internal static unsafe IntPtr cvCreateHist(int[] dims, int type)
        {
            return cvCreateHist(dims, type, null, true);
        }
        #endregion
        #endregion

    }
}
