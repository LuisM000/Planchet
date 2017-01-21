using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of management process
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        /// Kills a process
        /// </summary>
        /// <param name="process"></param>
        void Kill(Process process);
    }
}
