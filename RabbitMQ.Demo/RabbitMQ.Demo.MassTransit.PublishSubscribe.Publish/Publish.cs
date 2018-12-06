using MassTransit;
using RabbitMQ.Demo.MassTransit.Message;
using System;

namespace RabbitMQ.Demo.MassTransit.PublishSubscribe.Publish
{
    class Publish
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://192.168.17.129"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            bus.Start();

            var message = new DemoMessage
            {
                Id = 1,
                Text = "MassTransit.SendReceive.Demo",
                CurrentDateTime = DateTime.Now
            };
            bus.Publish(message).Wait();

            bus.Stop();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
