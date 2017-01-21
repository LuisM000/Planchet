using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// In charge of mangement process
    /// </summary>
    public class ProcessService:IProcessService
    {
        /// <summary>
        /// Kills a process
        /// </summary>
        /// <param name="process"></param>
        public void Kill(Process process)
        {
            if (process != null)
                process.Kill();
        }
    }
}
