using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PlanchetUI.IServices
{
    public interface IImageViewer
    {
        void OpenImage(BitmapImage image);
    }
}
