using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
    [Serializable]
    public abstract class TrackMedia: Entity
    {
		[JsonIgnore]
		protected int _progress;
        protected int _startsAt;
        protected int _finishesAt;
        protected bool _isLocked;
        protected TrackMedia _previous;
        protected TrackMedia _next;

		//@Override//todo
		//public boolean equals(Object obj)
		//{
		//    if (obj instanceof Entity) {
		//        return id.equals(((Entity)obj).id);
		//    } else return super.equals(obj);
		//}

		//  public abstract int GetDuration();

		[JsonIgnore]
		public abstract int Duration { get; set; }


		public  int StartsAt
        {
            get { return _startsAt; }
            set { _startsAt = value; }
        }


		[JsonIgnore]
		public int FinishesAt
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

		public int Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }
 
    }
}
