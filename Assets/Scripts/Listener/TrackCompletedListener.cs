using Baron.Controller;
using Baron.Service;
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

				//Branch branch = presenter.findCurrentBranch(false);

				CustomLogger.Log("TrackCompletedListener Track completed for branch: " + " branch");

				//BranchDecisionManager decisionManager = _brunchController.BranchDecisionManager;

				//CompletedDecision decision = new CompletedDecision(decisionManager, branch);
			//	decision.decide();

				//presenter.syncHistory();

			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackCompletedListener Exc" + e.Message);
			}
		}
		
	}
}
