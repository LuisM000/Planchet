using Domain.Interfaces;
using Model.Settings;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    /// <summary>
    /// Service in charge of provides, updates... configurations
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        internal class ConfigurationServiceConstants
        {
            public static Time DefaultTime = new Time() { ScreenshotTime = 120000, WebcamTime = 120000 };
            public static Interface DefaultInterface = new Interface() { ShowNotifyIcon = false };
            public static BusNotification DefaultBusNotification = new BusNotification() { IsActive = true };
        }
        IRepository<Time> repositoryTime;
        IRepository<Interface> repositoryInterface;
        IRepository<BusNotification> repositoryBusNotification;

        public ConfigurationService(IRepository<Time> repositoryTime, IRepository<Interface> repositoryInterface, IRepository<BusNotification> repositoryBusNotification)
        {
            this.repositoryTime = repositoryTime;
            this.repositoryInterface=repositoryInterface;
            this.repositoryBusNotification = repositoryBusNotification;
        }

        /// <summary>
        /// Returns current configuration of time
        /// </summary>
        /// <returns></returns>
        public Time GetConfigurationTime()
        {
            Time time = this.repositoryTime.GetAll().FirstOrDefault();
            if (time == null)
                time = ConfigurationServiceConstants.DefaultTime;
            return time;
        }
        /// <summary>
        /// Returns current configuration of interface (such as, if notification icon is visible)
        /// </summary>
        /// <returns></returns>
        public Interface GetConfigurationInterface()
        {
             Interface interfaceConfig=this.repositoryInterface.GetAll().FirstOrDefault();
             if (interfaceConfig == null)
                 interfaceConfig = ConfigurationServiceConstants.DefaultInterface;
             return interfaceConfig;
        }

        /// <summary>
        /// Returns current configuration of bus service
        /// </summary>
        /// <returns></returns>
        public BusNotification GetConfigurationBus()
        {
            BusNotification busNotificationConfig = this.repositoryBusNotification.GetAll().FirstOrDefault();
            if (busNotificationConfig == null)
                busNotificationConfig = ConfigurationServiceConstants.DefaultBusNotification;
            return busNotificationConfig;
        }
    }
}
