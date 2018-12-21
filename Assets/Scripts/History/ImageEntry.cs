using Newtonsoft.Json;
using System;

namespace Baron.History
{
	[Serializable]
	public class ImageEntry : Entry
	{

		[JsonProperty(PropertyName = "isViewed")]
		private bool _isViewed;

		public bool IsViewed
		{
			get { return _isViewed; }
			set { _isViewed = value; }
		}

		[JsonIgnore]
		private bool _isShown;

		public ImageEntry():base()
		{       
		}

		public ImageEntry(string id): base(id)
		{			
		}

		[JsonIgnore]
		public bool IsShown
		{
			get { return _isShown; }
			set { _isShown = value; }
		}
	}
}
