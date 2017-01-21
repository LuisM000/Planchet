using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// In charge of coordinate the sending of information through sockets
    /// </summary>
    public interface ISocketManagement
    {
        /// <summary>
        /// Updates the properties of sockets
        /// </summary>
        /// <param name="socket"></param>
        void Update(Socket socket);
    }
}

