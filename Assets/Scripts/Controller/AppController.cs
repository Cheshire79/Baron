
using Baron.Service;

namespace Baron.Controller
{
    public class AppController : IAppController
    {
        private ILobbyController _lobbyController;

        public AppController(ILobbyController lobbyController, IDataLoader dataLoader, IBranchViewController branchViewController)
        {
			GameBase gameBase = new GameBase();
			History.History history= new History.History(gameBase);
			gameBase.History = history;
			dataLoader.LoadData(gameBase);
			
			BrunchController brunchController = new BrunchController(gameBase, branchViewController);
			brunchController.StartGame(true);


					   _lobbyController = lobbyController;
			// ShowLobby();
			//dataLoader.TestLoadOptionJson();
			//dataLoader.TestLoadTreeJson();
			//dataLoader.TestLoadDataJson();
			//dataLoader.TestArrayJson();

			//dataLoader.ReadFromJson();
			//	dataLoader.TestArrayJson();
		}

		private void ShowLobby()
        {
            _lobbyController.ShowView();
        }
    }
}
