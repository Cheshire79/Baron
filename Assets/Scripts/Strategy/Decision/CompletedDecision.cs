using Baron.Controller;
using Baron.Entity;
using Baron.Listener;
using Baron.Service;
using CustomTools;


namespace Baron.Strategy.Decision
{
	public class CompletedDecision : Decision
	{
		ShowBranchesListener _showBranchesListener;


		public CompletedDecision(BranchDecisionManager manager, Branch branch, BranchController branchController, GameBase gameBase) : base(manager, branch, branchController, gameBase)
		{
			_showBranchesListener = new ShowBranchesListener(branchController.BranchViewController);
		}


		public override void Decide()
		{
			CustomLogger.Log("CompletedDecision  Handle: " + branch);

			//if (!BranchPresenter.isCreated()) return;

			if (!CheckIfCanContinueGame(branch)) return;

			//BranchPresenter presenter = BranchPresenter.getInstance();

			//presenter.dispatch(Event.SHOW_BRANCHES);
			_showBranchesListener.OnReceive(_gameBase, _branchController);
		}


		public Branch Peek()
		{
			return branch;
		}
	}
}
