using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.Entity
{
	[Serializable]
	public class Image:Entity
	{
		[JsonProperty(PropertyName = "sharing")]
		public Dictionary<string, List<Sharing>> Sharing;

		[JsonProperty(PropertyName = "file")]
		private string _file;

		[JsonProperty(PropertyName = "description")]
		private string _description;

		[JsonProperty(PropertyName = "isHidden")]
		private bool _isHidden;

		[JsonProperty(PropertyName = "isAnimation")]
		private bool _isAnimation;

		[JsonProperty(PropertyName = "isCompleteBonus")]
		private bool _isCompleteBonus;

		[JsonProperty(PropertyName = "isSuperBonus")]
		private bool _isSuperBonus;

		[JsonProperty(PropertyName = "isSpecialBonus")]
		private bool _isSpecialBonus;

		[JsonProperty(PropertyName = "requiredOptions")]
		private List<List<string>> _requiredOptions;

		public Image():base()
		{
			
			_requiredOptions = new List<List<string> > (3);
			Sharing = new Dictionary<string, List<Sharing>>(3);
		}

		public override string Id
		{
			get { return _id != null ? _id.ToLower() : null; }
		}

		public string File
		{
			get { return _file; }
		}

		public string Description
		{
			get { return _description; }
		}

		public bool Bonus
		{
			get { return _isSuperBonus || _isCompleteBonus || _isSpecialBonus; }
		}

		public bool IsHidden
		{
			get { return _isHidden; }
		}

		public bool IsAnimation
		{
			get { return _isAnimation; }
		}

		public bool IsSuperBonus
		{
			get	{ return _isSuperBonus; }
		}

		public bool IsCompleteBonus
		{
			get { return _isCompleteBonus; }
		}

		public List<List<String>> RequiredOptions
		{
			get { return _requiredOptions; }
		}

		public bool IsSpecialBonus
		{
			get	{ return _isSpecialBonus; }
		}

		public bool HasRequiredOptions()
		{
			return _requiredOptions != null && _requiredOptions.Count > 0;
		}
	}
}
