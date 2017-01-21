using Model.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// It is responsible for managing and transforming received data into packets
    /// </summary>
    public interface IPacketReceiver : IDisposable
    {
        /// <summary>
        /// Initializes and prepares the socket to receive packets
        /// </summary>
        /// <param name="localEP"></param>
        void Initialize(IPEndPoint localEP);

      
        /// <summary>
        /// Event generated when all packets with same identifier are received
        /// </summary>
        event EventHandler<ProcessedData> CompletePacket;
    }
}
