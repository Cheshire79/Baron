using Baron.Controller;
using Baron.Entity;
using Baron.Service;
using Baron.Strategy.Decision;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Listener
{
	public class TrackCompletedListener
	{
		private BrunchController _brunchController;
		GameBase _gameBase;

		public TrackCompletedListener(BrunchController brunchController, GameBase gameBase)
		{
			_brunchController = brunchController;
			_gameBase=gameBase;
		}

		public void OnReceive()
		{
			CustomLogger.Log("TrackCompletedListener");

		//	if (!BranchPresenter.isCreated()) return;

			//try
			//{
			//	InteractionFactory.destroy();
			//}
			//catch (Throwable e)
			//{
			//	Log.e(tag, e);
			//}

			try
			{
			//	final BranchPresenter presenter = BranchPresenter.getInstance();

				Branch branch = _brunchController.FindCurrentBranch(false);

				CustomLogger.Log("TrackCompletedListener Track completed for branch: " + " branch");

				BranchDecisionManager decisionManager = _brunchController.BranchDecisionManager;

				CompletedDecision decision = new CompletedDecision(decisionManager, branch, _brunchController, _gameBase);
				decision.Decide();

				//presenter.syncHistory();

			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackCompletedListener Exc" + e.Message);
			}
		}
		
	}
}
