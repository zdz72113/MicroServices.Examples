using MassTransit;
using RabbitMQ.Demo.MassTransit.Message;
using System;

namespace RabbitMQ.Demo.MassTransit.RequestResponse.Request
{
    class Request
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

            var message = new DemoRequest
            {
                Id = 1,
                Content = "MassTransit.RequestResponse.Demo",
            };

            var address = new Uri("rabbitmq://192.168.17.129/rabbitmq.demo.masstransit.requestresponse");
            var requestTimeout = TimeSpan.FromSeconds(30);
            var client = new MessageRequestClient<DemoRequest, DemoResponse>(bus, address, requestTimeout);
            var result = client.Request(message).GetAwaiter().GetResult();
            Console.WriteLine($"Response message: Code={result.ResultCode}, RequestId={result.RequestId}");

            bus.Stop();

            Console.WriteLine(" Press [enter] to exit request console.");
            Console.ReadLine();
        }
    }
}
