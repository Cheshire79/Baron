
using Baron.Service;

namespace Baron.Controller
{
    public class AppController : IAppController
    {
        private ILobbyController _lobbyController;

        public AppController(ILobbyController lobbyController, IDataLoader dataLoader)
        {
            _lobbyController = lobbyController;
            ShowLobby();
            //dataLoader.TestJson();
            dataLoader.ReadFromJson();
        }

        private void ShowLobby()
        {
            _lobbyController.ShowView();
        }
    }
}
