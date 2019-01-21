using Baron.Service;
using Baron.View;
using Baron.View.LobbyView;
using Barons.Controller;
using CustomTools;
using System;

namespace Baron.Controller
{
	public class LobbyController : BaseViewController, ILobbyController
	{
		private readonly ILobbyView _view;
		private readonly AbstractViewFactory _viewFactory;
		IBranchViewController _branchViewController;

		GameBase _gameBase;
		//private IDialogBoxWindowController _dialogBoxWindowChipsController;
		private BranchController _branchController;
		private BranchController BranchController
		{
			get
			{
				return _branchController ?? (_branchController = new BranchController(_gameBase, _branchViewController));
			}
		}
		private GameplayService _gameplayService;
		public LobbyController(IViewStack viewStack, AbstractViewFactory viewFactory, IBranchViewController branchViewController) : base(viewStack)
		{
			_viewFactory = viewFactory;
			_view = viewFactory.CreateLobbyView();
			_branchViewController = branchViewController;

			_view.BeginButtonClicked += Begin;
			_view.ContinueButtonClicked += Continue;
			
			//    _view.BackButtonClicked += ShowDialogBoxOnExit; todo
		}
		public void Init(GameBase gameBase)
		{
			_gameBase = gameBase;
			_gameplayService = new GameplayService(gameBase);
		}
		private void Begin()
		{
			CustomLogger.Log("LobbyController On Begin clicked");

			try
			{
				//boolean canShowConfirm = gameBase.hasHistory(); //todo adv

				//if (canShowConfirm)
				//{
				//	if (bannerManager != null)
				//		bannerManager.openNewStoryBanner(fragment);
				//}
				//else
				{

					StartNewGame();
				}
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}


			//BrunchController brunchController = new BrunchController(_gameBase, _branchViewController);



		}
		private void Continue()
		{
			//CustomLogger.Log("Continue clicked");
			//BrunchController brunchController = new BrunchController(_gameBase, _branchViewController);
			//_branchViewController.ShowView();
			//brunchController.StartGame(true);

			CustomLogger.Log("Continue clicked");
			try
			{
				//ntinueButton.setEnabled(false);
				PlayPlaySoundOnClick();
				
				//MenuSFXManager.m5(activity);
				//isableMenuButtons();
				_gameplayService.CleanUpBeforeContinueGame();
				//BrunchController.ShowView();
				BranchController.StartGame(true);
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
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
		public void StartNewGame()
		{
			try
			{
				//beginButton.setEnabled(false);
				PlayPlaySoundOnClick(); // just sound  &
				//	disableMenuButtons();
			
				//MainMenuButtonManager manager = new MainMenuButtonManager(this, handler);
				//manager.onBeginClicked(beginButton, new Runnable() {
				_gameplayService.NewGame();
				//	activity.moveToActivity(BranchActivity.class);
				//_branchViewController.ShowView();
				BranchController.StartGame(true);
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);			
			}
		}
		public void PlayPlaySoundOnClick()
		{
			try
			{

				//	isButtonClicked = true;

				//	AbstractActivity activity = (AbstractActivity)getActivity();
				//	if (activity == null) return;

				//	Presenter presenter = activity.getPresenter();
				//	final AudioService audioService = presenter.getAudioService();

				//	audioService.removeTrack(clickPlayer);
				//	clickPlayer = AudioService.getPlayer(activity, "sfx_tablet");
				//	clickPlayer.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
				//	@Override

				//	public void onCompletion(MediaPlayer mp)
				//	{
				//		audioService.removeTrack(clickPlayer);
				//	}
				//});

				//audioService.addTrack(clickPlayer);
				//clickPlayer.start();
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
		}
	}
}
