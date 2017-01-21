using Azure.Entities;
using Model.Actions;
using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanchetUI.ViewModels.Notifications
{
    public class NotificationBaseVM : MainViewModel
    {
        private NotificationBase notificationBase;

        public virtual NotificationBase NotificationBase
        {
            get 
            {
                return notificationBase; 
            }
            set { notificationBase = value; OnPropertyChanged(() => NotificationBase); }
        }

        public virtual Type NotificationType
        {
            get
            {
                return typeof(NotificationBase);
            }
        }

        private Azure.Entities.Action currentAction;

        public Azure.Entities.Action CurrentAction
        {
            get 
            {
                if (currentAction == null)
                    currentAction = new Azure.Entities.Action()
                    {
                        Activation = Azure.Entities.Action.ActivationType.Instant,
                        ActionType = typeof(ScreenshotCommand).Name
                    };
                return currentAction;
            }
            set { currentAction = value; OnPropertyChanged(() => CurrentAction); }
        }

        public ObservableCollection<Type> ActionTypes
        {
            get
            {
                return new ObservableCollection<Type>(Assembly.GetAssembly(typeof(ScreenshotCommand)).GetTypes());

            }
        }

        public ObservableCollection<Azure.Entities.Action.ActivationType> ActivationTypes
        {
            get
            {
                return new ObservableCollection<Azure.Entities.Action.ActivationType>(
                    Enum.GetValues(typeof(Azure.Entities.Action.ActivationType)).Cast<Azure.Entities.Action.ActivationType>()
                    );
            }
        }

        public ICommand AddActionCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    if(this.NotificationBase.Actions==null)
                        this.NotificationBase.Actions = new List<Azure.Entities.Action>();
                    
                    //ToDo: use automapper
                    var currentActions = this.NotificationBase.Actions;
                    currentActions.Add(this.CurrentAction);
                    List<Azure.Entities.Action> auxActions = new List<Azure.Entities.Action>();
                    foreach (var action in currentActions)
                    {
                        auxActions.Add(new Azure.Entities.Action() { ActionType = action.ActionType, Activation = action.Activation });
                    }
                    this.NotificationBase.Actions = new List<Azure.Entities.Action>(auxActions);
                    OnPropertyChanged(() => NotificationBase);
                });
            }
        }
        public ICommand RemoveActionCommand
        {
            get
            {
                return new BaseCommand<Azure.Entities.Action>((selectedAction) =>
                {
                    if(this.NotificationBase.Actions!=null)
                    {
                        this.NotificationBase.Actions.Remove(selectedAction);
                        //ToDo: use automapper
                        var currentActions = this.NotificationBase.Actions;
                        List<Azure.Entities.Action> auxActions = new List<Azure.Entities.Action>();
                        foreach (var action in currentActions)
                        {
                            auxActions.Add(new Azure.Entities.Action() { ActionType = action.ActionType, Activation = action.Activation });
                        }
                        this.NotificationBase.Actions = new List<Azure.Entities.Action>(auxActions);
                        OnPropertyChanged(() => NotificationBase);
                    }
                });
            }
        }


        public ICommand RemoveAllActionsCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.NotificationBase.Actions = new List<Azure.Entities.Action>();
                    OnPropertyChanged(() => NotificationBase);
                });
            }
        }
    }
}
