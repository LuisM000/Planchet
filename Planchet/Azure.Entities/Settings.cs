using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    [DataContract(Namespace = "BusSettings")]
    public class Settings : NotificationBase
    {
        [DataMember]
        public bool UpdateSettings { get; set; }

    }
}
