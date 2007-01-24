/**
 * (C) 2007 Elad Ben-Israel
 * This code is licenced under the GPL.
 */

#pragma once

#include "stdafx.h"

#include "CVRgbPixel.h"
#include "CVUtils.h"
#include "CVHistogram.h"
#include "CVPair.h"
#include "CVArr.h"


namespace OpenCVDotNet
{
	struct RgbPixel
	{
		Byte b;
		Byte g;
		Byte r;
	};

	public enum class CVInterpolation
	{
		NearestNeigbor = CV_INTER_NN,
		Linear = CV_INTER_LINEAR,
		Area = CV_INTER_AREA,
		Cubic = CV_INTER_CUBIC
	};

	public enum class CVDepth
	{
		Depth1U = IPL_DEPTH_1U,
		Depth8U = IPL_DEPTH_8U,
		Depth16U = IPL_DEPTH_16U,
		Depth32F = IPL_DEPTH_32F,
		Depth8S = IPL_DEPTH_8S,
		Depth16S = IPL_DEPTH_16S,
		Depth32S = IPL_DEPTH_32S,
	};

	public ref class CVConnectedComp
	{
	private:
		double area;
		unsigned char r, g, b;
		System::Drawing::Rectangle rect;
		CvSeq* contour;
	public:
		CVConnectedComp(CvConnectedComp* in)
		{
			area = in->area;

			RgbPixel* rgb = (RgbPixel*) in->value.val;
			r = rgb->r;
			g = rgb->g;
			b = rgb->b;

			rect = System::Drawing::Rectangle(in->rect.x, in->rect.y, in->rect.width, in->rect.height);
			contour = in->contour;
		}

		property System::Drawing::Rectangle Rect
		{
			System::Drawing::Rectangle get()
			{
				return this->rect;
			}
		}
	};

	public ref class CVImage : public CVArr, public IDisposable
	{
	private:
		IplImage* image;
		bool created;

	public:
		CVImage(IplImage* internal_image)
		{
			image = internal_image;
			created = false;
		}

		CVImage(CVImage^ clone)
		{
			Create(clone->Width, clone->Height, clone->Depth, clone->Channels);
			CopyFrom(clone, CV_CVTIMG_FLIP);
		}

		void CopyFrom(CVImage^ source)
		{
			CopyFrom(source, 0);
		}

		void CopyFrom(CVImage^ source, int flags)
		{
			cvConvertImage(source->image, this->image, flags);
		}

		CVImage(int width, int height, CVDepth depth, int channels)
		{
			Create(width, height, depth, channels);
		}

		CVImage(String^ filename)
		{
			LoadImage(filename, true);
		}

		CVImage(String^ filename, bool isColor)
		{
			LoadImage(filename, isColor);
		}

		virtual ~CVImage()
		{
			Release();
		}

		void LoadImage(String^ filename, bool isColor)
		{
			Release();

			char fn[1024 + 1];
			CVUtils::StringToCharPointer(filename, fn, 1024);
			image = cvLoadImage(fn, isColor ? 1 : 0);
			created = true;
		}

		void Create(int width, int height, CVDepth depth, int channels)
		{
			image = cvCreateImage(cvSize(width, height), (int) depth,  channels);
			created = true;
		}

		void Release()
		{
			if (!created) return;

			IplImage* ptr = image;
			cvReleaseImage(&ptr);
		}

		property CVRgbPixel^ default[int, int]
		{
			CVRgbPixel^ get(int row, int col)
			{
				RgbPixel* rowPixels = ((RgbPixel*)(image->imageData + row * image->widthStep));
				return gcnew CVRgbPixel(rowPixels[col].r, rowPixels[col].g, rowPixels[col].b);
			}

			void set(int row, int col, CVRgbPixel^ value)
			{
				RgbPixel* rowPixels = ((RgbPixel*)(image->imageData + row * image->widthStep));
				rowPixels[col].r = value->R;
				rowPixels[col].g = value->G;
				rowPixels[col].b = value->B;
			}
		}

		property int Width
		{
			int get()
			{
				return image->width;
			}
		}

		property int Height
		{
			int get()
			{
				return image->height;
			}
		}

		property int Channels
		{
			int get()
			{
				return image->nChannels;
			}
		}

		property CVDepth Depth
		{
			CVDepth get()
			{
				return (CVDepth) image->depth;
			}
		}

		virtual property CvArr* Array
		{
			CvArr* get() override
			{
				pin_ptr<CvArr> intr = image;
				return intr;
			}
		}

		virtual property IplImage* Internal
		{
			IplImage* get()
			{
				return (IplImage*) Array;
			}
		}

		void Zero()
		{
			if (image != NULL)
				cvZero(image);
		}
		
		void DrawRectangle(int left, int top, int right, int bottom, unsigned char r, unsigned char g, unsigned char b)
		{
			DrawRectangle(left, top, right, bottom, r, g, b, 1, 8, 0);
		}

		void DrawRectangle(int left, int top, int right, int bottom, unsigned char r, unsigned char g, unsigned char b, int thickness)
		{
			DrawRectangle(left, top, right, bottom, r, g, b, thickness, 8, 0);
		}

		void DrawRectangle(int left, int top, int right, int bottom, unsigned char r, unsigned char g, unsigned char b, int thickness, int lineType)
		{
			DrawRectangle(left, top, right, bottom, r, g, b, thickness, lineType, 0);
		}

		void DrawRectangle(int left, int top, int right, int bottom, unsigned char r, unsigned char g, unsigned char b, int thickness, int lineType, int shift)
		{
			cvRectangle(image, cvPoint(left, top), cvPoint(right, bottom), CV_RGB(r,g,b), thickness, lineType, shift);
		}

		void DrawRectangle(int left, int top, int right, int bottom, array<unsigned char>^ colorRbg)
		{
			DrawRectangle(left, top, right, bottom, colorRbg[0], colorRbg[2], colorRbg[1], -1);
		}

		void Split(CVImage^ ch0, CVImage^ ch1, CVImage^ ch2, CVImage^ ch3)
		{
			IplImage* d0 = ch0 != nullptr ? ch0->image : NULL;
			IplImage* d1 = ch1 != nullptr ? ch1->image : NULL;
			IplImage* d2 = ch2 != nullptr ? ch2->image : NULL;
			IplImage* d3 = ch3 != nullptr ? ch3->image : NULL;
			cvSplit(image, d0, d1, d2, d3);
		}

		array<CVImage^>^ Split()
		{
			List<CVImage^>^ channels = gcnew List<CVImage^>();

			int w = RegionOfInterest.Width;
			int h = RegionOfInterest.Height;

			CVImage^ c0 = Channels >= 1 ? gcnew CVImage(w, h, this->Depth, 1) : nullptr;
			CVImage^ c1 = Channels >= 2 ? gcnew CVImage(w, h, this->Depth, 1) : nullptr;
			CVImage^ c2 = Channels >= 3 ? gcnew CVImage(w, h, this->Depth, 1) : nullptr;
			CVImage^ c3 = Channels >= 4 ? gcnew CVImage(w, h, this->Depth, 1) : nullptr;

			Split(c0, c1, c2, c3);

			if (c0 != nullptr) channels->Add(c0);
			if (c1 != nullptr) channels->Add(c1);
			if (c2 != nullptr) channels->Add(c2);
			if (c3 != nullptr) channels->Add(c3);
			return channels->ToArray();
		}

		void Merge(CVImage^ blue, CVImage^ green, CVImage^ red)
		{
			IplImage* c0 = blue != nullptr ? blue->image : NULL;
			IplImage* c1 = green != nullptr ? green->image : NULL;
			IplImage* c2 = red != nullptr ? red->image : NULL;
			cvMerge(c0, c1, c2, NULL, image);
		}

		void Merge(array<CVImage^>^ rbgChannels)
		{
			assert(rbgChannels->Length == 3);
			Merge(rbgChannels[0], rbgChannels[1], rbgChannels[2]);
		}

		CVHistogram^ CalcHistogram(array<int>^ binSizes, array<CVPair^>^ binRanges)
		{
			CVHistogram^ h = gcnew CVHistogram(binSizes, binRanges);

			IplImage** images = new IplImage*[this->Channels];
			if (this->Channels == 1) 
			{
				images[0] = this->Internal;
			}
			else
			{
				array<CVImage^>^ planes = this->Split();
				for (int i = 0; i < planes->Length; ++i)
					images[i] = planes[i]->Internal;
			}

			cvCalcHist(images, h->Internal);

			delete [] images;

			return h;
		}

		void Resize(int newWidth, int newHeight)
		{
			Resize(newWidth, newHeight, CVInterpolation::Area);
		}

		void Resize(int newWidth, int newHeight, CVInterpolation interpolation)
		{
			CVImage^ newImage = gcnew CVImage(newWidth, newHeight, Depth, Channels);
			cvResize(this->image, newImage->image, (int) interpolation);
			Release();
			this->image = newImage->image;
			newImage->created = false;
		}

		Bitmap^ ToBitmap()
		{
			SIZE size = { 0, 0 };
			int channels = 0;
			void* dst_ptr = 0;
			const int channels0 = 3;
			int origin = 0;
			CvMat stub, dst, *image;
			bool changed_size = false; // philipg

			HDC hdc = CreateCompatibleDC(0);
			CvArr* arr = this->Array;

			if (CV_IS_IMAGE_HDR(arr)) origin = ((IplImage*)arr)->origin;

			image = cvGetMat(arr, &stub);

			uchar buffer[sizeof(BITMAPINFO) + 255*sizeof(RGBQUAD)];
			BITMAPINFO* binfo = (BITMAPINFO*)buffer;

			size.cx = image->width;
			size.cy = image->height;
			channels = channels0;

			FillBitmapInfo(binfo, size.cx, size.cy, channels*8, 1);

			HBITMAP hBitmap = CreateDIBSection(hdc, binfo, DIB_RGB_COLORS, &dst_ptr, 0, 0);
			if (hBitmap == NULL)
				return nullptr;

			cvInitMatHeader(&dst, size.cy, size.cx, CV_8UC3, dst_ptr, (size.cx * channels + 3) & -4);
			cvConvertImage(image, &dst, origin == 0 ? CV_CVTIMG_FLIP : 0);

			System::Drawing::Bitmap^ bmpImage = System::Drawing::Image::FromHbitmap(IntPtr(hBitmap));

			DeleteObject(hBitmap);
			DeleteDC(hdc);

			return bmpImage;
		}

		static void FillBitmapInfo( BITMAPINFO* bmi, int width, int height, int bpp, int origin )
		{
			assert( bmi && width >= 0 && height >= 0 && (bpp == 8 || bpp == 24 || bpp == 32));

			BITMAPINFOHEADER* bmih = &(bmi->bmiHeader);

			memset( bmih, 0, sizeof(*bmih));
			bmih->biSize = sizeof(BITMAPINFOHEADER);
			bmih->biWidth = width;
			bmih->biHeight = origin ? abs(height) : -abs(height);
			bmih->biPlanes = 1;
			bmih->biBitCount = (unsigned short)bpp;
			bmih->biCompression = BI_RGB;

			if( bpp == 8 )
			{
				RGBQUAD* palette = bmi->bmiColors;
				int i;
				for( i = 0; i < 256; i++ )
				{
					palette[i].rgbBlue = palette[i].rgbGreen = palette[i].rgbRed = (BYTE)i;
					palette[i].rgbReserved = 0;
				}
			}
		}

		property System::Drawing::Rectangle RegionOfInterest
		{
			void set(System::Drawing::Rectangle rect)
			{
				cvSetImageROI(Internal, cvRect(rect.X, rect.Y, rect.Width, rect.Height));
			}

			System::Drawing::Rectangle get()
			{
				CvRect rc = cvGetImageROI(Internal);
				return System::Drawing::Rectangle(rc.x, rc.y, rc.width, rc.height);
			}
		}

		void ResetROI()
		{
			cvResetImageROI(Internal);
		}

		CVImage^ CalcBackProject(CVHistogram^ histogram)
		{
			cli::array<CVImage^>^ planes = Split();

			CVImage^ backProjection = 
				gcnew CVImage(
					planes[0]->RegionOfInterest.Width, 
					planes[0]->RegionOfInterest.Height, 
					planes[0]->Depth, 
					planes[0]->Channels);

			IplImage** iplImages = new IplImage*[planes->Length];
			for (int i = 0; i < planes->Length; ++i)
				iplImages[i] = planes[i]->Internal;

			cvCalcBackProject(iplImages, backProjection->Internal, histogram->Internal);

			delete [] iplImages;

			for (int i = 0; i < planes->Length; ++i)
				planes[i]->Release();

			return backProjection;
		}

		CVConnectedComp^ MeanShift(System::Drawing::Rectangle window, int maxIterations)
		{
			return MeanShift(window, CV_TERMCRIT_ITER, maxIterations);
		}

		CVConnectedComp^ MeanShift(System::Drawing::Rectangle window, double eps)
		{
			return MeanShift(window, CV_TERMCRIT_EPS, eps);
		}

		CVConnectedComp^ MeanShift(System::Drawing::Rectangle window, int maxIterations, double eps)
		{
			return MeanShift(window, CV_TERMCRIT_ITER | CV_TERMCRIT_EPS, maxIterations, eps);
		}

		CVConnectedComp^ MeanShift(System::Drawing::Rectangle window, int termCriteria, int maxIterations, double eps)
		{
			CvRect wnd = cvRect(window.X, window.Y, window.Width, window.Height);
			CvTermCriteria tc = cvTermCriteria(termCriteria, maxIterations, eps);
			
			CvConnectedComp comp;
			cvMeanShift(Internal, wnd, tc, &comp);

			return gcnew CVConnectedComp(&comp);
		}

	};


};