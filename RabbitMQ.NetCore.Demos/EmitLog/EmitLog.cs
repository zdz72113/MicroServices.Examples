using RabbitMQ.Client;
using System;
using System.Text;

namespace EmitLog
{
    class EmitLog
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
                    channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    Console.WriteLine("EmitLog connected! Please input the logs or press [exit] to exit.");

                    string input;
                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);

                        //Publish messages
                        channel.BasicPublish("logs", "", null, sendBytes);

                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }
    }
}
