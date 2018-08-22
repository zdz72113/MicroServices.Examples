using Masstransit.RabbitMQ.Extensions;
using MassTransit.Event;
using System;

namespace MassTransit.EventPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "EventPublisher";

            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            var bus = BusCreator.CreateBus();

            //bus.Start();

            while (Console.ReadLine().ToLower() != "q")
            {
                bus.Publish<EventA>(new EventA() { Id = Guid.NewGuid(), DateTime = DateTime.Now });
            }

            //bus.Stop();
        }
    }
}
