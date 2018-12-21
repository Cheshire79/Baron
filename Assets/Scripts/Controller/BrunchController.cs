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
			_branchViewController.ShowView();
			try
			{
				//	activity.getBranchLayout().setVisibility(View.VISIBLE);
				//	activity.getBranchIntroContainer().setVisibility(View.GONE);
				//	activity.getBlackCurtains().setVisibility(isBlackBackground ? View.VISIBLE : View.GONE);

				Scenario scenario = _gameBase.History.GetScenario();
				if (scenario.Cid == null)// new Scenario
				{
					Branch branch = GetStartBranch();//checked 10_09_18
					scenario = _scenarioManager.CreateScenario(_gameBase, branch.Cid);

					_gameBase.History.SetScenario(scenario);
				}
				if (_backgroundImageService != null && _gameBase.History.GetCurrentBackground() != null)
				{
					_backgroundImageService.Execute(scenario);
				}

				_applicationResumedListener.onReceive(false, this);

				//dispatch(Event.APPLICATION_RESUMED, false);
			//	Test(scenario);
			}
			catch (Exception e)
			{
				CustomLogger.Log("BrunchController Exc" + e.Message);
			}
		}

		private void Test(Scenario scenario)
		{
			foreach (var item in scenario.Branches)
			{
				CustomLogger.Log("Getted Branches " + item.Id + " " + item.OptionId);
			}

			TrackBranch trackBranch = scenario.CurrentTrackBranch;
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

			CustomLogger.Log("________________________ " + cid);
		}
		public Branch GetStartBranch()
		{

			History.History history = _gameBase.History;
			if (history == null) return null;


			Branch defaultBranch = history.InitialBranch;
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



		public Branch FindCurrentBranch(bool enabledOnly)
		{

			if (_gameBase == null)
				throw new ArgumentNullException("BranchController:  gameBase is null"); ;



			History.History history = _gameBase.History;
			if (history == null)
			{
				CustomLogger.Log("BrunchController  history == null");
				return null;
			}

			TrackBranch trackBranch = history.GetScenario().CurrentTrackBranch;
			if (trackBranch == null)
			{
				return history.InitialBranch;
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

