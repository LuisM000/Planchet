using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using System.IO;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class ReaderServiceTest
    {
        /// <summary>
        /// Test that a file is read correctly
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void ReadFileTest()
        {
            var myFile = File.Create("DummyFile");
            myFile.Close();
            ReaderService readerService = new ReaderService();
            var file = readerService.Read("DummyFile");
            Assert.IsNotNull(file);
        }
    }
}
