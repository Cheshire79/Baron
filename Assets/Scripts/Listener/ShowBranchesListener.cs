using Baron.Controller;
using Baron.Service;
using Baron.Tools;
using CustomTools;
using System;

namespace Baron.Listener
{
	public class ShowBranchesListener
	{
		IBranchViewController _branchViewController;


		public ShowBranchesListener(IBranchViewController branchViewController)
		{
			_branchViewController = branchViewController;
		}


		public void OnReceive(GameBase gameBase, BranchController branchController)
		{
			//if (!BranchPresenter.isCreated()) return;

			try
			{
				CustomLogger.Log("ShowBranchesListener");

				//	BranchPresenter presenter = BranchPresenter.getInstance();
				try
				{
					//presenter.removeBranchOptions();
					//todo refesh view
				}
				catch (Exception e)
				{
					CustomLogger.Log("ShowBranchesListener Exc" + e.Message);
				}



				try
				{
					//presenter.dispatch(Event.APPLICATION_PAUSED);
					//todo pause
				}
				catch (Exception e)
				{
					CustomLogger.Log("ShowBranchesListener Exc" + e.Message);
				}


				try
				{


					_branchViewController.ShowLog(gameBase, branchController);
					MainThreadRunner.AddTask(() =>
					_branchViewController.PlaceOptions(gameBase, branchController));
					//FragmentManager fm = activity.getSupportFragmentManager();
					//Fragment interactionFragment = fm.findFragmentById(R.id.interaction_container);
					//if (interactionFragment != null)
					//{
					//	FragmentService.stop(activity, interactionFragment);
					//}
				}
				catch (Exception e)
				{
					CustomLogger.Log("ShowBranchesListener Exc" + e.Message);
					//CustomLogger.LogException("ShowBranchesListener Exc" + e.Message);
				}

				//FragmentService.start(activity, R.id.branch_container, new BranchOptionsFragment());

			}
			catch (Exception e)
			{
				CustomLogger.Log("ShowBranchesListener Exc" + e.Message);
			}
		}
	}
}
