using PlanchetUI.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PlanchetUI.Services
{
    public class ImageViewer : IImageViewer
    {

        public void OpenImage(BitmapImage image)
        {
            if(image!=null)
            {
                String tmpFile = Path.GetTempFileName();
                tmpFile = Path.ChangeExtension(tmpFile, "png");
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (var fileStream = new System.IO.FileStream(tmpFile, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
                if (File.Exists(tmpFile))
                    Process.Start(tmpFile);  //Windows will use file association to open a viewer
                  
            }
       
        }
    }
}
