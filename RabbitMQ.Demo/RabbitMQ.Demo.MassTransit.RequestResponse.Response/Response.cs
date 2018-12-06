using MassTransit;
using RabbitMQ.Demo.MassTransit.Message;
using System;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.MassTransit.RequestResponse.Response
{
    class Response
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

                cfg.ReceiveEndpoint(host, "rabbitmq.demo.masstransit.requestresponse", e =>
                {
                    e.Consumer<DemoRequestConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine(" Press [enter] to exit Response console.");
            Console.ReadLine();

            bus.Stop();
        }
    }

    public class DemoRequestConsumer : IConsumer<DemoRequest>
    {
        public async Task Consume(ConsumeContext<DemoRequest> context)
        {
            await Console.Out.WriteLineAsync($"Received message: Id={context.Message.Id}, Content={context.Message.Content}");

            var response = new DemoResponse
            {
                ResultCode = 200,
                RequestId = context.Message.Id
            };

            Console.WriteLine($"Response message: Code={response.ResultCode}, RequestId={response.RequestId}");
            await context.RespondAsync(response);
        }
    }
}
