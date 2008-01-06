using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCVDotNet
{
    
    public enum CVCodec
	{
		UserDefined = -1,
		Mpeg1 = (('P'&255) + (('I'&255)<<8) + (('M'&255)<<16) + (('1'&255)<<24)), // MPEG-1 codec
        MotionJpeg = (('M'&255) + (('J'&255)<<8) + (('P'&255)<<16) + (('G'&255)<<24)), // motion-jpeg codec (does not work well)
        Mpeg4_2 = (('M'&255) + (('P'&255)<<8) + (('4'&255)<<16) + (('2'&255)<<24)), // MPEG-4.2 codec
        Mpeg4_3 = (('D'&255) + (('I'&255)<<8) + (('V'&255)<<16) + (('3'&255)<<24)), // MPEG-4.3 codec
        
        Mpeg4 = (('D'&255) + (('I'&255)<<8) + (('V'&255)<<16) + (('X'&255)<<24)), // MPEG-4 codec
        H263 = (('U'&255) + (('2'&255)<<8) + (('6'&255)<<16) + (('3'&255)<<24)), // H263 codec
        H263I = (('I'&255) + (('2'&255)<<8) + (('6'&255)<<16) + (('3'&255)<<24)), // H263I codec
        Flv1 = (('F'&255) + (('L'&255)<<8) + (('V'&255)<<16) + (('1'&255)<<24)), // FLV1 codec
	}

	public enum CVFramesPerSecond
	{
		Fps25 = 25,
		Fps30 = 30,
	}

	public unsafe class CVVideoWriter
	{
	    private	__CvVideoWriter* vw_;

    	public		CVVideoWriter(string filename, int width, int height)
		{
			Create(filename, CVCodec.UserDefined, CVFramesPerSecond.Fps25, width, height, true);
		}

		public CVVideoWriter(string filename, CVCodec codec, int width, int height)
		{
			Create(filename, codec, CVFramesPerSecond.Fps25, width, height, true);
		}

		public CVVideoWriter(string filename, CVCodec codec, CVFramesPerSecond fps, int width, int height)
		{
			Create(filename, codec, fps, width, height, true);
		}

		public CVVideoWriter(string filename, CVCodec codec, CVFramesPerSecond fps, int width, int height, bool is_color)
		{
			Create(filename, codec, fps, width, height, is_color);
		}

		~CVVideoWriter()
		{
			Release();
		}

		public void WriteFrame(CVImage image)
		{
			PInvoke.cvWriteFrame(vw_, image.Internal);
		}

		public void Release()
		{
            __CvVideoWriter* ptr = vw_; // CvVideoWriter* prt = vw_
			PInvoke.cvReleaseVideoWriter(&ptr);
		}

	    protected void Create(string filename, CVCodec codec, CVFramesPerSecond fps, int width, int height, bool is_color)
		{
            vw_ = PInvoke.cvCreateVideoWriter(filename, (int)codec, (int)fps,
                     new __CvSize(width, height), is_color ? 1 : 0);
		}
	}
}
