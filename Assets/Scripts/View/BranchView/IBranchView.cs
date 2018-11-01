using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.View.BranchView
{
	public interface IBranchView : IView
	{
		void UpdateDisplayedData(string text);
	}
}
