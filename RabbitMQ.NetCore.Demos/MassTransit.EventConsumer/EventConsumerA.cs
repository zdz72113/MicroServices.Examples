using MassTransit.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.EventConsumer
{
    public class EventConsumerA : IConsumer<EventA>
    {
        public async Task Consume(ConsumeContext<EventA> context)
        {
            await Console.Out.WriteLineAsync($"receive greeting eventA: id {context.Message.Id}");
        }
    }
}
