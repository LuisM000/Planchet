using Domain.Interfaces;
using Infrastructure.IServices;
using Model.Entities.Image;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Actions
{
    /// <summary>
    /// Gets and saves a webcam image
    /// </summary>
    public class WebcamCommand:ICommand
    {
        IRepository<Webcam> repoWebcam;
        IWebcamService webcamService;
       
        public WebcamCommand() { }
        public WebcamCommand(IRepository<Webcam> repoWebcam, IWebcamService webcamService)
        {
            this.repoWebcam = repoWebcam;
            this.webcamService = webcamService;
        }
        public override void Execute()
        {
            byte[] webcam = webcamService.GetWebcamImage();
            repoWebcam.Insert(new Webcam() { Image = webcam });
            repoWebcam.SaveChanges();
        }
    }
}
