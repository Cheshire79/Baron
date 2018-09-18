using System;

namespace Baron.View.LobbyView
{
    public interface ILobbyView : IView
    {
        event Action PlayNowButtonClicked;
    }
}
