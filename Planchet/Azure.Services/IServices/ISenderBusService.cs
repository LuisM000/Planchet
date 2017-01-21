using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Services.IServices
{
    /// <summary>
    /// In charge of sending notifications
    /// </summary>
    public interface ISenderBusService
    {
        /// <summary>
        /// Returns all paths of queues
        /// </summary>
        /// <returns></returns>
        IList<string> GetQueues();

        /// <summary>
        /// Sends a bus notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        bool SendNotification(NotificationBase notification);
    }
}
