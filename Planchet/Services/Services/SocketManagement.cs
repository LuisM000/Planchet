using Azure.Entities;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    /// <summary>
    /// In charge of coordinate the sending of information through sockets
    /// </summary>
    public class SocketManagement : ISocketManagement
    {
        IScreenshotService screenshotService;
        IWebcamService webcamService;
        IPacketSender packetScreenshotSender;
        IPacketSender packetWebcamSender;

        Thread webcamThread;
        Thread screenshotThread;
        ManualResetEvent webcamMre;
        ManualResetEvent screenshotMre;
        SocketDescriptor webcamSocketDescriptor;
        SocketDescriptor screenshotSocketDescriptor;

        public SocketManagement(IScreenshotService screenshotService,IWebcamService webcamService,
            IPacketSender packetScreenshotSender, IPacketSender packetWebcamSender)
        {
            this.screenshotService = screenshotService;
            this.webcamService = webcamService;
            this.packetScreenshotSender = packetScreenshotSender;
            this.packetWebcamSender = packetWebcamSender;
            this.webcamService.WebcamAlwaysTurnedOn = true;

            this.screenshotThread = new Thread(ScreenshotSocketSender);
            this.webcamThread = new Thread(WebcamSocketSender);
            //ToDo: start on Builder??? I think that it smell
            this.screenshotThread.Start();
            this.webcamThread.Start();

            this.webcamMre = new ManualResetEvent(false);
            this.screenshotMre = new ManualResetEvent(false);
        }

        /// <summary>
        /// Updates the properties of sockets
        /// </summary>
        /// <param name="socket"></param>
        public void Update(Socket socket)
        {
            if(socket.ScreenshotSender!=null)
            {
                this.screenshotSocketDescriptor = socket.ScreenshotSender;
                this.Sender(this.screenshotSocketDescriptor, this.screenshotThread, this.ScreenshotSocketSender,
                    this.packetScreenshotSender, this.screenshotMre);
            }
            if (socket.WebcamSender != null)
             {
                this.webcamSocketDescriptor = socket.WebcamSender;
                this.Sender(this.webcamSocketDescriptor, this.webcamThread, this.WebcamSocketSender,
                    this.packetWebcamSender, this.webcamMre);
            }
        }
 
        
        /// <summary>
        /// Manages thread of sending information
        /// </summary>
        /// <param name="socketDescriptor"></param>
        /// <param name="thread"></param>
        /// <param name="threadStart"></param>
        /// <param name="packetSender"></param>
        private void Sender(SocketDescriptor socketDescriptor, Thread thread, ThreadStart threadStart,
            IPacketSender packetSender, ManualResetEvent resetEvent)
        {
            //If socket should be stop or restart, blocks the thread
            if (socketDescriptor.Stop || socketDescriptor.Restart)
            {
                resetEvent.Reset();
            }
            //Initialize thread if hasn't be stopped
            if (!socketDescriptor.Stop)
            {
                packetSender.Initialize(socketDescriptor.GetServerEndPoint());
                resetEvent.Set();
            }
        }
        
        /// <summary>
        /// Captures and sends screenshot
        /// </summary>
        private void ScreenshotSocketSender()
        {
            while (this.screenshotMre.WaitOne())
            {
                byte[] screenshot=this.screenshotService.GetScreenshot();
                this.packetScreenshotSender.SendData(screenshot, typeof(SocketManagement));
                Thread.Sleep(TimeSpan.FromMilliseconds(this.screenshotSocketDescriptor.TimeRate));
            }
        }

        /// <summary>
        /// Captures and sends webcam image
        /// </summary>
        private void WebcamSocketSender()
        {
            while (this.webcamMre.WaitOne())
            {
                byte[] webcam = this.webcamService.GetWebcamImage();
                this.packetWebcamSender.SendData(webcam, typeof(SocketManagement));
                Thread.Sleep(TimeSpan.FromMilliseconds(this.webcamSocketDescriptor.TimeRate));
            }
        }

    }
}

