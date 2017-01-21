using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// In charge of webcam management
    /// </summary>
    public interface IWebcamService
    {
        /// <summary>
        /// Returns current webcam image
        /// </summary>
        /// <returns></returns>
        byte[] GetWebcamImage();
        /// <summary>
        /// If true, when you get an image the webcam doesn't turn off
        /// </summary>
        bool WebcamAlwaysTurnedOn { get; set; }
    }
}
