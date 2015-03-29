# Hello, OpenCVDotNet #

We'll create a simple C# project that uses OpenCVDotNet to perform a simple image-wise operation on a video (AVI) file (show only the red color channel instead of all three color channels).

You will first need to [Install](Install.md) OpenCVDotNet (and all needed components).

## Creating the Project ##

  * Start Visual C# Express.
  * Create a new Windows Application project (File -> New Project, Windows Application).
  * Add references to the compiled DLL of OpenCVDotNet (if you used the installer to intsall OpenCVDotNet, the DLL should be under C:\Program Files\OpenCVDotNet) (Project -> Add Reference, Browse, find DLL).

## CVImage.ToBitmap() ##

OpenCVDotNet's `CVImage` object has a wondrous method called `ToBitmap()`. This method turns an OpenCV image into a .NET `Bitmap` object, which can be used whenever a Bitmap object is accepted.

A common use of this method is to place a `PictureBox` on your form, and assign the resulting `Bitmap` into the `Image` of the `PictureBox`. This way, a CV image can be displayed on a Windows Form.

  * Add a `PictureBox` component to the form using the Forms Designer (double click on the `PictureBox` item in the Toolbox.
  * Dock the picture box to your form (set the Dock property of the picture box to "Full").

## CVCapture ##

`CVCapture` can be used to capture images from a video - it can either be constructed to capture images from a Web Cam, or from an AVI file.

We will use a `CVCapture` and a .NET `Timer` object to play a video stream.

The `Timer` object will tick every 40 milliseconds (25 times per second), and in every tick, we will capture the next frame of the video, analyze it and display it into the picture box.

  * Add a Timer object to the form using the Forms Designer (double click on the Timer item from the Toolbox).
  * Configure the new timer object to tick every 40 milliseconds (set the `Interval` property to 40).
  * Enable the timer (set the `Enabled` property of the timer to `True`).
  * Add an event handler for the Timer.Tick event (double click on the Tick event of the Timer's event list).
  * Add a handler for the Load event of your main form (double click on the Load event of the form event list).
  * Press F7 to switch to code view of the form.
  * At the beginning of the file, add a `using` statement to include the `OpenCVDotNet` namespace.
  * Add a private member variable to contain the `CVCapture` object.

Your file should look like this:

```
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;

namespace HelloOpenCVDotNet
{
    public partial class Form1 : Form
    {
        private CVCapture capture;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
```

## Opening the Video File ##

Type in the following code under `Form1_Load` handler (under <path to AVI file>, put a path to the AVI file that you wish to display).

```
private void Form1_Load(object sender, EventArgs e)
{
    capture = new CVCapture("<path to AVI file>");
}
```

## QueryFrame ##

Use the `CVCapture.QueryFrame()` method to receive the next frame every tick, convert it to a `Bitmap` and assign it as the Picture Box's image.

```
private void timer1_Tick(object sender, EventArgs e)
{
    using (CVImage nextFrame = capture.QueryFrame())
    {
        pictureBox1.Image = nextFrame.ToBitmap();
    }            
}
```

Note that we use the [using](http://msdn2.microsoft.com/en-us/library/yh598w02.aspx) statement here, to make sure the `CVImage` object is disposed at the end of the method. Without 'using', we would have to explicitly call `nextFrame.Release()` at the end of the method.

**Congrats!** You can run your application and check it out. It should show the video you referenced in the picture box, and it should be playing!

## Split and Merge ##

Now, we will do some changes to the image before it's displayed. We'll flip the color channels of the image, so that we will use the red channel instead of the green channel, the blue channel instead of the red and the green channel instead of the blue.

To do that, we will use the `Split` and `Merge` methods of `CVImage`. The `Split` method returns an array of 3 `CVImage` objects, corresponding to the blue, green and red channels (in that order). The Merge takes an array of three `CVImage` objects and merges them into the three channels (BGR order as well).

```
using (CVImage nextFrame = capture.QueryFrame())
{
    using (CVImage emptyImage = new CVImage(nextFrame.Width, nextFrame.Height, CVDepth.Depth8U, 1))
    {
        // set entire image to "0".
        emptyImage.Zero();

        // split the image into three channels
        CVImage[] bgrChannels = nextFrame.Split();
        nextFrame.Merge(new CVImage[] { emptyImage, emptyImage, bgrChannels[2] });
        pictureBox1.Image = nextFrame.ToBitmap();

        // release channel images.
        foreach (CVImage ch in bgrChannels)
            ch.Release();
    }
}            
```

Notes:
  * After the `QueryFrame`, we create a new `CVImage` object named `emptyImage` that is in the same size of the original image, but with only one channel.
  * Then, we call `Zero()` to initialize the image to 0.
  * Then, we use `Split` to split the image to its 3 channels and `Merge` to merge the channels back, taking only the 3rd component (red) and using `emptyImage` for the blue and green.
  * Note that we must release the images received from `Split()`.