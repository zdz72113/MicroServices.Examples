using RabbitMQ.Client;
using System;
using System.Text;

namespace TopicEmitLog
{
    class TopicEmitLog
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
                    channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

                    Console.WriteLine("DirectEmitLog connected! Please input the logs or press [exit] to exit.");

                    string input;
                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);

                        var severity = GetSeverity();
                        Console.WriteLine("{0}", severity);

                        //Publish messages
                        channel.BasicPublish("topic_logs", severity, null, sendBytes);

                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }

        public static string GetSeverity()
        {
            Random rd = new Random();
            var rdResult = rd.Next(1, 6);
            switch (rdResult)
            {
                case 1:
                    return "machine1.info";
                case 2:
                    return "machine2.info";
                case 3:
                    return "machine1.error";
                case 4:
                    return "machine2.error";
                default:
                    return "anonymous.info";
            }
        }
    }
}
