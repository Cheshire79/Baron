using Assets.Scripts.View;
using Baron.Service;
using Baron.View;
using Baron.View.LobbyView;
using Barons.Controller;
using CustomTools;


namespace Baron.Controller
{
	public class LobbyController : BaseViewController, ILobbyController
	{
		private readonly ILobbyView _view;
		private readonly AbstractViewFactory _viewFactory;
		IBranchViewController _branchViewController;

		GameBase _gameBase;
		//private IDialogBoxWindowController _dialogBoxWindowChipsController;

		public LobbyController(IViewStack viewStack, AbstractViewFactory viewFactory, IBranchViewController branchViewController) : base(viewStack)
		{
			_viewFactory = viewFactory;
			_view = viewFactory.CreateLobbyView();
			_branchViewController = branchViewController;

			_view.PlayNowButtonClicked += PlayNow;
			//    _view.BackButtonClicked += ShowDialogBoxOnExit; todo
		}
		public void Init(GameBase gameBase)
		{
			_gameBase = gameBase;
		}
		private void PlayNow()
		{
			CustomLogger.Log("Start clicked");
			BrunchController brunchController = new BrunchController(_gameBase, _branchViewController);
			_branchViewController.ShowView();
			brunchController.StartGame(true);
		}

		public override IView View
		{
			get { return _view; }
		}

		//todo implement
		private void CloseView()
		{
			//_appController.AccountControllerWrapper.OnClientLoggedOut();
			//_view.LoginWithFacebookClicked -= SuggestFriendClicked;
			//_view.PlayNowButtonClicked -= PlayNow;
			//// _view.OptionsButtonClicked -= ShowOptions;
			//_view.OptionsButtonClicked -= SoundEnableChanged;
			//_view.HelpButtonClicked -= ShowHelp;
			//_view.ProfileButtonClicked -= ShowProfile;
			//_view.HoldemTablesButtonClicked -= ExecPlayHoldemTables;
			//_view.BuyChipsButtonClicked -= CheckAndShowInAppPurchasing;
			//DialogBoxWindowController.Close();// if we close this view because of network failed; we need to close DialogBox
			//_view.Close();
		}
		public void CloseAllViews()
		{
			//HoldemTableWindowController.CloseFromAplication();

		}
	}
}
