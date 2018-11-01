using Baron.Service;
using Barons.Controller;

namespace Baron.Controller
{
	public interface IBranchViewController: IBaseViewController
	{
		void ShowLog(GameBase gameBase,BrunchController brunchController);
		void UpdateDisplayedData(string text); 
		 void Reset();
	}
}
