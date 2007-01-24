#pragma once

namespace OpenCVDotNet
{
	public ref class CVArr abstract 
	{
	public:
		virtual property CvArr* Array
		{
			CvArr* get() = 0;
		}
	};
}