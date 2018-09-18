using Baron.Controller;
using CustomTools;
using Uniject;

namespace Baron  
{
    [GameObjectBoundary]
    public class Game : TestableComponent
    {

        private readonly IAppController _appController;

        public Game(TestableGameObject obj, IAppController appController) : base(obj)
        {
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