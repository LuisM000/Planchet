using Azure.Entities;
using Azure.Services.IServices;
using PlanchetUI.IServices;
using PlanchetUI.ViewModels.Base;
using PlanchetUI.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanchetUI.ViewModels
{
    public class NotificationsVM: MainViewModel
    {
        #region Attributes
        IBaseViewModelFactory baseViewModelFactory;
        ISenderBusService busService;
        #endregion

        #region Properties

        public NotificationBaseVM CurrentNotificationViewModel { get; set; }
        
        public ObservableCollection<Type> NotificationTypes
        {
           get
            {
                return new ObservableCollection<Type>(Assembly.GetExecutingAssembly().GetTypes().
                    Where(t => String.Equals(t.Namespace, typeof(NotificationBaseVM).Namespace, StringComparison.Ordinal)));
               
            }
        }

        #endregion

        #region Builder
        public NotificationsVM(IBaseViewModelFactory baseViewModelFactory, ISenderBusService busService)
        {
            this.baseViewModelFactory = baseViewModelFactory;
            this.busService = busService;
        }
        #endregion

        #region Commands

        public ICommand ChangeView
        {
            get
            {
                return new BaseCommand<Type>((viewModelType) =>
                {
                    CurrentNotificationViewModel = (NotificationBaseVM)this.baseViewModelFactory.GetBaseViewModel(viewModelType.Name);
                    OnPropertyChanged(() => CurrentNotificationViewModel);
                    CurrentNotificationViewModel.Initialize();
                });
            }
        }

        public ICommand SendNotificationCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    if (this.CurrentNotificationViewModel != null && this.CurrentNotificationViewModel.NotificationBase != null)
                    {
                        this.busService.SendNotification(this.CurrentNotificationViewModel.NotificationBase);
                    }
                });
            }
        }
       
        #endregion

        #region Public methods
        #endregion
    }
}
