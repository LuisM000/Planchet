using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of transform bitmap class to other data types
    /// </summary>
    public interface IBitmapTransformerService
    {
        byte[] ConvertTo(Bitmap bitmap);
    }
}
