using Model.Socket;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PacketSender : IPacketSender
    {
        AutoResetEvent sendDone;
        Socket socket;
        IPEndPoint remoteEP;

        /// <summary>
        /// Initializes and prepares the socket to send packets
        /// </summary>
        /// <param name="remoteEP"></param>
        public void Initialize(IPEndPoint remoteEP)
        {
            this.remoteEP = remoteEP;
            sendDone = new AutoResetEvent(false);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Connect(remoteEP);
        }

        /// <summary>
        /// Sends data to remote socket. Blocks thread until all data is sent.
        /// The data are sent synchronously
        /// </summary>
        /// <param name="data"></param>
        /// <param name="typeData"></param>
        public void SendData(byte[] data, Type typeData = null)
        {
            IList<byte[]> sendPackets = Packet.CreatePacket(data, typeData);
            foreach (var packet in sendPackets)
            {
                socket.BeginSendTo(packet, 0, packet.Length, SocketFlags.None, remoteEP, new AsyncCallback(SendDataCallback), socket);
                sendDone.WaitOne();
                Thread.Sleep(15);
            }
        }

        /// <summary>
        /// End send data and indicates that data sender is ended
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SendDataCallback(IAsyncResult asyncResult)
        {
            socket.EndSend(asyncResult);
            sendDone.Set();
        }

        /// <summary>
        /// Releases the socket
        /// </summary>
        public void Dispose()
        {
            if(socket!=null)
            {
                socket.Close();
                socket.Dispose();
            }
        }
    }
}
