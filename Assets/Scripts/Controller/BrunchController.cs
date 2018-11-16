using Baron.Entity;
using Baron.Listener;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Controller
{
	public class BrunchController
	{
		private GameBase _gameBase;
		private BranchScenarioManager _scenarioManager;
		private BackgroundImageService _backgroundImageService;
		private TrackService _trackService;
		private ApplicationResumedListener _applicationResumedListener;
		private BranchDecisionManager _branchDecisionManager;

		public BranchDecisionManager BranchDecisionManager
		{
			get { return _branchDecisionManager; }
		}

		IBranchViewController _branchViewController;

		public IBranchViewController BranchViewController
		{
			get { return _branchViewController; }
		}
		public BrunchController(GameBase gameBase, IBranchViewController branchViewController)
		{
			_gameBase = gameBase;
			_backgroundImageService = new BackgroundImageService(_gameBase, branchViewController);
			_trackService = new TrackService(_backgroundImageService, new BackgroundAudioService(_gameBase, branchViewController));

			_scenarioManager = new BranchScenarioManager(_gameBase, this, _trackService);
			//backgroundAudioService = new BackgroundAudioService(activity);
			_applicationResumedListener = new ApplicationResumedListener(_gameBase, _scenarioManager);

			_branchDecisionManager = new BranchDecisionManager(gameBase);
			_branchViewController = branchViewController;
			_branchViewController.Init(OnOptionClicked, OnClickedAnotherPosition, StartScroll);

		}
		public void StartGame(bool isBlackBackground)
		{
			CustomLogger.Log("BrunchController startGame");

			try
			{
				//	if (playerInteraction != null)
				//		playerInteraction.build(null);

				//	updateCurrentBranch();

				//	activity.getBranchLayout().setVisibility(View.VISIBLE);
				//	activity.getBranchIntroContainer().setVisibility(View.GONE);
				//	activity.getBlackCurtains().setVisibility(isBlackBackground ? View.VISIBLE : View.GONE);

				//	if (overlayManager == null)
				//	{
				//		overlayManager = new OverlayManager(activity);
				//	}

				//	overlayManager.start();

				//onHistoryAvailable(new OnHistoryAvailable() {
				//@Override

				//public void onSuccess(History history)
				//{
				//	History.History history = _gameBase.GetHistory();
				Scenario scenario = new Scenario();
				if (scenario.Cid == null)
				{
					Branch branch = GetStartBranch();//checked 10_09_18
					scenario = _scenarioManager.CreateScenario(_gameBase, branch.Cid);

					_gameBase.History.SetScenario(scenario);

					if (_backgroundImageService != null && _gameBase.History.GetCurrentBackground() != null)
					{
						_backgroundImageService.Resume(scenario);
					}


					// dispatch(Event.APPLICATION_RESUMED, false); next
					// finished origin code
					_applicationResumedListener.onReceive(false, this);

					foreach (var item in scenario.Branches)
					{
						CustomLogger.Log("Getted Branches " + item.Id + " " + item.OptionId);

					}


					//
					TrackBranch trackBranch = scenario.CurrentBranch;
					if (trackBranch == null) return;
					Option option = OptionRepository.Find(_gameBase, trackBranch.OptionId);

					foreach (var item in option.TrackImages)
					{
						CustomLogger.Log("Getted Branches " + item.Id + " " + item.Duration);

					}
					Branch currentBranch = FindCurrentBranch(false);

					//HashSet<string> uniqueItems = new HashSet<string>();
					//InventoryBranch currentInventoryBranch=TreeParser.FindInventoryBranch(_gameBase, currentBranch, uniqueItems);
					string cid = "";
					foreach (var item in currentBranch.InventoryBranches)
					{
						foreach (var item1 in item.Branches)
						{
							CustomLogger.Log("Getted inner  branches " + item1.OptionId);
							cid = item1.Cid;
						}

					}
					//	InventoryBranch currentInventoryBranch = currentBranch.InventoryBranches;
					//	foreach (var item in currentInventoryBranch.Branches)
					//{
					//	CustomLogger.Log("Getted inner  branches " + item.OptionId );

					//}

					CustomLogger.Log("________________________ " + cid);


					//scenario = _scenarioManager.CreateScenario(_gameBase, cid);

					//_gameBase.History.SetScenario(scenario);
					//foreach (var item in scenario.Branches)
					//{
					//	CustomLogger.Log(" 2        Getted Branches " + item.Id + " " + item.OptionId);
					//}

					// trackBranch = scenario.CurrentBranch;
					//if (trackBranch == null) return;
					// option = OptionRepository.Find(_gameBase, trackBranch.OptionId);

					//foreach (var item in option.TrackImages)
					//{
					//	CustomLogger.Log("2        Getted Branches " + item.Id + " " + item.Duration);

					//}

				}

				//NOTE Restore container
				//	if (backgroundImageService != null && history.getCurrentBackground() != null)
				//	{
				//		backgroundImageService.resume(scenario);
				//	}
				//}

				//	@Override

				//	public void onError()
				//	{

				//	}
				//});

				//dispatch(Event.APPLICATION_RESUMED, false);

			}
			catch (Exception e)
			{
				CustomLogger.Log("BrunchController Exc" + e.Message);
			}
		}
		public Branch GetStartBranch()
		{

			History.History history = _gameBase.History;
			if (history == null) return null;

			UpdateInitialBranch(); // just create initial brunch

			Branch defaultBranch = history.GetInitialBranch();
			Branch currentBranch;
			try
			{
				currentBranch = FindCurrentBranch(false);
				if (currentBranch == null)
				{
					currentBranch = defaultBranch;
				}

			}
			catch (Exception e)
			{
				CustomLogger.Log("BrunchController Exc" + e.Message);
				currentBranch = defaultBranch;
			}

			return currentBranch;
		}

		public void UpdateInitialBranch()
		{
			if (_gameBase == null)
			{
				CustomLogger.Log("BrunchController  _gameBase == null");
				return;
			}

			History.History history = _gameBase.History;
			if (history == null)
			{
				CustomLogger.Log("BrunchController  history == null");
				return;
			}

			history.UpdateInitialBranch();
		}

		public Branch FindCurrentBranch(bool enabledOnly)
		{

			if (_gameBase == null) return null;

			UpdateInitialBranch();

			History.History history = _gameBase.History;
			if (history == null)
			{
				CustomLogger.Log("BrunchController  history == null");
				return null;
			}

			TrackBranch trackBranch = history.GetScenario().CurrentBranch;
			if (trackBranch == null)
			{
				return history.GetInitialBranch();
			}

			string id = trackBranch.Id;

			if (enabledOnly)
			{
				//HashSet<String> disabledBranches = history.getDisabledBranchCID();
				//if (disabledBranches.Contains(id))
				//{
				//	CustomLogger.Log(" BrunchController Cid " + id + " is disabled");
				//	return null;
				//}
			}

			return TreeParser.FindBranchByCid(_gameBase, id);
		}

		public Option FindCurrentOption(bool enabledOnly)
		{
			try
			{
				History.History history = _gameBase.History;
				if (history == null) return null;

				Branch currentBranch = FindCurrentBranch(enabledOnly);
				if (currentBranch == null) return null;

				Option currentOption = OptionRepository.Find(_gameBase, currentBranch.OptionId);
				if (currentOption == null) return null;

				if (enabledOnly)
				{
					if (history.GetDisabledOptions().Contains(currentOption.Id))
					{
						CustomLogger.Log("BrunchController Option " + currentOption.Id + " is disabled");
						return null;
					}
				}
				return currentOption;
			}
			catch (Exception e)
			{
				CustomLogger.Log("BrunchController Exc" + e.Message);
			}
			return null;
		}
		private void OnOptionClicked(string cid)
		{
			_branchViewController.Reset();
			_gameBase.History.ActiveSave.ClickedBranches.Push(cid);

			Scenario scenario = _scenarioManager.CreateScenario(_gameBase, cid);

			_gameBase.History.SetScenario(scenario);

			////	GameplayService.resumeGameAndStartScenario();
			_scenarioManager.ResumeScenario();
		}

		public void SetSliderPosition(int pos, int max)
		{
			_branchViewController.SetSliderPosition(pos, max);
		}
		private void OnClickedAnotherPosition(float progress)
		{
			StartScroll();
			var scenario = _gameBase.History.GetScenario();
			_scenarioManager.UpdateScenario(_gameBase,
							scenario, (int)progress, scenario.Duration);

			StopScroll();
		}
		//====================================
		private void StopScroll()
		{
			_applicationResumedListener.onReceive(false, this);
		}

		public void StartScroll()
		{
			PauseGame();
			//presenter.dispatch(new Intent(Event.APPLICATION_PAUSED));

			// call public void TPause()

			/*
						final MediaPlayer player = AudioService.getPlayer(presenter.getActivity(), "sfx_audio_scroll_start");
						player.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
							@Override

							public void onCompletion(MediaPlayer mp)
						{
							audioService.removeTrack(player);
						}
					});

						audioService.addTrack(player);
						player.start();

						audioService.removeTrack(scrollPlayer);
						scrollPlayer = AudioService.getPlayer(presenter.getActivity(), "sfx_audio_scroll_end");
						scrollPlayer.setLooping(true);

						audioService.addTrack(scrollPlayer);
						scrollPlayer.start();
						*/
		}

		public void PauseGame()
		{

			CustomLogger.Log("BrunchController pauseGame");

			GameBase.isPaused = true;


			//	TransitionFactory.stop();

			_trackService.Pause();

			_scenarioManager.StopProgressBar();
			//	presenter.getPlayerFragment().toggleControls();

			//TrackBranch trackBranch = history.getScenario().currentBranch;
			//if (trackBranch != null)
			//{
			//	trackBranch.isLocked = false;
			//}

			//Option option = presenter.findCurrentOption(false);
			//if (option != null)
			//{
			//	if (option.currentAudio != null)
			//	{
			//		option.currentAudio.isLocked = false;
			//	}
			//	if (option.currentImage != null)
			//	{
			//		option.currentImage.isLocked = false;
			//	}
			//}
		}


	}


}
	
