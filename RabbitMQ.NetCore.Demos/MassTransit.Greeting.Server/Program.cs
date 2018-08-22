using Masstransit.RabbitMQ.Extensions;
using System;

namespace MassTransit.Greeting.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Greeting.Receiver";

            var bus = BusCreator.CreateBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.GreetingQueue, e =>
                {
                    e.Consumer<GreetingConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Greeting commands.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
