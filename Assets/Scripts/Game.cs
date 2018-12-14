using Baron.Controller;
using CustomTools;
using Uniject;

namespace Baron  
{
    [GameObjectBoundary]
    public class Game : TestableComponent
	{
		//Script - InjectionRoot
		//  public string TypeToInstantiate =Baron.Game

		//FBRLogger -UnityLogToFBR
		//public string LogViewerHost = 127.0.0.2

		//LobbyChildernReference
		// ButtonAdv StartButtonGUI
		private readonly IAppController _appController;

        public Game(TestableGameObject obj, IAppController appController) : base(obj)
        {
		//	string _dataPath = Application.persistentDataPath;
			_appController = appController;
            CustomLogger.Log("App Started");
            //   Statistics.OnStartSession();
            //  Preferences.Init();
            //   _appController.Connect();
        }

        public override void OnApplicationQuit()
        {
            //  Statistics.OnEndSession();
            //  _appController.Disconnect();
        }
    }
}