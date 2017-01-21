using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Base.Attributes;
using Test.Base.Enums;
using Services.Services;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using Model.Socket;
using System.Threading;
using System.Linq;

namespace Services.Test
{
    [TestClass]
    public class PacketSenderTest
    {
        private Socket GetServer()
        {
            IPAddress localIP = IPAddress.Any;
            IPEndPoint localEP = new IPEndPoint(localIP, 8080);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if(server.LocalEndPoint==null)
                server.Bind(localEP);
            return server;
        }
        private IPEndPoint GetEndPoint()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);

            return remoteEP;
        }

        /// <summary>
        /// Test that simple packet is sent correctly
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void SendSimplePacketTest()
        {            
            Socket server = this.GetServer();
            IList<Packet> received = new List<Packet>();
            byte[] buffer=new byte[Packet.PacketConstants.MaxPacketSize];
            server.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, (IAsyncResult asyncResult) =>
            {
                int bytesReceived = server.EndReceive(asyncResult);
                received.Add(new Packet(buffer.Take(bytesReceived).ToArray()));
            }, server);

            PacketSender packetSender = new PacketSender();
            packetSender.Initialize(this.GetEndPoint());

            byte[] data = new byte[3] { 255, 255, 1 };
            packetSender.SendData(data,typeof(PacketSenderTest));
            Thread.Sleep(100);

            Assert.AreEqual(1, received.Count);
            Assert.AreEqual(3, received[0].Content.Count());
            Assert.AreEqual(255, received[0].Content[0]);
            Assert.AreEqual(1, received[0].Content[2]);

            packetSender.Dispose();
            server.Dispose();
        }

        /// <summary>
        /// Test that multiple packets are sent correctly
        /// </summary>
        [TestTraits(Trait.Sockets)]
        [TestMethod]
        public void SendMultiplePacketTest()
        {
            Socket server = this.GetServer();
            IList<Packet> received = new List<Packet>();
            byte[] buffer = new byte[Packet.PacketConstants.MaxPacketSize];
            int bytesRead = 0;
            new Thread(() =>
                {
                    while ((bytesRead = server.Receive(buffer)) > 0)
                    {
                        received.Add(new Packet(buffer.Take(bytesRead).ToArray()));

                    }
                }).Start();
           

            PacketSender packetSender = new PacketSender();
            packetSender.Initialize(this.GetEndPoint());

            byte[] data = new byte[Packet.PacketConstants.MaxDataSize + 3];
            data[0] = 255;
            data[Packet.PacketConstants.MaxDataSize + 2] = 1;
            packetSender.SendData(data, typeof(PacketSenderTest));
            Thread.Sleep(500);

            Assert.AreEqual(2, received.Count);
            Assert.AreEqual(Packet.PacketConstants.MaxDataSize, received[0].Content.Count());
            Assert.AreEqual(255, received[0].Content[0]);

            Assert.AreEqual(3, received[1].Content.Count());
            Assert.AreEqual(1, received[1].Content[2]);

            packetSender.Dispose();
            server.Dispose();
        }
    }
}
