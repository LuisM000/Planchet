using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    [DataContract(Namespace = "BusSocketDescriptor")]
    public class SocketDescriptor
    { 
        /// <summary>
        /// Restart socket
        /// </summary>
        [DataMember]
        public bool Restart { get; set; }

        /// <summary>
        /// If true, socket sender should be stopped
        /// </summary>
        [DataMember]
        public bool Stop { get; set; }

        /// <summary>
        /// IP of endPoint
        /// </summary>
        [DataMember]
        public string ServerIp { get; set; }

        /// <summary>
        /// Port of endPoint
        /// </summary>
        [DataMember]
        public int ServerPort { get; set; }

        /// <summary>
        /// Time (in milliseconds) between each send data 
        /// </summary>
        [DataMember]
        public int TimeRate { get; set; }

        /// <summary>
        /// Creates the end point from the ip and port
        /// </summary>
        /// <returns></returns>
        public IPEndPoint GetServerEndPoint()
        {
            IPEndPoint serverEndPoint = null;
            IPAddress serverIp = null;
            if (IPAddress.TryParse(this.ServerIp, out serverIp) && this.ServerPort != 0)
                serverEndPoint = new IPEndPoint(serverIp, this.ServerPort);
            
            return serverEndPoint;
        }  
    }
}
