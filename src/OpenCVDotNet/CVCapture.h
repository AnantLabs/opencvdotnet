/**
 * (C) 2007 Elad Ben-Israel
 * This code is licenced under the GPL.
 */

#pragma once

namespace OpenCVDotNet
{
	public ref class CVCapture
	{
	private:
		CvCapture* capture;
		String^ filename;

	public:
		CVCapture(String^ filename)
		{
			capture = NULL;
			Open(filename);
		}

		CVCapture()
		{
			capture = cvCreateCameraCapture(CV_CAP_ANY);
		}

		CVCapture(int cameraId)
		{
			capture = cvCreateCameraCapture(cameraId);
		}

		~CVCapture()
		{
			Release();
		}

		void Release()
		{
			if (capture == NULL) return;

			pin_ptr<CvCapture*> cap = &capture;
			cvReleaseCapture(cap);
		}

		property int Width
		{
			int get()
			{
				return (int) cvGetCaptureProperty(capture, CV_CAP_PROP_FRAME_WIDTH);
			}
		}

		property int Height
		{
			int get()
			{
				return (int) cvGetCaptureProperty(capture, CV_CAP_PROP_FRAME_HEIGHT);
			}
		}

		CVImage^ QueryFrame()
		{
			IplImage* frame = cvQueryFrame(capture);
			if (frame == NULL) return nullptr;
			CVImage^ newImage = gcnew CVImage(gcnew CVImage(frame));
			return newImage;
		}

		void Open(String^ filename)
		{
			Release();

			char fn[1024 + 1];
			CVUtils::StringToCharPointer(filename, fn, 1024);
			capture = cvCreateFileCapture(fn);

			if (capture == NULL)
			{
				throw gcnew CVException(
					String::Format("Unable to open file	'{0}' for capture", filename));
			}

			this->filename = filename;
		}

		property int FramesPerSecond
		{
			int get()
			{
				return (int) cvGetCaptureProperty(capture, CV_CAP_PROP_FPS);
			}
		}

		void Restart()
		{
			Open(filename);
		}

		CVImage^ CreateCompatibleImage()
		{
			CVImage^ img = gcnew CVImage(this->Width, this->Height, CVDepth::Depth8U, 3);
			return img;
		}
	};
};