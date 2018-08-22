using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TopicReceiveLog
{
    class TopicReceiveLog
    {
        public static int Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.174.137"
            };

            //Create a connection
            using (var connection = factory.CreateConnection())
            {
                //Create a channel
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

                    var queueName = channel.QueueDeclare().QueueName;

                    if (args.Length < 1)
                    {
                        Console.Error.WriteLine("Usage: {0} [binding_key...]", Environment.GetCommandLineArgs()[0]);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                        return 1;
                    }

                    foreach (var bindingKey in args)
                    {
                        channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
                    }

                    Console.WriteLine(" [*] Waiting for logs.");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    return 0;
                }
            }
        }
    }
}
