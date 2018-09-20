using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Baron.Entity
{
	[Serializable]
	public class Option : Entity
	{
		public static string BEGIN = "BEGIN";
		public static string DEATH = "DEATH";
		public static string VICTORY = "VICTORY";
		public static string ACTION_INCREMENT_DAY = "INCREMENT_DAY";
		public static string ACTION_ADVERTISEMENT = "ADV";
		public static string TYPE_DEFAULT = "DEFAULT";
		public static string TYPE_PROXY = "PROXY";
		public static string ACTION_DISABLE_ME = "DISABLE_ME";

		[JsonProperty(PropertyName = "images")]
		private List<TrackImage> _images;
		[JsonProperty(PropertyName = "audio")]
		private List<TrackAudio> _audio;
		[JsonProperty(PropertyName = "items")]
		private List<Item> _items;
		[JsonProperty(PropertyName = "requiredItems")]
		private List<Item> _requiredItems;
		[JsonProperty(PropertyName = "actions")]
		private List<string> _actions;
		[JsonProperty(PropertyName = "type")]
		private string _type;
		[JsonProperty(PropertyName = "text")]
		private string _text;
		[JsonProperty(PropertyName = "duration")]
		private int _duration;

		public Option()
		{
			_images = new List<TrackImage>(2);
			_audio = new List<TrackAudio>(2);
			_items = new List<Item>(2);
			_requiredItems = new List<Item>(1);
			_actions = new List<string>(1);
		}


		public override string ToString()
		{
			return _id + " " + Type;
		}

		public string Type
		{
			get { return _type != null ? _type : TYPE_DEFAULT; }
			//get { return !string.IsNullOrWhiteSpace(_type) ? _type : TYPE_DEFAULT; }
		}

		public string Text
		{
			get { return _text; }
		}

		public List<TrackImage> TrackImages
		{
		   get{ return _images; }
		}

		public List<TrackAudio> TrackAudio
		{
		   get{ return _audio; }
		}

		public List<Item> _Items
		{
		    get { return _items; }
		}

		public List<Item> RequiredItems
		{
		   get { return _requiredItems; }
		}

		public List<string> Actions
		{
		    get { return _actions; }
		}

		public int Duration
		{
		    get { return _duration; }
			set { _duration = value; }
		}

		

		public void Reset()
		{
			
			foreach(var media in TrackAudio)
		   
		    {
		        media.IsLocked=false;
		        media.Completed=false;
		    }

			foreach (var media in TrackImages)
		    {
		        media.IsLocked=false;

		        InteractionObject interaction = media.InteractionObject;
		        if (interaction != null)
		        {
		            interaction.IsLocked=false;
		        }
		    }
		}

		public bool IsProxy
		{
		   get { return TYPE_PROXY.Equals(_type); }
		}
	}
}
