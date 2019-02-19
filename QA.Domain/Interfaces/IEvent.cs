using System;

namespace QA.Domain.Events
{
    public interface IEvent
    {
        DateTime DateTimeStamp { get; }
    }
}
