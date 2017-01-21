using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.ViewModels.Notifications
{
    public class SocketNotificationVM : NotificationBaseVM
    {
        public override Type NotificationType
        {
            get
            {
                return typeof(Socket);
            }
        }

        private bool webcamSender;

        public bool WebcamSender
        {
            get { return webcamSender; }
            set 
            { 
                webcamSender = value;
                SocketDescriptor socketDescriptor = null;
                if (webcamSender)
                    socketDescriptor = new SocketDescriptor()
                    {
                        ServerPort=8000,

                    };
                ((Socket)this.NotificationBase).WebcamSender = socketDescriptor;
                OnPropertyChanged(() => WebcamSender);
                OnPropertyChanged(() => NotificationBase);
            }
        }

        private bool screenshotSender;

        public bool ScreenshotSender
        {
            get { return screenshotSender; }
            set
            {
                screenshotSender = value;
                SocketDescriptor socketDescriptor = null;
                if (screenshotSender)
                    socketDescriptor = new SocketDescriptor()
                        {
                            ServerPort=8080,
                        };
                ((Socket)this.NotificationBase).ScreenshotSender = socketDescriptor;
                OnPropertyChanged(() => ScreenshotSender);
                OnPropertyChanged(() => NotificationBase);
            }
        }

    }
}
