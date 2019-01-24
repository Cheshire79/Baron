using Baron.Controller;
using Baron.Entity;
using Baron.Entity.Chrono;
using Baron.Listener;
using CustomTools;
using System;
using System.Text;

namespace Baron.Service
{
	public class BranchScenarioManager
	{
		private GameBase _gameBase;
		private ProgressBarManager _progressBarManager;
		private AchievementPointService _achievementPoint;
		public BranchScenarioManager(GameBase gameBase, TrackService trackService, Action ScenarioCompleted)
		{
			
			_gameBase = gameBase;
			_progressBarManager = new ProgressBarManager(gameBase, trackService);
			_achievementPoint = new AchievementPointService(gameBase);

			_progressBarManager.OnResetScenario += ResetScenario;
			_progressBarManager.OnUpdateScenario += UpdateScenario;
			_progressBarManager.OnFinaleReached += OnFinaleReached;
			_progressBarManager.OnInteractionReached += OnInteractionReached;
			_progressBarManager.OnNewScenarioReached += OnNewScenarioReached;
			_progressBarManager.OnScenarioCompleted += ScenarioCompleted;
			_progressBarManager.OnBranchCompleted += OnBranchCompleted;
			_progressBarManager.OnBranchStarted += OnBranchStarted;
		}

		public void SetChangeSliderPosition(Action<int, int> ChangeSliderPosition)
		{
			_progressBarManager.OnChangeSliderPosition += ChangeSliderPosition;
		}

		public Scenario CreateScenario(GameBase gameBase, String cid)
		{
			Scenario scenario = new Scenario();
			scenario.Cid = StringUtils.Cid();

			Branch branch = TreeParser.FindBranchByCid(gameBase, cid);//+

			if (branch.IsReference)
			{
				Branch reference = TreeParser.FindBranchByOption(
						gameBase, branch.OptionId, true, null);

				if (reference == null)
				{
					throw new ArgumentException("Branch reference not found for " + branch);
				}

				branch = reference;
			}

			FindScenario(gameBase, branch, scenario); //todo ask

			scenario.Init();

			StringBuilder builder = new StringBuilder();
			foreach (TrackBranch trackBranch in scenario.Branches)
			{
				Branch b = TreeParser.FindBranchByCid(gameBase, trackBranch.Id);
				builder.Append("=> "
						+ trackBranch.StartsAt + "-" + trackBranch.FinishesAt
						+ " " + b
						+ ";\r\n");
			}

			String log = builder.ToString();

			CustomLogger.Log("BranchScenarioManager Created scenario:\r\n" + log);

			if (!scenario.IsValid())
			{
				throw new ArgumentException("Created scenario is not valid!");
			}

			return scenario;
		}

		private void FindScenario(GameBase gameBase, Branch from, Scenario scenario) // need to check
		{
			CustomLogger.Log("BranchScenarioManager  findScenario " + from + " #" + scenario.Branches.Count);

			BranchDecisionManager decisionManager = new BranchDecisionManager(gameBase);

			Branch altBranch = decisionManager.DecideCurrentBranch(from);
			Option option = OptionRepository.Find(gameBase, altBranch.OptionId);

			if (option != null //&& BranchPresenter.isCreated()
				)
			{
				if (option.IsInteraction)// to ask
				{
					//InteractionStrategy strategy = InteractionFactory.CreateInstance(option.interaction);

					//BranchPresenter presenter = BranchPresenter.getInstance();
					//RobinzonActivity activity = presenter.getActivity();

					if (false//!strategy.isSupported(activity)
						)
					{
						//Branch nextBranch = TreeParser.FindNextBranchByInventory(gameBase, altBranch);
						//if (nextBranch != null)
						//{
						//		FindScenario(gameBase, nextBranch, scenario);
						//		return;
						//	}
					}
				}
			}

			if (!CanStartScenario(scenario, option))
			{

				//NOTE Scenario is completed due to INCREMENT_DAY action in option.
				//But new scenario will be created after.
				TrackBranch lastBranch = scenario.Last();// to ask
				if (lastBranch != null)
				{
					lastBranch.IsBeforeNewScenario = true;
				}
				return;
			}

			TrackBranch trackBranch = new TrackBranch();
			trackBranch.Id = altBranch.Cid;

			scenario.Branches.Add(trackBranch);

			if (option != null)
			{
				trackBranch.Duration = option.Duration;
				trackBranch.OptionId = option.Id;
				trackBranch.OptionType = option.Type;
				trackBranch.ContainsDayAction = option.ContainsDayAction();
			}

			if (CanFinishScenario(altBranch, option)) return;

			Branch match = TreeParser.FindNextBranchByInventory(gameBase, altBranch);

			if (match != null)
			{

				if (!match.IsFinal && match.IsReference)
				{
					Branch reference = TreeParser.FindBranchByOption(
							gameBase, match.OptionId, true, null);

					if (reference == null)
					{
						throw new ArgumentException("Branch reference not found for " + match);
					}

					match = reference;
				}

				FindScenario(gameBase, match, scenario);
			}
		}

		public bool CanStartScenario(Scenario scenario, Option option)
		{
			if (scenario.Branches.Count == 0) return true;

			foreach (TrackBranch branch in scenario.Branches)
			{
				if (branch.ContainsDayAction)
				{
					return false;
				}
			}

			return option == null || !option.ContainsDayAction();
		}

		public bool CanFinishScenario(Branch branch, Option option)
		{

			bool isFinalBranch = branch.IsFinal || branch.IsClick;
			bool isInteraction = option != null && option.IsInteraction;

			return isFinalBranch || isInteraction;
		}

		public void ResumeScenario()
		{
			CustomLogger.Log("BranchScenarioManager resumeScenario");
			try
			{
				Scenario scenario = _gameBase.History.GetScenario();

				CustomLogger.Log(CustomLogger.LogComponents.Branch, string.Format("Branches Count = {0} id = {1}", scenario.Branches.Count, scenario.CurrentTrackBranch.Id));

				if (!scenario.IsValid())
				{
					CustomLogger.Log("BranchScenarioManager  Scenario is not valid. Resetting");
					Branch branch = _gameBase.GetCurrentSavedBranch();//todo !!!!
					scenario = CreateScenario(_gameBase, branch.Cid);
					_gameBase.History.SetScenario(scenario);
				}
				_progressBarManager.Start(scenario);
			}
			catch (Exception e)
			{
				CustomLogger.Log("BranchScenarioManager ResumeScenario" + e);
			}
		}

		public void ResetScenario(GameBase gameBase, Scenario scenario)
		{
			if (gameBase == null) return;
			foreach (TrackBranch branch in scenario.Branches)
			{
				Option option = OptionRepository.Find(gameBase, branch.OptionId);
				if (option != null)
				{
					//option.IsInitialized = false;
					option.Init((int)branch.StartsAt);
				}
			}
		}

		public bool UpdateScenario(GameBase gameBase, Scenario scenario, int progress, int duration)
		{
			CustomLogger.Log(CustomLogger.LogComponents.Branch, string.Format(" Update Branches Count = {0} id = {1} opion = {2}", scenario.Branches.Count, scenario.CurrentTrackBranch.Id, scenario.CurrentTrackBranch.OptionId));
			bool hasBranchChanged = scenario.Update(progress, duration);
			foreach (TrackBranch branch in scenario.Branches)
			{
				Option option = OptionRepository.Find(gameBase, branch.OptionId);
				if (option == null) continue;
				//if (!option.IsInitialized)
				//	option.Init((int)branch.StartsAt); //todo
				option.Update(progress, duration);
			}
			return hasBranchChanged;
		}

		public void OnFinaleReached(Scenario scenario)
		{
			try
			{
				Branch branch = TreeParser.FindBranchByCid(_gameBase, scenario.CurrentTrackBranch.Id);
				String option = scenario.CurrentTrackBranch.OptionId;
				CustomLogger.Log("BranchScenarioManager Final branch reached " + option);

				OptionParams @params;
				switch (option)
				{
					case Option.DEATH:

						GameBase.isFinaleReached = true;

						//NOTE OptionParams are ignored

						//activity.redirectToDefeatActivity(); //todo
						CustomLogger.Log("BranchScenarioManager Defeat");

						break;
					case Option.VICTORY:

						GameBase.isFinaleReached = true;

						@params = branch.Params;
						if (@params != null)
						{
							CustomLogger.Log("BranchScenarioManager Victory");
							//			if (handler != null)
							//				handler.postDelayed(new Runnable() {
							//				@Override

							//				public void run()
							//			{
							//				activity.redirectToVictoryActivity(params);
							//			}
							//		}, params.getDelayX());
						}
						else
						{
							CustomLogger.Log("BranchScenarioManager Victory");
							//	activity.redirectToVictoryActivity();
						}
						break;
				}
			}

			catch (Exception e)
			{
				CustomLogger.Log("BranchScenarioManager Exc" + e.Message);
			}

		}

		public void OnInteractionReached(Scenario scenario)
		{

			CustomLogger.Log("BranchScenarioManager onInteractionReached");

			//if (!BranchPresenter.isCreated()) return;

			//final BranchPresenter presenter = BranchPresenter.getInstance();

			//	presenter.getBackgroundAudioService().destroy();

			//presenter.getInteractionService().execute(scenario); //todo 
		}

		public void OnNewScenarioReached(Scenario scenario)
		{
			CustomLogger.Log("BranchScenarioManager onNewScenarioReached");
			//presenter.getBackgroundAudioService().destroy();

			Branch currentBranch = TreeParser.FindBranchByCid(_gameBase, scenario.CurrentTrackBranch.Id);
			Branch nextBranch = TreeParser.FindNextBranchByInventory(_gameBase, currentBranch);
			if (nextBranch == null)
			{
				throw new NullReferenceException("Next branch was not found for " + currentBranch);
			}
			_gameBase.History.ActiveSave.ClickedBranches.Push(nextBranch.Cid);

			Scenario nextScenario = CreateScenario(_gameBase, nextBranch.Cid);

			_gameBase.History.SetScenario(nextScenario);
			_gameBase.syncHistory();//TODO
									//GameplayService.resumeGameAndStartScenario();
			ResumeGameAndStartScenario();
		}

		private void ResumeGameAndStartScenario()
		{
			GameBase.isPaused = false;
			_gameBase.syncHistory();//TODO
									//syncHistory();
			ResumeScenario();
		}

		//public void onError()
		//{
		//	presenter.getActivity().redirectToDefeatActivity();
		//}
		public void OnBranchCompleted(TrackBranch trackBranch)
		{
			if (trackBranch == null) return;
			History.History history = _gameBase.History;																				
			Branch currentBranch = TreeParser.FindBranchByCid(_gameBase, trackBranch.Id);
			Option currentOption = OptionRepository.Find(_gameBase, trackBranch.OptionId);
			CustomLogger.Log("BranchScenarioManager  onBranchCompleted " + currentBranch);
			if (currentOption != null)
			{
				foreach (String action in currentOption.Actions)
				{
					switch (action)
					{
						case Option.ACTION_DISABLE_ME:
							if (history.ContainsInOptionActionHistory(currentBranch, action)) continue;
							history.AddCompletedOptionAction(currentBranch.Cid, action);  // save Branch with specifyed action which was viseted

							history.AddDisabledOption(currentOption.Id);//здесь храняться конкретно ACTION_DISABLE_ME, но они же храняться массивом выше.
							break;
					}
				}
				history.AddPlayerEvent(currentOption.Id, Category.BRANCH);
				if (history.AddCompletedOption(currentOption.Id))
				{
					_achievementPoint.CheckIfOptionCompleted(currentOption.Id);
				}
			}
			history.AddStep(currentBranch);
			//SuperBonusManager.unlock(gameBase, aps); todo
		}

		public void OnBranchStarted(TrackBranch trackBranch)
		{
			if (trackBranch == null) return;
			History.History history = _gameBase.History;			
			// if (!BranchPresenter.isCreated()) return;
			// BranchPresenter presenter = BranchPresenter.getInstance();		
			Branch branch = TreeParser.FindBranchByCid(_gameBase, trackBranch.Id);
			CustomLogger.Log("BranchScenarioManager  onBranchStarted " + branch);

			Option option = OptionRepository.Find(_gameBase, trackBranch.OptionId);
			if (option != null)
			{
				foreach (Item item in option.Items)
				{
					history.AddItem(item);
				}

				foreach (String action in option.Actions)
				{
					switch (action)
					{
						case Option.ACTION_INCREMENT_DAY:
							if (!history.ContainsInOptionActionHistory(branch, action))
							{
								history.incrementDay();
								history.AddCompletedOptionAction(branch.Cid, action);
								//presenter.dispatch(Event.INCREMENT_DAY, history.getDay());
								//todo
							}
							break;
						case Option.ACTION_ADVERTISEMENT:
							if (!history.ContainsInOptionActionHistory(branch, action))
							{
								//if (AdvService.isEnabled && NetworkService.isNetworkAvailable(presenter.getActivity())) {
								//    history.addCompletedOptionAction(branch.cid, action);
								//    presenter.dispatch(Event.ADVERTISEMENT, option.id);
								//    throw new ScenarioInterruptedException(action);
								// }
							}
							break;
					}
				}
			}
		}

		public void StopProgressBar()
		{
			_progressBarManager.Stop();
		}
	}

}

