using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class Entry//: IValid todo the reason
	{
		[JsonProperty(PropertyName = "name")]
		public string name;
		[JsonProperty(PropertyName = "createdAt")]
		public string createdAt;


		public Entry()
		{
			createdAt = DateTime.Today.ToString();
			//todo check
			//DateUtils.now();
		}

		public Entry(string name) : this()
		{
			this.name = name;
		}

		public String getName()
		{
			return name;
		}

		public String getCreatedAt()
		{
			return createdAt;
		}

		public override string ToString()
		{
			return createdAt + ": " + name;
		}

		public bool IsValid()
		{
			return name != null && createdAt != null;
		}
	}
}
