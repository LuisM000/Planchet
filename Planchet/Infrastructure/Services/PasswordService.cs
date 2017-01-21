using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// In charge of management of passwords
    /// </summary>
    public class PasswordService : IPasswordService
    {
        internal class PasswordServiceConstants
        {
            public static string FileName = "Pass";
            public static int Iterations = 1000;
            public static string PrivateKey = "asdAsld34iu234i3/(565dsadsadjad";
            public static string Salt = "sakjndsa8764738(/&(/&%&%$sd";
        }

        ISaverService saverService;
        IReaderService readerService;
        IEncryptionService encryptionService;

        public PasswordService(ISaverService saverService,IReaderService readerService,
            IEncryptionService encryptionService)
        {
            this.saverService = saverService;
            this.readerService = readerService;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Creates new key
        /// </summary>
        /// <param name="password"></param>
        public void CreateKey(string password)
        {
            var hash = this.GetHashKey(password);
            //ToDo: this shouldn't save where can be modified and adds encryption (think pass of encrytion)
            //surely if is saved in db, encryption is not necessary (without user/pass of db, no access)
            byte[] encryted = this.encryptionService.Encrypt(PasswordServiceConstants.PrivateKey, hash);
            this.saverService.Save(encryted, Directory.GetCurrentDirectory(),
                PasswordServiceConstants.FileName, String.Empty);
        }

        /// <summary>
        /// Cheks if the password is correct
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsCorrect(string password)
        {
            bool isCorrect = false;
            byte[] passwordFile = this.readerService.Read(string.Concat(Directory.GetCurrentDirectory(),"/", PasswordServiceConstants.FileName));
            if(passwordFile!=null)
            {
                byte[] decryptedFile = this.encryptionService.Decrypt(PasswordServiceConstants.PrivateKey, passwordFile);
                byte[] inputPassword = this.GetHashKey(password);
                isCorrect = decryptedFile.SequenceEqual(inputPassword);
            }
            return isCorrect;
        }

        /// <summary>
        /// Returns hash password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private byte[] GetHashKey(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(PasswordServiceConstants.Salt);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, salt, "SHA256", PasswordServiceConstants.Iterations);
            return passwordDeriveBytes.GetBytes(32);
        }
    }
}
