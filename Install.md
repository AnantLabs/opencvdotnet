# Installing OpenCVDotNet #

If all you want is to play around with the binary version of OpenCVDotNet (write some OpenCV apps from C#), follow these steps:

  1. Download and install [OpenCV](http://sourceforge.net/project/showfiles.php?group_id=22870&package_id=16937). Make sure to install OpenCV to `C:\Program Files\OpenCV`, or otherwise OpenCVDotNet DLLs will not be able to find it.

  1. Download and install [Visual C# Express](http://msdn.microsoft.com/vstudio/express/visualcsharp/download/).

  1. Download & install the latest version of [OpenCVDotNet](http://code.google.com/p/opencvdotnet/downloads/list).

After installation, binaries and source code will be installed to `C:\Program Files\OpenCVDotNet`.

You can install example code if you wish to try things out...

Press CTRL+F5 to build and run the examples program.

_If you receive a FileNotFoundException with HRESULT 0x8007007E) after you execute your program, see [this issue](http://code.google.com/p/opencvdotnet/issues/detail?id=1) for possible solutions._