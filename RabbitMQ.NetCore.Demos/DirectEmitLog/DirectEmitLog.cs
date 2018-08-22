using RabbitMQ.Client;
using System;
using System.Text;

namespace DirectEmitLog
{
    class DirectEmitLog
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
                    channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

                    Console.WriteLine("DirectEmitLog connected! Please input the logs or press [exit] to exit.");

                    string input;
                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);

                        var severity = GetSeverity();
                        Console.Write("{0}", severity);

                        //Publish messages
                        channel.BasicPublish("direct_logs", severity, null, sendBytes);

                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }

        public static string GetSeverity()
        {
            Random rd = new Random();
            var rdResult = rd.Next(1, 4);
            switch (rdResult)
            {
                case 1:
                    return "info";
                case 2:
                    return "warning";
                default:
                    return "error";
            }
        }
    }
}
