using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class EncryptionServiceTest
    {
        /// <summary>
        /// Test that encrypt and decrypt data correctly
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void EncryptAndDecryptTest()
        {
            EncryptionService encryptionService = new EncryptionService();

            byte[] bytes=new byte[3]{0,1,2};

            var encryptData = encryptionService.Encrypt("Klapaucius", bytes);
            var decryptData = encryptionService.Decrypt("Klapaucius", encryptData);

            Assert.AreEqual(3, decryptData.Length);
            Assert.AreEqual(0, decryptData[0]);
            Assert.AreEqual(1, decryptData[1]);
            Assert.AreEqual(2, decryptData[2]);
        }

        /// <summary>
        /// Test that if the password is incorrect, the decryption data fails
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void NoDecryptTest()
        {
            EncryptionService encryptionService = new EncryptionService();

            byte[] bytes = new byte[3] { 0, 1, 2 };

            var encryptData = encryptionService.Encrypt("Klapaucius", bytes);
            var decryptData = encryptionService.Decrypt("DummyPassoword", encryptData);

            Assert.IsNull(decryptData);
        }
    }
}
