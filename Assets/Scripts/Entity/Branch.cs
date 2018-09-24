using System;
using System.Collections.Generic;
using Baron.Service;
using Newtonsoft.Json;

namespace Baron.Entity
{
	[Serializable]
	public class Branch
	{
		public static string ACTION_GOTO = "goto";
		public static string ACTION_CLICK = "click";

		[JsonProperty(PropertyName = "cid")]
		private string _cid;

		[JsonProperty(PropertyName = "option")]
		private string _optionId;

		[JsonProperty(PropertyName = "action")]
		private string _action;

		[JsonProperty(PropertyName = "params")]
		private OptionParams _params;

		[JsonProperty(PropertyName = "options")]
		private List<InventoryBranch> _options;

		public Branch()
		{
			_options = new List<InventoryBranch>();
		}

		public Branch(string optionId) : this()
		{

			_optionId = optionId;
		}

		public Branch(String option, String cid) : this(option)
		{
			_cid = cid;
		}

		public string OptionId
		{
			get { return _optionId; }
		}

		public string Action
		{
			get { return _action; }
		}

		public bool IsReference
		{
			get { return _action == null; }
		}

		public List<InventoryBranch> InventoryBranches
		{
			get { return _options; }
		}

		public void AddInventoryBranch(InventoryBranch inventoryBranch)
		{
			_options.Add(inventoryBranch);
		}		

		public string Cid
		{
			get
			{
				if (_cid == null)
				{
					_cid = StringUtils.Cid();
				}
				return _cid;
			}
		}


		public override string ToString()
		{
			return Signature;
		}

		public String Signature
		{
			get { return _cid + " " + _optionId;
			}
		}

		public OptionParams Params
		{
			get { return _params; }
		}
		public bool IsClick
		{
			get { return ACTION_CLICK.Equals(_action); }
		}
		public bool IsFinal
		{
			get { return Option.DEATH.Equals(_optionId) || Option.VICTORY.Equals(_optionId); }
		}

	}
}
