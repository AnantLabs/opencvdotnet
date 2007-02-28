#include <stdio.h>
#include <cv.h>
#include <highgui.h>

void main()
{
	// open a window named "example".
	cvNamedWindow("example");

	// create capture from file.
	CvCapture* cap = cvCaptureFromFile("..\\sphere.avi");
	if (cap == NULL) 
	{
		printf("Unable to open video file.\n");
		return;
	}

	// get capture parameters.
	int width = (int) cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_WIDTH);
	int height = (int) cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_HEIGHT);
	int fps = (int) cvGetCaptureProperty(cap, CV_CAP_PROP_FPS);

	// create video writer for the output.
	CvVideoWriter* writer = cvCreateVideoWriter("c:\\myoutput.avi", 0, fps, cvSize(width, height));

	IplImage* frame;
	while((frame = cvQueryFrame(cap)))
	{
		// clone the image, so it could be manipulated.
		IplImage* clone = cvCloneImage(frame);

		// iterate over all the pixels in the frame.
		for (int row = 0; row < clone->height; ++row)
		{
			for (int col = 0; col < clone->width * clone->nChannels; col = col + clone->nChannels)
			{
				// iterate over all the color channels.
				for (int ch = 0; ch < clone->nChannels; ++ch)
				{
					// get the pixel value.
					BYTE pixel = CV_IMAGE_ELEM(clone, BYTE, row, col + ch);

					// invert it.
					CV_IMAGE_ELEM(clone, BYTE, row, col + ch) = 255 - pixel;
				}
			}
		}		

		// display image in window and write to output file.
		cvShowImage("example", clone);
		cvWriteFrame(writer, clone);
		
		// release the clone.
		cvReleaseImage(&clone);

		// hold on for 100 / fps time.
		int key = cvWaitKey(1000 / fps);
		if (key == 27) break;
	}

	// release the capture and video writer.
	cvReleaseCapture(&cap);
	cvReleaseVideoWriter(&writer);

	printf("Done\n");
}