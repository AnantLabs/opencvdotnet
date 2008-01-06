using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    public class CVException :  Exception
	{
	public CVException(string message) : 
        base(message) 
		{
		}
	}
}
