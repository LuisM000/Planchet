using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;
using Moq;
using Infrastructure.IServices;
using Domain.Interfaces;
using Model.Settings;
using System.Collections.Generic;
using Test.Base.Attributes;
using Test.Base.Enums;
using System.Text;

namespace Services.Test
{
    [TestClass]
    public class CredentialsServiceTest
    {
        /// <summary>
        /// Test that credentials are correctly storaged
        /// </summary>
       [TestTraits(Trait.Security)]
       [TestMethod]
        public void CreateCredentialsTest()
        {
            Credential credential = new Credential()
            {
                User = "User",
                Password = "Password"
            };

            Mock<IEncryptionService> encryptionServiceMoq = new Mock<IEncryptionService>();
            Mock<IRepository<Credential>> repositoryCredentialMoq = new Mock<IRepository<Credential>>();
            repositoryCredentialMoq.Setup(r => r.GetAll()).Returns(new List<Credential>());

            CredentialsService credentialsService = new CredentialsService(encryptionServiceMoq.Object,repositoryCredentialMoq.Object);
            credentialsService.CreateCredentials(credential);

            repositoryCredentialMoq.Verify(r => r.Insert(credential), Times.Once());
            encryptionServiceMoq.Verify(e => e.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>()), Times.AtLeastOnce());
        }

        /// <summary>
        /// Test that credentials are correctly compared based on Hash key
        /// </summary>
        [TestTraits(Trait.Security)]
        [TestMethod]
        public void IsCorrectCredentialsTest()
        {
            byte[] returnHash = new byte[1];
            returnHash[0] = 0;

            Credential credential = new Credential()
            {
                User = "User",
                Password = "Password"
            };
            Credential storageCredential = new Credential()
            {
                User = "User",
                Hash = Convert.ToBase64String(returnHash)
            };
           

            Mock<IEncryptionService> encryptionServiceMoq = new Mock<IEncryptionService>();
            Mock<IRepository<Credential>> repositoryCredentialMoq = new Mock<IRepository<Credential>>();
            repositoryCredentialMoq.Setup(r => r.GetAll()).Returns(new List<Credential>() { storageCredential });
            encryptionServiceMoq.Setup(r => r.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>())).
              Returns<string, byte[]>((k, b) => Encoding.ASCII.GetString(b).Equals("Password") ? returnHash : null);


            CredentialsService credentialsService = new CredentialsService(encryptionServiceMoq.Object, repositoryCredentialMoq.Object);
            var result = credentialsService.IsCorrect(credential);

            Assert.IsTrue(result);
            encryptionServiceMoq.Verify(e => e.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>()), Times.AtLeastOnce());
        }

        /// <summary>
        /// Test that credentials aren't correctly compared based on Hash key
        /// </summary>
        [TestTraits(Trait.Security)]
        [TestMethod]
        public void IsNotCorrectCredentialsTest()
        {
            byte[] returnHash = new byte[1];
            returnHash[0] = 0;
            byte[] returnHashFail = new byte[1];
            returnHash[0] = 1;

            Credential credential = new Credential()
            {
                User = "User",
                Password = "PasswordFail"
            };
            Credential storageCredential = new Credential()
            {
                User = "User",
                Hash = Convert.ToBase64String(returnHash)
            };
           

            Mock<IEncryptionService> encryptionServiceMoq = new Mock<IEncryptionService>();
            Mock<IRepository<Credential>> repositoryCredentialMoq = new Mock<IRepository<Credential>>();
            repositoryCredentialMoq.Setup(r => r.GetAll()).Returns(new List<Credential>() { storageCredential });


            encryptionServiceMoq.Setup(r => r.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>())).
                Returns<string,byte[]>((k,b)=>Encoding.ASCII.GetString(b).Equals("Password")?returnHash:returnHashFail);


            CredentialsService credentialsService = new CredentialsService(encryptionServiceMoq.Object, repositoryCredentialMoq.Object);
            var result = credentialsService.IsCorrect(credential);

            Assert.IsFalse(result);
        }
    }
}
