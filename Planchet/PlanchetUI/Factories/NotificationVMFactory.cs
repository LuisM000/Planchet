using Azure.Entities;
using PlanchetUI.IServices;
using PlanchetUI.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.Factories
{
    /// <summary>
    /// Factory to create NotificationVMs
    /// </summary>
    public class NotificationVMFactory : IBaseViewModelFactory
    {
         IBaseViewModelFactory baseViewModelFactory;
         public NotificationVMFactory(IBaseViewModelFactory commandFactory)
        {
            this.baseViewModelFactory = commandFactory;
        }
        public ViewModels.MainViewModel GetBaseViewModel(string name)
        {
            NotificationBaseVM vm = (NotificationBaseVM)baseViewModelFactory.GetBaseViewModel(name);
            vm.NotificationBase = (NotificationBase)Activator.CreateInstance(vm.NotificationType);
            return vm;
        }
    }
}
