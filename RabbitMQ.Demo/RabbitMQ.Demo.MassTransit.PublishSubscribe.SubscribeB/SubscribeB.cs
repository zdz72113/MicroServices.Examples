using MassTransit;
using RabbitMQ.Demo.MassTransit.Message;
using System;
using System.Threading.Tasks;
namespace RabbitMQ.Demo.MassTransit.PublishSubscribe.SubscribeB
{
    class SubscribeB
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

                cfg.ReceiveEndpoint(host, "rabbitmq.demo.masstransit.publishsubscribe_B", e =>
                {
                    e.Consumer<DemoMessageConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            bus.Stop();
        }
    }

    public class DemoMessageConsumer : IConsumer<DemoMessage>
    {
        public async Task Consume(ConsumeContext<DemoMessage> context)
        {
            await Console.Out.WriteLineAsync($"SubscribeB Receive message: {context.Message.Id} {context.Message.Text} {context.Message.CurrentDateTime}");
        }
    }
}
