﻿using Newtonsoft.Json;
using System;

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


		public string OptionId
		{
			get { return _optionId; }
			set { _optionId = value; }
		}


		public string OptionType
		{
			get { return _optionType; }
			set { _optionType = value; }
		}

		//public String Id
		//{
		//	get { return _id; }
		//}

		public bool ContainsDayAction
		{
			get { return _containsDayAction; }
			set { _containsDayAction = value; }
		}


		public bool IsBeforeNewScenario
		{
			get { return _isBeforeNewScenario; }
			set { _isBeforeNewScenario = value; }
		}

		public override int Duration
		{
			get { return (int)_duration; } //todo ask
			set { _duration = value; }
		}


		public override string ToString()
		{
			return base.ToString() + " " + _optionType;
		}

		public bool IsFinal
		{
			get { return Option.DEATH.Equals(_optionId) || Option.VICTORY.Equals(_optionId); }
		}

		public bool IsInteraction
		{
			get { return Option.TYPE_INTERACTION.Equals(_optionType); }
		}
	}
}
