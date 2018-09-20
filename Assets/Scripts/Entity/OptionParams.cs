using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
	[Serializable]
	public class OptionParams
	{
		[JsonProperty(PropertyName = "delayX")]
		private int _delayX;
		[JsonProperty(PropertyName = "delayY")]
		private int _delayY;

		public int DelayX
		{
			get { return _delayX * 1000; }
		}

		public int DelayY
		{
			get { return _delayY * 1000; }
		}
	}
}
