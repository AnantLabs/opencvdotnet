#pragma once

namespace OpenCVDotNet
{
	public ref class CVUtils
	{
	public:
		static void StringToCharPointer(String^ string, char* output, int size)
		{
			pin_ptr<const __wchar_t> p = PtrToStringChars(string);
			wcstombs_s(NULL, output, size, p, size);
		}

		static int Round(double val)
		{
			return cvRound(val);
		}
	};
};