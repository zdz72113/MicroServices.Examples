using EasyNetQ;
using RabbitMQ.Demo.EasyNetQ.Message;
using System;

namespace RabbitMQ.Demo.EasyNetQ.RequestResponse.Response
{
    class Response
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                bus.Respond<DemoMessage, DemoResponse>(request => new DemoResponse { Result = string.Format("Got Message from request: {0} {1} {2}", request.Id, request.Text, request.CurrentDateTime) });
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
