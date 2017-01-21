using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;
using System.Net;
using System.Collections.Generic;
using Model.Socket;
using System.Net.Sockets;
using System.Threading;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Services.Test
{
    [TestClass]
    public class PacketReceiverTest
    {

        private Socket GetClient()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Connect(remoteEP);
            return client;
        }
        private IPEndPoint GetEndPoint()
        {
            IPAddress localIP = IPAddress.Any;
            IPEndPoint localEP = new IPEndPoint(localIP, 8080);
            return localEP;
        }

        /// <summary>
        /// Test that simple packet is received correctly
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void ReceiveSimplePacketTest()
        {
            PacketReceiver packetReceiver = new PacketReceiver();
            packetReceiver.Initialize(this.GetEndPoint());

            List<ProcessedData> completedPackets = new List<ProcessedData>();
            packetReceiver.CompletePacket += delegate(object sender, ProcessedData e)
            {
                completedPackets.Add(e);
            };

            Socket client = this.GetClient();

            byte[] data = new byte[3] { 255, 255, 255 };
            IList<byte[]> sendData = Packet.CreatePacket(data, typeof(PacketReceiverTest));

            foreach (var dt in sendData)
            {
                client.Send(dt);
            }
            Thread.Sleep(100);

            Assert.AreEqual(1, completedPackets.Count);
            Assert.AreEqual(255, completedPackets[0].Packets[0].Content[0]);
            Assert.AreEqual(255, completedPackets[0].Packets[0].Content[2]);


            packetReceiver.Dispose();
        }

        /// <summary>
        /// Test that composed packet is received correctly
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void ReceiveComposedPacketPacketTest()
        {
            PacketReceiver packetReceiver = new PacketReceiver();
            packetReceiver.Initialize(this.GetEndPoint());

            List<ProcessedData> completedPackets = new List<ProcessedData>();
            packetReceiver.CompletePacket += delegate(object sender, ProcessedData e)
            {
                completedPackets.Add(e);
            };
            
            Socket client = this.GetClient();

            byte[] data = new byte[Packet.PacketConstants.MaxDataSize + 3];
            data[0] = 255;
            data[Packet.PacketConstants.MaxDataSize + 2] = 255;

            IList<byte[]> sendData = Packet.CreatePacket(data, typeof(PacketReceiverTest));

            foreach (var dt in sendData)
            {
                client.Send(dt);
            }
            Thread.Sleep(100);

            Assert.AreEqual(1, completedPackets.Count);
            Assert.AreEqual(255, completedPackets[0].Packets[0].Content[0]);
            Assert.AreEqual(255, completedPackets[0].Packets[1].Content[2]);
            packetReceiver.Dispose();
        }


        /// <summary>
        /// Test that composed packet is received correctly even if a packet with another identifier is received 
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void ReceiveComposedPacketWithIntercalatePacketTest()
        {
            PacketReceiver packetReceiver = new PacketReceiver();
            packetReceiver.Initialize(this.GetEndPoint());

            List<ProcessedData> completedPackets = new List<ProcessedData>();
            packetReceiver.CompletePacket += delegate(object sender, ProcessedData e)
            {
                completedPackets.Add(e);
            };

            Socket client = this.GetClient();

            byte[] data = new byte[Packet.PacketConstants.MaxDataSize + 3];
            data[0] = 255;
            data[Packet.PacketConstants.MaxDataSize + 2] = 255;

            byte[] malignedData = new byte[Packet.PacketConstants.MaxDataSize + 3];
            malignedData[0] = 1;
            malignedData[Packet.PacketConstants.MaxDataSize + 2] = 1;

            IList<byte[]> sendData = Packet.CreatePacket(data, typeof(PacketReceiverTest));
            IList<byte[]> sendMalignedData = Packet.CreatePacket(malignedData, typeof(PacketReceiverTest));

            client.Send(sendData[0]);
            client.Send(sendMalignedData[0]);
            client.Send(sendData[1]);

             
            Thread.Sleep(100);

            Assert.AreEqual(1, completedPackets.Count);
            Assert.AreEqual(255, completedPackets[0].Packets[0].Content[0]);
            Assert.AreEqual(255, completedPackets[0].Packets[1].Content[2]);
            packetReceiver.Dispose();
        }


            
        /// <summary>
        /// Test that two composed packet are received correctly even if are received disordered
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void ReceiveTwoComposedPacketWithIntercalatePacketTest()
        {
            PacketReceiver packetReceiver = new PacketReceiver();
            packetReceiver.Initialize(this.GetEndPoint());

            List<ProcessedData> completedPackets = new List<ProcessedData>();
            packetReceiver.CompletePacket += delegate(object sender, ProcessedData e)
            {
                completedPackets.Add(e);
            };

            Socket client = this.GetClient();

            byte[] data = new byte[Packet.PacketConstants.MaxDataSize + 3];
            data[0] = 255;
            data[Packet.PacketConstants.MaxDataSize + 2] = 255;

            byte[] data2 = new byte[Packet.PacketConstants.MaxDataSize + 3];
            data2[0] = 1;
            data2[Packet.PacketConstants.MaxDataSize + 2] = 1;

            IList<byte[]> sendData = Packet.CreatePacket(data, typeof(PacketReceiverTest));
            IList<byte[]> sendData2 = Packet.CreatePacket(data2, typeof(PacketReceiverTest));

            client.Send(sendData[0]);
            client.Send(sendData2[0]);
            client.Send(sendData[1]);
            client.Send(sendData2[1]);


            Thread.Sleep(100);

            Assert.AreEqual(2, completedPackets.Count);
            Assert.AreEqual(255, completedPackets[0].Packets[0].Content[0]);
            Assert.AreEqual(255, completedPackets[0].Packets[1].Content[2]);

            Assert.AreEqual(1, completedPackets[1].Packets[0].Content[0]);
            Assert.AreEqual(1, completedPackets[1].Packets[1].Content[2]);
            
            packetReceiver.Dispose();
        }
    }
}
