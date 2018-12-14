using System;

namespace Baron.View.LobbyView
{
    public interface ILobbyView : IView
    {
        event Action BeginButtonClicked;
		event Action ContinueButtonClicked;
	}
}
