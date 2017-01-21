using Ninject.Modules;
using PlanchetUI.IServices;
using SharedComposer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using SharedComposer.Providers;
using PlanchetUI.ViewModels;
using Databases.Factories;
using Azure.Services.IServices;
using Azure.Services.Services;
using Services.IServices;
using Services.Services;
using PlanchetUI.Services;
using PlanchetUI.Factories;

namespace PlanchetUI
{
    public class PlanchetUIComposer : Composer
    {
        public override void Load()
        {
            base.Load();
            //Database
            this.Unbind<IFactoryDB>();
            this.Bind<IFactoryDB>().To<FactorySQL>().InSingletonScope();

            //Services
            this.Bind<ISenderBusService>().To<SenderBusService>().InSingletonScope();
            this.Bind<IPacketReceiver>().To<PacketReceiver>().InSingletonScope();
            
            //Services UI
            this.Bind<IImageViewer>().To<ImageViewer>().InSingletonScope();
            this.Bind<ISQLService>().To<SQLServerService>().InSingletonScope();

            //UI bindings
            Bind<IBaseViewModelFactory>().ToFactory(() => new NameProvider());
            Bind<IBaseViewModelFactory>().To<NotificationVMFactory>().WhenInjectedInto<NotificationsVM>();

            this.Bind(x => x.FromAssembliesMatching("*PlanchetUI*").SelectAllIncludingAbstractClasses().InheritedFrom<MainViewModel>()
                .BindAllBaseClasses().Configure((b, c) => b.InSingletonScope().Named(c.Name)));
        }
    }
}
