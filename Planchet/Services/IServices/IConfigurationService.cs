using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// Service in charge of provides, updates... configurations
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Returns current configuration of time
        /// </summary>
        /// <returns></returns>
        Time GetConfigurationTime();
        /// <summary>
        /// Returns current configuration of interface (such as, if notification icon is visible)
        /// </summary>
        /// <returns></returns>
        Interface GetConfigurationInterface();

        /// <summary>
        /// Returns current configuration of bus service
        /// </summary>
        /// <returns></returns>
        BusNotification GetConfigurationBus();
    }
}
