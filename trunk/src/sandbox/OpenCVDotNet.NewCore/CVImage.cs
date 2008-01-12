using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace OpenCVDotNet
{
	
    


    #region CVConnectedComp
	public class CVConnectedComp
	{
	    private	double area;
        private byte r, g, b;
        private System.Drawing.Rectangle rect;
        private __CvSeqPtr contour;
	    private System.Drawing.Color avgColor;

	    internal CVConnectedComp(ref __CvConnectedComp input)
		{
			area = input.area;

			avgColor = CVUtils.ScalarToColor(input.value);

			rect = new System.Drawing.Rectangle(input.rect.x, input.rect.y, input.rect.width, input.rect.height);
			contour = input.contour;
		}

		public CVConnectedComp(System.Drawing.Rectangle rect)
		{
			this.rect = rect;
		}

		public System.Drawing.Rectangle Rect
		{
			get
			{
				return this.rect;
			}
		}
		
		public double Area
		{
			get
			{
				return this.area;
			}
		}

		public System.Drawing.Color AverageColor
		{
			get
			{
				return this.avgColor;
			}
		}
	}
    #endregion

    
    

	public class CVImage : CVArr, IDisposable
	{
        #region Data Members
	    internal __IplImagePtr image;
		internal bool created;  
        #endregion

        #region CTOR(s)

        #region From IplImage
	    internal CVImage(__IplImagePtr internal_image)
		{
			image = internal_image;
			created = false;
		}
        #endregion
		
        #region From CVImage (Copy CTOR)
        public unsafe CVImage(CVImage clone)
		{
			Create(clone.Width, clone.Height, clone.Depth, clone.Channels);
            PInvoke.cvConvertImage(clone.Array, this.image, clone.Internal.ToPointer()->origin == 1 ? (int)CVConvertImageFlags.Flip : 0);
		}
        #endregion

        #region From System.Drawing.Bitmap
		public unsafe  CVImage(System.Drawing.Bitmap sourceImage) 
        { 
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(); 
            rect.X = 0 ; 
            rect.Y = 0 ; 
            rect.Width = sourceImage.Width; 
            rect.Height = sourceImage.Height; 

            System.Drawing.Imaging.BitmapData bData = 
                    sourceImage.LockBits(rect, 
                    System.Drawing.Imaging.ImageLockMode.ReadWrite, 
                    sourceImage.PixelFormat);

            __IplImagePtr tempImage = PInvoke.cvCreateImageHeader(new __CvSize(sourceImage.Width, sourceImage.Height), 8, Bitmap.GetPixelFormatSize(sourceImage.PixelFormat) / 8);
            tempImage.ToPointer()->imageData = (byte*)bData.Scan0.ToPointer();

            __IplImagePtr[] dst = new __IplImagePtr[4];
            for (int i = 0; i < 4; ++i)
            {
                dst[i] = IntPtr.Zero;
            }
            for (int i = 0; i < tempImage.ToPointer()->nChannels; i++)
            {
                dst[i] = PInvoke.cvCreateImage(new __CvSize(sourceImage.Width, sourceImage.Height), 8, 1);
            }

            PInvoke.cvSplit(
                tempImage,
                dst[0],
                dst[1],
                dst[2],
                dst[3]);

            image = PInvoke.cvCreateImage(new __CvSize(sourceImage.Width, sourceImage.Height), 8, 3); 
            PInvoke.cvMerge(dst[0], dst[1], dst[2], IntPtr.Zero, image) ;

            for (int i = 0; i < tempImage.ToPointer()->nChannels; i++)
            {
                PInvoke.cvReleaseImage(ref dst[i]);
            }

            created = true; 
            sourceImage.UnlockBits(bData); 
        } 
        #endregion		

		public CVImage(int width, int height, CVDepth depth, int channels)
		{
			Create(width, height, depth, channels);
		}

		
		public CVImage(string filename)
		{
			LoadImage(filename, true);
		}

		
		public CVImage(String filename, bool isColor)
		{
			LoadImage(filename, isColor);
		}

		
		public CVImage(CVImage[] bgrChannels)
		{
			Create(bgrChannels[0].Width, bgrChannels[0].Height, bgrChannels[0].Depth, 3);
			this.Merge(bgrChannels);
		}

		~CVImage()
		{
			Release();
		}
        #endregion
		
        #region Load Image
		public void LoadImage(String filename, bool isColor)
        {
            if (!System.IO.File.Exists(filename)) {
				throw new System.IO.FileNotFoundException(filename);
			}

			Release();

            image = PInvoke.cvLoadImage(filename, isColor ? 1 : 0);
			created = true;
		}
        #endregion
		
        #region Release
		public void Release()
		{
			if (!created) return;
            //created = false;
			__IplImagePtr ptr = image;
            if (ptr.ptr != IntPtr.Zero)
            {
                PInvoke.cvReleaseImage(ref ptr);
                image = new __IplImagePtr();
            }
		}
        #endregion

        #region this (Point)
		public OpenCVDotNet.CVRgbPixel this[System.Drawing.Point pt]
		{
			get
			{
				return this[pt.Y, pt.X];
			}
			set
			{
				this[pt.Y, pt.X] = value;
			}
		}
        #endregion

        #region GetPixelPtr (row, col)
		public unsafe byte* GetPixelPtr(int row, int col)
		{
			if (row < 0 || row >= Height || col < 0 || col >= Width)
			{
				throw new CVException(String.Format("Attempt to access a pixel ({0},{1}) outside of bounds of the image (w={2},h={3})",
					col, row, Width, Height));
			}

			byte* rowPtr = image.ToPointer()->imageData + row * image.ToPointer()->widthStep;
			byte* pixelPtr = rowPtr + (col * image.ToPointer()->nChannels);
			return pixelPtr;
		}
        #endregion
		
        #region this (row, col)
		public OpenCVDotNet.CVRgbPixel this[int row, int col]
		{
			get
			{
                unsafe
                {
                    byte* pixel = GetPixelPtr(row, col);
                    if (Channels == 3)
                    {
                        return new OpenCVDotNet.CVRgbPixel(pixel[2], pixel[1], pixel[0]);
                    }
                    else if (Channels == 1)
                    {
                        return new OpenCVDotNet.CVRgbPixel(0, 0, pixel[0]);
                    }
                    else
                    {
                        return null;
                    }
                }
			}
			set
			{
                unsafe
                {
                    byte* pixel = GetPixelPtr(row, col);

                    if (Channels == 3)
                    {
                        pixel[2] = value.R;
                        pixel[1] = value.G;
                        pixel[0] = value.B;
                    }
                    else if (Channels == 1)
                    {
                        pixel[0] = value.B;
                    }
                    else
                    {
                        System.Diagnostics.Debug.Fail("Unsupported number of channels.");
                    }
                }
			}
		}
        #endregion

        #region Width
		public int Width
		{
			get
			{
                unsafe
                {
                    return image.ToPointer()->width;
                }
			}
		}
        #endregion

        #region Height
        public int Height
		{
			get
			{
                unsafe
                {
                    return image.ToPointer()->height;
                }
			}
        }
        #endregion

        #region Channels
        public int Channels
		{
			get
			{
                unsafe
                {
                    return image.ToPointer()->nChannels;
                }
			}
        }
        #endregion

        public CVDepth Depth
		{
			get
            {
                unsafe
                {
                    return (CVDepth)image.ToPointer()->depth;
                }
			}
		}

		
        //virtual public __CvArrPtr Array
        //{
        //    get
        //    {
        //        pin_ptr<CvArr> intr = image;
        //        return intr;
        //    }
        //}
        internal override __CvArrPtr  Array
        {
	        get { 
                return image; 
            }
        }
		
		internal __IplImagePtr Internal
		{
			get
			{
				return image;
			}
		}

		public void Zero()
		{
			if (image.ptr != null)
				PInvoke.cvZero(image);
		}

        public void DrawLine(System.Drawing.Point pt1, System.Drawing.Point pt2, System.Drawing.Color color)
		{
			DrawLine(pt1, pt2, color, 1);
		}


        public void DrawLine(System.Drawing.Point pt1, System.Drawing.Point pt2, System.Drawing.Color color, int thickness)
		{
			DrawLine(pt1, pt2, color, thickness, 8);
		}


        public void DrawLine(System.Drawing.Point pt1, System.Drawing.Point pt2, System.Drawing.Color color, int thickness, int lineType)
		{
			DrawLine(pt1, pt2, color, thickness, lineType, 0);
		}

        public void DrawLine(System.Drawing.Point pt1, System.Drawing.Point pt2, System.Drawing.Color color, int thickness, int lineType, int shift)
		{
            // __CvScalar here is come instread CV_RGB
			PInvoke.cvLine(this.Internal, new __CvPoint(pt1.X, pt1.Y), new __CvPoint(pt2.X, pt2.Y),
                new __CvScalar(color.R, color.G, color.B, 0), thickness, lineType, shift);
		}

        public void DrawRectangle(System.Drawing.Rectangle rect, System.Drawing.Color color)
		{
			DrawRectangle(rect, color, 1);
		}

        public void DrawRectangle(System.Drawing.Rectangle rect, System.Drawing.Color color, int thickness)
		{
			DrawRectangle(rect, color, thickness, 8, 0);
		}

        public void DrawRectangle(System.Drawing.Rectangle rect, System.Drawing.Color color, int thickness, int lineType)
		{
			DrawRectangle(rect, color, thickness, lineType, 0);
		}

        public void DrawRectangle(System.Drawing.Rectangle rect, System.Drawing.Color color, int thickness, int lineType, int shift)
		{
			PInvoke.cvRectangle(
                image,
                new __CvPoint(rect.Left, rect.Top), 
                new __CvPoint(rect.Right, rect.Bottom), 
                new __CvScalar(color.R,color.G,color.B, 0), thickness, lineType, shift);
		}

        public void Split(CVImage ch0, CVImage ch1, CVImage ch2, CVImage ch3)
		{
			__IplImagePtr d0 = ch0 != null ? ch0.image : IntPtr.Zero;
            __IplImagePtr d1 = ch1 != null ? ch1.image : IntPtr.Zero;
            __IplImagePtr d2 = ch2 != null ? ch2.image : IntPtr.Zero;
            __IplImagePtr d3 = ch3 != null ? ch3.image : IntPtr.Zero;

			PInvoke.cvSplit(image, d0, d1, d2, d3);
		}

        public CVImage[] Split()
		{
			List<CVImage> channels = new List<CVImage>();

			int w = RegionOfInterest.Width;
			int h = RegionOfInterest.Height;

			CVImage c0 = Channels >= 1 ? new CVImage(w, h, this.Depth, 1) : null;
			CVImage c1 = Channels >= 2 ? new CVImage(w, h, this.Depth, 1) : null;
			CVImage c2 = Channels >= 3 ? new CVImage(w, h, this.Depth, 1) : null;
			CVImage c3 = Channels >= 4 ? new CVImage(w, h, this.Depth, 1) : null;

			Split(c0, c1, c2, c3);

			if (c0 != null) channels.Add(c0);
			if (c1 != null) channels.Add(c1);
			if (c2 != null) channels.Add(c2);
			if (c3 != null) channels.Add(c3);
			return channels.ToArray();
		}

        public void Merge(CVImage blue, CVImage green, CVImage red)
		{
			__IplImagePtr c0 = blue != null ? blue.image : IntPtr.Zero;
            __IplImagePtr c1 = green != null ? green.image : IntPtr.Zero;
            __IplImagePtr c2 = red != null ? red.image : IntPtr.Zero;
            PInvoke.cvMerge(c0, c1, c2, IntPtr.Zero, image);
		}

        public void Merge(CVImage[] rbgChannels)
		{
			System.Diagnostics.Debug.Assert(rbgChannels.Length == 3, "rgbChannels array must be of length 3.");
			Merge(rbgChannels[0], rbgChannels[1], rbgChannels[2]);
		}

        public CVHistogram CalcHistogram(int binsSize)
		{
			return CalcHistogram(binsSize, null);
		}


        public CVHistogram CalcHistogram(int binsSize, CVImage mask)
		{
			Int32[] binSizes = new Int32[3];
			CVPair[] binRanges = new CVPair[3];

			binSizes[0] = binSizes[1] = binSizes[2] = binsSize;
			binRanges[0] = binRanges[1] = binRanges[2] = new CVPair(0, 255);

			return CalcHistogram(binSizes, binRanges, mask);
		}

        public CVHistogram CalcHistogram(int[] binSizes, CVPair[] binRanges)
		{
			return CalcHistogram(binSizes, binRanges, null);
		}

        public CVHistogram CalcHistogram(int[] binSizes, CVPair[] binRanges, CVImage mask)
		{
            CVHistogram h = new CVHistogram(binSizes, binRanges);

            __IplImagePtr[] images = new __IplImagePtr[this.Channels];
            if (this.Channels == 1)
            {
                images[0] = this.Internal;
            }
            else
            {
                CVImage[] planes = this.Split();
                for (int i = 0; i < planes.Length; ++i)
                {
                    images[i] = planes[i].Internal;
                }
            }

            __CvArrPtr maskArr = IntPtr.Zero;
            if (mask != null) maskArr = mask.Array;

            PInvoke.cvCalcHist(images, h.Internal, 0, maskArr);
            return h;
		}

        public void Resize(int newWidth, int newHeight)
		{
			Resize(newWidth, newHeight, CVInterpolation.Area);
		}

        public void Resize(int newWidth, int newHeight, CVInterpolation interpolation)
		{
			CVImage newImage = new CVImage(newWidth, newHeight, Depth, Channels);
			PInvoke.cvResize(this.image, newImage.image, (int) interpolation);
			Release();
			this.image = newImage.image;
			newImage.created = false;
		}


        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr dc);

        public System.Drawing.Bitmap ToBitmap()
		{
            
            #region Initialize Bitmap And Pixel Format
            PixelFormat pixelFormat;
            if (this.Depth == CVDepth.Depth8U)
            {
                switch (this.Channels)
                {
                    case 1:
                        pixelFormat = PixelFormat.Format8bppIndexed;
                        break;
                    case 3:
                        pixelFormat = PixelFormat.Format24bppRgb;
                        break;
                    case 4:
                        pixelFormat = PixelFormat.Format32bppArgb;
                        break;
                    default:
                        throw new NotImplementedException("Format is not supported.");
                }
            }
            else
            {
                throw new NotImplementedException("Format is not supported.");
            }
            Bitmap result = new Bitmap(this.Width, this.Height, pixelFormat);
            #endregion

            BitmapData resultData = result.LockBits(new Rectangle(Point.Empty, new Size(this.Width, this.Height)), ImageLockMode.WriteOnly, pixelFormat);
            unsafe
            {
                byte* pWrite = (byte*)resultData.Scan0;
                int cols = this.Width;
                int rows = this.Height;
                for (int row = 0; row < rows; ++row, pWrite += resultData.Stride - cols * Channels)
                {
                    for (int col = 0; col < cols; ++col, pWrite += Channels)
                    {
                        // TODO: Improve performance
                        CVRgbPixel c = this[row, col];
                        pWrite[0] = c.B;
                        pWrite[1] = c.G;
                        pWrite[2] = c.R;
                    }
                }
            }

            result.UnlockBits(resultData);

            //BitmapData resultData = result.LockBits(new Rectangle(Point.Empty, new Size(this.Width, this.Height)), ImageLockMode.WriteOnly, pixelFormat);
            //__CvMatPtr stubMat = PInvoke.cvCreateMat(1, 1, 0);
            //__CvMatPtr dstMat = PInvoke.cvCreateMat(1, 1, 0);
            //__CvMatPtr imageMat;
            //__CvArrPtr arrPtr = this.Array;
            //int origin = 0;
            //unsafe {
            //    if (PInvoke.CV_IS_IMAGE_HDR(arrPtr)) {
            //        origin = (new __IplImagePtr(arrPtr.ptr)).ToPointer()->origin;
            //    }
            //}
            //imageMat = PInvoke.cvGetMat(arrPtr, stubMat);
            
            //IntPtr hBitmapData = resultData.Scan0;

            //PInvoke.cvInitMatHeader(dstMat, this.Height, this.Width, PInvoke.CV_MAKETYPE(8, 3), hBitmapData, (this.Width * this.Channels + 3) & -4);

            //unsafe
            //{
            //    PInvoke.cvConvertImage(image, dstMat.ptr,
            //        Internal.ToPointer()->origin == 1 ? (int)CVConvertImageFlags.Flip : 0);
            //}

            //result.UnlockBits(resultData);
            
            // TODO: Realese resources...
            
            // Setting Pallete
            //if (pixelFormat == PixelFormat.Format8bppIndexed)
            //{
            //    for (byte i = 0; i <= 255; ++i) result.Palette.Entries[i] = Color.FromArgb(i, i, i);
            //}
            return result;

                //throw new NotImplementedException();
            //__CvSize size = new __CvSize(0,0);
            //int channels = 0;
            //IntPtr dst_ptr = IntPtr.Zero;
            //const int channels0 = 3;
            //int origin = 0;
            //__CvMatPtr stub = PInvoke.cvCreateMat(1,1,0), dst;
            //IntPtr image;
            //bool changed_size = false; // philipg

            ////HDC hdc = CreateCompatibleDC(0);
            //IntPtr hdc = CreateCompatibleDC(IntPtr.Zero);

            //__CvArrPtr arr = this.Array;

            //if (PInvoke.CV_IS_IMAGE_HDR(arr)) origin = ((__IplImage*) arr.ptr.ToPointer())->origin;

            //image = PInvoke.cvGetMat(arr, &stub);
            
            //byte buffer = new byte[sizeof(BITMAPINFO) + 255*sizeof(RGBQUAD)];
            ////BITMAPINFO* binfo = (BITMAPINFO*)buffer;

            //size.cx = image->width;
            //size.cy = image->height;
            //channels = channels0;

            //FillBitmapInfo(binfo, size.cx, size.cy, channels*8, 1);

            //HBITMAP hBitmap = CreateDIBSection(hdc, binfo, DIB_RGB_COLORS, &dst_ptr, 0, 0);
            //if (hBitmap == NULL)
            //    return nullptr;

            //PInvoke.cvInitMatHeader(&dst, size.cy, size.cx, CV_8UC3, dst_ptr, (size.cx * channels + 3) & -4);
            //PInvoke.cvConvertImage(image, &dst, origin == 0 ? CV_CVTIMG_FLIP : 0);

            //System.Drawing.Bitmap^ bmpImage = System.Drawing.Image::FromHbitmap(IntPtr(hBitmap));

            //DeleteObject(hBitmap);
            //DeleteDC(hdc);

            //return bmpImage;


        }

        //static unsafe void FillBitmapInfo( BITMAPINFO* bmi, int width, int height, int bpp, int origin )
        //{
        ////    throw new NotImplementedException();
        //    assert( bmi && width >= 0 && height >= 0 && (bpp == 8 || bpp == 24 || bpp == 32));
            
        //    BITMAPINFOHEADER* bmih = &(bmi->bmiHeader);

        //    //memset( bmih, 0, System.Runtime.InteropServices.SizeOf(*bmih));
        //    bmih->biSize = System.Runtime.InteropServices.Marshal.SizeOf(BITMAPINFOHEADER);
        //    bmih->biWidth = width;
        //    bmih->biHeight = origin ? abs(height) : -abs(height);
        //    bmih->biPlanes = 1;
        //    bmih->biBitCount = (ushort)bpp;
        //    bmih->biCompression = BI_RGB;

        //    //if( bpp == 8 )
        //    //{
        //    //    RGBQUAD* palette = bmi->bmiColors;
        //    //    int i;
        //    //    for( i = 0; i < 256; i++ )
        //    //    {
        //    //        palette[i].rgbBlue = palette[i].rgbGreen = palette[i].rgbRed = (BYTE)i;
        //    //        palette[i].rgbReserved = 0;
        //    //    }
        //    //}
        //}

		public System.Drawing.Rectangle RegionOfInterest
		{
            get
			{
				__CvRect rc = PInvoke.cvGetImageROI(Internal.ptr);
                return new System.Drawing.Rectangle(rc.x, rc.y, rc.width, rc.height);
			}
			set
			{
				System.Drawing.Rectangle irect = new System.Drawing.Rectangle(0, 0, Width, Height);
				if (!irect.IntersectsWith(value)) return;

				irect.Intersect(value);
				PInvoke.cvSetImageROI(Internal, new __CvRect(irect));
			}
		}

		public void ResetROI()
		{
			PInvoke.cvResetImageROI(Internal);
		}

		public CVImage CalcBackProject(CVHistogram histogram)
		{
            //throw new NotImplementedException();

            CVImage[] planes = Split();

            CVImage backProjection =
                new CVImage(
                    planes[0].RegionOfInterest.Width,
                    planes[0].RegionOfInterest.Height,
                    planes[0].Depth,
                    planes[0].Channels);

            __IplImagePtr[] iplImages = new __IplImagePtr[planes.Length];
            for (int i = 0; i < planes.Length; ++i)
                iplImages[i] = planes[i].Internal;

            PInvoke.cvCalcBackProject(iplImages, backProjection.Internal, histogram.Internal);


            for (int i = 0; i < planes.Length; ++i)
                planes[i].Release();

            return backProjection;
		}

		public CVConnectedComp MeanShift(System.Drawing.Rectangle window)
		{
			return MeanShift(window, 0.1);
		}

		public CVConnectedComp MeanShift(System.Drawing.Rectangle window, int maxIterations)
		{
			return MeanShift(window, (int)CVTermCriteriaType.CV_TERMCRIT_ITER, maxIterations);
		}

		public CVConnectedComp MeanShift(System.Drawing.Rectangle window, double eps)
		{
            return MeanShift(window, (int)CVTermCriteriaType.CV_TERMCRIT_EPS, eps);
		}

		public CVConnectedComp MeanShift(System.Drawing.Rectangle window, int maxIterations, double eps)
		{
            return MeanShift(window, (int)(CVTermCriteriaType.CV_TERMCRIT_ITER | CVTermCriteriaType.CV_TERMCRIT_EPS), maxIterations, eps);
		}

		public CVConnectedComp MeanShift(System.Drawing.Rectangle window, int termCriteria, int maxIterations, double eps)
		{
            System.Drawing.Rectangle realWindow = new System.Drawing.Rectangle(0, 0, Width, Height);
            if (!realWindow.IntersectsWith(window))
            {
                CVConnectedComp cc = new CVConnectedComp(window);
                return cc;
            }

            realWindow.Intersect(window);

            __CvRect wnd = new __CvRect(realWindow);
            __CvTermCriteria tc = PInvoke.cvTermCriteria(termCriteria, maxIterations, eps);

            __CvConnectedComp comp = new __CvConnectedComp();
            PInvoke.cvMeanShift(Internal, wnd, tc, ref comp);

            return new CVConnectedComp(ref comp);
		}

		public CVImage CopyRegion(System.Drawing.Rectangle rect)
		{
			CVImage roi = new CVImage(this);
			roi.RegionOfInterest = rect;
			return roi;
		}

		public void DrawPixel(System.Drawing.Point pt)
		{
			DrawPixel(pt, System.Drawing.Color.FromArgb(255,255,255));
		}

		public void DrawPixel(System.Drawing.Point pt, System.Drawing.Color color)
		{
			DrawPixel(pt, color, 1);
		}

		public void DrawPixel(System.Drawing.Point pt, System.Drawing.Color color, int thickness)
		{
			DrawRectangle(
				new System.Drawing.Rectangle(pt, new Size(0, 0)), 
				color, thickness);
		}

		public CVConnectedComp CamShift(System.Drawing.Rectangle window, double eps)
		{
			return CamShift(window, (int) CVTermCriteriaType.CV_TERMCRIT_EPS, 0, eps);
		}

		public CVConnectedComp CamShift(System.Drawing.Rectangle window, int maxIterations)
		{
			return CamShift(window, (int) CVTermCriteriaType.CV_TERMCRIT_ITER, maxIterations, 0.0);
		}

		public CVConnectedComp CamShift(System.Drawing.Rectangle window, int termCriteria, int maxIterations, double eps)
		{
            throw new NotImplementedException();
            //__CvConnectedComp cc = new __CvConnectedComp();
            //PInvoke.cvCamShift(Internal, new __CvRect(window), PInvoke.cvTermCriteria(termCriteria, maxIterations, eps), ref cc);
            //return new CVConnectedComp(&cc);
		}

		public CVImage Clone()
		{
			CVImage n = new CVImage((__IplImagePtr)IntPtr.Zero);
			n.image = PInvoke.cvCloneImage(this.Internal);
			return n;
		}

		public CVImage ToGrayscale()
		{
			CVImage gs = new CVImage(Width, Height, Depth, 1);
			System.Drawing.Rectangle prevRoi = this.RegionOfInterest;
			this.ResetROI();
			PInvoke.cvConvertImage(this.Internal, gs.Internal, (int)CVConvertImageFlags.Default);
			this.RegionOfInterest = prevRoi;
			gs.RegionOfInterest = prevRoi;

			return gs;
		}

		public bool Contains(System.Drawing.Point pt)
		{
			System.Drawing.Rectangle rc = new System.Drawing.Rectangle(0, 0, Width, Height);
			return (rc.Contains(pt));
		}

	
		private void Create(int width, int height, CVDepth depth, int channels)
		{
			image = PInvoke.cvCreateImage(new __CvSize(width, height), (int) depth,  channels);
			created = true;
		}

        #region IDisposable Members

        public void Dispose()
        {
            Release();
        }

        #endregion
    }
}
