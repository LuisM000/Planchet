using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class SHAEncryptionServiceTest
    {
        /// <summary>
        /// Test that bytes are correctly encripted
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void SHAEncryptionTest()
        {
            SHAEncryptionService shaEncryptionService = new SHAEncryptionService();
            byte[] dummyBytes=new byte[2];
            dummyBytes[0]=2;
            dummyBytes[1]=0;
            string result = Convert.ToBase64String(shaEncryptionService.Encrypt(String.Empty, dummyBytes));

            Assert.AreEqual("3JjkiIL1slcBsm/bRgkzYWi2U8YH4vAKlz+Y8bcOhZVALkkw2yNIyyOtwnNYtQ6wU9crKFQHPPPLHPgon1ckRA==", result);
        }
    }
}
