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

        internal CVException(CvErrorContext context) : base(context._errMsg)
        {
            base.Data.Add("User Data", context._userData);
            base.Data.Add("Status", context._status);
            base.Source = string.Format("filename: {0} (line: {1})", context._fileName, context._line);
        }

        public CVException(string message)
            : base(message)
        {
            base.HelpLink = "http://code.google.com/p/opencvdotnet/";
        }
	}

    internal class CvErrorContext
    {
        public int _line;
        public int _status;
        public string _fileName;
        public string _errMsg;
        public string _userData;

        public CvErrorContext(string message, int line, string filename, int status, string userdata)
        {
            _errMsg = message;
            _line = line;
            _fileName = filename;
            _status = status;
            _userData = userdata;
        }
    }
}
