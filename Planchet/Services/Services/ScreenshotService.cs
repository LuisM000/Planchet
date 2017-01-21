using Infrastructure.IServices;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.Services
{
    /// <summary>
    /// In charge of screenshot
    /// </summary>
    public class ScreenshotService : IScreenshotService
    {
        IBitmapTransformerService bitmapTransformerService;

        public ScreenshotService(IBitmapTransformerService bitmapTransformerService)
        {
            this.bitmapTransformerService = bitmapTransformerService;
        }

        /// <summary>
        /// Returns current screenshot
        /// </summary>
        /// <returns></returns>
        public byte[] GetScreenshot()
        {
            Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                          Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(screenCapture))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                 Screen.PrimaryScreen.Bounds.Y,
                                 0, 0,
                                 screenCapture.Size,
                                 CopyPixelOperation.SourceCopy);
            }
            var imageBytes = this.bitmapTransformerService.ConvertTo(screenCapture);

            return imageBytes;
        }
    }
}
