using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using System.IO;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class SaverServiceTest
    {
        /// <summary>
        /// Test that saves text
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void SaveTextTest()
        {
            SaverService saverService = new SaverService();
            saverService.Save("Dummy text", "DummyDirectory", "dummyFile", "dum");
            var textSave = File.ReadAllText(Directory.GetCurrentDirectory() + "/DummyDirectory/dummyFile.dum");
            Assert.AreEqual("Dummy text", textSave);
        }

        /// <summary>
        /// Test that saves text
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void SaveBytesTest()
        {
            SaverService saverService = new SaverService();
            byte[] saveBytes = new byte[1] { 255 };
            saverService.Save(saveBytes, "DummyDirectory", "dummyFile", "dum");
            var bytesSave = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/DummyDirectory/dummyFile.dum");
            Assert.AreEqual(1, bytesSave.Length);
            Assert.AreEqual(255, bytesSave[0]);
        }
    }
}
