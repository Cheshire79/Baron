using Baron.Service;
using Barons.Controller;
using System;

namespace Baron.Controller
{
	public interface IBranchViewController: IBaseViewController
	{
		void ShowLog(GameBase gameBase,BrunchController brunchController);
		void PlaceOptions(GameBase gameBase, BrunchController brunchController);
		void UpdateDisplayedData(string text); 
		 void Reset();
		void Init(Action<string> OnClickedTest, Action<float> OnClickedAnotherPosition, Action OnStartDebuging);
		void SetImage(string image);
		 void SetSliderPosition(int pos, int max);
	}
}
