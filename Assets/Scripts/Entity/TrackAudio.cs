using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Entity
{
   public class TrackAudio: TrackMedia
    {
        private int _duration;
        private bool _isCompleted;

      
        public override string Id
        {
            get { return _id != null ? _id.ToLower() : null; }
        }

        public override int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        //public void setDuration(int duration)
        //{this.duration = duration;}


        public bool Completed
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

       // public bool isCompleted()
        //{return isCompleted;}

        //public void setCompleted(boolean completed)
        //{isCompleted = completed;}
    }
}
