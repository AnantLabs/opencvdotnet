/**
 * (C) 2007 Elad Ben-Israel
 * This code is licenced under the GPL.
 */

#pragma once

#include "CVException.h"

namespace OpenCVDotNet
{
	static int ErrorHandler(int status, const char* func_name, const char* err_msg, const char* file_name, int line, void* userdata)
	{
		throw gcnew CVException(gcnew System::String(err_msg));
		return 0;
	}

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

		static void ErrorsToExceptions()
		{
			cvRedirectError(ErrorHandler);
		}
	};
};