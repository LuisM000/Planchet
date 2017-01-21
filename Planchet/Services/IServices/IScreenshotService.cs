using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// In charge of screenshot
    /// </summary>
    public interface IScreenshotService
    {
        /// <summary>
        /// Returns current screenshot
        /// </summary>
        /// <returns></returns>
        byte[] GetScreenshot();
    }
}
