using System;
using System.Drawing;

namespace OpenCVDotNet
{
    /// <summary>
    /// This class extends the CVImage class and adds to it capabilities 
    /// from OpenCV's "cv" library, using PInvoke.
    /// </summary>
    public class CVImageWithCapabilities : CVImage
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

        #region Enums

        public enum SmoothType { 
            CV_BLUR_NO_SCALE = 0,
            CV_BLUR = 1,
            CV_GAUSSIAN = 2,
            CV_MEDIAN = 3,
            CV_BILATERAL = 4 
        }

        public enum ColorSpace
        {
            CV_BGR2BGRA = 0,
            CV_RGB2RGBA = CV_BGR2BGRA,

            CV_BGRA2BGR = 1,
            CV_RGBA2RGB = CV_BGRA2BGR,

            CV_BGR2RGBA = 2,
            CV_RGB2BGRA = CV_BGR2RGBA,

            CV_RGBA2BGR = 3,
            CV_BGRA2RGB = CV_RGBA2BGR,

            CV_BGR2RGB = 4,
            CV_RGB2BGR = CV_BGR2RGB,

            CV_BGRA2RGBA = 5,
            CV_RGBA2BGRA = CV_BGRA2RGBA,

            CV_BGR2GRAY = 6,
            CV_RGB2GRAY = 7,
            CV_GRAY2BGR = 8,
            CV_GRAY2RGB = CV_GRAY2BGR,
            CV_GRAY2BGRA = 9,
            CV_GRAY2RGBA = CV_GRAY2BGRA,
            CV_BGRA2GRAY = 10,
            CV_RGBA2GRAY = 11,

            CV_BGR2BGR565 = 12,
            CV_RGB2BGR565 = 13,
            CV_BGR5652BGR = 14,
            CV_BGR5652RGB = 15,
            CV_BGRA2BGR565 = 16,
            CV_RGBA2BGR565 = 17,
            CV_BGR5652BGRA = 18,
            CV_BGR5652RGBA = 19,

            CV_GRAY2BGR565 = 20,
            CV_BGR5652GRAY = 21,

            CV_BGR2BGR555 = 22,
            CV_RGB2BGR555 = 23,
            CV_BGR5552BGR = 24,
            CV_BGR5552RGB = 25,
            CV_BGRA2BGR555 = 26,
            CV_RGBA2BGR555 = 27,
            CV_BGR5552BGRA = 28,
            CV_BGR5552RGBA = 29,

            CV_GRAY2BGR555 = 30,
            CV_BGR5552GRAY = 31,

            CV_BGR2XYZ = 32,
            CV_RGB2XYZ = 33,
            CV_XYZ2BGR = 34,
            CV_XYZ2RGB = 35,

            CV_BGR2YCrCb = 36,
            CV_RGB2YCrCb = 37,
            CV_YCrCb2BGR = 38,
            CV_YCrCb2RGB = 39,

            CV_BGR2HSV = 40,
            CV_RGB2HSV = 41,

            CV_BGR2Lab = 44,
            CV_RGB2Lab = 45,

            CV_BayerBG2BGR = 46,
            CV_BayerGB2BGR = 47,
            CV_BayerRG2BGR = 48,
            CV_BayerGR2BGR = 49,

            CV_BayerBG2RGB = CV_BayerRG2BGR,
            CV_BayerGB2RGB = CV_BayerGR2BGR,
            CV_BayerRG2RGB = CV_BayerBG2BGR,
            CV_BayerGR2RGB = CV_BayerGB2BGR,

            CV_BGR2Luv = 50,
            CV_RGB2Luv = 51,
            CV_BGR2HLS = 52,
            CV_RGB2HLS = 53,

            CV_HSV2BGR = 54,
            CV_HSV2RGB = 55,

            CV_Lab2BGR = 56,
            CV_Lab2RGB = 57,
            CV_Luv2BGR = 58,
            CV_Luv2RGB = 59,
            CV_HLS2BGR = 60,
            CV_HLS2RGB = 61,
        }

        /// <summary>
        /// Filters used in pyramid decomposition
        /// </summary>
        public enum PyrFilter { 
            CV_GAUSSIAN_5x5 = 7
        }

        public enum InpaintFlags { CV_INPAINT_NS = 0, CV_INPAINT_TELEA = 1 }

        public enum TemplateMatchMethod
        {
            CV_TM_SQDIFF = 0,
            CV_TM_SQDIFF_NORMED = 1,
            CV_TM_CCORR = 2,
            CV_TM_CCORR_NORMED = 3,
            CV_TM_CCOEFF = 4,
            CV_TM_CCOEFF_NORMED = 5
        }

        public enum ResizeInterpolation { CV_INTER_NN = 0, CV_INTER_LINEAR = 1, CV_INTER_CUBIC = 2, CV_INTER_AREA = 3 }

        public enum WarpFlags { CV_WARP_FILL_OUTLIERS = 8, CV_WARP_INVERSE_MAP = 16 }


        #endregion

        #region Smooth
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
        #endregion

        #region Filter2D
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
        #endregion

        #region Integral
        /// <summary>
        /// Finds integral image: SUM(X,Y) = sum(x<X,y<Y)I(x,y)
        /// TODO: Remove 'sum' argument and return a CVArr object
        /// </summary>
        public void Integral(CVArr sum, CVArr sqsum, CVArr titled_sum) { PInvoke.cvIntegral(new __CvImagePtr(this), new __CvArrPtr(sum), new __CvArrPtr(sqsum), new __CvArrPtr(titled_sum)); }
        public void Integral(CVArr sum) { Integral(sum, null); }
        public void Integral(CVArr sum, CVArr sqsum) { Integral(sum, sqsum, null); }
        #endregion

        #region PyrDown
        /// <summary>
        /// Smoothes the input image with gaussian kernel and then down-samples it.
        ///   dst_width = floor(src_width/2)[+1]
        ///   dst_height = floor(src_height/2)[+1]
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrDown(CVImage dst, PyrFilter filter) { PInvoke.cvPyrDown(new __CvImagePtr(this), new __CvImagePtr(dst), (int)filter); }
        public void PyrDown(CVImage dst) { PyrDown(dst, PyrFilter.CV_GAUSSIAN_5x5); }
        #endregion

        #region PyrUp
        /// <summary>
        /// Up-samples image and smoothes the result with gaussian kernel.
        ///   dst_width = src_width*2,
        ///   dst_height = src_height*2
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrUp(CVImage dst, PyrFilter filter) { PInvoke.cvPyrUp(new __CvImagePtr(this), new __CvImagePtr(dst), (int)filter); }
        public void PyrUp(CVImage dst) { PyrUp(dst, PyrFilter.CV_GAUSSIAN_5x5); }
        #endregion

        #region Calc Pyramid
        /// <summary>
        /// Builds the whole pyramid at once. Output array of CvMat headers (levels[*])
        //  is initialized with the headers of subsequent pyramid levels
        /// TODO: Remove 'container' argument and return an object
        /// </summary>
        public void CalcPyramid(CVArr container, CVMat levels, int level_count, PyrFilter filter) { PInvoke.cvCalcPyramid(new __CvImagePtr(this), new __CvArrPtr(container), levels.Ptr, level_count, (int)filter); }
        public void CalcPyramid(CVArr container, CVMat levels, int level_count) { CalcPyramid(container, levels, level_count, PyrFilter.CV_GAUSSIAN_5x5); }
        #endregion

        #region Pyramid Segmentation
        /// <summary>
        /// Splits color or grayscale image into multiple connected components
        //  of nearly the same color/brightness using modification of Burt algorithm.
        //  comp with contain a pointer to sequence (CvSeq)
        //  of connected components (CvConnectedComp)
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrSegmentation(CVImage dst, IntPtr storage, IntPtr comp, int level, double threshold1, double threshold2) { PInvoke.cvPyrSegmentation(new __CvImagePtr(this), new __CvImagePtr(dst), new __CvMemStoragePtr(storage), new __CvSeqPtr(comp), level, threshold1, threshold2); }
        #endregion

        #region Pyramid Mean Shift Filtering
        /// <summary>
        /// Filters image using meanshift algorithm
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr) { PyrMeanShiftFiltering(dst, sp, sr, 1); }
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr, int max_level) { PyrMeanShiftFiltering(dst, sp, sr, max_level, new CVTermCriteria(5, 1.0)); }
        public void PyrMeanShiftFiltering(CVArr dst, double sp, double sr, int max_level, CVTermCriteria termcrit) { PInvoke.cvPyrMeanShiftFiltering(new __CvArrPtr(this), new __CvArrPtr(dst), sp, sr, max_level, termcrit); }
        #endregion

        #region Watershed
        /// <summary
        /// Segments image using seed "markers"
        /// </summary>
        public void Watershed(CVArr markers) { PInvoke.cvWatershed(new __CvArrPtr(this), new __CvArrPtr(markers)); }
        #endregion

        #region Inpaint
        /// <summary>
        /// Inpaints the selected region in the image
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Inpaint(CVArr inpaint_mask, CVArr dst, double inpaintRange, int flags) { 
            PInvoke.cvInpaint(new __CvArrPtr(this), new __CvArrPtr(inpaint_mask), new __CvArrPtr(dst), inpaintRange, flags);
        }
        #endregion

        #region Sobel
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
        #endregion

        #region Laplace
        /// <summary>
        /// Calculates the image Laplacian: (d2/dx + d2/dy)I
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Laplace(CVArr dst, int aperture_size) { 
            PInvoke.cvLaplace(new __CvArrPtr(this), new __CvArrPtr(dst), aperture_size);
        }
        public void Laplace(CVArr dst) { Laplace(dst, 3); }
        #endregion

        #region ConvertColorSpace
        /// <summary>
        /// Converts input array pixels from one color space to another
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        [Obsolete("This method is obsolete. Please use ConvertColorSpace instead.")]
        public void CvtColor(CVArr dst, ColorSpace colorSpace) { 
            PInvoke.cvCvtColor(new __CvArrPtr(this), new __CvArrPtr(dst), (int)colorSpace);
        }

        /// <summary>
        /// Converts input array pixels from one color space to another.
        /// </summary>
        public static CVImage ConvertColorSpace(CVImage image, ColorSpace colorSpace)
        {
            CVImage dst = new CVImage(image.Width, image.Height, image.Depth, 3);
            PInvoke.cvCvtColor(new __CvArrPtr(image), new __CvArrPtr(dst), (int)colorSpace);
            return dst;
        }
        #endregion

        #region Resize
        /// <summary>
        /// Resizes image (input array is resized to fit the destination array)
        /// TODO: Remove 'dst' argument and return a CVImage object
        /// </summary>
        public void Resize(CVArr dst, ResizeInterpolation interpolation) { PInvoke.cvResize(new __CvArrPtr(this), new __CvArrPtr(dst), (int)interpolation); }
        public void Resize(CVArr dst) { Resize(dst, ResizeInterpolation.CV_INTER_LINEAR); }
        #endregion

        #region WarpAffine
        ///<summary>
        /// Warps image with affine transform
        /// TODO: Remove 'dst' argument and return a CVImage object
        ///</summary>
        public void WarpAffine(CVArr dst, CVMat map_matrix, WarpFlags flags, CVScalar fillval) { PInvoke.cvWarpAffine(new __CvArrPtr(this), new __CvArrPtr(dst), new __CvMatPtr(map_matrix), (int)flags, new __CvScalar(fillval)); }
        public void WarpAffine(CVArr dst, CVMat map_matrix, WarpFlags flags) { WarpAffine(dst, map_matrix, flags, new CVScalar()); }
        public void WarpAffine(CVArr dst, CVMat map_matrix) { WarpAffine(dst, map_matrix, WarpFlags.CV_WARP_FILL_OUTLIERS | WarpFlags.CV_WARP_INVERSE_MAP); }
        #endregion

        #region Match Template
        ///<summery>
		/// Compares template against overlapped image regions
		/// <param name="image">
		/// Image where the search is running. 
		/// It should be 8-bit or 32-bit floating-point.
		/// </param>
		/// <param name="templ">
		/// Searched template;
		/// must be not greater than the source image 
		/// and the same data type as the image. 
		/// </param>
		/// <param name="result">
		/// A map of comparison results; single-channel 
		/// 32-bit floating-point. 
		/// If image is W×H and templ is w×h then result must be W-w+1×H-h+1. 
		/// </param>
		/// <param name="method">
		/// Specifies the way the template must be compared with 
		/// image regions (see below). 
		/// </param>
		/// <remarks>
		/// The function cvMatchTemplate is similiar to 
		/// cvCalcBackProjectPatch. It slids through image, 
		/// compares overlapped patches of size w×h with templ 
		/// using the specified method and stores the comparison results to result. Here are the formula for the different comparison methods one may use (I denotes image, T - template, R - result. The summation is done over template and/or the image patch: x'=0..w-1, y'=0..h-1):
		/// 
		/// method=CV_TM_SQDIFF:
		/// R(x,y)=sumx',y'[T(x',y')-I(x+x',y+y')]2

		/// method=CV_TM_SQDIFF_NORMED:
		/// R(x,y)=sumx',y'[T(x',y')-I(x+x',y+y')]2/sqrt[sumx',y'T(x',y')2•sumx',y'I(x+x',y+y')2]

		/// method=CV_TM_CCORR:
		/// R(x,y)=sumx',y'[T(x',y')•I(x+x',y+y')]

		/// method=CV_TM_CCORR_NORMED:
		/// R(x,y)=sumx',y'[T(x',y')•I(x+x',y+y')]/sqrt[sumx',y'T(x',y')2•sumx',y'I(x+x',y+y')2]

		/// method=CV_TM_CCOEFF:
		/// R(x,y)=sumx',y'[T'(x',y')•I'(x+x',y+y')],

		/// where T'(x',y')=T(x',y') - 1/(w•h)•sumx",y"T(x",y")
		///       I'(x+x',y+y')=I(x+x',y+y') - 1/(w•h)•sumx",y"I(x+x",y+y")

		/// method=CV_TM_CCOEFF_NORMED:
		/// R(x,y)=sumx',y'[T'(x',y')•I'(x+x',y+y')]/sqrt[sumx',y'T'(x',y')2•sumx',y'I'(x+x',y+y')2]

		/// After the function finishes comparison, the best matches can be found as global minimums (CV_TM_SQDIFF*) or maximums (CV_TM_CCORR* and CV_TM_CCOEFF*) using cvMinMaxLoc function. In case of color image and template summation in both numerator and each sum in denominator is done over all the channels (and separate mean values are used for each channel). 
		/// </remarks>
		///</summery>
        public static CVImage MatchTemplate(CVImage image,
            CVImage templateToSearch,
            TemplateMatchMethod method)
        {

            //specify the size needed by the match function
            int resultW = image.Width - templateToSearch.Width + 1;
            int resultH = image.Height - templateToSearch.Height + 1;

            if (image.Channels > 1)
            {
                throw new CVException("CVMatchTemplate supports only one channel image format.");
            }
            if (!(image.Depth == CVDepth.Depth32F || image.Depth == CVDepth.Depth8U))
            {
                throw new CVException("CVMatchTemplate supports only 32F or 8U image format.");
            }
            if (image.Depth != templateToSearch.Depth || image.Channels != templateToSearch.Channels)
            {
                throw new CVException("image and template should be of the same type format.");
            }

            CVImage result = new CVImage(resultW, resultH, CVDepth.Depth32F, 1);

            // Native call to openCV cvMatchTemplate function:
            PInvoke.cvMatchTemplate(new __CvArrPtr(image), new __CvArrPtr(templateToSearch), new __CvArrPtr(result), (int)method);

            return result;
        }
        #endregion

        public static CVImage CannyEdgeDetection(CVImage image, double threshold1, double threshold2, int aperture_size) {
				
				if (image.Channels != 1) {
					throw new CVException("Canny edge detection supports only one channel image format.");
				}

				CVImage result = new CVImage(image.Width, image.Height, CVDepth.Depth8U, 1);

				// Native call to openCV canny algorithm:
				PInvoke.cvCanny(new __CvArrPtr(image), new __CvArrPtr(result), threshold1, threshold2, aperture_size);

				return result;
		}
    }
}
