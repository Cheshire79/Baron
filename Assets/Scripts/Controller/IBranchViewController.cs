using Baron.Service;
using Barons.Controller;
using System;

namespace Baron.Controller
{
	public interface IBranchViewController : IBaseViewController
	{
		void ShowLog(GameBase gameBase);
		void PlaceOptions(GameBase gameBase);
		void UpdateDisplayedData(string text);
		void Reset();
		void Init(Action<string> OnClickedTest, Action<float> OnClickedAnotherPosition, Action OnStartDebuging, Action PauseGame, Action ResumeGameAndStartScenario, Action MoveToStartScenarioPoint, Action MoveToEndtScenarioPoint);
		void SetImage(string image);
		void SetSliderPosition(int pos, int max);
		void ToggleControls();
	}
}
