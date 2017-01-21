using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SHAEncryptionService:IEncryptionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">This parameters is ignored</param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(string key, byte[] bytes)
        {
            SHA512Managed hash = new SHA512Managed();
            byte[] encryptedBytes = hash.ComputeHash(bytes);
            hash.Clear();
            return encryptedBytes;
        }

        /// <summary>
        /// Hash cannot be decrypted. Throws a exception
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Decrypt(string key, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
