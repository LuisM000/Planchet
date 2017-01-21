using Model.Socket;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PacketReceiver : IPacketReceiver
    {
        private IList<ProcessedData> fragmentData;
        private Socket socket;
        Thread receiverThread;
        Thread dataProcessing;
        private Queue<byte[]> receivedData;

        /// <summary>
        /// Event generated when all packets with same identifier are received
        /// </summary>
        public event EventHandler<ProcessedData> CompletePacket;

        /// <summary>
        /// Initializes and prepares the socket to receive packets
        /// </summary>
        /// <param name="localEP"></param>
        public void Initialize(System.Net.IPEndPoint localEP)
        {
            fragmentData = new List<ProcessedData>();
            receivedData = new Queue<byte[]>();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEP);
            receiverThread = new Thread(() => this.ListenerSocket());
            dataProcessing = new Thread(() => this.ProcessingLastReceivedData());
            receiverThread.Start();
            dataProcessing.Start();
        }

        /// <summary>
        /// In charge of receive the data 
        /// </summary>
        private void ListenerSocket()
        {
            byte[] receivedData = new byte[Packet.PacketConstants.MaxPacketSize];
            int bytesRead;
            while ((bytesRead = socket.Receive(receivedData)) > 0)
            {
                byte[] receivedDataCopy = new byte[Packet.PacketConstants.MaxPacketSize];
                Array.Copy(receivedData, receivedDataCopy, receivedData.Length);
                this.receivedData.Enqueue(receivedDataCopy);
            }
        }

        private void ProcessingLastReceivedData()
        {
            while (true)
            {
                if (this.receivedData.Count > 0)
                {
                    var lastDataReceived = this.receivedData.Dequeue();
                    if(lastDataReceived!=null)
                    {
                        this.StoreDataInPacket(lastDataReceived);
                        this.GenerateEventWithCompletedPackets();
                    }
                }
            }
        }

        /// <summary>
        /// Adds the data to ProcessedData
        /// </summary>
        /// <param name="data"></param>
        private void StoreDataInPacket(byte[] data)
        {
            Packet packet = new Packet(data);
            ProcessedData fragment = fragmentData.FirstOrDefault(f => f.FragmentationGuid.Equals(packet.PacketHeader.FragmentationGuid));
            if(fragment==null)
            {
                fragment = new ProcessedData();
                fragmentData.Add(fragment);
            }
            fragment.Add(packet);
        }

        /// <summary>
        /// Searches in the data received those that have been completed and generates the event
        /// </summary>
        private void GenerateEventWithCompletedPackets()
        {
            foreach (var processed in fragmentData.Where(f => f.IsCompleted).ToList())
            {
                this.CompletePacket(this, processed);
                fragmentData.Remove(processed);
            }
        }

        /// <summary>
        /// Releases the socket and the thread associated with data reception
        /// </summary>
        public void Dispose()
        {
            receiverThread.Abort();
            socket.Dispose();
        }

    }
}
