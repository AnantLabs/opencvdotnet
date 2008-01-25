using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    public class CVException :  Exception
	{
	    public CVException(string message, int line, string filename, int status, string userdata) : 
            this(message) 
		{
            base.Data.Add("User Data", userdata);
            base.Data.Add("Status", status);
            base.Source = string.Format("filename: {0} (line: {1})", filename, line);
		}

        public CVException(string message)
            : base(message)
        {
            base.HelpLink = "http://code.google.com/p/opencvdotnet/";
        }
	}
}
