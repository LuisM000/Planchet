using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// In charge of managing data to send
    /// </summary>
    public interface IPacketSender: IDisposable
    {
        /// <summary>
        /// Initializes and prepares the socket to send packets
        /// </summary>
        /// <param name="localEP"></param>
        void Initialize(IPEndPoint remoteEP);

        /// <summary>
        /// Sends data to remote socket. Blocks thread until all data is sent.
        /// The data are sent synchronously
        /// </summary>
        /// <param name="data"></param>
        /// <param name="typeData"></param>
        /// <returns></returns>
        void SendData(byte[] data, Type typeData = null);
    }
}
