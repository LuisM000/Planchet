using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using System.Drawing;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class BitmapTransformerServiceTest
    {
        /// <summary>
        /// Test that bitmap is converted to bytes
        /// </summary>
        [TestTraits(Trait.Infrastructure)]
        [TestMethod]
        public void BitmapToBytesTest()
        {
            Bitmap bmp = new Bitmap(1,1);
            
            BitmapTransformerService bitmapTransformer = new BitmapTransformerService();
            var bytesBmp = bitmapTransformer.ConvertTo(bmp);
            Assert.IsNotNull(bytesBmp);
        }
    }
}
