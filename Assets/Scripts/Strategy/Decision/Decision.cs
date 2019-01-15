using Baron.Controller;
using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Strategy.Decision
{
	public abstract class Decision
	{
		protected Branch branch;
		protected BranchDecisionManager manager;
		protected  BranchController _branchController;
		protected GameBase _gameBase;




		protected Decision(BranchDecisionManager manager, Branch branch, BranchController brunchController, GameBase gameBase)
		{
			this.branch = branch;
			this.manager = manager;
			_branchController = brunchController;
			 _gameBase=gameBase;
		}

		public abstract void Decide();

		public Branch peek()
		{
			return null;
		}

		protected bool CheckIfCanContinueGame(Branch branch)
		{

			//	if (!BranchPresenter.isCreated()) return false;

			try
			{
				//	final BranchPresenter presenter = BranchPresenter.getInstance();
				//	final BranchActivity activity = presenter.getActivity();
				//	final Handler handler = presenter.getHandler();

				OptionParams @params;
				switch (branch.OptionId)
				{
					case Option.DEATH:

						GameBase.isFinaleReached = true;

						//NOTE OptionParams are ignored

						//						activity.redirectToDefeatActivity();

						CustomLogger.Log("Decision Final branch reached " + branch.OptionId);

						return false;
						break;
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
						break;
				}

			}
			catch (Exception e)
			{
				CustomLogger.Log("Decision Exc" + e.Message);
			}

			return true;
		}
	}
}
