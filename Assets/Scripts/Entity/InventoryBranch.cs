using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Baron.Entity
{
	[Serializable]
	public class InventoryBranch
	{
		public static string DEFAULT = "DEFAULT";
		public static string EMPTY = "EMPTY";
		public static string DAY_PREFIX = "DAY_";

		[JsonProperty(PropertyName = "inventory")]
		private List<string> _inventory;
		[JsonProperty(PropertyName = "options")]
		private List<Branch> _options;// ??

		public InventoryBranch()
		{
			_inventory = new List<string>(1);
			_options = new List<Branch>();
		}

		public List<String> Inventory
		{
			get { return _inventory; }
		}

		public List<Branch> Branches
		{
			get { return _options; }
		}

		public void DddInventory(String item)
		{
			_inventory.Add(item);
		}

		public void addBranch(Branch item)
		{
			_options.Add(item);
		}
	}
}
