using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.Entity
{
	[Serializable]
	public class Audio : Entity
	{
		[JsonProperty(PropertyName = "file")]
		private string _file;

		[JsonProperty(PropertyName = "description")]
		private string _description;

		[JsonProperty(PropertyName = "duration")]
		private int _duration;

		[JsonProperty(PropertyName = "isFail")]
		private bool _isFail;

		[JsonProperty(PropertyName = "isVideo")]
		private bool _isVideo;

		[JsonProperty(PropertyName = "fails")]
		private List<string> _fails;

		public Audio() : base()
		{
			_fails = new List<string>();
		}

		public override string Id
		{
			get { return _id != null ? _id.ToLower() : null; }
		}

		public string File
		{
			get { return _file; }
			set { _file = value; }// todo why (in compare with Image)
		}

		public string Description
		{
			get { return _description; }
		}

		public int Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

		public bool IsFail
		{
			get { return _isFail; }
		}

		public bool IsVideo
		{
			get { return _isVideo; }
		}

		public List<string> Fails
		{
			get { return _fails; }
		}
	}
}
