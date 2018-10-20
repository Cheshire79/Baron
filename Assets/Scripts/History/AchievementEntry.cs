using Baron.Service;
using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class AchievementEntry : Entry
	{
		[JsonProperty(PropertyName = "type")]
		public string Type;
		[JsonProperty(PropertyName = "value")]
		public string Value;
		[JsonProperty(PropertyName = "reward")]
		public int Reward;

		public AchievementEntry() : base()
		{
		}

		public AchievementEntry(string type, int reward) : this(type, reward, null)
		{
		}

		public AchievementEntry(string type, int reward, String value) : base(StringUtils.Cid())
		{
			Type = type;
			Value = value;
			Reward = reward;
		}

		public override string ToString()
		{
			return base.ToString() + " " + Type + " " + Reward + " " + Value;
		}
	}
}
