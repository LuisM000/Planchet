using Azure.Services.IServices;
using PlanchetUI.Entities;
using PlanchetUI.IServices;
using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlanchetUI.ViewModels
{
    public class SettingsVM : MainViewModel
    {
        ISenderBusService busService;
        ISQLService sqlServerService;

        public string EndPoint
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Endpoint"].ToString(); }
        }


        private string selectedQueue;
        public string SelectedQueue
        {
            get 
            { 
                if(String.IsNullOrEmpty(selectedQueue))
                    selectedQueue = System.Configuration.ConfigurationManager.AppSettings["Queue"].ToString();
                return selectedQueue;
            }
            set
            {
                selectedQueue = value;
                OnPropertyChanged(() => SelectedQueue);
            }
        }

        private ObservableCollection<string> queues;
        public ObservableCollection<string> Queues
        {
            get { return queues; }
            set { queues = value; OnPropertyChanged(() => Queues); }
        }
     
        private Connection connection;

        public Connection Connection
        {
            get 
            {
                if (connection == null)
                    connection = Connection.GetConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabasePlanchet"].ToString()); 
                return connection; 
            }
            set 
            { 
                connection = value; 
                OnPropertyChanged(() => Connection);
            }
        }

       

        private ObservableCollection<string> databases;

        public ObservableCollection<string> Databases
        {
            get { return databases; }
            set { databases = value; OnPropertyChanged(() => Databases); }
        }

        public SettingsVM(ISenderBusService busService, ISQLService sqlServerService)
        {
            this.busService = busService;
            this.sqlServerService = sqlServerService;
        }


        public ICommand LoadQueuesCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.Queues=new ObservableCollection<string>(this.busService.GetQueues());
                });
            }
        }
        public ICommand LoadDatabasesCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.Databases=new ObservableCollection<string>(this.sqlServerService.GetDatabases(this.Connection.ConnectionString));
                });
            }
        }

        public ICommand SaveSettingsCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    this.SaveAppSettings("Queue", this.SelectedQueue);
                    this.SaveConnectionString("DatabasePlanchet", this.Connection.ConnectionString);
                });
            }
        }
        
        private void SaveAppSettings(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();
        }

        private void SaveConnectionString(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            configuration.Save();
        }

       
    }
}
