using PlanchetUI.ViewModels.Base;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanchetUI.ViewModels
{
    public class SocketVM : MainViewModel
    {
        #region Attributes
        IPacketReceiver packetReceiver;
        #endregion

        #region Properties
        private IPEndPoint iPEndPoint;

        public IPEndPoint IPEndPoint
        {
            get 
            {
                if(iPEndPoint==null)
                {
                    IPAddress localIP = IPAddress.Any;
                    iPEndPoint = new IPEndPoint(localIP, 8080);
                }
                return iPEndPoint; 
            }
            set 
            {
                iPEndPoint = value; 
            }
        }

        private byte[] image;

        public byte[] Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(() => Image); }
        }
        #endregion
        public SocketVM(IPacketReceiver packetReceiver)
        {
            this.packetReceiver = packetReceiver;
        }

        #region Commands
        public ICommand InitializeReceiverCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.packetReceiver.Initialize(this.IPEndPoint);
                    this.packetReceiver.CompletePacket += packetReceiver_ReceivedPacket;
                });
            }
        }

        public ICommand StopReceiverCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.packetReceiver.Dispose();
                    this.packetReceiver.CompletePacket -= packetReceiver_ReceivedPacket;
                });
            }
        }

        void packetReceiver_ReceivedPacket(object sender, Model.Socket.ProcessedData e)
        {
            this.Image = e.GetContent();
        }
  
        #endregion
    }
}
