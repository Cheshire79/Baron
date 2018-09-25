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

		public static string TYPE_DEFAULT = "DEFAULT";
		public static string TYPE_PROXY = "PROXY";
		public static string TYPE_INTERACTION = "INTERACTION";

		public static string ACTION_DISABLE_ME = "DISABLE_ME";
		public static string ACTION_INCREMENT_DAY = "INCREMENT_DAY";
		public static string ACTION_ADVERTISEMENT = "ADV";

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

		[JsonProperty(PropertyName = "interaction")]
		private string _interactionId;
		[JsonProperty(PropertyName = "text")]
		private string _text;
		[JsonProperty(PropertyName = "duration")]
		private float _duration;


		[JsonIgnore]
		private TrackImage _currentImage;

		public TrackImage CurrentImage
		{
			get	{ return _currentImage; }
			set	{ _currentImage = value; }
		}

		[JsonIgnore]
		private TrackAudio _currentAudio;

		public TrackAudio СurrentAudioe
		{
			get	{ return _currentAudio; }
			set	{ _currentAudio = value; }
		}

		public Option()
		{
			_images = new List<TrackImage>(2);
			_audio = new List<TrackAudio>(2);
			_items = new List<Item>(2);
			_requiredItems = new List<Item>(1);
			_actions = new List<string>(1);
		}

		public void Init()
		{
			CurrentImage = null;
			_currentAudio = null;

			float offset = 0;
			int size = _images.Count;

			for (int i = 0; i < size; i++)
			{
				TrackImage current = _images[i];

				current.IsLocked = false;
				current.IsCompleted = false;

				Interaction interaction = current.InteractionObject;
				if (interaction != null)
				{
					interaction.IsLocked = false;
				}

				TrackImage next = null;
				TrackImage prev = null;
				if (size > 1)
				{
					if (i == 0)
					{
						next = _images[i + 1];
					}
					else if (i == size - 1)
					{
						prev = _images[i - 1];
					}
					else
					{
						next = _images[i + 1];
						prev = _images[i - 1];
					}
				}

				current.Previous = prev;
				current.Next = next;

				current.StartsAt = offset;
				current.FinishesAt = offset += current.Duration;

				if (i == size - 1)
				{
					current.FinishesAt = Math.Max(current.FinishesAt, _duration);
				}
			}

			offset = 0;
			size = _audio.Count;

			for (int i = 0; i < size; i++)
			{
				TrackAudio current = _audio[i];

				current.IsLocked = false;
				current.IsCompleted = false;

				TrackAudio next = null;
				TrackAudio prev = null;
				if (size > 1)
				{
					if (i == 0)
					{
						next = _audio[i + 1];
					}
					else if (i == size - 1)
					{
						prev = _audio[i - 1];
					}
					else
					{
						next = _audio[i + 1];
						prev = _audio[i - 1];
					}
				}

				current.Previous = prev;
				current.Next = next;

				current.StartsAt = offset;
				current.FinishesAt = offset += current.Duration;

				if (i == size - 1)
				{
					current.FinishesAt = Math.Max(current.FinishesAt, _duration);
				}
			}

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
			get { return _images; }
		}

		public List<TrackAudio> TrackAudio
		{
			get { return _audio; }
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

		public float Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

		public void Update(int progress, int max)
		{
			CurrentImage = null;
			_currentAudio = null;

			if (progress == 0)
			{
				CurrentImage = firstImage();
				_currentAudio = firstAudio();
			}
			else if (progress == max)
			{
				CurrentImage = lastImage();
				_currentAudio = lastAudio();
			}

			foreach (TrackImage trackImage in _images)
			{

				trackImage.Progress = progress;

				if (trackImage.StartsAt <= progress && progress < trackImage.FinishesAt)
				{
					trackImage.IsCompleted = false;
					CurrentImage = trackImage;
				}
				else if (progress < trackImage.StartsAt)
				{
					trackImage.IsCompleted = false;
				}
				else if (trackImage.FinishesAt <= progress)
				{
					trackImage.IsCompleted = true;
				}
			}

			foreach (TrackAudio trackAudio in _audio)
			{

				if (trackAudio.StartsAt <= progress && progress < trackAudio.FinishesAt)
				{
					trackAudio.IsCompleted = false;
					_currentAudio = trackAudio;
				}
				else if (progress < trackAudio.StartsAt)
				{
					trackAudio.IsCompleted = false;
				}
				else if (trackAudio.FinishesAt <= progress)
				{
					trackAudio.IsCompleted = true;
				}
			}
		}

		public bool IsProxy
		{
			get { return TYPE_PROXY.Equals(_type); }
		}

		public bool ContainsDayAction()
		{
			return _actions.Contains(ACTION_INCREMENT_DAY);
		}

		public bool IsInteraction
		{
			get { return TYPE_INTERACTION.Equals(_type) && _interactionId != null; }
		}

		public bool IsFinal
		{
			get { return DEATH.Equals(_id) || VICTORY.Equals(_id); }
		}

		private TrackImage firstImage()
		{
			if (_images.Count == 0) return null;

			return _images[0];
		}

		private TrackImage lastImage()
		{
			if (_images.Count == 0) return null;

			return _images[_images.Count - 1];
		}

		private TrackAudio firstAudio()
		{
			if (_audio.Count == 0) return null;

			return _audio[0];
		}

		private TrackAudio lastAudio()
		{
			if (_audio.Count == 0) return null;

			return _audio[_audio.Count - 1];
		}

	}
}
