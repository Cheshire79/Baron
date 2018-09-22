using Baron.Service;
using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
	public class Interaction
	{
		[JsonProperty(PropertyName = "option")]
		private string _option;

		[JsonProperty(PropertyName = "name")]
		private string _name;

		[JsonProperty(PropertyName = "isLocked")]
		private bool _isLocked;

		public Interaction(string name) : this(name, StringUtils.Cid())
		{
		}

		public Interaction(String name, String option)
		{
			_option = option;
			_name = name;
		}

		public string Option
		{
			get { return _option; }
		}

		public string Id
		{
			get { return _option + "#" + _name; }
		}

		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public override string ToString()
		{
			return Id;
		}

		public bool IsLocked
		{
			get { return _isLocked; }
			set { _isLocked = value; }
		}

	}
}
