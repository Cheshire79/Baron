
using Baron.Service;
using Barons.Controller;

namespace Baron.Controller
{
    public interface ILobbyController : IBaseViewController
    {
		void Init(GameBase gameBase);
	}
}
