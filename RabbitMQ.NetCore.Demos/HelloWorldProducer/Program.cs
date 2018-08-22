using RabbitMQ.Client;
using System;
using System.Text;

namespace HelloWorldProducer
{
    class Program
    {
        public static void Main(string[] args)
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
                    //Declare a queue named "hello"
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    Console.WriteLine("Connected! Please input the send messages or press [exit] to exit.");

                    string input;
                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);

                        //Publish messages
                        channel.BasicPublish("", "hello", null, sendBytes);

                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }
    }
}
