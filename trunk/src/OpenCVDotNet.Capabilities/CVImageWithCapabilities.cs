using System;
using System.Drawing;

namespace OpenCVDotNet
{
    /// <summary>
    /// This class extends the CVImage class and adds to it capabilities 
    /// from OpenCV's "cv" library, using PInvoke.
    /// </summary>
    public sealed class CVImageWithCapabilities : CVImage
    {
        public CVImageWithCapabilities(CVImage clone) : base(clone) { }
        public CVImageWithCapabilities(string filename) : base(filename) { }
        public CVImageWithCapabilities(int width, int height, CVDepth depth, int channels) : base(width, height, depth, channels) { }

        /// <summary>
        /// Copies source 2D array inside of the larger destination array and 
        /// makes a border of the specified type (IPL_BORDER_*) around the copied area.
        /// </summary>
        public CVImage CopyMakeBorder(CVImage dst, Point offset, int bordertype, CVScalar value)
        {
            PInvoke.cvCopyMakeBorder(new __CvArrPtr(this), new __CvArrPtr(dst), new __CvPoint(offset), bordertype, new __CvScalar(value));
            return dst;
        }

        public enum SmoothType { CV_BLUR_NO_SCALE = 0, CV_BLUR = 1, CV_GAUSSIAN = 2, CV_MEDIAN = 3, CV_BILATERAL = 4 }

        /// <summary>
        /// Smoothes array (removes noise)
        /// </summary>
        public CVImage Smooth() { return Smooth(SmoothType.CV_GAUSSIAN); }
        public CVImage Smooth(SmoothType smoothtype) { return Smooth(smoothtype, 3); }
        public CVImage Smooth(SmoothType smoothtype, int param1) { return Smooth(smoothtype, param1, 0); }
        public CVImage Smooth(SmoothType smoothtype, int param1, int param2) { return Smooth(smoothtype, param1, param2, 0); }
        public CVImage Smooth(SmoothType smoothtype, int param1, int param2, double param3) { return Smooth(smoothtype, param1, param2, param3, 0); }
        public CVImage Smooth(SmoothType smoothtype, int param1, int param2, double param3, double param4) 
        {
            CVImage dst = this.Clone();
            PInvoke.cvSmooth(new __CvArrPtr(this), new __CvArrPtr(dst), (int)smoothtype, param1, param2, param3, param4);
            return dst;
        }
        

        /// <summary>
        /// Convolves the image with the kernel
        /// </summary>
        public CVImage Filter2D(CVMat kernel, Point anchor) 
        {
            CVImage dst = this.Clone();
            PInvoke.cvFilter2D(new __CvArrPtr(this), new __CvArrPtr(dst), new __CvMatPtr(kernel), new __CvPoint(anchor));
            return dst;
        }
        
        public CVImage Filter2D(CVMat kernel) { return Filter2D(kernel, new Point(-1, -1)); }

        /// <summary>
        /// Finds integral image: SUM(X,Y) = sum(x<X,y<Y)I(x,y)
        /// TODO: Remove 'sum' argument and return a CVArr object
        /// </summary>
        public void Integral(CVArr sum, CVArr sqsum, CVArr titled_sum) { PInvoke.cvIntegral(new __CvImagePtr(this), new __CvArrPtr(sum), new __CvArrPtr(sqsum), new __CvArrPtr(titled_sum)); }
        public void Integral(CVArr sum) { Integral(sum, null); }
        public void Integral(CVArr sum, CVArr sqsum) { Integral(sum, sqsum, null); }

        /// <summary>
        /// Filters used in pyramid decomposition
        /// </summary>
        public enum PyrFilter { CV_GAUSSIAN_5x5 = 7 }

        /// <summary>
        /// Smoothes the input image with gaussian kernel and then down-samples it.
        ///   dst_width = floor(src_width/2)[+1]
        ///   dst_height = floor(src_height/2)[+1]
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrDown(CVImage dst, PyrFilter filter) { PInvoke.cvPyrDown(new __CvImagePtr(this), new __CvImagePtr(dst), (int)filter); }
        public void PyrDown(CVImage dst) { PyrDown(dst, PyrFilter.CV_GAUSSIAN_5x5); }

        /// <summary>
        /// Up-samples image and smoothes the result with gaussian kernel.
        ///   dst_width = src_width*2,
        ///   dst_height = src_height*2
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrUp(CVImage dst, PyrFilter filter) { PInvoke.cvPyrUp(new __CvImagePtr(this), new __CvImagePtr(dst), (int)filter); }
        public void PyrUp(CVImage dst) { PyrUp(dst, PyrFilter.CV_GAUSSIAN_5x5); }

        /// <summary>
        /// Builds the whole pyramid at once. Output array of CvMat headers (levels[*])
        //  is initialized with the headers of subsequent pyramid levels
        /// TODO: Remove 'container' argument and return an object
        /// </summary>
        public void CalcPyramid(CVArr container, CVMat levels, int level_count, PyrFilter filter) { PInvoke.cvCalcPyramid(new __CvImagePtr(this), new __CvArrPtr(container), levels.Ptr, level_count, (int)filter); }
        public void CalcPyramid(CVArr container, CVMat levels, int level_count) { CalcPyramid(container, levels, level_count, PyrFilter.CV_GAUSSIAN_5x5); }
        
        /// <summary>
        /// Splits color or grayscale image into multiple connected components
        //  of nearly the same color/brightness using modification of Burt algorithm.
        //  comp with contain a pointer to sequence (CvSeq)
        //  of connected components (CvConnectedComp)
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrSegmentation(CVImage dst, IntPtr storage, IntPtr comp, int level, double threshold1, double threshold2) { PInvoke.cvPyrSegmentation(new __CvImagePtr(this), new __CvImagePtr(dst), new __CvMemStoragePtr(storage), new __CvSeqPtr(comp), level, threshold1, threshold2); }
        
        /// <summary>
        /// Filters image using meanshift algorithm
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr) { PyrMeanShiftFiltering(dst, sp, sr, 1); }
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr, int max_level) { PyrMeanShiftFiltering(dst, sp, sr, max_level, new CVTermCriteria(5, 1.0)); }
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr, int max_level, CVTermCriteria termcrit) { PInvoke.cvPyrMeanShiftFiltering(new __CvArrPtr(this), new __CvArrPtr(dst), sp, sr, max_level, termcrit); }

        /// <summary
        /// Segments image using seed "markers"
        /// </summary>
        public void Watershed(CVArr markers) { PInvoke.cvWatershed(new __CvArrPtr(this), new __CvArrPtr(markers)); }

        /// <summary>
        /// Inpaints the selected region in the image
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Inpaint(CVArr inpaint_mask, CVArr dst, double inpaintRange, int flags) { PInvoke.cvInpaint(new __CvArrPtr(this), new __CvArrPtr(inpaint_mask), new __CvArrPtr(dst), inpaintRange, flags); }
        public enum InpaintFlags { CV_INPAINT_NS = 0, CV_INPAINT_TELEA = 1 }

        /// <summary>
        /// Calculates an image derivative using generalized Sobel
        /// (aperture_size = 1,3,5,7) or Scharr (aperture_size = -1) operator.
        /// Scharr can be used only for the first dx or dy derivative
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Sobel(CVArr dst, int xorder, int yorder, int aperture_size) { PInvoke.cvSobel(new __CvArrPtr(this), new __CvArrPtr(dst), xorder, yorder, aperture_size); }
        public void Sobel(CVArr dst, int xorder, int yorder) { Sobel(dst, xorder, yorder, 3); }
        public static int CV_SCHARR = -1;
        public static int CV_MAX_SOBEL_KSIZE = 7;

        /// <summary>
        /// Calculates the image Laplacian: (d2/dx + d2/dy)I
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Laplace(CVArr dst, int aperture_size) { PInvoke.cvLaplace(new __CvArrPtr(this), new __CvArrPtr(dst), aperture_size); }
        public void Laplace(CVArr dst) { Laplace(dst, 3); }

        /// <summary>
        /// Converts input array pixels from one color space to another
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void CvtColor(CVArr dst, int code) { PInvoke.cvCvtColor(new __CvArrPtr(this), new __CvArrPtr(dst), code); }


        /// <summary>
        /// Resizes image (input array is resized to fit the destination array)
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Resize(CVArr dst, ResizeInterpolation interpolation) { PInvoke.cvResize(new __CvArrPtr(this), new __CvArrPtr(dst), (int)interpolation); }
        public void Resize(CVArr dst) { Resize(dst, ResizeInterpolation.CV_INTER_LINEAR); }
        public enum ResizeInterpolation { CV_INTER_NN = 0, CV_INTER_LINEAR = 1, CV_INTER_CUBIC = 2, CV_INTER_AREA  = 3 }

        ///<summary>
        /// Warps image with affine transform
        /// TODO: Remove 'dst' argument and return a CVImage object
        ///</summary>
        public void WarpAffine(CVArr dst, CVMat map_matrix, WarpFlags flags, CVScalar fillval) { PInvoke.cvWarpAffine(new __CvArrPtr(this), new __CvArrPtr(dst), new __CvMatPtr(map_matrix), (int)flags, new __CvScalar(fillval)); }
        public void WarpAffine(CVArr dst, CVMat map_matrix, WarpFlags flags) { WarpAffine(dst, map_matrix, flags, new CVScalar()); }
        public void WarpAffine(CVArr dst, CVMat map_matrix) { WarpAffine(dst, map_matrix, WarpFlags.CV_WARP_FILL_OUTLIERS | WarpFlags.CV_WARP_INVERSE_MAP); }
        public enum WarpFlags { CV_WARP_FILL_OUTLIERS = 8, CV_WARP_INVERSE_MAP = 16 }
    }
}
