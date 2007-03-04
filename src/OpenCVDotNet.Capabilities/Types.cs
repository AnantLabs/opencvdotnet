using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    public struct CVTermCriteria
    {
        public enum Type { CV_TERMCRIT_ITER = 1, CV_TERMCRIT_EPS = 2 };
        public Type type;
        public int max_iter;
        public double epsilon;
        public CVTermCriteria(int max_iter, double epsilon) { this.type = Type.CV_TERMCRIT_EPS | Type.CV_TERMCRIT_ITER; this.max_iter = max_iter; this.epsilon = epsilon; }
        public CVTermCriteria(double epsilon) { this.type = Type.CV_TERMCRIT_EPS; this.max_iter = 0; this.epsilon = epsilon; }
        public CVTermCriteria(int max_iter) { this.type = Type.CV_TERMCRIT_ITER; this.max_iter = max_iter; this.epsilon = 0.0; }
        public CVTermCriteria(Type type, int max_iter, double epsilon) { this.type = type; this.max_iter = max_iter; this.epsilon = epsilon; }
    }

    public class CVScalar
    {
        public double[] val;
        public CVScalar() { val = new double[4];  }
        public CVScalar(double[] val) { this.val = val; }
    }

    /// <summary>
    /// THIS IS JUST TEMP -- NEED TO BE REPLACED WITH A REAL CLASS
    /// </summary>
    public class CVMat : CVImage
    {
        public CVMat() : base("") { }
    }
}

