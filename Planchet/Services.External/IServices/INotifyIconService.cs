using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.External.IServices
{
    /// <summary>
    /// Service in charge of create notify icon to close application
    /// </summary>
    public interface INotifyIconService
    {
        /// <summary>
        /// Creates notify icon
        /// </summary>
        /// <param name="sourceProcess">Process that calls this method</param>
        void CreateNotifyIcon(Process sourceProcess);
    }
}
