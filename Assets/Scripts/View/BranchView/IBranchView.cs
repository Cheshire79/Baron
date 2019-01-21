using Baron.Service;
using System;

namespace Baron.View.BranchView
{
	public interface IBranchView : IView
	{
		void UpdateDisplayedData(string text);
		void PlaceOptions(GameBase gameBase);
		void Reset();
		void Init(Action<string> OnClickedTest, Action<float> OnClickedAnotherPosition, Action OnStartDebuging);
		void SetImage(string image);
		void InitSlider(int max);

		void ChangeSliderPosition(int pos, int max);
	}
}
