
using Baron.Service;
/*
  //History history = new History(gameBase);
	 
	 */
namespace Baron.Controller
{
    public class AppController : IAppController
    {
        private ILobbyController _lobbyController;

        public AppController(ILobbyController lobbyController, IDataLoader dataLoader, IHistoryManager historyManager)
        {
			GameBase gameBase = new GameBase();
			gameBase.Init(historyManager);

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
