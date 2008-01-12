using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    
    internal static partial class PInvoke
    {


        #region highgui.dll

        //private const string HIGHGUI_DLL = @"C:\dev\personal\OpenCV\opencv\bin\highgui100d.dll";
        private const string HIGHGUI_DLL = @"highgui100.dll";

        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __IplImagePtr cvCreateImageHeader(__CvSize src, int depth, int channels);

        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvConvertImage(__CvArrPtr src, __CvArrPtr dst, int flags);
        
        /// <summary>
        /// load a color image from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static __IplImagePtr cvLoadImage(string filename)
        {
            return cvLoadImage(filename, 1);
        }

        /// <summary>
        /// /// <summary>
        /// load an image from file 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="iscolor">can be a combination of above flags where CV_LOAD_IMAGE_UNCHANGED
        /// overrides the other flags using CV_LOAD_IMAGE_ANYCOLOR alone is 
        /// equivalent to CV_LOAD_IMAGE_UNCHANGED unless CV_LOAD_IMAGE_ANYDEPTH is 
        /// specified images are converted to 8bit</param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __IplImagePtr cvLoadImage([MarshalAs(UnmanagedType.LPStr)]string filename, int iscolor);

        /// <summary>
        /// stop capturing/reading and free resources
        /// </summary>
        /// <param name="capture"></param>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvReleaseCapture(ref __CvCapturePtr capture);

        
        /// <summary>
        /// start capturing frames from camera: index = camera_index + domain_offset (CV_CAP_*)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __CvCapturePtr cvCreateCameraCapture(int index);

        /// <summary>
        /// retrieve capture properties
        /// </summary>
        /// <param name="capture"></param>
        /// <param name="property_id"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern double cvGetCaptureProperty(__CvCapturePtr capture, int property_id);

        /// <summary>
        /// set capture properties
        /// </summary>
        /// <param name="capture"></param>
        /// <param name="property_id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern int cvSetCaptureProperty(__CvCapture* capture, int property_id, double value);

        /// <summary>
        /// Just a combination of cvGrabFrame and cvRetrieveFrame
        /// </summary>
        /// <remarks>!!!DO NOT RELEASE or MODIFY the retrieved frame!!!</remarks>
        /// <param name="capture"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __IplImagePtr cvQueryFrame(__CvCapturePtr capture);

        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __CvCapturePtr cvCreateFileCapture([MarshalAs(UnmanagedType.LPStr)]string filename);

        internal static char CV_FOURCC(char c1, char c2,char c3,char c4)  {
            return (char)(((c1)&255) + (((c2)&255)<<8) + (((c3)&255)<<16) + (((c4)&255)<<24));
        }

        /// <summary>
        /// initialize video file writer
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fourcc"></param>
        /// <param name="fps"></param>
        /// <param name="frame_size"></param>
        /// <param name="is_color"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __CvVideoWriter* cvCreateVideoWriter([MarshalAs(UnmanagedType.LPStr)]string filename, int fourcc, double fps, __CvSize frame_size, int is_color);

        /// <summary>
        /// initialize video file writer
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fourcc"></param>
        /// <param name="fps"></param>
        /// <param name="frame_size"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern __CvVideoWriter* cvCreateVideoWriter([MarshalAs(UnmanagedType.LPStr)]string filename, int fourcc, double fps, __CvSize frame_size);

        /// <summary>
        /// write frame to video file
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern int cvWriteFrame(__CvVideoWriter* writer, __IplImagePtr image);

        /// <summary>
        /// close video file writer
        /// </summary>
        /// <param name="writer"></param>
        [DllImport(HIGHGUI_DLL, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern void cvReleaseVideoWriter(__CvVideoWriter** writer);
        #endregion
    }
}
