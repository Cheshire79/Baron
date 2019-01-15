using Baron.Controller;
using Baron.Service;
using CustomTools;
using System;


namespace Baron.Listener
{
	public class ApplicationResumedListener // todo remove
	{
		private GameBase _gameBase;
		private BranchScenarioManager _scenarioManager;

		public ApplicationResumedListener(GameBase gameBase, BranchScenarioManager scenarioManager)
		{
			_gameBase = gameBase;
			_scenarioManager = scenarioManager;
		}
		public void onReceive(bool canHideBlack, BranchController branchController)
		{
			CustomLogger.Log("ApplicationResumedListener ");

			

			try
			{
				//if (canHideBlack)
				//{
				//	View black = activity.getBlackCurtains();
				//	if (black != null && black.getVisibility() != View.GONE)
				//		black.setVisibility(View.GONE);
				//}

				//if (activity instanceof BranchActivity) {
				//	BranchActivity branchActivity = (BranchActivity)activity;

				//	branchActivity.getBranchLayout().setVisibility(View.VISIBLE);
				//	branchActivity.getBranchIntroContainer().setVisibility(View.GONE);
				//}

				// опускаем GameplayService
				//GameplayService.resumeGameAndStartScenario();
				GameBase.isPaused = false;
				_scenarioManager.ResumeScenario();
			}
			catch (Exception e)
			{
				CustomLogger.Log("ApplicationResumedListener " + e);
			}
		}
	}
}
