using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class AudioEntry : Entry
	{
		[JsonProperty(PropertyName = "isViewed")]
		private bool _isViewed;

		public bool IsViewed
		{
			get { return _isViewed; }
			set { _isViewed = value; }
		}

		[JsonIgnore]
		private bool _isPlaying;

		[JsonIgnore]
		private bool _isPaused;

		public AudioEntry() : base()
		{
		}

		public AudioEntry(string id) : base(id)
		{
		}

		[JsonIgnore]
		public bool IsPlaying
		{
			get { return _isPlaying; }
			set { _isPlaying = value; }
		}

		[JsonIgnore]
		public bool IsPaused
		{
			get { return _isPaused; }
			set { _isPaused = value; }
		}

	}
}
