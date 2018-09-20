using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Baron.Entity
{
   public class TrackAudio: TrackMedia
    {
		[JsonProperty(PropertyName = "duration")]
		private int _duration;
        private bool _isCompleted;

		//[JsonIgnore]
		public override string Id
        {
            get { return _id != null ? _id.ToLower() : null; }
        }

		//[JsonIgnore]
		public override int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
		
		//[JsonIgnore]
		public bool Completed
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

    }
}
