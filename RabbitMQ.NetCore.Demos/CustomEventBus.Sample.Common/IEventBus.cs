using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEventBus.Sample.Common
{
    public interface IEventBus : IEventPublisher, IEventSubscriber
    {

    }
}
