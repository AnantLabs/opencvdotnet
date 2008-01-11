using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    public enum CVHistogramType
	{
		Array = 0, //CV_HIST_ARRAY,
		Sparse = 1, //CV_HIST_SPARSE,
	}

	public class CVHistogram : IDisposable
	{
        private IntPtr _hist = IntPtr.Zero;
        private int[] _binSizes;
        private CVPair[] _binRanges;

	    public CVHistogram(int[] binSizes, CVPair[] binRanges)
		{
			CreateHist(binSizes, binRanges);
        }

		~CVHistogram()
		{
			Release();
		}

        public void Release()
		{
            if (this.Internal != IntPtr.Zero)
            {
                PInvoke.cvReleaseHist(ref _hist);
            }
		}

		public CVPair MinMaxValue
		{
		    get
			{
                float min_value = 0, max_value = 0;
                // TODO: Use the P/Invoke call
                PInvoke.cvGetMinMaxHistValue(this.Internal, ref min_value, ref max_value);
                //int min_idx = 0, max_idx = 0;
                //for (int i = 0; i < 256; ++i)
                //{
                //    if (min_value > this[i]) min_value = (float)this[i];
                //    if (max_value < this[i]) max_value = (float)this[i];
                //}
                return new CVPair((float)min_value, (float)max_value);
			}
		}

		public double this[int idx0]
		{
			get
			{
				return PInvoke.cvQueryHistValue_1D(_hist, idx0);
			}
		}
		
		internal IntPtr Internal
		{
			get
			{
				return _hist;
			}
            private set
            {
                _hist = value;
            }
		}

		public int[] BinSizes
		{
			get
			{
				return this._binSizes;
			}
		}

		public CVPair[] BinRanges
		{
			get
			{
				return this._binRanges;
			}

		}

	    protected void CreateHist(int[] binSizes, CVPair[] binRanges)
		{
            this._binSizes = binSizes;
            this._binRanges = binRanges;

			

            float[][] ranges = new float[binRanges.Length][];

            // make sure ranges & sizes are of the same dimention.
            System.Diagnostics.Debug.Assert(binSizes.Length == binRanges.Length, "Ranges & sizes must be of the same dimention");

            // create ranges array.
            for (int i = 0; i < binRanges.Length; ++i)
            {
                float[] range = new float[2];
                range[0] = binRanges[i].First;
                range[1] = binRanges[i].Second;
                ranges[i] = range;
            }

            this.Internal = PInvoke.cvCreateHist(this._binSizes, (int)CVHistogramType.Array, ranges, true);    		
		}

        #region IDisposable Members

        public void Dispose()
        {
            this.Release();
            System.GC.SuppressFinalize(this);
        }

        #endregion
    }
}
