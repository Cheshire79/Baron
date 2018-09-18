using System;
using Baron.View;
using Baron.View.LobbyView;
using Uniject;
using Uniject.Impl;

namespace Assets.Scripts.View.LobbyView
{
    public class LobbyView : TestableView, ILobbyView
    {
       

        public LobbyView([Resource("prefabs/view/LobbyView")] TestableGameObject obj)
            : base(obj)
        {
            var references = ((UnityGameObject)obj).obj.GetComponent<LobbyChildernReference>();
            //  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);
            if (references.StartButtonGUI != null)
                references.StartButtonGUI.onClick.AddListener(OnPlayNowButtonClicked);
        }

        public event Action PlayNowButtonClicked;

        protected virtual void OnPlayNowButtonClicked()
        {
            Action handler = PlayNowButtonClicked;
            if (handler != null) handler();
        }

    }
}
