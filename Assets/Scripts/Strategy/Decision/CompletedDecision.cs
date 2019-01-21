using Baron.Controller;
using Baron.Entity;
using Baron.Listener;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Strategy.Decision
{
	public class CompletedDecision
	{
		protected Branch branch;
		protected BranchController _branchController;
		protected GameBase _gameBase;

		ShowBranchesListener _showBranchesListener;


		public CompletedDecision(Branch branch, BranchController branchController, GameBase gameBase)
		{
			this.branch = branch;
			_branchController = branchController;
			_gameBase = gameBase;
			_showBranchesListener = new ShowBranchesListener(branchController.BranchViewController);
		}


		public void Decide()
		{
			CustomLogger.Log("CompletedDecision  Handle: " + branch);

			//if (!BranchPresenter.isCreated()) return;

			if (!CheckIfCanContinueGame(branch)) return;

			//BranchPresenter presenter = BranchPresenter.getInstance();

			//presenter.dispatch(Event.SHOW_BRANCHES);
			_showBranchesListener.OnReceive(_gameBase, _branchController);
		}


		//public Branch Peek()
		//{
		//	return branch;
		//}
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
