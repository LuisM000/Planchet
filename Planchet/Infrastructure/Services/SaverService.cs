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
    /// In charge of saving the files
    /// </summary>
    public class SaverService : ISaverService
    {
        /// <summary>
        /// Saves the byte[] in the location indicated
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        public void Save(byte[] bytes, string directory, string fileName, string extension)
        {
            this.InitializeDirectory(directory);
            string path = this.GetPath(directory, fileName, extension);
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Saves the text in the location indicated
        /// </summary>
        /// <param name="text"></param>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        public void Save(string text, string directory, string fileName, string extension)
        {
            this.InitializeDirectory(directory);
            string path = this.GetPath(directory, fileName, extension);
            File.WriteAllText(path, text);
        }

        private string GetPath(string directory, string fileName, string extension)
        {
            return directory + "/" + fileName + "." + extension;
        }

        /// <summary>
        /// Creates directory if not exists
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        private void InitializeDirectory(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}
