using MassTransit.Greeting.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.Greeting.Receiver
{
    public class GreetingConsumer : IConsumer<GreetingCommandA>
    {
        public async Task Consume(ConsumeContext<GreetingCommandA> context)
        {
            await Console.Out.WriteLineAsync($"receive greeting commmand: {context.Message.Id},{context.Message.DateTime}");
        }
    }
}
