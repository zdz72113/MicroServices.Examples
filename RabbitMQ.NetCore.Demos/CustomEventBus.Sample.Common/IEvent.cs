using System;

namespace CustomEventBus.Sample.Common
{
    public interface IEvent
    {
         Guid Id { get; }

         DateTime Timestamp { get; }
    }
}
