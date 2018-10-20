using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class OptionActionEntry: Entry
	{
		[JsonProperty(PropertyName = "branchCid")]
		private string _branchCid;		

		public OptionActionEntry(): base()
		{			
		}

		public OptionActionEntry(string id) : base(id)
		{			
		}
		
		public string BranchCid
		{
			get { return _branchCid; }
			set { _branchCid = value; }
		}

	}
}
