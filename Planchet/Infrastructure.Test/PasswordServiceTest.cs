using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using Moq;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class PasswordServiceTest
    {
        /// <summary>
        /// Test that checks password
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void CheckPasswordTest()
        {
            SaverService saverService = new SaverService();
            ReaderService readerService = new ReaderService();
            EncryptionService encryptionService = new EncryptionService();

            PasswordService passwordService = new PasswordService(saverService, readerService, encryptionService);
            passwordService.CreateKey("klapaucius");
            Assert.IsTrue(passwordService.IsCorrect("klapaucius"));
        }

        /// <summary>
        /// Test that if password is not correct, returns false
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void CheckIncorrectPasswordTest()
        {
            SaverService saverService = new SaverService();
            ReaderService readerService = new ReaderService();
            EncryptionService encryptionService = new EncryptionService();
            PasswordService passwordService = new PasswordService(saverService, readerService, encryptionService);
            passwordService.CreateKey("klapaucius");
            Assert.IsFalse(passwordService.IsCorrect("dummyPassword"));


        }
    }
}
