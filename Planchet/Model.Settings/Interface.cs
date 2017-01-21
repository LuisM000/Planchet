using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Settings
{
    /// <summary>
    /// Describes the characteristics of the interface
    /// </summary>
    public class Interface:Entity
    {
        /// <summary>
        /// Indicates if the icon is showed in the notification area
        /// </summary>
        public bool ShowNotifyIcon { get; set; }
    }
}
