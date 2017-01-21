using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;

namespace Services.Test
{
    [TestClass]
    public class GZipCompressionServiceTest
    {
        [TestMethod]
        public void CompressAndDecompressArrayOfBytesTest()
        {
            byte[] dummyData = new byte[1];
            dummyData[0] = 255;

            GZipCompressionService compressionService = new GZipCompressionService();
            var compressedData = compressionService.Compress(dummyData);
            var decompressedData = compressionService.Decompress(compressedData);

            Assert.AreEqual(255, decompressedData[0]);
        }
        
    }
}
