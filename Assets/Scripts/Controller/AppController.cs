
using Baron.Service;

namespace Baron.Controller
{
    public class AppController : IAppController
    {
        private ILobbyController _lobbyController;

        public AppController(ILobbyController lobbyController, IDataLoader dataLoader)
        {
			GameBase gameBase = new GameBase();
			History.History history= new History.History(gameBase);
			gameBase.History = history;
			dataLoader.LoadData(gameBase);
			
			


			 _lobbyController = lobbyController;
			_lobbyController.Init(gameBase);
			// ShowLobby();
			//dataLoader.TestLoadOptionJson();
			//dataLoader.TestLoadTreeJson();
			//dataLoader.TestLoadDataJson();
			//dataLoader.TestArrayJson();

			//dataLoader.ReadFromJson();
			//	dataLoader.TestArrayJson();
			ShowLobby();
		}

		private void ShowLobby()
        {
            _lobbyController.ShowView();
        }
    }
}
