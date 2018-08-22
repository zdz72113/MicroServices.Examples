using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEventBus.Sample.Common
{
    public class EventProcessedEventArgs : EventArgs
    {
        public EventProcessedEventArgs(IEvent @event)
        {
            this.Event = @event;
        }

        public IEvent Event { get; }

    }
}
