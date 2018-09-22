using Baron.Service;
using Baron.Strategy.Background;
using Newtonsoft.Json;

namespace Baron.Entity
{
	public class TrackImage : TrackMedia
	{
		[JsonProperty(PropertyName = "altId")]
		private string _altId;

		[JsonProperty(PropertyName = "duration")]			
		private float _duration; 

		[JsonProperty(PropertyName = "interaction")]
		private string _interaction;

		[JsonProperty(PropertyName = "transition")]
		private string _transition;

		[JsonIgnore]
		private Interaction _interactionObject;//todo check rename


		public override string Id
		{
			get { return _id; }
		}

		public override float Duration
		{
			get { return _interaction != null ? 1500 : _duration; }
			set { _duration = value; }
		}

		public string Interaction
		{
			get { return _interaction != null ? _interaction : InteractionFactory.INTERACTION_NONE; }
			set { _interaction = value; }
		}

		public string Transition
		{
			get
			{
				return _transition != null
				  ? _transition
				  : TransitionFactory.DEFAULT;
			}
			set { _interaction = value; }
		}

		public Interaction InteractionObject
		{
			get { return _interactionObject; }
			set { _interactionObject = value; }
		}

		public string AltId
		{
			get { return _altId;}
			set	{ _altId = value;}
		}

	}
}
