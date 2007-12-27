using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    public class CVArithmetic
    {
        
	
		///<summery>
		///Finds global minimum and maximum in array or subarray.
		///</summery>
        public static void CVMinMaxLoc(CVImage image,
            out double minVal,
            out double maxVal,
            out System.Drawing.Point minLocation,
            out System.Drawing.Point maxLocation,
            CVArr mask)
        {

            // Prepare out paramaters:
            __CvPoint min_loc = new __CvPoint();
            __CvPoint max_loc = new __CvPoint();
            double min_val = -1;
            double max_val = -1;

            
            //CvArr tempMask = 0;
            //if (mask != nullptr) {
            //    tempMask = mask->Array;
            //}
            //CVArr tempMask = mask.Array;

            minLocation = new System.Drawing.Point(0, 0);
            maxLocation = new System.Drawing.Point(0, 0);

            // Native call to openCV cvMinMaxLoc:
            PInvoke.cvMinMaxLoc(
                new __CvArrPtr(image),
                ref min_val, ref max_val,
                ref min_loc, ref max_loc, new __CvArrPtr(mask));

            minVal = min_val;
            maxVal = max_val;
            minLocation = new System.Drawing.Point(min_loc.x, min_loc.y);
            maxLocation = new System.Drawing.Point(max_loc.x, max_loc.y);

        }
    }
}
