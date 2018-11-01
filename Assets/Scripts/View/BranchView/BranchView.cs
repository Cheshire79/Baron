
using Uniject;
using Uniject.Impl;
using UnityEngine.UI;

namespace Baron.View.BranchView
{
	public class BranchView : TestableView, IBranchView
	{
		private Text _info;
		public BranchView([Resource("prefabs/view/BranchView")] TestableGameObject obj)
			: base(obj)
		{
			var references = ((UnityGameObject)obj).obj.GetComponent<BranchChildernReference>();
			//  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);
			//if (references.StartButtonGUI != null)
			//	references.StartButtonGUI.onClick.AddListener(OnPlayNowButtonClicked);
			_info = references.Info;
		}
		public void UpdateDisplayedData(string text)
		{
			_info.text = _info.text + "\n"+text+" ";
		}

	}
}
