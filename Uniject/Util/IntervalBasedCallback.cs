using System;
using Uniject;

namespace UnijectUtil
{
    public class IntervalBasedCallback : TestableComponent
    {
        private readonly ITime time;

        private float timeRemaining;

        public IntervalBasedCallback(TestableGameObject obj, ITime time) : base(obj)
        {
            this.time = time;
        }

        public Action callback { get; set; }
        public TimeSpan interval { get; set; }

        public override void Update()
        {
            timeRemaining -= time.DeltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = (float) interval.TotalSeconds;
                callback.Invoke();
            }
        }
    }
}