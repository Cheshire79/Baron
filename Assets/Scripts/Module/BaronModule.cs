﻿using Baron.View.LobbyView;
using Baron.Controller;
using Baron.Service;
using Baron.View;
using Baron.View.BranchView;
using Ninject.Modules;
using Baron.Tools;

namespace Baron
{
    public class BaronModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<INetworkWorker>().To<NetworkWorker>().InSingletonScope()
            //    .WithConstructorArgument("host", AppConfig.ServerHost)
            //    .WithConstructorArgument("port", AppConfig.ServerPort);


            //Bind<IClient>().To<Client>().InSingletonScope()
            //    .WithConstructorArgument("devicePin", AppConfig.DeviceId)
            //    .WithConstructorArgument("appPlatform", AppConfig.OperatingSystem)
            //    .WithConstructorArgument("appVersion", AppConfig.Version);

        //    Bind<ILobby>().To<Lobby>().InSingletonScope();

            #region Context

        //    Bind<ILobbyContext>().To<LobbyContext>();

            #endregion

            #region Factory binds

            Bind<AbstractViewFactory>().To<ViewFactory>().InSingletonScope();
        //    Bind<IGameFactory>().To<GameFactory>().InSingletonScope();
        
            #endregion

            #region Controller

            Bind<IAppController>().To<AppController>().InSingletonScope();
            Bind<ILobbyController>().To<LobbyController>().InSingletonScope();
			Bind<IBranchViewController>().To<BranchViewController>().InSingletonScope();
			
	   //     Bind<IGameController>().To<GameController>().InSingletonScope();


			#endregion


			#region View binds

			Bind<IViewStack>().To<ViewStack>().InSingletonScope();
            Bind<ILobbyView>().To<LobbyView>().InSingletonScope();
			Bind<IBranchView>().To<BranchView>().InSingletonScope();
			Bind<IInstancesCache>().To<InstancesCache>();
			Bind<IOptionItem>().To<OptionItem>();
			

			#endregion

			#region Self Binding


			// Bind<UIAtlas>().ToProvider<ResourceProvider<UIAtlas>>().WhenTargetHas(typeof(Resource));

			#endregion

			#region Service
			Bind<IDataLoader>().To<DataLoader>().InSingletonScope();
			Bind<IHistoryManager>().To<HistoryManager>();//.InSingletonScope();
			



			#endregion
		}
	}
}