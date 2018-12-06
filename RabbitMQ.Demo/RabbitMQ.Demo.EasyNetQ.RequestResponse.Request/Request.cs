using EasyNetQ;
using RabbitMQ.Demo.EasyNetQ.Message;
using System;

namespace RabbitMQ.Demo.EasyNetQ.RequestResponse.Request
{
    class Request
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                var input = "EasyNetQ.PublishSubscribe.Demo";
                var request = new DemoMessage
                {
                    Id = 1,
                    Text = input,
                    CurrentDateTime = DateTime.Now
                };

                var response = bus.Request<DemoMessage, DemoResponse>(request);
                Console.WriteLine(response.Result);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
