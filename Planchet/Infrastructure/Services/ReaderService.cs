using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// In charge of read data
    /// </summary>
    public class ReaderService:IReaderService
    {
        /// <summary>
        /// Opens file and returns data. If file not exits, returns null
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] Read(string path)
        {
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            else
                return null;
        }
    }
}
