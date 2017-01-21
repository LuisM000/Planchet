using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;
using Infrastructure.IServices;
using Moq;
using System.Drawing;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Services.Test
{
    [TestClass]
    public class WebcamServiceTest
    {
        /// <summary>
        /// Test that image of webcam is correctly generated and if not webcam, not failed
        /// </summary>
        [TestTraits(Trait.CaptureData)]
        [TestMethod]
        public void GetWebcamImageTest()
        {
            Mock<IBitmapTransformerService> bitmapTransformerServiceMoq = new Mock<IBitmapTransformerService>();
            bitmapTransformerServiceMoq.Setup(bt => bt.ConvertTo(It.IsAny<Bitmap>())).Returns(new byte[5]);

            WebcamService webcamService = new WebcamService(bitmapTransformerServiceMoq.Object);
            var webcamImagen = webcamService.GetWebcamImage();

            bitmapTransformerServiceMoq.Verify(bt => bt.ConvertTo(It.IsAny<Bitmap>()), Times.Once());
            Assert.AreEqual(5, webcamImagen.Length);
        }
    }
}
