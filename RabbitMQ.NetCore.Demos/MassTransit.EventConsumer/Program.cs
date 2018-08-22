using Masstransit.RabbitMQ.Extensions;
using System;

namespace MassTransit.EventConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EventConsumer");
            var bus = BusCreator.CreateBus((cfg, host) => cfg.ReceiveEndpoint(host, "MassTransit.Event:EventA", e =>
            {
                e.Consumer<EventConsumerA>();
            }));

            bus.Start();

            Console.WriteLine("Listening for Greeting events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
