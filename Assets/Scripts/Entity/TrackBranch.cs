using Newtonsoft.Json;

namespace Baron.Entity
{
	public class TrackBranch : TrackMedia
	{

		[JsonProperty(PropertyName = "duration")]
		private float _duration;

		[JsonProperty(PropertyName = "option")]
		private string _optionId;

		[JsonProperty(PropertyName = "optionType")]
		private string _optionType;

		[JsonProperty(PropertyName = "isBeforeNewScenario")]
		private bool _isBeforeNewScenario;

		[JsonProperty(PropertyName = "containsDayAction")]
		private bool _containsDayAction;

		[JsonIgnore]
		public string OptionId
		{
			get { return _optionId; }
			set { _optionId = value; }
		}

		[JsonIgnore]
		public string OptionType
		{
			get { return _optionType; }
			set { _optionType = value; }
		}


		[JsonIgnore]
		public bool ContainsDayAction
		{
			get { return _containsDayAction; }
			set { _containsDayAction = value; }
		}
		[JsonIgnore]
		public bool IsBeforeNewScenario
		{
			get { return _isBeforeNewScenario; }
			set { _isBeforeNewScenario = value; }
		}

		[JsonIgnore]
		public override int Duration
		{
			get { return (int)_duration; } //todo ask
			set { _duration = value; }
		}

		public override string ToString()
		{
			return base.ToString() + " " + _optionType;
		}

		[JsonIgnore]
		public bool IsFinal
		{
			get { return Option.DEATH.Equals(_optionId) || Option.VICTORY.Equals(_optionId); }
		}

		[JsonIgnore]
		public bool IsInteraction
		{
			get { return Option.TYPE_INTERACTION.Equals(_optionType); }
		}
	}
}
