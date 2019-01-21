using Baron.Entity;
using Baron.Service;
using Baron.Tools;
using CustomTools;
using System;

namespace Baron.Controller
{
	public class BranchController
	{
		private GameBase _gameBase;
		private BranchScenarioManager _scenarioManager;
		//private BackgroundImageService _backgroundImageService;
		//private TrackService _trackService;
		//private ApplicationResumedListener _applicationResumedListener;
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
		public BranchController(GameBase gameBase, IBranchViewController branchViewController)
		{
			_gameBase = gameBase;
			var backgroundImageService = new BackgroundImageService(_gameBase);//, branchViewController);
			backgroundImageService.OnChangeImage += branchViewController.SetImage;
			backgroundImageService.OnShowMessage += branchViewController.UpdateDisplayedData;
			var backgroundAudioService = new BackgroundAudioService(_gameBase);
			backgroundAudioService.OnShowMessage += branchViewController.UpdateDisplayedData;
			var trackService = new TrackService(backgroundImageService, backgroundAudioService);

			_scenarioManager = new BranchScenarioManager(_gameBase, this, trackService, ScenarioCompleted);
			
			//backgroundAudioService = new BackgroundAudioService(activity);
			//_applicationResumedListener = new ApplicationResumedListener(_gameBase, _scenarioManager);

			_branchDecisionManager = new BranchDecisionManager(gameBase);
			_branchViewController = branchViewController;
			_branchViewController.Init(OnOptionClicked, OnClickedAnotherPosition, StartScroll);
			_scenarioManager.SetChangeSliderPosition(_branchViewController.SetSliderPosition);
		}
		public void StartGame(bool isBlackBackground)
		{
			CustomLogger.Log("BrunchController startGame");
			
			try
			{
				//	activity.getBranchLayout().setVisibility(View.VISIBLE);
				//	activity.getBranchIntroContainer().setVisibility(View.GONE);
				//	activity.getBlackCurtains().setVisibility(isBlackBackground ? View.VISIBLE : View.GONE);

				Scenario scenario = _gameBase.History.GetScenario();
				if (scenario.Cid == null)// this means that the  Scenario is new
				{
					Branch branch = _gameBase.GetCurrentSavedBranch();
					scenario = _scenarioManager.CreateScenario(_gameBase, branch.Cid);

					_gameBase.History.SetScenario(scenario);
				}
				// на всякий случай что бы быстрее подгрузилась картинка
				if ( _gameBase.History.GetCurrentBackground() != null) // вроде лишнее 
				{// по умолчанию показываю черный бекГраунд
				//	_backgroundImageService.Execute(scenario);
				}


				ResumedAplication();
				//_applicationResumedListener.onReceive(false, this);
				//dispatch(Event.APPLICATION_RESUMED, false);
				_branchViewController.ShowView();
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
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
			Branch currentBranch = _gameBase.FindCurrentBranch(false);

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
	

		private void OnOptionClicked(string cid)
		{
			_branchViewController.Reset();
			_gameBase.History.ActiveSave.ClickedBranches.Push(cid);

			Scenario scenario = _scenarioManager.CreateScenario(_gameBase, cid);

			_gameBase.History.SetScenario(scenario);

			////	GameplayService.resumeGameAndStartScenario();
			_scenarioManager.ResumeScenario();
		}

		//public void SetSliderPosition(int pos, int max)
		//{
		//	_branchViewController.SetSliderPosition(pos, max);
		//}
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
			//_applicationResumedListener.onReceive(false, this);
			ResumedAplication();
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

			// _trackService.Pause(); move into scenarioManager

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


		private void ResumedAplication()
		{
			GameBase.isPaused = false;
			// made buttons Disable here
			_gameBase.syncHistory();

			//if (InteractionFactory.hasInteractionStrategy())
			//{
			//	Log.w(tag, "resumeGameAndStartScenario is ignored due to interaction: "
			//			+ InteractionFactory.getCurrentInteractionType());
			//	return;
			//}
			_scenarioManager.ResumeScenario();
		}


		private void ScenarioCompleted()
		{
			CustomLogger.Log(" Scenario completed");
			try
			{
				//	final BranchPresenter presenter = BranchPresenter.getInstance();

				Branch branch = _gameBase.FindCurrentBranch(false);

				CustomLogger.Log("TrackCompletedListener Track completed for branch: " + " branch");

				//	BranchDecisionManager decisionManager = _branchController.BranchDecisionManager;

				//CompletedDecision decision = new CompletedDecision(//decisionManager,
				//	branch, this, _gameBase);
				//decision.Decide();

				CustomLogger.Log("CompletedDecision  Handle: " + branch);

				//if (!BranchPresenter.isCreated()) return;

				if (!CheckIfCanContinueGame(branch)) return;

				//BranchPresenter presenter = BranchPresenter.getInstance();

				//presenter.dispatch(Event.SHOW_BRANCHES);
				//_showBranchesListener.OnReceive(_gameBase, _branchController);


				try
				{
					CustomLogger.Log("ShowBranchesListener");

					//	BranchPresenter presenter = BranchPresenter.getInstance();
					try
					{
						//presenter.removeBranchOptions();
						//todo refesh view
					}
					catch (Exception e)
					{
						CustomLogger.LogException(e);
					}



					try
					{
						//presenter.dispatch(Event.APPLICATION_PAUSED);
						//todo pause
					}
					catch (Exception e)
					{
						CustomLogger.LogException(e);
					}


					try
					{


						_branchViewController.ShowLog(_gameBase);
						MainThreadRunner.AddTask(() =>
						_branchViewController.PlaceOptions(_gameBase));
						//FragmentManager fm = activity.getSupportFragmentManager();
						//Fragment interactionFragment = fm.findFragmentById(R.id.interaction_container);
						//if (interactionFragment != null)
						//{
						//	FragmentService.stop(activity, interactionFragment);
						//}
					}
					catch (Exception e)
					{
						CustomLogger.LogException(e);
					}

					//FragmentService.start(activity, R.id.branch_container, new BranchOptionsFragment());

				}
				catch (Exception e)
				{
					CustomLogger.LogException(e);
				}



				//presenter.syncHistory(); todo
				_gameBase.syncHistory();

			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
		}

		protected bool CheckIfCanContinueGame(Branch branch)
		{

			//	if (!BranchPresenter.isCreated()) return false;

			try
			{

				OptionParams @params;
				switch (branch.OptionId)
				{
					case Option.DEATH:
						GameBase.isFinaleReached = true;
						//NOTE OptionParams are ignored
						//						activity.redirectToDefeatActivity();
						CustomLogger.Log("Decision Final branch reached " + branch.OptionId);
						return false;
					case Option.VICTORY:

						GameBase.isFinaleReached = true;

						@params = branch.Params;

						if (@params != null)
						{
							//	if (handler != null)
							//		handler.postDelayed(new Runnable() {
							//		@Override

							//		public void run()
							//	{
							//		activity.redirectToVictoryActivity(params);
							//	}
							//}, params.getDelayX());
							CustomLogger.Log("Decision redirectToVictoryActivity ");
						}
						else
						{
							//activity.redirectToVictoryActivity();
						}
						CustomLogger.Log("Decision Final branch reached " + branch.OptionId);
						return false;

				}

			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}

			return true;
		}
	}


}

