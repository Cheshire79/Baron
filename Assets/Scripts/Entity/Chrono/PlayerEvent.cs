using Newtonsoft.Json;
using System;

namespace Baron.Entity.Chrono
{
	[Serializable]
	public class PlayerEvent
	{
		[JsonProperty(PropertyName = "name")]
		public string Name;
		[JsonProperty(PropertyName = "type")]
		public string Type;
	}
}
