using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of generating unique identifiers for each machine
    /// </summary>
    public interface IIdentifierMachineService
    {
        /// <summary>
        /// Returns identifier for this machine
        /// </summary>
        /// <returns></returns>
        string GetUniqueIdentifier();
    }
}
