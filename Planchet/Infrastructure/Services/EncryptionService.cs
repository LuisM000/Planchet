using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// In charge of encrypt and decrypt data
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// Encrypt data (bytes[]) with a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(string key, byte[] bytes)
        {
            Rijndael rijndael = Rijndael.Create();

            //Initialize key and vector
            rijndael.GenerateIV();
            rijndael.Key = this.GetByteKey(key);

            byte[] encrypted = null;
            byte[] encryptedIV = null;
            //Create encrypted data
            encrypted = (rijndael.CreateEncryptor()).TransformFinalBlock(bytes, 0, bytes.Length);
            //Adds to return value vector and encrypted data
            encryptedIV = new byte[rijndael.IV.Length + encrypted.Length];
            rijndael.IV.CopyTo(encryptedIV, 0);
            encrypted.CopyTo(encryptedIV, rijndael.IV.Length);
            rijndael.Clear();
            return encryptedIV;
        }

        /// <summary>
        /// Decrypt encrypted data (bytes[]) with a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Decrypt(string key, byte[] bytes)
        {
            Rijndael rijndael = Rijndael.Create();
            //Creates correct key
            rijndael.Key = this.GetByteKey(key);
            
            //Prepares arrays
            byte[] tempArray = new byte[rijndael.IV.Length];
            byte[] encrypted = new byte[bytes.Length - rijndael.IV.Length];
            byte[] decrypted = null;

            Array.Copy(bytes, tempArray, tempArray.Length);
            Array.Copy(bytes, tempArray.Length, encrypted, 0, encrypted.Length);
            rijndael.IV = tempArray;

            //if encryption fails, it means that the key is not correct
            try
            {
                decrypted = rijndael.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
            }
            catch (Exception){}
            rijndael.Clear();  

            return decrypted;
        }

        /// <summary>
        /// Returns string key convert to byte[] with correct lenght
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] GetByteKey(string key)
        {
            byte[] keyByte = new PasswordDeriveBytes(key, null).GetBytes(32);
            return keyByte;

        }
    }
}
