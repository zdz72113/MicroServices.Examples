using EasyNetQ;
using RabbitMQ.Demo.EasyNetQ.Message;
using System;

namespace RabbitMQ.Demo.EasyNetQ.PublishSubscribe.Publish
{
    class Publish
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                var input = "EasyNetQ.PublishSubscribe.Demo";

                bus.Publish(new DemoMessage
                {
                    Id = 1,
                    Text = input,
                    CurrentDateTime = DateTime.Now
                });
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
