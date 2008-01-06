using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    #region CVArr
    public abstract class CVArr
    {
        internal abstract __CvArrPtr Array
        {
            get;
        }

        public System.IntPtr Ptr
        {
            get
            {
                return this.Array.ptr;
            }
        }
    }
    #endregion
}
