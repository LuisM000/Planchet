using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    [DataContract(Namespace = "BusSocket")]
    public class Socket : NotificationBase
    {
        /// <summary>
        /// Descriptor of webcam socket
        /// </summary>
        [DataMember]
        public SocketDescriptor WebcamSender { get; set; }

        /// <summary>
        /// Descriptor of screenshot socket
        /// </summary>
        [DataMember]
        public SocketDescriptor ScreenshotSender { get; set; }

        
    }
}
