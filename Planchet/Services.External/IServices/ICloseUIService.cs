using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.External.IServices
{
    /// <summary>
    /// In charge of opens close application UI
    /// </summary>
    public interface ICloseUIService
    {
        /// <summary>
        /// Opens close application UI.
        /// If the credentials are correct, kill the process
        /// </summary>
        /// <param name="sourceProcess">Process that calls this method</param>
        void Open(Process sourceProcess);
    }
}
