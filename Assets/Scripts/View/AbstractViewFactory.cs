using Baron.View.BranchView;
using Baron.View.LobbyView;

namespace Baron.View
{
    public abstract class AbstractViewFactory
    {
        public abstract ILobbyView CreateLobbyView();
		public abstract IBranchView CreateBranchView();

		public abstract TestableView CreateView<T>() where T : TestableView;

	}
}
