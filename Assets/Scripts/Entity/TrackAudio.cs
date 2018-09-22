using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Baron.Entity
{
	public class TrackAudio : TrackMedia
	{
		[JsonProperty(PropertyName = "duration")]
		private float _duration;

		//[JsonIgnore] //todo
		//	public MediaPlayer player;

		[JsonProperty(PropertyName = "isPrepared")]
		private bool _isPrepared;

		[JsonProperty(PropertyName = "isPreparing")]
		private bool _isPreparing;

		public override string Id
		{
			get { return _id; }
		}

		public override float Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

		public bool IsPrepared
		{
			get	{	return _isPrepared;	}
			set	{	_isPrepared = value;}
		}

		public bool IsPreparing
		{
			get	{	return _isPreparing;}
			set	{_isPreparing = value;	}
		}
	}
}
