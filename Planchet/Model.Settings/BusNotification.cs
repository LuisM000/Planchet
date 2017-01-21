using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Settings
{
    /// <summary>
    /// Defines bus notification settings
    /// </summary>
    public class BusNotification:Entity
    {
        /// <summary>
        /// If true, receive and manage bus notifications
        /// </summary>
        public bool IsActive { get; set; }
    }
}
