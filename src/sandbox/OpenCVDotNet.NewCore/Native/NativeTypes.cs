using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    #region Native Types

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __MarshaledStructurePtr<T> where T : struct
    {
        public IntPtr ptr;
        
        public __MarshaledStructurePtr(IntPtr ptr) { this.ptr = ptr; }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMemStoragePtr { public IntPtr ptr; public __CvMemStoragePtr(IntPtr ptr) { this.ptr = ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeqPtr { public IntPtr ptr; public __CvSeqPtr(IntPtr ptr) { this.ptr = ptr; } }

    internal struct __CvHistogramPtr {
        public IntPtr ptr; public __CvHistogramPtr(IntPtr ptr) { this.ptr = ptr; }
        
    }
    
    #region __IplImage
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __IplImage
    {
        /// <summary>
        /// sizeof(IplImage)
        /// </summary>
        public int nSize;
        /// <summary>
        /// version (=0)
        /// </summary>
        public int ID;
        /// <summary>
        /// Most of OpenCV functions support 1,2,3 or 4 channels
        /// </summary>
        public int nChannels;

        int pad11;
        /// <summary>
        /// pixel depth in bits: IPL_DEPTH_8U, IPL_DEPTH_8S, IPL_DEPTH_16S,
        /// IPL_DEPTH_32S, IPL_DEPTH_32F and IPL_DEPTH_64F are supported
        /// </summary>
        public int depth;

        int pad12, pad13;

        /// <summary>
        /// 0 - public interleaved color channels, 
        /// 1 - separate color channels.
        /// cvCreateImage can only create interleaved images
        /// </summary>
        public int dataOrder;    
 
        /// <summary>
        /// 0 - top-left origin,
        /// 1 - bottom-left origin (Windows bitmaps style)
        /// </summary>
        public int origin;

        /// <summary>
        /// Alignment of image rows (4 or 8).
        /// OpenCV ignores it and uses widthStep instead
        /// </summary>
        public int align;

        /// <summary>
        /// image width in pixels
        /// </summary>
        public int width;

        /// <summary>
        /// image height in pixels
        /// </summary>
        public int height;

        /// <summary>
        /// image ROI. when it is not NULL, this specifies image region to process
        /// </summary>
        public byte* roi;

        byte* pad8, pad9, pad10;
        /// <summary>
        /// image data size in bytes
        /// (=image->height*image->widthStep
        /// in case of interleaved data)
        /// </summary>
        public int imageSize;
        
        /// <summary>
        /// pointer to aligned image data
        /// </summary>
        public byte* imageData;   /*  */
        
        /// <summary>
        /// size of aligned image row in bytes
        /// </summary>
        public int widthStep;
        
        /// <summary>
        /// ignored by OpenCV
        /// </summary>
        int pad0, pad1, pad2, pad3, pad4, pad5, pad6, pad7; 
        
        /// <summary>
        /// pointer to a very origin of image data
        /// (not necessarily aligned) - it is needed for correct image deallocation
        /// </summary>
        public byte* imageDataOrigin; /*  */
    }
    #endregion

    #region __CvPoint
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint { public int x, y; 
        public __CvPoint(Point pt) { x = pt.X; y = pt.Y; }
        public __CvPoint(int x, int y) { this.x = x; this.y = y; }
    }
    #endregion

    #region __CvScalar
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvScalar { 
        public double v0, v1, v2, v3; 
        public __CvScalar(params double[] val) { v0 = val[0]; v1 = val[1]; v2 = val[2]; v3 = val[3]; } 
        public __CvScalar(CVScalar scalar) { 
            v0 = scalar.val[0]; v1 = scalar.val[1]; v2 = scalar.val[2]; v3 = scalar.val[3]; 
        }
    }
    #endregion

    #region __CvArr
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvArr { }
    #endregion

    #region __CvArrPtr
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvArrPtr
    {
        public IntPtr ptr;
        public __CvArrPtr(CVArr arr)
        {
            ptr = (arr != null ? arr.Ptr : IntPtr.Zero);
        }
        public __CvArrPtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
        //public unsafe __CvArrPtr(__CvArr* arrPtr)
        //{
        //    ptr = (IntPtr) arrPtr;
        //}
        //public unsafe __CvArrPtr(__IplImage* iplImagePtr)
        //{
        //    ptr = (IntPtr)iplImagePtr;
        //}

        //public static unsafe implicit operator __CvArrPtr(__IplImage* iplImagePtr)
        //{
        //    return new __CvArrPtr(iplImagePtr);
        //}

        public static implicit operator __CvArrPtr(IntPtr iplImagePtr)
        {
            return new __CvArrPtr(iplImagePtr);
        }
    }
    #endregion

    #region __IplImagePointer
    internal struct __IplImagePtr
    {
        public IntPtr ptr;
        public __IplImagePtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
        public static implicit operator __CvArrPtr(__IplImagePtr iplImagePtr)
        {
            return new __CvArrPtr(iplImagePtr.ptr);
        }
        public static implicit operator __IplImagePtr(IntPtr ptr)
        {
            return new __IplImagePtr(ptr);
        }
        
        public unsafe __IplImage* ToPointer() {
            return (__IplImage*)this.ptr.ToPointer();
        }
    }
    #endregion

    #region __CvSize
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSize
    {
        int width; /* width of the rectangle */
        int height; /* height of the rectangle */
        public __CvSize(int width, int height) { this.width = width; this.height = height; }
    }
    #endregion

    #region __CvCapturePointer
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvCapturePtr { public IntPtr ptr; public __CvCapturePtr(IntPtr ptr) { this.ptr = ptr; } }
    #endregion

    #region __CvVideoWriterPointer
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvVideoWriterPtr { public IntPtr ptr; public __CvVideoWriterPtr(IntPtr ptr) { this.ptr = ptr; } }
    #endregion

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvImagePtr { public IntPtr ptr; public __CvImagePtr(CVImage img) { ptr = img.Ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMatPtr { public IntPtr ptr; public __CvMatPtr(CVMat mat) { ptr = mat.Ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint2D32f {
        float x;
        float y;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSize2D32f
    {
        public float width;
        public float height;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint2D32fPtr { public IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __IplConvKernelPtr { public IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMomentsPtr { public IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvHuMomentsPtr { public IntPtr ptr; }

    #region __CvConnectedComp
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvConnectedComp
    {
        internal double area;
        internal __CvScalar value;
        internal __CvRect rect;
        internal __CvSeqPtr contour;
    }
    #endregion

    #region __CvRect
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvRect
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public __CvRect(Rectangle rect)
        {
            x = rect.X; y = rect.Y; width = rect.Width; height = rect.Height;
        }
    }
    #endregion

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    struct __SizeAndStep
    {
        int size;
        int step;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct __DataUnion
    {
        //[FieldOffset(0)]
        //char* ptr; // TODO: actually is uchar
        //[FieldOffset(0)]
        //float* fl;
        //[FieldOffset(0)]
        //double* db;
        //[FieldOffset(0)]
        //int* i;
        //[FieldOffset(0)]
        //short* s;
        IntPtr data;
    }


    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential, Size = 20 + (int)CVGlobalConsts.CV_MAX_DIM * 2)]
    internal unsafe struct __CvMatND
    {
        int type;
        int dims;

        int* refcount;
        int hdr_refcount;

        //__DataUnion data;
        IntPtr data;
        
        [MarshalAsAttribute(UnmanagedType.ByValArray , SizeConst = (int)CVGlobalConsts.CV_MAX_DIM)]
        // This should be an array, however this is a private field so we don't care, instead the size of the struct is explicited above.
        __SizeAndStep _dim0;
        

    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CvHistogram
    {
        public int type;
        public __CvArrPtr bins;

        //[MarshalAsAttribute(UnmanagedType.LPArray, SizeConst = 64)] // Maybe this one?
        //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = (int)CVGlobalConsts.CV_MAX_DIM * 2)] // Or maybe this one?
        fixed float thresh[64]; /* for uniform histograms */
        float** thresh2; /* for non-uniform histograms */
        //[MarshalAs(UnmanagedType.LPStruct)]
        __CvMatND mat; /* embedded matrix header for array histograms */
    }

    public enum CVTermCriteriaType
    {
        CV_TERMCRIT_ITER = 1,
        CV_TERMCRIT_NUMBER = CV_TERMCRIT_ITER,
        CV_TERMCRIT_EPS = 2
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvTermCriteria
    {
        internal int    type;  /* may be combination of
                                 CV_TERMCRIT_ITER
                                 CV_TERMCRIT_EPS */
        internal int    max_iter;
        internal double epsilon;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvBox2D
    {
        /// <summary>
        /// center of the box
        /// </summary>
        [MarshalAs(UnmanagedType.LPStruct)]
        public __CvPoint2D32f center;
        /// <summary>
        /// box width and length
        /// </summary>
        [MarshalAs(UnmanagedType.LPStruct)]
        public __CvSize2D32f size;
        /// <summary>
        /// angle between the horizontal axis and the first side (i.e. length) in degrees
        /// </summary>
        public float angle;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CvCapture
    {
        void* vtable; //CvCaptureVTable* vtable;
    }

    internal struct __CvVideoWriter { }

    
    
    #endregion
}
