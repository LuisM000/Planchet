using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of read data
    /// </summary>
    public interface IReaderService
    {
        /// <summary>
        /// Opens file and returns data
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        byte[] Read(string path);
    }
}
