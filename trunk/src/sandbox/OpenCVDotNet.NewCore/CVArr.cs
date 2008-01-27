using System;
using System.Collections.Generic;
using System.Text;
using OpenCVDotNet.Native;

namespace OpenCVDotNet
{
    #region CVArr
    public abstract class CVArr
    {
        internal abstract __CvArrPtr Array
        {
            get;
        }

        internal System.IntPtr Ptr
        {
            get
            {
                return this.Array.ptr;
            }
        }
    }
    #endregion
}
