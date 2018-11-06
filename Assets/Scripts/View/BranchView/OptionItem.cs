using Baron.Tools.GUI;
using CustomTools;
using System;
using Uniject;
using Uniject.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Baron.View.BranchView
{
	public interface IOptionItem
	{
		//	Text Legend { get; set; }
		//	string OptionId { get; set; }
		//event Action<string> OnOptionClicked;
		//void Show();
		void Init(string cid, string legend, Action<string> optionClicked);
		void Reset();

	}
	public class OptionItem : TestableView, IOptionItem
	{
		private Text _legend { get; set; }
		private string _optionCid { get; set; }
		public event Action<string> _onOptionClicked;

		private readonly ButtonAdv _button;
		public override Transform transform { get; set; }
		public override string name { get; set; }
		

		public OptionItem([Resource("prefabs/view/OptionItem")] TestableGameObject obj)
		  : base(obj)
		{
			var references = ((UnityGameObject)obj).obj.GetComponent<OptionItemChildrenReference>();

			//OverBackGround = references.OverBackGround;
			_legend = references.Legend;
			//Name = references.Name;
			//Invite = references.Invite;
			//OverBackGround.gameObject.SetActive(false);
			//InviteToggle = Invite.GetComponent<UIToggle>();
			//InviteBackGround = Invite.GetComponent<UISprite>();

			references.Button.onClick.AddListener(OnClicked);
			//UIEventListener onInviteclickListener = UIEventListener.Get(Invite.gameObject);
			//onInviteclickListener.onPress +=
			//	(go, isPressed) =>
			//	{
			//		if (InviteToggle.enabled && !InviteToggle.value) OverBackGround.gameObject.SetActive(isPressed);
			//	};
			//onInviteclickListener.onClick += go =>
			//{
			//	Audio.Play(Audio.SoundType.Press);
			//	OverBackGround.gameObject.SetActive(false);
			//};

			//InviteToggle.enabled = false;
		}

		
		public void  Init(string cid, string legend, Action<string> optionClicked)
		{
			_optionCid = cid;
			_legend.text = legend;
			_onOptionClicked += optionClicked;
		}
		public void Reset()
		{
			_optionCid = null;
			_legend.text = "";
			_onOptionClicked =null;
		}

		private void OnClicked()
		{
			CustomLogger.Log("Clicked ");
			if (_onOptionClicked != null)
				_onOptionClicked(_optionCid);
		}
	}
}
