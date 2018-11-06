
using Baron.Controller;
using Baron.Entity;
using Baron.Service;
using Baron.Tools;
using CustomTools;
using System;
using System.Collections.Generic;
using Uniject;
using Uniject.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Baron.View.BranchView
{
	public class BranchView : TestableView, IBranchView
	{
		private readonly IInstancesCache _OptionsCache;
		private Text _info;
		private GridLayoutGroup _optionsList;
		private List<OptionItem> _optionItems = new List<OptionItem>();
		private Action<string> _optionClicked;

		public void Init(Action<string> optionClicked)
		{
			_optionClicked = optionClicked;
		}

		public BranchView([Resource("prefabs/view/BranchView")] TestableGameObject obj, IInstancesCache instancesCache)
			: base(obj)
		{
			var references = ((UnityGameObject)obj).obj.GetComponent<BranchChildernReference>();
			//  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);
			//if (references.StartButtonGUI != null)
			//	references.StartButtonGUI.onClick.AddListener(OnPlayNowButtonClicked);
			_info = references.Info;
			_OptionsCache = instancesCache;
			_optionsList = references.OptionsList;
		}

		public void UpdateDisplayedData(string text)
		{
			_info.text = _info.text + "\n" + text + " ";
		}

		public void PlaceOptions(GameBase gameBase, BrunchController brunchController)
		{
			Branch currentBranch = brunchController.FindCurrentBranch(false);

			CustomLogger.Log("BranchViewController onCreate for " + currentBranch);

			InventoryBranch currentInventoryBranch = TreeParser.FindInventoryBranch(gameBase, currentBranch);

			if (currentInventoryBranch != null)
			{
				List<Branch> _branches;
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
					var newOption = _OptionsCache.Get<OptionItem>();
					_optionItems.Add(newOption);
					newOption.transform.parent = _optionsList.transform;
					newOption.Init(branch.Cid, option.Text, _optionClicked);
					newOption.transform.localScale = new Vector3(1, 1, 1);				
					newOption.Show();				
				}

				//	updatePosition();
			}
		}

		public void Reset()
		{
			foreach (var item in _optionItems)
			{
				_optionsList.transform.DetachChildren();
				item.Hide();
				//friend.StateChange -= OnStateChange;
				//friend.InviteToggle.value = false;
				item.Reset();
				_OptionsCache.Put(item);
			}
			_info.text = _info.text + "\n" + "-------------------------------------------------------------" + "\n";
		}
	}
}
