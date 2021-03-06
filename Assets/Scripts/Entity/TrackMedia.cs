﻿using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
    [Serializable]
    public abstract class TrackMedia: Entity
    {
		[JsonProperty(PropertyName = "startsAt")]
		private float _startsAt;

		[JsonProperty(PropertyName = "finishesAt")]
		private float _finishesAt;

		[JsonProperty(PropertyName = "isLocked")]
		private bool _isLocked;

		[JsonProperty(PropertyName = "isCompleted")]
		private bool _isCompleted;

		[JsonIgnore]
		protected float _progress;
		[JsonIgnore]
		protected TrackMedia _previous;
		[JsonIgnore]
		protected TrackMedia _next;

		//@Override//todo!
		//public boolean equals(Object obj)
		//{
		//    if (obj instanceof Entity) {
		//        return id.equals(((Entity)obj).id);
		//    } else return super.equals(obj);
		//}

		public abstract int Duration { get; set; }

		[JsonIgnore]
		public  int StartsAt
        {
            get { return (int)_startsAt; }
            set { _startsAt = value; }
        }
		[JsonIgnore]
		public float FinishesAt
        {
            get { return _finishesAt; }
            set { _finishesAt = value; }
        }

		[JsonIgnore]
		public TrackMedia Previous
        {
            get { return _previous; }
            set { _previous = value; }
        }

		[JsonIgnore]
		public TrackMedia Next
        {
            get { return _next; }
            set { _next = value; }
        }
		[JsonIgnore]
		public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

		
		public bool IsLast()
        {
            return _next == null;
        }

		[JsonIgnore]
		public int Progress
        {
            get { return (int)_progress; }
            set { _progress = value; }
        }

		[JsonIgnore]
		public bool IsCompleted
		{
			get { return _isCompleted; }
			set { _isCompleted = value; }
		}

	}
}
