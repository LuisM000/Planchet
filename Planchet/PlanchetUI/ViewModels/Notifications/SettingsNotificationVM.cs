using Azure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.ViewModels.Notifications
{
    public class SettingsNotificationVM : NotificationBaseVM
    {
        public override Type NotificationType
        {
            get
            {
                return typeof(Settings);
            }
        }
    }
}
