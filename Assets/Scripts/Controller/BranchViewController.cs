using Baron.Entity;
using Baron.Service;
using Baron.View;
using Baron.View.BranchView;
using Barons.Controller;
using CustomTools;
using System;
using System.Collections.Generic;

namespace Baron.Controller
{
	public class BranchViewController : BaseViewController, IBranchViewController
	{
		private readonly AbstractViewFactory _viewFactory;
		private readonly IBranchView _view;
		public override IView View
		{
			get { return _view; }
		}
		private List<Branch> _branches;
		public void Init(Action<string> OptionClicked, Action<float> OnClickedAnotherPosition)
		{
			_view.Init(OptionClicked, OnClickedAnotherPosition);
		}
	
		public BranchViewController(IViewStack viewStack, AbstractViewFactory viewFactory) : base(viewStack)
		{
			_viewFactory = viewFactory;
			_view = viewFactory.CreateBranchView();
			
			//_view.Show();
			//    _view.BackButtonClicked += ShowDialogBoxOnExit; todo
		}

		//todo implement
		private void CloseView()
		{

		}
		public void CloseAllViews()
		{
			//HoldemTableWindowController.CloseFromAplication();

		}
		public void ShowLog(GameBase gameBase, BrunchController brunchController)
		{

			//	BranchPresenter presenter = BranchPresenter.getInstance();
			//	BranchActivity activity = presenter.getActivity();

			try
			{
				Branch currentBranch = brunchController.FindCurrentBranch(false);

				CustomLogger.Log("BranchViewController onCreate for " + currentBranch);

				InventoryBranch currentInventoryBranch = TreeParser.FindInventoryBranch(gameBase, currentBranch);

				if (currentInventoryBranch != null)
				{
					_branches = currentInventoryBranch.Branches;
					// 	adapter = new BranchesAdapter(activity, currentInventoryBranch, this);

					for (int i = 0; i < _branches.Count; i++)
					{

						Branch branch = _branches[i];
						Option option = OptionRepository.Find(gameBase, branch.OptionId);
						CustomLogger.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
						CustomLogger.Log("BranchViewController Cid=" + branch.Cid + " Text=" + option.Text);
						//	View v = adapter.getView(i);

						//	listView.addView(v);
					}

					//	updatePosition();
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("BranchViewController Exc" + e.Message);
			}
		}

		public void UpdateDisplayedData(string text)
		{
			_view.UpdateDisplayedData(text);
		}
 
		public void PlaceOptions(GameBase gameBase, BrunchController brunchController)
		{
			_view.PlaceOptions(gameBase, brunchController);

		}

		public void SetImage(string image)
		{
			_view.SetImage(image);
		}

		public void Reset()
		{
			_view.Reset();
		}
		public void SetSliderPosition(int pos, int max)
		{
			_view.SetSliderPosition(pos,max);
		}
	}
}
