using MassTransit;
using RabbitMQ.Demo.MassTransit.Message;
using System;

namespace RabbitMQ.Demo.MassTransit.SendReceive.Send
{
    class Send
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

            var uri = new Uri("rabbitmq://192.168.17.129/rabbitmq.demo.masstransit.sendreceive");
            var endPoint = bus.GetSendEndpoint(uri).GetAwaiter().GetResult();
            var message = new DemoMessage
            {
                Id = 1,
                Text = "MassTransit.SendReceive.Demo",
                CurrentDateTime = DateTime.Now
            };
            endPoint.Send(message).Wait();
            //EndpointConvention.Map<DemoMessage>(new Uri("rabbitmq://192.168.17.129/rabbitmq.demo.masstransit.sendreceive"));
            //var message = new DemoMessage
            //{
            //    Id = 1,
            //    Text = "MassTransit.SendReceive.Demo",
            //    CurrentDateTime = DateTime.Now
            //};
            //bus.Send(message).Wait();

            bus.Stop();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
