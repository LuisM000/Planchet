using Azure.Entities;
using Azure.Services.IServices;
using Domain.Interfaces;
using Infrastructure.IFactories;
using Infrastructure.IServices;
using Model.Entities.Image;
using Model.Settings;
using Model.Socket;
using Services.External.IServices;
using Services.External.Services;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Planchet
{
    public class MainCapturer
    {
        IWebcamService webcamService;
        IScreenshotService screenshotService;
        IConfigurationService configurationService;
        IRepository<Screenshot> repoScreenshot;
        IRepository<Webcam> repoWebcam;
        INotifyIconService notifyIconService;
        IBusService busService;
        INotificationUIService notificationUIService;
        ICommandFactory commandFactory;
        ISocketManagement socketManagement;

        EventWaitHandle waitHandle;
        Timer timerScreenshot;
        Timer timerWebcam;
        bool signaled;

        public MainCapturer(IWebcamService webcamService, IScreenshotService screenshotService,
            IConfigurationService configurationService, IRepository<Screenshot> repoScreenshot,
            IRepository<Webcam> repoWebcam,INotifyIconService notifyIconService,IBusService busService,
            INotificationUIService notificationUIService,ICommandFactory commandFactory,ISocketManagement socketManagement)
        {
            this.webcamService = webcamService;
            this.screenshotService = screenshotService;
            this.configurationService = configurationService;
            this.repoScreenshot = repoScreenshot;
            this.repoWebcam = repoWebcam;
            this.notifyIconService = notifyIconService;
            this.busService = busService;
            this.notificationUIService = notificationUIService;
            this.commandFactory = commandFactory;
            this.socketManagement = socketManagement;
            waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString());
        }

        public void Initialize()
        {
            this.SetConfigurations();
            this.InitializeBusService();
            do
            {
                signaled = waitHandle.WaitOne();
            } while (!signaled);
        }

        /// <summary>
        /// Sets all configurations of application
        /// </summary>
        private void SetConfigurations()
        {
            this.SetTimers();
            this.SetInterface();
        }
        /// <summary>
        /// Creates timers to capture and save data
        /// </summary>
        private void SetTimers()
        {
            Time timeConfigurations = this.configurationService.GetConfigurationTime();
            //Screenshot timer
            if(!timeConfigurations.NotCaptureScreenshot)
                timerScreenshot = new Timer(CaptureScreenshot, null, TimeSpan.FromMilliseconds(timeConfigurations.ScreenshotTime),
                                                TimeSpan.FromMilliseconds(timeConfigurations.ScreenshotTime));
            //Webcam timer
            if(!timeConfigurations.NotCaptureWebcam)
                timerWebcam = new Timer(CaptureWebcam, null, TimeSpan.FromMilliseconds(timeConfigurations.WebcamTime),
                                                TimeSpan.FromMilliseconds(timeConfigurations.WebcamTime));
        }
        /// <summary>
        /// Creates interface of application based on interface configuration
        /// </summary>
        private void SetInterface()
        {
            Interface interfaceConfiguration = this.configurationService.GetConfigurationInterface();
            if (interfaceConfiguration.ShowNotifyIcon)
                this.notifyIconService.CreateNotifyIcon(Process.GetCurrentProcess());
        }

        /// <summary>
        /// Initializes the bus service and suscribed to event
        /// </summary>
        private void InitializeBusService()
        {
            BusNotification busConfiguration = this.configurationService.GetConfigurationBus();
            if(busConfiguration.IsActive)
            {
                this.busService.Initialize();
                this.busService.ReceivedData += busService_ReceivedData;
            }
        }

        /// <summary>
        /// Captures bus message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void busService_ReceivedData(object sender, NotificationBase e)
        {
            if(e!=null)
            {
                this.ExecuteInstantActions(e);
                if (e is Settings && ((Settings)e).UpdateSettings)
                    this.SetConfigurations();
                else if(e is Message && ((Message)e)!=null)
                    this.notificationUIService.OpenMessage(((Message)e));
                else if(e is Socket && ((Socket)e)!=null)
                    this.socketManagement.Update((Socket)e);
            }
        }

        /// <summary>
        /// Executes all instant actions associated to notification
        /// </summary>
        /// <param name="notification"></param>
        private void ExecuteInstantActions(NotificationBase notification)
        {
            if(notification.Actions!=null)
                foreach (var action in notification.Actions.Where(a=>a.Activation.Equals(Azure.Entities.Action.ActivationType.Instant)))
                {
                    ICommand command = this.commandFactory.GetCommand(action.ActionType);
                    command.Execute();
                }
        }

        /// <summary>
        /// Captures and saves screenshot
        /// </summary>
        /// <param name="state"></param>
        private void CaptureScreenshot(object state)
        {
            byte[] screenShot = screenshotService.GetScreenshot();
            repoScreenshot.Insert(new Screenshot() { Image = screenShot });
            repoScreenshot.SaveChanges();
        }
        /// <summary>
        /// Captures and saves webcam
        /// </summary>
        /// <param name="state"></param>
        private void CaptureWebcam(object state)
        {
            byte[] webcam = webcamService.GetWebcamImage();
            repoWebcam.Insert(new Webcam() { Image = webcam });
            repoWebcam.SaveChanges();
        }

    }
}
