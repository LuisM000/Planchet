using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Socket
{
    /// <summary>
    /// In charge of store packets of the same group
    /// </summary>
    public class ProcessedData
    {
        public ProcessedData()
        {
            this.Packets = new List<Packet>();
        }
        private bool isInitialized;
        
        /// <summary>
        /// Related packets (with same FragmentationGuid)
        /// </summary>
        public IList<Packet> Packets { get; set; }
        /// <summary>
        /// Identifier of the packets
        /// </summary>
        public Guid FragmentationGuid { get; private set; }
        /// <summary>
        /// Packets remaining to complete the data
        /// </summary>
        public int Remaining { get; private set; }
        /// <summary>
        /// If true, all Packets has been added
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Adds the packet
        /// </summary>
        /// <param name="packet"></param>
        public bool Add(Packet packet)
        {
            if (!isInitialized)
            {
                isInitialized = true;
                this.Remaining = packet.PacketHeader.Fragmentation;
                this.FragmentationGuid = packet.PacketHeader.FragmentationGuid;
            }
            if(this.FragmentationGuid.Equals(packet.PacketHeader.FragmentationGuid))
            {
                this.Packets.Add(packet);
                this.Remaining--;
                if (this.Remaining == 0)
                    this.IsCompleted = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the content of all packets
        /// </summary>
        /// <returns></returns>
        public byte[] GetContent()
        {
            return this.Packets.OrderBy(p=>p.PacketHeader.Order).SelectMany(p => p.Content).ToArray();
        }

    }

}
