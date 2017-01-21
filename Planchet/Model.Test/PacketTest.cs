using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Base.Attributes;
using Test.Base.Enums;
using Model.Socket;
using System.Collections.Generic;

namespace Model.Test
{
    [TestClass]
    public class PacketTest
    {
        /// <summary>
        /// Test that creates one packet is lower than max size
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateOneIncompletePacketTest()
        {
            byte[] data=new byte[20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            Assert.AreEqual(1, packet.Count);
            Assert.AreEqual(20 + Packet.PacketConstants.TypeLenght + PacketHeader.PacketHeaderConstants.HeaderLenght, 
                packet[0].Length);
        }

        /// <summary>
        /// Test that creates one packet exactly the same as the maximum size 
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateOneCompletePacketTest()
        {
            byte[] data = new byte[Packet.PacketConstants.MaxDataSize];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            Assert.AreEqual(1, packet.Count);
            Assert.AreEqual(Packet.PacketConstants.MaxPacketSize, packet[0].Length);
        }

        /// <summary>
        /// Test that creates two packets, one is exactly the same as the maximum size and
        /// the other is lower than max size
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateTwoIncompletePacketTest()
        {
            byte[] data = new byte[Packet.PacketConstants.MaxDataSize+20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            Assert.AreEqual(2, packet.Count);
            Assert.AreEqual(Packet.PacketConstants.MaxPacketSize, packet[0].Length);
            Assert.AreEqual(20 + Packet.PacketConstants.TypeLenght + PacketHeader.PacketHeaderConstants.HeaderLenght,
                packet[1].Length);
        }

        /// <summary>
        /// Test that creates two packets that are exactly the same as the maximum size
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateTwoCompletePacketTest()
        {
            byte[] data = new byte[Packet.PacketConstants.MaxDataSize * 2];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            Assert.AreEqual(2, packet.Count);
            Assert.AreEqual(Packet.PacketConstants.MaxPacketSize, packet[0].Length);
            Assert.AreEqual(Packet.PacketConstants.MaxPacketSize, packet[1].Length);
        }


        /// <summary>
        /// Test that creates and deserializes one packet
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateAndDeserializePacketTest()
        {
            byte[] data = new byte[20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)i;
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            var deserializePacket = new Packet(packet[0]);

            Assert.AreEqual(1, deserializePacket.PacketHeader.Fragmentation);
            Assert.AreEqual(0, deserializePacket.PacketHeader.Order);
            Assert.AreEqual(20, deserializePacket.PacketHeader.Size);
            Assert.IsTrue(DateTime.Now.Subtract(deserializePacket.PacketHeader.Time).TotalMilliseconds > 0);
            Assert.IsTrue(deserializePacket.PacketHeader.Guid != deserializePacket.PacketHeader.FragmentationGuid);

            Assert.AreEqual(typeof(PacketTest), deserializePacket.ContentType);
            Assert.AreEqual(20, deserializePacket.Content.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(data[i], deserializePacket.Content[i]);
            }
        }

        /// <summary>
        /// Test that creates and deserializes one packet
        /// </summary>
        [TestTraits(Trait.Model)]
        [TestMethod]
        public void CreateAndDeserializeTwoPacketTest()
        {
            byte[] data = new byte[Packet.PacketConstants.MaxDataSize + 20];
            Random rdn=new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)rdn.Next(256);
            }

            IList<byte[]> packet = Packet.CreatePacket(data, typeof(PacketTest));

            var deserializePacket = new Packet(packet[0]);
            Assert.AreEqual(2, deserializePacket.PacketHeader.Fragmentation);
            Assert.AreEqual(0, deserializePacket.PacketHeader.Order);
            Assert.AreEqual(Packet.PacketConstants.MaxDataSize, deserializePacket.PacketHeader.Size);
            Assert.IsTrue(DateTime.Now.Subtract(deserializePacket.PacketHeader.Time).TotalMilliseconds > 0);
            Assert.IsTrue(deserializePacket.PacketHeader.Guid != deserializePacket.PacketHeader.FragmentationGuid);

            Assert.AreEqual(typeof(PacketTest), deserializePacket.ContentType);
            Assert.AreEqual(Packet.PacketConstants.MaxDataSize, deserializePacket.Content.Length);

            for (int i = 0; i < Packet.PacketConstants.MaxDataSize; i++)
            {
                Assert.AreEqual(data[i], deserializePacket.Content[i]);
            }
            
            var deserializePacket2 = new Packet(packet[1]);
            Assert.AreEqual(2, deserializePacket2.PacketHeader.Fragmentation);
            Assert.AreEqual(1, deserializePacket2.PacketHeader.Order);
            Assert.AreEqual(20, deserializePacket2.PacketHeader.Size);
            Assert.IsTrue(DateTime.Now.Subtract(deserializePacket2.PacketHeader.Time).TotalMilliseconds > 0);
            Assert.IsTrue(deserializePacket2.PacketHeader.Guid != deserializePacket2.PacketHeader.FragmentationGuid);

            Assert.AreEqual(typeof(PacketTest), deserializePacket2.ContentType);
            Assert.AreEqual(20, deserializePacket2.Content.Length);

            int index = 0;
            for (int i = Packet.PacketConstants.MaxDataSize; i < Packet.PacketConstants.MaxDataSize+20; i++)
            {
                Assert.AreEqual(data[i], deserializePacket2.Content[index]);
                index++;
            }

            Assert.AreEqual(deserializePacket.PacketHeader.Time, deserializePacket2.PacketHeader.Time);
            Assert.AreNotEqual(deserializePacket.PacketHeader.Guid, deserializePacket2.PacketHeader.Guid);
            Assert.AreEqual(deserializePacket.PacketHeader.FragmentationGuid, deserializePacket2.PacketHeader.FragmentationGuid);
        }
    }
}
