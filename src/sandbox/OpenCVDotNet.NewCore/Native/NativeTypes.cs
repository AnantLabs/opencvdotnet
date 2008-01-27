using System;
using System.Runtime.InteropServices;

namespace OpenCVDotNet.Native
{
    #region Native Types

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __MarshaledStructurePtr<T> where T : struct
    {
        internal IntPtr ptr;
        
        internal __MarshaledStructurePtr(IntPtr ptr) { this.ptr = ptr; }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMemStoragePtr { internal IntPtr ptr; internal __CvMemStoragePtr(IntPtr ptr) { this.ptr = ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeqPtr { 
        internal IntPtr ptr; 
        public __CvSeqPtr(IntPtr ptr) { this.ptr = ptr; }
        public unsafe __CvSeqPtr(__CvSeq* ptr) { this.ptr = new IntPtr((void*)ptr); }
        public unsafe __CvSeq* ToPointer()
        {
            return ((__CvSeq*)ptr.ToPointer());
        }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CvTreeNodeFields__CvSeqPtr
    {
        public int       flags;         /* micsellaneous flags */
        public int       header_size;   /* size of sequence header */
        public __CvSeqPtr h_prev; /* previous sequence */
        public __CvSeqPtr h_next; /* next sequence */
        public __CvSeqPtr v_prev; /* 2nd previous sequence */
        public __CvSeqPtr v_next;  /* 2nd next sequence */
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CV_SEQUENCE_FIELDS
    {
        public __CvTreeNodeFields__CvSeqPtr __cvTreeNodeFields;
        public int total;                       /* total number of elements */
        public int elem_size;                   /* size of sequence element in bytes */
        public char* block_max;                 /* maximal bound of the last block */
        char* ptr;                              /* current write pointer */
        int delta_elems;                        /* how many elements allocated when the seq grows */
        __CvMemStoragePtr storage;              /* where the seq is stored */
        __CvSeqBlockPtr free_blocks;            /* free blocks list */
        __CvSeqBlockPtr first;                  /* pointer to the first sequence block */
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeq
    {
        public __CV_SEQUENCE_FIELDS _cvSequenceFields;
    }


    internal struct __CvHistogramPtr {
        public IntPtr ptr; internal __CvHistogramPtr(IntPtr ptr) { this.ptr = ptr; }
    }
    
    #region __IplImage
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __IplImage
    {
        /// <summary>
        /// sizeof(IplImage)
        /// </summary>
        internal int nSize;
        /// <summary>
        /// version (=0)
        /// </summary>
        internal int ID;
        /// <summary>
        /// Most of OpenCV functions support 1,2,3 or 4 channels
        /// </summary>
        internal int nChannels;

        int pad11;
        /// <summary>
        /// pixel depth in bits: IPL_DEPTH_8U, IPL_DEPTH_8S, IPL_DEPTH_16S,
        /// IPL_DEPTH_32S, IPL_DEPTH_32F and IPL_DEPTH_64F are supported
        /// </summary>
        internal int depth;

        int pad12, pad13;

        /// <summary>
        /// 0 - internal interleaved color channels, 
        /// 1 - separate color channels.
        /// cvCreateImage can only create interleaved images
        /// </summary>
        internal int dataOrder;    
 
        /// <summary>
        /// 0 - top-left origin,
        /// 1 - bottom-left origin (Windows bitmaps style)
        /// </summary>
        internal int origin;

        /// <summary>
        /// Alignment of image rows (4 or 8).
        /// OpenCV ignores it and uses widthStep instead
        /// </summary>
        internal int align;

        /// <summary>
        /// image width in pixels
        /// </summary>
        internal int width;

        /// <summary>
        /// image height in pixels
        /// </summary>
        internal int height;

        /// <summary>
        /// image ROI. when it is not NULL, this specifies image region to process
        /// </summary>
        internal byte* roi;

        byte* pad8, pad9, pad10;
        /// <summary>
        /// image data size in bytes
        /// (=image->height*image->widthStep
        /// in case of interleaved data)
        /// </summary>
        internal int imageSize;
        
        /// <summary>
        /// pointer to aligned image data
        /// </summary>
        internal byte* imageData;   /*  */
        
        /// <summary>
        /// size of aligned image row in bytes
        /// </summary>
        internal int widthStep;
        
        /// <summary>
        /// ignored by OpenCV
        /// </summary>
        int pad0, pad1, pad2, pad3, pad4, pad5, pad6, pad7; 
        
        /// <summary>
        /// pointer to a very origin of image data
        /// (not necessarily aligned) - it is needed for correct image deallocation
        /// </summary>
        internal byte* imageDataOrigin; /*  */
    }
    #endregion

    #region __CvPoint
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint { internal int x, y; 
        internal __CvPoint(System.Drawing.Point pt) { x = pt.X; y = pt.Y; }
        internal __CvPoint(int x, int y) { this.x = x; this.y = y; }
    }
    #endregion

    #region __CvScalar
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvScalar { 
        public double v0, v1, v2, v3; 
        public __CvScalar(params double[] val) {
            switch (val.Length)
            {
                case 1:
                    v0 = val[0]; v1 = 0; v2 = 0; v3 = 0;
                    break;
                case 2:
                    v0 = val[0]; v1 = val[1]; v2 = 0; v3 = 0;
                    break;
                case 3:
                    v0 = val[0]; v1 = val[1]; v2 = val[2]; v3 = 0;
                    break;
                case 4:
                    v0 = val[0]; v1 = val[1]; v2 = val[2]; v3 = val[3];
                    break;
                default:
                    throw new ArgumentException("array length should be between 1 and 4");
            }
        }
        public __CvScalar(CVScalar cvScalar) : this(cvScalar.val) { }
    }
    #endregion

    #region __CvArr
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvArr { }
    #endregion

    #region __CvArrPtr
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct __CvArrPtr
    {
        internal IntPtr ptr;
        internal __CvArrPtr(CVArr arr)
        {
            ptr = (arr != null ? arr.Ptr : IntPtr.Zero);
        }
        internal __CvArrPtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
        //internal unsafe __CvArrPtr(__CvArr* arrPtr)
        //{
        //    ptr = (IntPtr) arrPtr;
        //}
        //internal unsafe __CvArrPtr(__IplImage* iplImagePtr)
        //{
        //    ptr = (IntPtr)iplImagePtr;
        //}

        //internal static unsafe implicit operator __CvArrPtr(__IplImage* iplImagePtr)
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
        internal IntPtr ptr;
        internal __IplImagePtr(IntPtr ptr)
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
        
        internal unsafe __IplImage* ToPointer() {
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
        internal __CvSize(int width, int height) { this.width = width; this.height = height; }
    }
    #endregion

    #region __CvCapturePointer
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvCapturePtr { internal IntPtr ptr; internal __CvCapturePtr(IntPtr ptr) { this.ptr = ptr; } }
    #endregion

    #region __CvVideoWriterPointer
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvVideoWriterPtr { internal IntPtr ptr; internal __CvVideoWriterPtr(IntPtr ptr) { this.ptr = ptr; } }
    #endregion

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvImagePtr { internal IntPtr ptr; internal __CvImagePtr(CVImage img) { ptr = img.Ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMatPtr { internal IntPtr ptr; internal __CvMatPtr(CVMat mat) { ptr = mat.Ptr; } }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint2D32f {
        float x;
        float y;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSize2D32f
    {
        internal float width;
        internal float height;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvPoint2D32fPtr { internal IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __IplConvKernelPtr { internal IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMomentsPtr { internal IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvHuMomentsPtr { internal IntPtr ptr; }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeqBlockPtr { internal IntPtr ptr; }

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
        internal int x;
        internal int y;
        internal int width;
        internal int height;

        internal __CvRect(System.Drawing.Rectangle rect)
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
        internal int type;
        internal __CvArrPtr bins;

        //[MarshalAsAttribute(UnmanagedType.LPArray, SizeConst = 64)] // Maybe this one?
        //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = (int)CVGlobalConsts.CV_MAX_DIM * 2)] // Or maybe this one?
        fixed float thresh[64]; /* for uniform histograms */
        float** thresh2; /* for non-uniform histograms */
        //[MarshalAs(UnmanagedType.LPStruct)]
        __CvMatND mat; /* embedded matrix header for array histograms */
    }

    internal enum CVTermCriteriaType
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
        internal __CvPoint2D32f center;
        /// <summary>
        /// box width and length
        /// </summary>
        [MarshalAs(UnmanagedType.LPStruct)]
        internal __CvSize2D32f size;
        /// <summary>
        /// angle between the horizontal axis and the first side (i.e. length) in degrees
        /// </summary>
        internal float angle;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CvCapture
    {
        void* vtable; //CvCaptureVTable* vtable;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __CvContour
    {
        //CV_CONTOUR_FIELDS()
        
        //CV_SEQUENCE_FIELDS()

        //CV_TREE_NODE_FIELDS(CvSeq);
        /// <summary>
        /// micsellaneous flags
        /// </summary>
        int       flags;
        /// <summary>
        /// size of sequence header
        /// </summary>
        int       header_size;
        /// <summary>
        /// previous sequence
        /// </summary>
        __CvSeqPtr h_prev;
        /// <summary>
        /// next sequence
        /// </summary>
        __CvSeqPtr  h_next;
        /// <summary>
        /// 2nd previous sequence
        /// </summary>
        __CvSeqPtr  v_prev;
        /// <summary>
        /// 2nd next sequence
        /// </summary>
        __CvSeqPtr  v_next;

        /// <summary>
        /// total number of elements
        /// </summary>
        int  total;
        /// <summary>
        /// size of sequence element in bytes
        /// </summary>
        int       elem_size;
        /// <summary>
        /// maximal bound of the last block
        /// </summary>
        char*     block_max;
        /// <summary>
        /// current write pointer
        /// </summary>
        char*     ptr;
        /// <summary>
        /// how many elements allocated when the seq grows
        /// </summary>
        int       delta_elems;
        /// <summary>
        /// where the seq is stored
        /// </summary>
        __CvMemStoragePtr storage;
        /// <summary>
        /// free blocks list
        /// </summary>
        __CvSeqBlockPtr free_blocks;
        /// <summary>
        /// pointer to the first sequence block
        /// </summary>
        __CvSeqBlockPtr first;
        
        __CvRect rect;
        int color;
        fixed int reserved[3];
    }

    internal struct __CvVideoWriter { }

    
    
    #endregion
}
