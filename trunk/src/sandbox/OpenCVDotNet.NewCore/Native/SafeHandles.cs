using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace OpenCVDotNet.Native
{
    #region SafeHandles

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
    internal sealed class __IplImagePtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        public __IplImagePtr()
            : base(false)
        {
        }

        // We need this so that we can do our own marshalling (and may be for user supplied handles)
        internal __IplImagePtr(IntPtr preexistingHandle)
            : base(true)
        {
            SetHandle(preexistingHandle);
        }

        // We should not provide a finalizer - SafeHandle's critical finalizer will call ReleaseHandle inside a CER for us.
        override protected bool ReleaseHandle()
        {
            PInvoke.cvReleaseImage(ref handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public static implicit operator __CvArrPtr(__IplImagePtr iplImagePtr)
        {
            return new __CvArrPtr(iplImagePtr.handle);
        }

        public static implicit operator __IplImagePtr(IntPtr ptr)
        {
            return new __IplImagePtr(ptr);
        }

        internal unsafe __IplImage* ToPointer()
        {
            return (__IplImage*)this.handle.ToPointer();
        }

        [Obsolete("Use DangerousGetHandle instead")]
        public IntPtr ptr
        {
            get
            {
                return this.handle;
            }
        }
    }
    #endregion


    internal sealed class __CvHistogramPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvHistogramPtr()
            : base(false)
        {
        }

        internal __CvHistogramPtr(IntPtr ptr)
            : base(true)
        {
            SetHandle(ptr);
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }


    #region __CvCapturePointer
    internal sealed class __CvCapturePtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvCapturePtr()
            : base(false)
        {
            SetHandle(IntPtr.Zero);
        }

        internal __CvCapturePtr(IntPtr ptr)
            : base(true)
        {
            SetHandle(ptr);
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }

        [Obsolete("Use Dangarous Get Handle instead")]
        public IntPtr ptr
        {
            get
            {
                return this.handle;
            }
            set
            {
                SetHandle(value);
            }
        }
    }
    #endregion

    #region __CvVideoWriterPointer
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvVideoWriterPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvVideoWriterPtr()
            : base(false)
        {
        }

        internal __CvVideoWriterPtr(IntPtr ptr)
            : base(true)
        {
            SetHandle(ptr);
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }
    #endregion

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvImagePtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvImagePtr()
            : base(false)
        {
        }

        internal __CvImagePtr(CVImage img)
            : base(true)
        {
            SetHandle(img.Ptr);
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvMatPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvMatPtr()
            : base(false)
        {
        }

        internal __CvMatPtr(CVMat mat)
            : base(true)
        {
            SetHandle(mat.Ptr);
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    #endregion


    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeqPtr
    {
        internal IntPtr ptr;
        public __CvSeqPtr(IntPtr ptr) { this.ptr = ptr; }
        public unsafe __CvSeqPtr(__CvSeq* ptr) { this.ptr = new IntPtr((void*)ptr); }
        public unsafe __CvSeq* ToPointer()
        {
            return ((__CvSeq*)ptr.ToPointer());
        }
    }


    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __MarshaledStructurePtr<T> where T : struct
    {
        internal IntPtr ptr;

        internal __MarshaledStructurePtr(IntPtr ptr) { this.ptr = ptr; }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvMemStoragePtr
    {
        internal IntPtr ptr;
        internal __CvMemStoragePtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
    }

    [Obsolete("Use ref to System.Drawing.PointF instead")]
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvPoint2D32fPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvPoint2D32fPtr()
            : base(false)
        {
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }


    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __IplConvKernelPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __IplConvKernelPtr()
            : base(false)
        {
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvMomentsPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvMomentsPtr()
            : base(false)
        {
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal sealed class __CvHuMomentsPtr : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal __CvHuMomentsPtr()
            : base(false)
        {
        }

        override protected bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    // TODO: Implement as SafeHandle
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    internal struct __CvSeqBlockPtr { internal IntPtr ptr; }
    /*[System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
	internal sealed class __CvSeqBlockPtr : SafeHandleZeroOrMinusOneIsInvalid 
	{
		internal __CvSeqBlockPtr () : base(false) {
		}
		
		override protected bool ReleaseHandle()	{
			SetHandle (IntPtr.Zero);
			return true;
		}
	}*/

}
