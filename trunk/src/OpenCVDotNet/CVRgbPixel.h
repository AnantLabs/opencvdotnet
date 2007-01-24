#pragma once

namespace OpenCVDotNet
{
	public ref class CVRgbPixel
	{
		Byte r, b, g;

	public:
		CVRgbPixel(Byte r, Byte g, Byte b)
		{
			this->r = r;
			this->b = b;
			this->g = g;
		}

		property Byte R { Byte get() { return r; } }
		property Byte G { Byte get() { return g; } }
		property Byte B { Byte get() { return b; } }

		property Byte BwValue
		{
			Byte get()
			{
				Byte bw_value = (Byte)
					((double) b * 0.114 + 
					 (double) g * 0.587 + 
					 (double) r * 0.299);
				return bw_value;
			}
		}

		virtual String^ ToString() override
		{
			return String::Format("({0},{1},{2})", r, g, b);
		}
	};
};