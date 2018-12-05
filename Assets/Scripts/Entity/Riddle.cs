using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Baron.Entity
{
	[Serializable]
	//to test todo
	public class Riddle : Entity
	{
		[JsonProperty(PropertyName = "audio")]
		private List<Entity> _audio;

		[JsonProperty(PropertyName = "options")]
		private List<RiddleOption> _options;

		public List<Entity> Audio
		{
			get { return _audio; }
		}

		public List<RiddleOption> Options()
		{
			return _options;
		}

		public void Reset()
		{
			foreach (RiddleOption option in _options)
			{
				option.IsEnabled = true;
			}
		}
	}
}
