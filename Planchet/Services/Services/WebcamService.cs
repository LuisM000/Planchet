using AForge.Video.DirectShow;
using Infrastructure.IServices;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class WebcamService:IWebcamService
    {
        IBitmapTransformerService bitmapTransformerService;
        private VideoCaptureDevice videoSource;
        private AutoResetEvent autoResetEvent;
        private Bitmap webcamImage;

        /// <summary>
        /// If true, when you get an image the webcam doesn't turn off
        /// </summary>
        public bool WebcamAlwaysTurnedOn { get; set; }

        public WebcamService(IBitmapTransformerService bitmapTransformerService)
        {
            this.bitmapTransformerService = bitmapTransformerService;
        }

        /// <summary>
        /// Returns current webcam image
        /// </summary>
        /// <returns></returns>
        public byte[] GetWebcamImage()
        {
            byte[] imageBytes = null;
            autoResetEvent = new AutoResetEvent(false);
            //get Vid
            var videoDevice = this.GetVideoDevice();
            if (!String.IsNullOrEmpty(videoSource.Source))
            {
                //set NewFrame and error event handler
                videoDevice.NewFrame += videoSource_NewFrame;
                videoDevice.VideoSourceError += videoSource_VideoSourceError;
                // start the video source
                videoDevice.Start();
                autoResetEvent.WaitOne();
            }
            //Transform bitmap to bytes[]
            imageBytes = this.bitmapTransformerService.ConvertTo(webcamImage);

            return imageBytes;
        }

        /// <summary>
        /// Initialize and returns default videoCaptureDevice
        /// </summary>
        /// <returns></returns>
        private VideoCaptureDevice GetVideoDevice()
        {
            if (videoSource == null)
            {
                // enumerate video devices
                var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                // create video source
                videoSource = new VideoCaptureDevice();
                //Sets source of video
                if (videoDevices != null && videoDevices.Count > 0)
                    videoSource.Source = videoDevices[0].MonikerString;
            }
            return videoSource;
        }

        /// <summary>
        /// Capture is stopped if an error occurs.
        /// Indicates to autoResetEvent that has ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void videoSource_VideoSourceError(object sender, AForge.Video.VideoSourceErrorEventArgs eventArgs)
        {
            videoSource.SignalToStop();
            autoResetEvent.Set();
        }
        /// <summary>
        /// Saves capture in webcamImage.
        /// Indicates to autoResetEvent that has ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            webcamImage = new Bitmap(eventArgs.Frame);
            if(!this.WebcamAlwaysTurnedOn)
                videoSource.SignalToStop();
            autoResetEvent.Set();
        }
    }
}
