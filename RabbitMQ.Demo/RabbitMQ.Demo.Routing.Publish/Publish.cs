using RabbitMQ.Client;
using System;
using System.Linq;

namespace RabbitMQ.Demo.Routing.Publish
{
    class Publish
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.17.129" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

                    var severity = (args.Length > 0) ? args[0] : "info";
                    var message = (args.Length > 1)
                                  ? string.Join(" ", args.Skip(1).ToArray())
                                  : "Hello World!";

                    var body = System.Text.Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
