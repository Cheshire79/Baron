using Baron.Entity;
using Baron.Tools;
using CustomTools;
using System;
//todo stop timer on exit
namespace Baron.Service
{
	public class ProgressBarManager
	{
		public event Action<GameBase, Scenario> OnResetScenario;
		public event Func<GameBase, Scenario, int, int, bool> OnUpdateScenario;
		public event Action<Scenario> OnFinaleReached;
		public event Action<Scenario> OnInteractionReached;
		public event Action<Scenario> OnNewScenarioReached;
		public event Action OnScenarioCompleted;
		public event Action<TrackBranch> OnBranchCompleted;
		public event Action<TrackBranch> OnBranchStarted;

		public event Action<int, int> OnChangeSliderPosition;

		private int _delay = 50;
		private bool _isSpecialAudioEnabled;
		GameBase _gameBase;
		private long _lastUpdatedAt;
		
	
		
		private readonly System.Timers.Timer _scheduledTask = new System.Timers.Timer(5);



		private bool _isRunnig = false;
		private readonly TrackService _trackService;

		public ProgressBarManager(GameBase gameBase, TrackService trackService)
		{
			_gameBase = gameBase;
			_trackService = trackService;

			_scheduledTask.Elapsed += (sender, args) => Runnable();
		}
		public void Start(Scenario scenario)
		{

			int progress = scenario.Progress;
			int max = scenario.Duration;

			_isSpecialAudioEnabled = false;

			Stop();

			//setUIProgress(progress, max);
			if (OnResetScenario != null)
			{
				OnResetScenario(_gameBase, scenario);
				CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnResetScenario");
			}
			if (OnUpdateScenario != null)
			{
				OnUpdateScenario(_gameBase, scenario, progress, max);
				CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnUpdateScenario");
			}
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
			_trackService.Pause();
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

					CustomLogger.Log(CustomLogger.LogComponents.Branch, " Track is completed: " + progress + "/" + max + " ms");

					if (scenario.CurrentTrackBranch.IsFinal)
					{
						if (OnFinaleReached != null)
						{
							OnFinaleReached(scenario);
							CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnFinaleReached");
						}
					}
					else if (scenario.CurrentTrackBranch.IsInteraction)
					{
						if (OnInteractionReached != null)
						{
							OnInteractionReached(scenario);
							CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnInteractionReached " + scenario.Cid);
						}
					}
					else
					{

						if (scenario.CurrentTrackBranch.IsBeforeNewScenario)// what does it mean
						{
							if (OnNewScenarioReached != null)
							{
								OnNewScenarioReached(scenario);
								CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnNewScenarioReached " + scenario.Cid);
							}
						}
						else
						{
							if (OnScenarioCompleted != null)
							{
								OnScenarioCompleted();
								CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnScenarioCompleted " + scenario.Cid);
							}
						}
					}
				}

			}

			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
		}
		// https://gunnarpeipman.com/net/avoid-overlapping-timer-calls/
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
				int delta = Math.Max(_delay, (int)(DateTime.Now.Ticks - _lastUpdatedAt) / 10000);
				return (_delay + delta) / 2;
			}

			return 0;
		}

		public void UpdateScenario(Scenario scenario, float delta)
		{

			bool isFirstTrackItem = delta == 0;
			int progress = (int)(scenario.Progress + delta);

			bool hasBranchChanged = false;
			if (OnUpdateScenario != null)
				hasBranchChanged = OnUpdateScenario(_gameBase, scenario, progress, scenario.Duration);
			else
			{
				CustomLogger.LogException(new Exception("OnUpdateScenario is null"));
			}

			//updatePlayerFragment(); Update graphic

			if (scenario.IsCompleted)
			{
				if (OnBranchCompleted != null)
				{
					OnBranchCompleted(scenario.CurrentTrackBranch);
					CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnBranchCompleted " + scenario.CurrentTrackBranch.Id);
				}

				Finish();

				return;
			}

			if (isFirstTrackItem)
			{
				try
				{
					if (OnBranchStarted != null)
					{
						OnBranchStarted(scenario.CurrentTrackBranch);
						CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnBranchStarted " + scenario.CurrentTrackBranch.Id);
					}
				}
				catch (Exception e)
				{
					CustomLogger.LogException(e);
					Stop();
					scenario.Unlock();
					_gameBase.syncHistory();

					return;
				}
			}
			else if (hasBranchChanged)
			{

				TrackBranch prev = (TrackBranch)scenario.CurrentTrackBranch.Previous;
				if (OnBranchCompleted != null)
				{
					OnBranchCompleted(prev);
					CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnBranchCompleted " + scenario.CurrentTrackBranch.Id);
				}

				try
				{
					if (OnBranchStarted != null)
					{
						OnBranchStarted(scenario.CurrentTrackBranch);
						CustomLogger.Log(CustomLogger.LogComponents.ProgressBarManager, " OnBranchStarted " + scenario.CurrentTrackBranch.Id);
					}
				}
				catch (Exception e)
				{
					CustomLogger.LogException(e);
					Stop();
					scenario.Unlock();
					_gameBase.syncHistory();

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

			setUIProgress(scenario.Progress, scenario.Duration);

			_trackService.Resume(scenario);
		
			_lastUpdatedAt = DateTime.Now.Ticks;
		}

		private void setUIProgress(int progress, int max)
		{
			if (OnChangeSliderPosition != null)
				MainThreadRunner.AddTask(() => OnChangeSliderPosition(progress, max));

			//bar.setProgress(progress);

		}


		public void TPause() // todo
		{
			CustomLogger.Log("ApplicationPausedListener ");
			GameBase.isPaused = true;
			_trackService.Pause();
			Stop();


			//presenter.getPlayerFragment().toggleControls(); todo
		}
	}

}

