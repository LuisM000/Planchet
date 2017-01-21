using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Entities
{
    /// <summary>
    /// Describes an action (command)
    /// </summary>
    public class Action
    {
        public enum ActivationType { Instant, Accept, Cancel }

        /// <summary>
        /// Type of the action
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Instant at which the action is executed
        /// </summary>
        public ActivationType Activation { get; set; }
    }
}
