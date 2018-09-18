using System;


namespace Baron.Entity
{
    [Serializable]
    public abstract class TrackMedia: Entity
    {
        [NonSerialized]
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

        public abstract int Duration { get; set; }

        //public int getStartsAt()
        //{return startsAt;}
        //public void setStartsAt(int startsAt)
        //{this.startsAt = startsAt;}

        public  int StartsAt
        {
            get { return _startsAt; }
            set { _startsAt = value; }
        }

       // public int getFinishesAt()
       // {return finishesAt;}

        //public void setFinishesAt(int finishesAt)
        //{this.finishesAt = finishesAt;}

        public int FinishesAt
        {
            get { return _finishesAt; }
            set { _finishesAt = value; }
        }

        public TrackMedia Previous
        {
            get { return _previous; }
            set { _previous = value; }
        }


        // public TrackMedia getPrevious()
        // {return previous;}

        //public void setPrevious(TrackMedia previous)
        //{this.previous = previous;}


        public TrackMedia Next
        {
            get { return _next; }
            set { _next = value; }
        }
        //   public TrackMedia getNext()
        //{return next;}

        //public void setNext(TrackMedia next)
        //{this.next = next;}

        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        //public bool isLocked()
        //{return isLocked;}

        //public void setLocked(boolean locked)
        //{    isLocked = locked;}


       // [NonSerialized]
        public bool IsLast()
        {
            return _next == null;
        }


        public int Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }
        //@JsonIgnore
        //public int getProgress()
        //{//    return progress;//}

        //@JsonIgnore
        //public void setProgress(int progress)
        //{//    this.progress = progress;//}
    }
}
