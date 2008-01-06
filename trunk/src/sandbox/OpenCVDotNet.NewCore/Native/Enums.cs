using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    internal enum CVGlobalConsts
    {
        CV_MAX_DIM = 32
    }

    public enum CVInterpolation
    {
        NearestNeigbor = 0,
        Linear = 1,
        Area = 2,
        Cubic = 3
    }

    public enum CVDepth : uint
    {
        Depth1U = 1,
        Depth8U = 8,
        Depth16U = 16,
        Depth32F = 32,
        Depth8S = (0x80000000 | 8),
        Depth16S = (0x80000000 | 16),
        Depth32S = (0x80000000 | 32),
    }

    public enum CVConvertImageFlags
    {
        Default = 0, Flip = 1, Swap_RB = 2
    }

    
    public enum LoadImageMode {
        /// <summary>
        /// 8bit, color or not
        /// </summary>
        CV_LOAD_IMAGE_UNCHANGED  = -1,
        /// <summary>
        /// 8bit, gray
        /// </summary>
         CV_LOAD_IMAGE_GRAYSCALE  = 0,
        /// <summary>
        /// ?, color
        /// </summary>
         CV_LOAD_IMAGE_COLOR       =1,
        /// <summary>
        /// any depth, ?
        /// </summary>
        CV_LOAD_IMAGE_ANYDEPTH   = 2,
        /// <summary>
        /// ?, any color
        /// </summary>
        CV_LOAD_IMAGE_ANYCOLOR    =4,
    }

    enum CV_CAP_PROP {
        CV_CAP_PROP_POS_MSEC       =0,
        CV_CAP_PROP_POS_FRAMES     =1,
        CV_CAP_PROP_POS_AVI_RATIO  =2,
        CV_CAP_PROP_FRAME_WIDTH    =3,
        CV_CAP_PROP_FRAME_HEIGHT   =4,
        CV_CAP_PROP_FPS            =5,
        CV_CAP_PROP_FOURCC         =6,
        CV_CAP_PROP_FRAME_COUNT    =7,
        CV_CAP_PROP_FORMAT         =8,
        CV_CAP_PROP_MODE           =9,
        CV_CAP_PROP_BRIGHTNESS    =10,
        CV_CAP_PROP_CONTRAST      =11,
        CV_CAP_PROP_SATURATION    =12,
        CV_CAP_PROP_HUE           =13,
        CV_CAP_PROP_GAIN          =14,
        CV_CAP_PROP_CONVERT_RGB   =15,

    }

    enum CV_CAP
    {
        
        CV_CAP_ANY       = 0,     // autodetect

        CV_CAP_MIL      =100 ,  // MIL proprietary drivers

        CV_CAP_VFW      =200,   // platform native
        CV_CAP_V4L      =200,
        CV_CAP_V4L2     =200,

        CV_CAP_FIREWARE =300,   // IEEE 1394 drivers
        CV_CAP_IEEE1394 =300,
        CV_CAP_DC1394   =300,
        CV_CAP_CMU1394  =300,

        CV_CAP_STEREO   =400,   // TYZX proprietary drivers
        CV_CAP_TYZX     =400,
        CV_TYZX_LEFT    =400,
        CV_TYZX_RIGHT   =401,
        CV_TYZX_COLOR   =402,
        CV_TYZX_Z       =403,

        CV_CAP_QT       =500,   // QuickTime

    }
}
