using RabbitMQ.Client;
using System;

namespace RabbitMQ.Demo.HelloWorld
{
    class Send
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.17.129" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "helloworld",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    var body = System.Text.Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                 routingKey: "helloworld",
                                 basicProperties: null,
                                 body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
