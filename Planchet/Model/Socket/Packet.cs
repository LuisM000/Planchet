using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Socket
{
    /// <summary>
    /// Represents data sent by sockets
    /// </summary>
    public class Packet
    {
        public class PacketConstants
        {
            public const int MaxPacketSize = 62056;
            public const int MaxDataSize = 60000;
            public const int TypeLenght = 2000;
        }

        /// <summary>
        /// Header of packet
        /// </summary>
        public PacketHeader PacketHeader { get; private set; }
        /// <summary>
        /// Real content sent. It can represent, for example, an image
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Type of the content. Is used to transform the content in correct class.
        /// </summary>
        public Type ContentType { get; private set; }

        public Packet(){}

        /// <summary>
        /// Deserializes the data and convert it into a packet
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public Packet(byte[] data)
        {
            if(data.Length>Packet.PacketConstants.MaxDataSize + Packet.PacketConstants.TypeLenght + 
                PacketHeader.PacketHeaderConstants.HeaderLenght)
                throw new Exception("The size of data exceeds the maximum size of the package");

            //Packet header
            this.PacketHeader = new PacketHeader(data);
            int position = PacketHeader.PacketHeaderConstants.HeaderLenght;
            //Content type
            string contentType = Encoding.UTF8.GetString(data, position, PacketConstants.TypeLenght);
            this.ContentType = Type.GetType(contentType);
            position += PacketConstants.TypeLenght;
            //Content
            this.Content = data.Skip(position).Take(this.PacketHeader.Size).ToArray();
        }

        /// <summary>
        /// Creates a list of packets separating the data into different packages (respecting the maximum size per package)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static IList<byte[]> CreatePacket(byte[] data, Type contentType)
        {
            List<byte[]> buffers;
            int packetsNumber = (int)(Math.Ceiling((float)data.Length / (float)PacketConstants.MaxDataSize));
            buffers = new List<byte[]>(packetsNumber);

            string now = DateTime.UtcNow.ToString(PacketHeader.PacketHeaderConstants.DateTimeFormat);
            byte[] packetGuid = Guid.NewGuid().ToByteArray();
            for (int order = 0; order < packetsNumber; order++)
            {
                List<byte> buffer = new List<byte>();
                int initialBufferPosition = order * PacketConstants.MaxDataSize;
                int sizeBuffer = PacketConstants.MaxDataSize;
                //If is the last packet, size is the rest to the end
                if (order + 1 >= packetsNumber)
                    sizeBuffer = data.Length - initialBufferPosition;

                //Guid
                buffer.AddRange(Guid.NewGuid().ToByteArray());
                //Packet Guid
                buffer.AddRange(packetGuid);
                //DateTime
                buffer.AddRange(Encoding.UTF8.GetBytes(now));
                //Total number of packets
                buffer.AddRange(BitConverter.GetBytes(packetsNumber));
                //Current number of packet
                buffer.AddRange(BitConverter.GetBytes(order));
                //Size of data
                buffer.AddRange(BitConverter.GetBytes(sizeBuffer));
                //Type of data
                byte[] byteType = new byte[PacketConstants.TypeLenght];
                byte[] byteTypeAux=Encoding.UTF8.GetBytes(contentType.AssemblyQualifiedName);
                Array.Copy(byteTypeAux, byteType, byteTypeAux.Length);
                buffer.AddRange(byteType);

                //Data
                buffer.AddRange(data.Skip(initialBufferPosition).Take(sizeBuffer));
                buffers.Add(buffer.ToArray());
            }
            return buffers;
        }
      
    }
}
