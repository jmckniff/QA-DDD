using System;

namespace QA.Framework
{
    public class Event : IHappened
    {
        public DateTime HappenedDateTime { get; }

        public Event()
        {
            HappenedDateTime = DateTime.Now;
        }
    }
}
