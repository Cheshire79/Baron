using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.Entity.Chrono
{
	public class Category
	{
		public const string BRANCH = "BRANCH";
		public const string INTERACTION = "INTERACTION";

		[JsonProperty(PropertyName = "id")]
		public int id;
		[JsonProperty(PropertyName = "locale")]
		public Locale locale;
		[JsonProperty(PropertyName = "type")]
		public String type;
		[JsonProperty(PropertyName = "events")]
		public List<GameEvent> events;

		public Category()
		{
			events = new List<GameEvent>();
		}
	}
}
