using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PlanchetUI.Converters
{
    public class BytesToImageConverter:IValueConverter
    {
        /// <summary>
        /// Obtains wpf image from byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var byteArrayImage = value as byte[];
 
            if (byteArrayImage != null && byteArrayImage.Length > 0)
            {
                try
                {
                    var ms = new MemoryStream(byteArrayImage);
                    var bitmapImg = new BitmapImage();
                    bitmapImg.BeginInit();
                    bitmapImg.StreamSource = ms;
                    bitmapImg.EndInit();
                    return bitmapImg;
                }
                catch (Exception)
                {
                    
                    return null;
                }
               
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
