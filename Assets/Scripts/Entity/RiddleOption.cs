using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.Entity
{
	[Serializable]
	public class RiddleOption : Entity
	{
		[JsonProperty(PropertyName = "text")]
		private string _text;

		[JsonProperty(PropertyName = "type")]
		private string _type;

		[JsonProperty(PropertyName = "audio")]
		private List<RiddleAudio> _audio;

		[JsonProperty(PropertyName = "duration")]
		private int _duration;

		[JsonIgnore]
		private bool _isEnabled = true;

		public string Text
		{
			get { return _text; }
		}

		public string Type
		{
			get { return _type; }
			set { value = _type; }
		}

		public List<RiddleAudio> Audio
		{
			get { return _audio; }
			set { _audio = value; }
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		public int Duration
		{
			set { _duration = value; }
		}
	}
}
