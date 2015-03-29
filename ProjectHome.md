# OpenCVDotNet #

As part of an entry-level University [project](http://opencvdotnet.googlecode.com/files/Tracking%20of%20Humans%20Using%20Masked%20Histograms%20and%20Mean%20Shift.pdf) in Computer Vision at the [IDC Herzliya](http://www.idc.ac.il) with [Dr. Yael Moses](http://www.faculty.idc.ac.il/moses/), I have created a small OpenCV wrapper for .NET Framework, so I could use OpenCV from C#, and create more elaborate user interfaces.

  * [OpenCVDotNet-0.7](http://opencvdotnet.googlecode.com/files/opencvdotnet-0.7-setup.exe) Released.
  * Examples are now in separate installer.
  * Check out OpenCVDotNet [Tutorial](Tutorial.md).

The project consists of a DLL (written in Managed C++) that wraps the OpenCV library in .NET classes, so that they will be available from any managed language (C#, VB.NET or Managed C++).

You will need to [download](http://sourceforge.net/projects/opencvlibrary/files/opencv-win/1.0/OpenCV_1.0.exe/download) and install the OpenCV library 1.0 in order for this to compile. Make sure to install it into `C:\Program Files\OpenCV` so that the installer will be able to locate them.

I have also uploaded a few examples (stuff that I created during the University project).

Please note that I didn't make any attempt to be compatible with OpenCV API. I preferred to design the wrappers in a way that they will be natural to .NET users, so don't be mad if it's not 100% in the spirit of OpenCV.

Everyone is welcome to contribute!
Post any queries to [opencvdotnet-discuss](http://groups.google.com/group/opencvdotnet-discuss).

Thanks,
Elad Ben-Israel

![http://opencvdotnet.googlecode.com/files/MeanShiftTracker.jpg](http://opencvdotnet.googlecode.com/files/MeanShiftTracker.jpg)