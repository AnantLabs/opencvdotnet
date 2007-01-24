#pragma once

namespace OpenCVDotNet
{
	public ref class CVException : public Exception
	{
	public:
		CVException(String^ message) : Exception(message) 
		{
		}
	};
};