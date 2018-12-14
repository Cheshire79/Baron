using System;
using Uniject;
using Uniject.Impl;

namespace Baron.View.LobbyView
{
	public class LobbyView : TestableView, ILobbyView
	{
		public LobbyView([Resource("prefabs/view/LobbyView")] TestableGameObject obj)
			: base(obj)
		{
			var references = ((UnityGameObject)obj).obj.GetComponent<LobbyChildernReference>();
			//  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);
			if (references.StartButtonGUI != null)
				references.StartButtonGUI.onClick.AddListener(OnBeginButtonClicked);
			if (references.ContinueButtonGUI != null)
				references.ContinueButtonGUI.onClick.AddListener(OnContinueButtonClicked);
		}

		public event Action BeginButtonClicked;
		public event Action ContinueButtonClicked;

		private  void OnBeginButtonClicked()
		{
			Action handler = BeginButtonClicked;
			if (handler != null) handler();
		}

		private  void OnContinueButtonClicked()
		{
			Action handler = ContinueButtonClicked;
			if (handler != null) handler();
		}
	}
}
