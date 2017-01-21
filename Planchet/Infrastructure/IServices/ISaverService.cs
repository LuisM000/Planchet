using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of saving the files
    /// </summary>
    public interface ISaverService
    {
        /// <summary>
        /// Saves the byte[] in the location indicated
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        void Save(byte[] bytes, string directory, string fileName, string extension);

        /// <summary>
        /// Saves the text in the location indicated
        /// </summary>
        /// <param name="text"></param>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        void Save(string text, string directory, string fileName, string extension);
    }
}
