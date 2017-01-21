using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of encrypt and decrypt data
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypt data (bytes[]) with a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        byte[] Encrypt(string key, byte[] bytes);

        /// <summary>
        /// Decrypt encrypted data (bytes[]) with a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        byte[] Decrypt(string key, byte[] bytes);
    }
}

