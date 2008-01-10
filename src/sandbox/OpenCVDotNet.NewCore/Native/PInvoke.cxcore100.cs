using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    
    internal static partial class PInvoke
    {

        #region cxcore100.dll
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __IplImagePtr cvCreateImage(__CvSize size, int depth, int channels);

        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvCreateImage(__CvArrPtr src0, __CvArrPtr src1, __CvArrPtr src2,
            __CvArrPtr src3, __CvArrPtr dst);


        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvCreateImage(out __IplImagePtr image);

        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvSplit(__CvArrPtr arr, __CvArrPtr dst0, __CvArrPtr dst1,
            __CvArrPtr dst2, __CvArrPtr dst3);

        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvMinMaxLoc(__CvArrPtr arr, out double min_val, out double max_val, out __CvPoint min_loc, out __CvPoint max_loc, __CvArrPtr mask);
        #endregion

        #region cvMerge
        /// <summary>
        /// Merges a set of single-channel arrays into the single multi-channel array
        /// or inserts one particular [color] plane to the array
        /// </summary>
        /// <param name="src0"></param>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <param name="src3"></param>
        /// <param name="dst"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvMerge(__CvArrPtr src0, __CvArrPtr src1, __CvArrPtr src2,
            __CvArrPtr src3, __CvArrPtr dst);
        #endregion

        #region cvReleaseImage
        /// <summary>
        /// Releases IPL image header and data
        /// </summary>
        /// <param name="image"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvReleaseImage(ref __IplImagePtr image);
        #endregion

        #region cvZero
        /// <summary>
        /// Clears all the array elements (sets them to 0)
        /// </summary>
        /// <param name="image"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvSetZero(__CvArrPtr image);

        /// <summary>
        /// Clears all the array elements (sets them to 0)
        /// </summary>
        /// <param name="image"></param>
        internal static void cvZero(__CvArrPtr image) { cvSetZero(image); }
        #endregion

        #region cvLine
        /// <summary>
        /// Draws 4-connected, 8-connected or antialiased line segment connecting two points
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="line_type"></param>
        /// <param name="shift"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLine(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2,
            __CvScalar color, int thickness,
            int line_type, int shift);

        /// <summary>
        /// Draws 4-connected, 8-connected or antialiased line segment connecting two points
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="line_type"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLine(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2,
            __CvScalar color, int thickness,
            int line_type);

        /// <summary>
        /// Draws 4-connected, 8-connected or antialiased line segment connecting two points
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLine(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2,
            __CvScalar color, int thickness);

        /// <summary>
        /// Draws 4-connected, 8-connected or antialiased line segment connecting two points
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvLine(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2, __CvScalar color);
        #endregion

        #region cvRectangle
        /// <summary>
        /// Draws a rectangle given two opposite corners of the rectangle (pt1 & pt2),
        /// if thickness is smaller than 0 (e.g. thickness == CV_FILLED), the filled box is drawn
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="line_type"></param>
        /// <param name="shift"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvRectangle(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2, 
            __CvScalar color, int thickness, int line_type, int shift);

        /// <summary>
        /// Draws a rectangle given two opposite corners of the rectangle (pt1 & pt2),
        /// if thickness is smaller than 0 (e.g. thickness == CV_FILLED), the filled box is drawn
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="line_type"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvRectangle(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2, 
            __CvScalar color, int thickness, int line_type);

        /// <summary>
        /// Draws a rectangle given two opposite corners of the rectangle (pt1 & pt2),
        /// if thicknessis smaller than 0 (e.g. thickness == CV_FILLED), the filled box is drawn
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvRectangle(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2, 
            __CvScalar color, int thickness);

        /// <summary>
        /// Draws a rectangle given two opposite corners of the rectangle (pt1 & pt2),
        /// if thicknessis smaller than 0 (e.g. thickness == CV_FILLED), the filled box is drawn
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="color"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cvRectangle(__CvArrPtr image, __CvPoint pt1, __CvPoint pt2, 
            __CvScalar color);
        #endregion
        
        /// <summary>
        /// Retrieves image ROI
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __CvRect cvGetImageROI(IntPtr image);

        /// <summary>
        /// Sets image ROI (region of interest) (COI is not changed)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvSetImageROI(__IplImagePtr image, __CvRect rect);

        /// <summary>
        /// Resets image ROI and COI
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rect"></param>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvResetImageROI(__IplImagePtr image);

        /// <summary>
        /// Creates a copy of IPL image (widthStep may differ)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __IplImagePtr cvCloneImage(__IplImagePtr image);

        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern double cvGetReal1D(__CvArrPtr arr, int idx);

        internal static unsafe float cvQueryHistValue_1D(IntPtr hist, int idx0 ) {
            //throw new NotImplementedException();
            return (float) cvGetReal1D(  ((__CvHistogram*)hist.ToPointer())->bins , idx0);
            //return (float)cvGetReal1D(hist->bins, idx0);
        }

        public unsafe delegate int __CvErrorCallback(
            int status,
            [MarshalAs(UnmanagedType.LPStr)]string func_name,
            [MarshalAs(UnmanagedType.LPStr)]string err_msg,
            [MarshalAs(UnmanagedType.LPStr)]string file_name,
            int line,
            [MarshalAs(UnmanagedType.LPStr)]string userdata);

        
        /// <summary>
        /// Assigns a new error-handling function
        /// </summary>
        /// <remarks>
        /// The actual return type of this function is:
        /// <code>
        /// typedef int (CV_CDECL *CvErrorCallback)( int status, const char* func_name,
        ///            const char* err_msg, const char* file_name, int line, void* userdata );
        /// </code>
        /// </remarks>
        /// <param name="userdata"></param>
        /// <param name="prev_userdata"></param>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void* cvRedirectError(__CvErrorCallback userdata, void** prev_userdata);

        /// <summary>
        /// Assigns a new error-handling function
        /// </summary>
        /// <remarks>
        /// The actual return type of this function is:
        /// <code>
        /// typedef int (CV_CDECL *CvErrorCallback)( int status, const char* func_name,
        ///            const char* err_msg, const char* file_name, int line, void* userdata );
        /// </code>
        /// </remarks>
        /// <param name="userdata"></param>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void* cvRedirectError(__CvErrorCallback userdata);

        /// <summary>
        /// Assigns a new error-handling function
        /// </summary>
        /// <remarks>
        /// The actual return type of this function is:
        /// <code>
        /// typedef int (CV_CDECL *CvErrorCallback)( int status, const char* func_name,
        ///            const char* err_msg, const char* file_name, int line, void* userdata );
        /// </code>
        /// </remarks>
        /// <returns></returns>
        [DllImport("cxcore100.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void* cvRedirectError();


        //typedef int (CV_CDECL *CvErrorCallback)( int status, const char* func_name,
        //            const char* err_msg, const char* file_name, int line, void* userdata );

    }
}
