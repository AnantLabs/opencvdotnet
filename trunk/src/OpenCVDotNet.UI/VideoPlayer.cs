using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenCVDotNet;

namespace OpenCVDotNet.UI
{
    public partial class VideoPlayer : Component
    {
        private CVCapture capture;
        private Timer videoTimer = new Timer();
        private PictureBox pictureBox = null;
        private bool loop = true;
        private CVImage lastFrame = null;

        /// <summary>
        /// This event is fired for each video frame.
        /// </summary>
        public event NextFrameEventHandler NextFrame;

        /// <summary>
        /// Fired when the video is playing.
        /// </summary>
        public event CancelEventHandler Playing;
        
        /// <summary>
        /// Fired when the video stream is pausing.
        /// </summary>
        public event CancelEventHandler Pausing;

        /// <summary>
        /// Fired when a video file is being opened by the video player.
        /// </summary>
        public event OpeningEventHandler Opening;

        /// <summary>
        /// Creates a video player object.
        /// </summary>
        public VideoPlayer()
        {
            videoTimer.Tick += new EventHandler(videoTimer_Tick);
        }

        /// <summary>
        /// Opens a video file.
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path)
        {
            CVCapture newCapture = new CVCapture(path);

            if (Opening != null)
            {
                OpeningEventArgs oea = new OpeningEventArgs();
                oea.CurrentCapture = capture;
                oea.NewCapture = newCapture;
                oea.Cancel = false;
                Opening(this, oea);

                if (oea.Cancel)
                {
                    newCapture.Dispose();
                    return;
                }
            }

            videoTimer.Enabled = false;
            capture = newCapture;
            videoTimer.Interval = 1000 / capture.FramesPerSecond;
        }

        /// <summary>
        /// Plays the video.
        /// </summary>
        public void Play()
        {
            if (Playing != null)
            {
                CancelEventArgs cea = new CancelEventArgs();
                
                Playing(this, cea);
             
                if (cea.Cancel)
                    return;
            }

            videoTimer.Enabled = true;
        }

        /// <summary>
        /// Moves to the next frame of the video stream.
        /// </summary>
        public void Step()
        {
            HandleNextFrame();
        }

        /// <summary>
        /// Pauses the video playback.
        /// </summary>
        public void Pause()
        {
            if (Pausing != null)
            {
                CancelEventArgs cea = new CancelEventArgs();
                Pausing(this, cea);
                if (cea.Cancel) return;
            }

            videoTimer.Enabled = false;
        }

        /// <summary>
        /// Restarts the video to the beginning.
        /// </summary>
        public void Rewind()
        {
            if (capture != null)
            {
                capture.Restart();
            }
        }

        /// <summary>
        /// Gets or sets the picture box associated with this video player.
        /// For each new frame, the picture box will hold the next frame.
        /// </summary>
        public PictureBox PictureBox
        {
            get { return pictureBox; }
            set { pictureBox = value; }
        }

        /// <summary>
        /// Determines if the video will loop or not.
        /// </summary>
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        /// <summary>
        /// Returns the last frame captured from the video source.
        /// </summary>
        public CVImage LastFrame
        {
            get { return lastFrame; }
        }

        /// <summary>
        /// Returns the capture object associated with this video player or null if
        /// there's no video loaded.
        /// </summary>
        public CVCapture Capture
        {
            get { return capture; }
        }

        /// <summary>
        /// Handles the next video frame.
        /// </summary>
        private void HandleNextFrame()
        {
            if (capture == null) return;

            // release last frame.
            if (lastFrame != null)
                lastFrame.Release();

            // query next frame.
            lastFrame = capture.QueryFrame();

            if (lastFrame == null)
            {
                if (loop)
                {
                    capture.Restart();
                    lastFrame = capture.QueryFrame();
                }
                else
                {
                    Pause();
                    return;
                }
            }

            // put next frame to picture box, if defined.
            if (pictureBox != null)
            {
                pictureBox.Image = lastFrame.ToBitmap();
            }

            // fire event.
            if (NextFrame != null)
            {
                NextFrameEventArgs ea = new NextFrameEventArgs();
                ea.Frame = lastFrame;
                ea.Capture = capture;
                NextFrame(this, ea);
            }
        }

        /// <summary>
        /// Creates a compatible video player with the default codec.
        /// </summary>
        /// <param name="filename">The output file name</param>
        /// <returns></returns>
        public CVVideoWriter CreateVideoWriter(string filename)
        {
            return CreateVideoWriter(filename, CVCodec.Mpeg4_2);
        }

        /// <summary>
        /// Creates a CVVideoWriter compatible with this video.
        /// </summary>
        /// <param name="filename">The file name of the output video</param>
        /// <param name="codec">The codec to use</param>
        /// <returns>A CVVideoWriter object</returns>
        public CVVideoWriter CreateVideoWriter(string filename, CVCodec codec)
        {
            return new CVVideoWriter(filename, codec, Capture.Width, Capture.Height);
        }

        /// <summary>
        /// Called when the timer ticks.
        /// </summary>
        private void videoTimer_Tick(object sender, EventArgs e)
        {
            HandleNextFrame();
        }
    }

    /// <summary>
    /// Arguments passed to the next frame handler.
    /// </summary>
    public class NextFrameEventArgs : EventArgs
    {
        public CVImage Frame;
        public CVCapture Capture;
    }

    /// <summary>
    /// Arguments passed to the Opening event handler.
    /// </summary>
    public class OpeningEventArgs : EventArgs
    {
        public CVCapture CurrentCapture;
        public CVCapture NewCapture;
        public bool Cancel;
    }

    public delegate void NextFrameEventHandler(VideoPlayer sender, NextFrameEventArgs args);
    public delegate void OpeningEventHandler(VideoPlayer sender, OpeningEventArgs args);
}
