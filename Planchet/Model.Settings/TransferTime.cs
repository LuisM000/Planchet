using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Settings
{
    /// <summary>
    /// Describes the transfer times of sending data
    /// </summary>
    public class TransferTime:Entity
    {
        /// <summary>
        /// Time sending data to the db
        /// </summary>
        public int SendKeyboardTime { get; set; }
    }
}
