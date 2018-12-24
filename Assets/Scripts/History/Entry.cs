using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class Entry//: IValid todo the reason
	{
		[JsonProperty(PropertyName = "name")]
		private string _name;
		[JsonProperty(PropertyName = "createdAt")]
		public string createdAt;


		public Entry()
		{
			createdAt = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"); //DateTime.Today.ToString();
			//todo check
			//DateUtils.now();
		}

		public Entry(string name) : this()
		{
			_name = name;
		}

		[JsonIgnore]
		public String Name
		{
			get { return _name; }
		}

		public String getCreatedAt()
		{
			return createdAt;
		}

		public override string ToString()
		{
			return createdAt + ": " + _name;
		}

		public bool IsValid()
		{
			return _name != null && createdAt != null;
		}
	}
}
