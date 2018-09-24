using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Controller
{
	public class BrunchController
	{
		private GameBase _gameBase;
		protected BranchScenarioManager _scenarioManager;

		public BrunchController(GameBase gameBase)
		{
			_gameBase = gameBase;
			_scenarioManager = new BranchScenarioManager();

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
					Branch branch = GetStartBranch();
					scenario = _scenarioManager.CreateScenario(_gameBase, branch.Cid);

					_gameBase.History.SetScenario(scenario);

					foreach( var item in scenario.Branches)
					{
						CustomLogger.Log("Getted Branches "+item.Id +" "+item.OptionId);
					}

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

			UpdateInitialBranch();

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

	}
}
