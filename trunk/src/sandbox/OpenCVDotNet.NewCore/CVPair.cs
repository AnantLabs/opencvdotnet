using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    public  class CVPair
	{
	private	float _first, _second;

	public	CVPair(float first, float second)
		{
			_first = first;
			_second = second;
		}

		public float First { get { return _first; } }
		public float Second { get { return _second; }}

		public override string ToString()
		{
			return String.Format("({0},{1})", _first, _second);
		}
	}
}
