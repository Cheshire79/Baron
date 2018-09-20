
using Baron.Service;

namespace Baron.Controller
{
    public class AppController : IAppController
    {
        private ILobbyController _lobbyController;

        public AppController(ILobbyController lobbyController, IDataLoader dataLoader)
        {
            _lobbyController = lobbyController;
			// ShowLobby();
			//dataLoader.TestLoadOptionJson();
			dataLoader.TestLoadTreeJson();
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
