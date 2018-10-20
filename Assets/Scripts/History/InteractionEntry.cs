using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class InteractionEntry : Entry
	{
		public static string BEACON_FALL = "BEACON_FALL";
		public static string BEACON_BURN = "BEACON_BURN";
		public static string RIDDLE_BOOM = "RIDDLE_BOOM";

		[JsonProperty(PropertyName = "type")]
		private string _type;

		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}

		[JsonProperty(PropertyName = "isCompleted")]
		private bool _isCompleted;

		public bool IsCompleted
		{
			get { return _isCompleted; }
			set { _isCompleted = value; }
		}


		public InteractionEntry() : base()
		{
		}

		public InteractionEntry(string id) : base(id)
		{
		}
	}
}
