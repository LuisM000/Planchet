using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Services.IServices
{
    /// <summary>
    /// Service in charge of managing of bus notifications
    /// </summary>
    public interface IBusService
    {
        /// <summary>
        /// Initializes bus service
        /// </summary>
        void Initialize();

        /// <summary>
        /// Close bus service 
        /// </summary>
        void CancelBus();
        
        /// <summary>
        /// Event generated when there is a notification
        /// </summary>
        event EventHandler<NotificationBase> ReceivedData;

    }
}
