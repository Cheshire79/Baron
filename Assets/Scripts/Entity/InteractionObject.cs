using Baron.Service;
using System;

namespace Baron.Entity
{
	public class InteractionObject
	{
		private string _option;
		private string _name;
		private bool _isLocked;

		public InteractionObject(string name) : this(name, StringUtils.Cid())
		{
		}

		public InteractionObject(String name, String option)
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
