using Baron.Controller;
using Baron.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.View.BranchView
{
	public interface IBranchView : IView
	{
		void UpdateDisplayedData(string text);
		void PlaceOptions(GameBase gameBase, BrunchController brunchController);
		void Reset();
		void Init(Action<string> OnClickedTest, Action<float> OnClickedAnotherPosition, Action OnStartDebuging);
		void SetImage(string image);
		void InitSlider(int max);

		void SetSliderPosition(int pos, int max);
	}
}
