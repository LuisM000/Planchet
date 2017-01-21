using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    [DataContract(Namespace = "BusMessage")]
    public class Message : NotificationBase
    {
        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
