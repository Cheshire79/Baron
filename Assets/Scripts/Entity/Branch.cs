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
		private string _option;

		[JsonProperty(PropertyName = "action")]
		private string _action;

		[JsonProperty(PropertyName = "isCompleted")]
		private bool _isCompleted;

		[JsonProperty(PropertyName = "order")]
		private int _order;

		[JsonProperty(PropertyName = "params")]
		private OptionParams _params;

		[JsonProperty(PropertyName = "options")]
		private List<InventoryBranch> _options;

		public Branch()
		{
			_options = new List<InventoryBranch>();
		}

		public Branch(string option) : this()
		{

			_option = option;
		}

		public Branch(String option, String cid) : this(option)
		{
			_cid = cid;
		}

		public string Option
		{
			get { return _option; }
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

		public int Order
		{
			get { return _order; }
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
			get { return _cid + " " + _option
					 + " " + _action
					 + " " + (_isCompleted ? "+" : "-");
			}
		}

		public OptionParams Params
		{
			get { return _params; }
		}

		public bool IsCompleted
		{
			get { return _isCompleted; }
			set { _isCompleted = value; }
		}
		
		public void Reset()
		{
			_isCompleted = false;
		}
	}
}
