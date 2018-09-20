using Baron.Service;
using Baron.Strategy.Background;
using Newtonsoft.Json;

namespace Baron.Entity
{
	public class TrackImage : TrackMedia
	{
		[JsonProperty(PropertyName = "duration")]
		private int _duration;
		private string _interaction;
		private string _transition;

		private InteractionObject _interactionObject;//todo check rename

	//	[JsonIgnore]
		public override string Id
		{
			get { return _id != null ? _id.ToLower() : null; }
		}

	//	[JsonIgnore]
		public override int Duration
		{
			get { return _interaction != null ? 1500 : _duration; }
			set { _duration = value; }
		}

		//	@Override
		//public int getDuration()
		//	{		return interaction != null ? 1500 : duration;	}

		//	public void setDuration(int duration)
		//	{		this.duration = duration;	}


		[JsonIgnore]
		public string Interaction
		{
			get { return _interaction != null ? _interaction : InteractionFactory.INTERACTION_NONE; }
			set { _interaction = value; }
		}

		//public String getInteraction()
		//{
		//	return interaction != null ? interaction : InteractionFactory.INTERACTION_NONE;
		//}

		//public void setInteraction(String interaction)
		//{
		//	this.interaction = interaction;
		//}


		//[JsonIgnore]
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

		//public String getTransition()
		//{			return transition != null					? transition					: TransitionFactory.DEFAULT;		}


	//	[JsonIgnore]
		public InteractionObject InteractionObject
		{
			get { return _interactionObject; }
			set { _interactionObject = value; }
		}

		//public Interaction getInteractionObject()
		//{			return interactionObject;		}

		//public void setInteractionObject(Interaction interactionObject)
		//{			this.interactionObject = interactionObject;		}
	}
}
