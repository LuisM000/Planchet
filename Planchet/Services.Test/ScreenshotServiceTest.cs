using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;
using Moq;
using Infrastructure.IServices;
using System.Drawing;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Services.Test
{
    [TestClass]
    public class ScreenshotServiceTest
    {
        /// <summary>
        /// Test that screenshot is correctly capture
        /// </summary>
        [TestTraits(Trait.CaptureData)]
        [TestCategory("NotBuild")]
        [TestMethod]
        public void GetScreenshotTest()
        {
            Mock<IBitmapTransformerService> bitmapTransformerServiceMoq=new Mock<IBitmapTransformerService>();
            bitmapTransformerServiceMoq.Setup(bt => bt.ConvertTo(It.IsNotNull<Bitmap>())).Returns(new byte[5]);
            ScreenshotService screenshotService = new ScreenshotService(bitmapTransformerServiceMoq.Object);
            var screenshot = screenshotService.GetScreenshot();

            bitmapTransformerServiceMoq.Verify(bt => bt.ConvertTo(It.IsAny<Bitmap>()), Times.Once());
            Assert.AreEqual(5, screenshot.Length);
        }
    }
}
