using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    [DataContract(Namespace = "NotificationBase")]
    public class NotificationBase : EventArgs
    {
        /// <summary>
        /// Indicates the action associted to notification
        /// </summary>
        [DataMember]
        public virtual IList<Action> Actions { get; set; }
    }
}
