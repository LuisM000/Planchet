using Azure.Services.IServices;
using Azure.Services.Services;
using Databases;
using Databases.Factories;
using Databases.Initializers;
using Domain.Interfaces;
using Infrastructure.Factories;
using Infrastructure.IFactories;
using Infrastructure.IServices;
using Infrastructure.Services;
using Model.Entities;
using Model.Entities.Image;
using Model.Actions;
using Model.Settings;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using Repositories;
using Services.External.IServices;
using Services.External.Services;
using Services.Input.IServices;
using Services.Input.Services;
using Services.IServices;
using Services.Services;
using SharedComposer.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Socket;

namespace SharedComposer
{
    public class Composer : NinjectModule
    {
        public override void Load()
        {
            //Infrastructure
            this.Bind<IBitmapTransformerService>().To<BitmapTransformerService>();
            this.Bind<ISaverService>().To<SaverService>();
            this.Bind<IEncryptionService>().To<SHAEncryptionService>();
            this.Bind<IIdentifierMachineService>().To<IdentifierMachineService>().InSingletonScope();
            Bind<ICommandFactory>().ToFactory(() => new NameProvider()).WhenInjectedInto<CommandFactory>();
            Bind<ICommandFactory>().To<CommandFactory>();
            //Inject all commands
            this.Bind(x => x.FromAssembliesMatching("*Model.Actions*").SelectAllIncludingAbstractClasses().InheritedFrom<ICommand>()
               .BindAllBaseClasses().Configure((b, c) => b.InSingletonScope().Named(c.Name)));

            //Azure services
            this.Bind<IBusService>().To<BusService>().InSingletonScope();

            //Services
            this.Bind<IScreenshotService>().To<ScreenshotService>();
            this.Bind<IWebcamService>().To<WebcamService>().InTransientScope();
            this.Bind<IConfigurationService>().To<ConfigurationService>();
            this.Bind<ICredentialsService>().To<CredentialsService>();
            this.Bind<IPacketSender>().To<PacketSender>().InTransientScope();
            this.Bind<ISocketManagement>().To<SocketManagement>();

            //Services.External
            this.Bind<ICloseUIService>().To<CloseWindowsFormService>();
            this.Bind<INotifyIconService>().To<NotifyIconService>();
            this.Bind<INotificationUIService>().To<NotificationUIService>();

            //Services.Input
            this.Bind<IKeyboardService>().To<KeyboardService>();

            //Databases
            this.Bind<IFactoryDB>().To<FactoryMachineSQL>().InSingletonScope();
            this.Bind<IDatabaseInitializer<DataBaseSQL>>().To<UpdateInitializer>();
            //this.Bind<IDatabaseInitializer<DataBaseSQL>>().To<CreateDatabaseIfNotExists<DataBaseSQL>>().InSingletonScope();

            //Repositories
            this.Bind<IRepository<Time>>().To<BaseRepository<Time>>().InSingletonScope();
            this.Bind<IRepository<TransferTime>>().To<BaseRepository<TransferTime>>().InSingletonScope();
            this.Bind<IRepository<Screenshot>>().To<BaseRepository<Screenshot>>().InSingletonScope();
            this.Bind<IRepository<Webcam>>().To<BaseRepository<Webcam>>().InSingletonScope();
            this.Bind<IRepository<KeyboardInteraction>>().To<BaseRepository<KeyboardInteraction>>().InSingletonScope();
            this.Bind<IRepository<Credential>>().To<BaseRepository<Credential>>().InSingletonScope();
            this.Bind<IRepository<Interface>>().To<BaseRepository<Interface>>().InSingletonScope();
            this.Bind<IRepository<BusNotification>>().To<BaseRepository<BusNotification>>().InSingletonScope();
        }
    }
}
