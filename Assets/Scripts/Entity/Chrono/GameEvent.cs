using Newtonsoft.Json;
using System;

namespace Baron.Entity.Chrono
{
	public class GameEvent
	{
		[JsonProperty(PropertyName = "id")]
		public int id;
		[JsonProperty(PropertyName = "category")]
		public Category category;
		[JsonProperty(PropertyName = "code")]
		public String code;
		[JsonProperty(PropertyName = "name")]
		public String name;
		[JsonProperty(PropertyName = "icon")]
		public String icon;
		[JsonProperty(PropertyName = "isSelected")]
		public bool isSelected;
	}
}
