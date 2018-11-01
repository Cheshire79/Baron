using Baron.View.BranchView;
using Baron.View.LobbyView;

namespace Assets.Scripts.View
{
    public abstract class AbstractViewFactory
    {
        public abstract ILobbyView CreateLobbyView();
		public abstract IBranchView CreateBranchView();

	}
}
