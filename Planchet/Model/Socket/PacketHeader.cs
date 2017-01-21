using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Socket
{
    /// <summary>
    /// Header of a packet. Indicates the properties of a packet, for example his identifier.
    /// </summary>
    public class PacketHeader
    {
        public class PacketHeaderConstants
        {
            public const string DateTimeFormat = "HH:mm:ss.fff";
            public const int HeaderLenght = 56;
            public const int FragmentationLenght = 4;
            public const int OrderLenght = 4;
            public const int DateTimeLenght = 12;
            public const int MessageSizeLenght = 4;
            public const int GuidLenght = 16;
            public const int PacketGuidLenght = 16;
        }

        /// <summary>
        /// Unique identifier of a packet
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Identifier of a set of related packages 
        /// </summary>
        public Guid FragmentationGuid { get; set; }
        /// <summary>
        /// Number of packets representing the total of the information. All packets will have the same FragmentationGuid
        /// </summary>
        public int Fragmentation { get; private set; }
        /// <summary>
        /// Packet order in a fragmented set. Starts at 0.
        /// </summary>
        public int Order { get; private set; }
        /// <summary>
        /// Time the header was created
        /// </summary>
        public DateTime Time { get; private set; }
        /// <summary>
        /// Size of content
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Deserializes the header in the data
        /// </summary>
        /// <param name="data"></param>
        public PacketHeader(byte[] data)
        {
           int position = 0;
           this.Guid = new Guid(data.Skip(position).Take(PacketHeaderConstants.GuidLenght).ToArray());
           position += PacketHeaderConstants.GuidLenght;

           this.FragmentationGuid = new Guid(data.Skip(position).Take(PacketHeaderConstants.PacketGuidLenght).ToArray());
           position += PacketHeaderConstants.PacketGuidLenght;

           this.Time = DateTime.ParseExact(Encoding.UTF8.GetString(data, position, PacketHeaderConstants.DateTimeLenght),
               PacketHeaderConstants.DateTimeFormat, CultureInfo.InvariantCulture);
           position += PacketHeaderConstants.DateTimeLenght;

           this.Fragmentation = BitConverter.ToInt32(data, position);
           position += PacketHeaderConstants.FragmentationLenght;

           this.Order = BitConverter.ToInt32(data, position);
           position += PacketHeaderConstants.OrderLenght;

           this.Size = BitConverter.ToInt32(data, position);
           position += PacketHeaderConstants.MessageSizeLenght;
        }
    }
}
