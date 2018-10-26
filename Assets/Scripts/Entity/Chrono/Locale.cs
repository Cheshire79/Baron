using Newtonsoft.Json;
using System;

namespace Baron.Entity.Chrono
{
	[Serializable]
	public class Locale
	{
		[JsonProperty(PropertyName = "id")]
		public int id;
		[JsonProperty(PropertyName = "code")]
		public string code;
	}
}
