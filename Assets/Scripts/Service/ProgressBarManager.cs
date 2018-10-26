using Baron.Entity;
using CustomTools;
using System;

namespace Baron.Service
{
	public class ProgressBarManager
	{
		private int _delay = 50;
		private bool _isSpecialAudioEnabled;
		GameBase _gameBase;
		private long _lastUpdatedAt;
		BranchScenarioManager _scenarioManager;
		//	private BackgroundImageService _backgroundImageService;
		private readonly System.Timers.Timer _scheduledTask = new System.Timers.Timer(3);

		private bool _isRunnig = false;

		private readonly TrackService _trackService;

		public ProgressBarManager(GameBase gameBase, BranchScenarioManager scenarioManager, TrackService trackService)
		{
			_gameBase = gameBase;
			_scenarioManager = scenarioManager;
			//_backgroundImageService = bgImServ;
			_trackService = trackService;


			_scheduledTask.Elapsed += (sender, args) => Runnable();
		}
		public void Start(Scenario scenario)
		{
			//BranchPresenter presenter = BranchPresenter.getInstance();
			//GameBase gameBase = presenter.getGameBase();
			//	BranchScenarioManager scenarioManager = presenter.getScenarioManager();

			int progress = scenario.Progress;
			int max = scenario.Duration;

			_isSpecialAudioEnabled = false;

			Stop();

			//setUIProgress(progress, max);

			_scenarioManager.ResetScenario(_gameBase, scenario);

			_scenarioManager.UpdateScenario(_gameBase, scenario, progress, max);

			CustomLogger.Log(" ProgressBarManager start " + progress + "/" + max);

			//	GameplayService.resumeGame();// never mind

			if (max == 0)
			{

				//setUIProgress(50, 50);

				Finish();

				return;
			}
			if (max < 0)
			{

				//setUIProgress(50, 50);

				Finish();

				return;
			}

			//scheduler = Executors.newScheduledThreadPool(1);

			//presenter.getBackgroundAudioService().preloadScenarioAudio(scenario, new Runnable() {


			//public void run()
			//{
			//	scheduledTask = scheduler.scheduleAtFixedRate(task, 0, delay, TimeUnit.MILLISECONDS);
			//}
			_trackService.PreloadScenarioAudio(scenario);
			_scheduledTask.Start();
		}


		public void Stop()
		{
			//if (!BranchPresenter.isCreated()) return;

			CustomLogger.Log(" ProgressBarManager stop");
			try
			{

				if (_scheduledTask != null)
				{
					_scheduledTask.Stop();
					//	scheduledTask.cancel(false);
					//	scheduledTask = null;
					//}
				}
				_lastUpdatedAt = 0;
			}
			catch (Exception e)
			{
				CustomLogger.Log("ProgressBarManager Exc" + e.Message);
			}
		}

		private void Finish()
		{
			// if (!BranchPresenter.isCreated()) return;

			CustomLogger.Log(" ProgressBarManager finish");

			try
			{
				//	final BranchPresenter presenter = BranchPresenter.getInstance();
				//	final BranchScenarioManager scenarioManager = presenter.getScenarioManager();

				//	presenter.getHandler().post(audioTask);

				//	presenter.hideLoadingIcon();

				//	presenter.onHistoryAvailable(new Presenter.OnHistoryAvailable() {
				//	@Override

				//	public void onSuccess(History history)
				{
					Scenario scenario = _gameBase.History.GetScenario();

					if (!scenario.IsValid())
					{
						//presenter.getActivity().redirectToDefeatActivity();
						CustomLogger.Log("ProgressBarManager Defeat");
						return;
					}

					int progress = scenario.Progress;
					int max = scenario.Duration;

					//setUIProgress(scenario.duration, scenario.duration);

					Stop();

					CustomLogger.Log(" Track is completed: " + progress + "/" + max + " ms");

					if (scenario.CurrentBranch.IsFinal)
					{
						_scenarioManager.OnFinaleReached(scenario);
					}
					else if (scenario.CurrentBranch.IsInteraction)
					{
						_scenarioManager.OnInteractionReached(scenario);
					}
					else
					{

						if (scenario.CurrentBranch.IsBeforeNewScenario)
						{
							_scenarioManager.OnNewScenarioReached(scenario);
						}
						else
						{
							_scenarioManager.OnScenarioCompleted();
						}
					}
				}

			}

			catch (Exception e)
			{
				CustomLogger.Log("ProgressBarManager Exc" + e.Message);
			}
		}

		private void Runnable()
		{
			if (!_isRunnig)
			{
				_isRunnig = true;
				float delta = getDelayFromLastRun();

				//if (!BranchPresenter.isCreated()) return;

				//	final BranchPresenter presenter = BranchPresenter.getInstance();

				//presenter.onHistoryAvailable(new Presenter.OnHistoryAvailable() {
				//		@Override

				//		public void onSuccess(final History history)
				//	{

				Scenario scenario = _gameBase.History.GetScenario();

				UpdateScenario(scenario, delta);
				_isRunnig = false;
			}
		}

		private long getDelayFromLastRun()
		{
			if (_lastUpdatedAt > 0)
			{
				int delta = Math.Max(_delay, (int)(DateTime.Now.Millisecond - _lastUpdatedAt));
				return (_delay + delta) / 2;
			}

			return 0;
		}

		public void UpdateScenario(Scenario scenario, float delta)
		{
			//final BranchPresenter presenter = BranchPresenter.getInstance();
			//final BranchScenarioManager scenarioManager = presenter.getScenarioManager();
			//final GameBase gameBase = presenter.getGameBase();
			//final BranchActivity activity = presenter.getActivity();
			//Handler handler = presenter.getHandler();

			bool isFirstTrackItem = delta == 0;
			int progress = (int)(scenario.Progress + delta);

			bool hasBranchChanged = _scenarioManager.UpdateScenario(_gameBase,
					scenario, progress, scenario.Duration);

			//updatePlayerFragment(); Update graphic

			if (scenario.IsCompleted)
			{

				_scenarioManager.OnBranchCompleted(scenario.CurrentBranch);

				Finish();

				return;
			}

			if (isFirstTrackItem)
			{
				try
				{
					_scenarioManager.OnBranchStarted(scenario.CurrentBranch);
				}
				catch (Exception e)
				{
					CustomLogger.Log("ProgressBarManager Exc" + e.Message);
					Stop();
					scenario.Unlock();
					//presenter.syncHistory(); todo

					return;
				}
			}
			else if (hasBranchChanged)
			{

				TrackBranch prev = (TrackBranch)scenario.CurrentBranch.Previous;
				_scenarioManager.OnBranchCompleted(prev);

				try
				{
					_scenarioManager.OnBranchStarted(scenario.CurrentBranch);
				}
				catch (Exception e)
				{
					CustomLogger.Log("ProgressBarManager Exc" + e.Message);
					Stop();
					scenario.Unlock();
					//presenter.syncHistory();

					return;
				}
			}

			//if (handler != null)
			//{
			//	if (scenario.progress > scenario.duration * 0.95f)
			//	{
			//		handler.post(audioTask);
			//	}
			//}

			//if (activity != null)
			//	activity.runOnUiThread(new Runnable() {
			//	@Override

			//	public void run()
			//{

			//presenter.hideLoadingIcon();

			//setUIProgress(scenario.progress, scenario.duration);

			//	TrackService trackService = presenter.getTrackService();
			//	if (trackService != null)
			//	trackService.resume(scenario);

			_trackService.Resume(scenario);
			//}
			//});

			_lastUpdatedAt = DateTime.Now.Millisecond;
		}
	}

}

