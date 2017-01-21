using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Settings
{
    /// <summary>
    /// In charge of describes times
    /// </summary>
    public class Time:Entity
    {
        /// <summary>
        /// If true, screenshot are not captured
        /// </summary>
        public bool NotCaptureScreenshot { get; set; }
        /// <summary>
        /// Time between screenshots (milliseconds)
        /// </summary>
        public int ScreenshotTime { get; set; }

        /// <summary>
        /// If true, the webcam images are not captured
        /// </summary>
        public bool NotCaptureWebcam { get; set; }
        /// <summary>
        /// Time between screenshots (milliseconds)
        /// </summary>
        public int WebcamTime { get; set; }
        
    }
}
