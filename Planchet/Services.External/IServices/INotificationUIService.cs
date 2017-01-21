using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.External.IServices
{
    /// <summary>
    /// In charge of opens notification UI
    /// </summary>
    public interface INotificationUIService
    {
        /// <summary>
        /// Opens message notification UI.
        /// </summary>
        /// <param name="message"></param>
        void OpenMessage(Message message);
    }
}
