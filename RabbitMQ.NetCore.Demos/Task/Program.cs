using RabbitMQ.Client;
using System;
using System.Text;

namespace Task
{
    class Program
    {
        public static void Main()
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
                    //Declare a queue named "task_queue"
                    //set durabl=true to persist queue
                    channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    Console.WriteLine("Connected! Please input the send messages or press [exit] to exit.");

                    string input;

                    var properties = channel.CreateBasicProperties();
                    //set persitenet=ture to persist message
                    properties.Persistent = true;

                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);

                        //Publish messages
                        channel.BasicPublish("", "task_queue", properties, sendBytes);

                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }
    }
}
