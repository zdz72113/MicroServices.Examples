using Masstransit.RabbitMQ.Extensions;
using MassTransit.Greeting.Message;
using System;
using System.Threading.Tasks;

namespace MassTransit.Greeting.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Greeting.Sender";

            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            var bus = BusCreator.CreateBus();
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}{RabbitMqConstants.GreetingQueue}");

            while (Console.ReadLine() != null)
            {
                Task.Run(() => SendCommand(bus, sendToUri)).Wait();
            }

            Console.ReadLine();
        }

        private static async void SendCommand(IBusControl bus, Uri sendToUri)
        {
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            var command = new GreetingCommandA()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now
            };

            await endPoint.Send(command);

            Console.WriteLine($"send command:id={command.Id},{command.DateTime}");
        }
    }
}
