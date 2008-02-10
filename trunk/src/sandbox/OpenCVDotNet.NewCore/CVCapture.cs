using System;
using System.Collections.Generic;
using System.Text;
using OpenCVDotNet.Native;

namespace OpenCVDotNet
{
/**
 * (C) 2007 Elad Ben-Israel and Yoav HaCohen
 * This code is licenced under the GPL.
 */
	public class CVCapture : IDisposable
	{
	    private __CvCapturePtr capture;
	    private string filename;

        private CVImage asImage;

	    public CVCapture(string filename)
		{
			capture.ptr = IntPtr.Zero;
			asImage = null;
			Open(filename);
		}

		public CVCapture()
		{
			asImage = null;
			capture = PInvoke.cvCreateCameraCapture((int)CV_CAP.CV_CAP_ANY);
            CVUtils.CheckLastError();
		}

		public CVCapture(int cameraId)
		{
			asImage = null;
			capture = PInvoke.cvCreateCameraCapture(cameraId);
            CVUtils.CheckLastError();
		}

		~CVCapture()
		{
			Release();
		}

		public void Release()
		{
			if (asImage != null)
			{
				asImage.Release();
				asImage = null;
			}

			if (capture.ptr != IntPtr.Zero)
			{
                PInvoke.cvReleaseCapture(ref capture);
                CVUtils.CheckLastError();
			}
		}

		public int Width
		{
			get
			{
				if (asImage != null) return asImage.Width;
				int result = (int) PInvoke.cvGetCaptureProperty(capture,(int)CV_CAP_PROP.CV_CAP_PROP_FRAME_WIDTH);
                CVUtils.CheckLastError();
                return result;
			}
		}

		public int Height
		{
			get
			{
				if (asImage != null) return asImage.Height;
                int result = (int)PInvoke.cvGetCaptureProperty(capture, (int)CV_CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT);
                CVUtils.CheckLastError();
                return result;
			}
		}

		public CVImage QueryFrame()
		{
			if (asImage != null) return asImage.Clone();

			__IplImagePtr frame = PInvoke.cvQueryFrame(capture);
            CVUtils.CheckLastError();
			if (frame.ptr == IntPtr.Zero) return null;
			CVImage newImage = new CVImage(new CVImage(frame));
			return newImage;
		}

		public void Open(string filename)
		{
			Release();
			
			capture.ptr = IntPtr.Zero;
			asImage = null;

			string ext = System.IO.Path.GetExtension(filename);

			// if the extension of the filename is not AVI, try opening as an image.
			if (ext.ToUpper().CompareTo(".AVI") != 0)
			{
				asImage = new CVImage(filename);
			}
			else
			{
                capture = PInvoke.cvCreateFileCapture(filename);
                CVUtils.CheckLastError();
				if (capture.ptr == IntPtr.Zero)
				{
					throw new CVException(
						string.Format("Unable to open file	'{0}' for capture.", filename));
				}
			}

			this.filename = filename;
		}

		public int FramesPerSecond
		{
			get
			{
				// if this is an image, return 30 as default FPS.
				if (asImage != null) return 30;
				
				int res = (int) PInvoke.cvGetCaptureProperty(capture, (int)CV_CAP_PROP.CV_CAP_PROP_FPS);
                CVUtils.CheckLastError();
                return res;
			}
		}

		public void Restart()
		{
			Open(filename);
		}

		public CVImage CreateCompatibleImage()
		{
			CVImage img = new CVImage(this.Width, this.Height, CVDepth.Depth8U, 3);
			return img;
		}

        #region IDisposable Members

        public void Dispose()
        {
            this.Release();
        }

        #endregion
    }
}
