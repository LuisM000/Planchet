using Ninject;
using PlanchetUI.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlanchetUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.Compose();
            if (Application.Current != null)
                Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// In charge of app composition using the IOC container (ninject)
        /// </summary>
        private void Compose()
        {
            StandardKernel kernel = new StandardKernel(new PlanchetUIComposer());
            Application.Current.MainWindow = kernel.Get<MainWindow>();
        }
    }
}
