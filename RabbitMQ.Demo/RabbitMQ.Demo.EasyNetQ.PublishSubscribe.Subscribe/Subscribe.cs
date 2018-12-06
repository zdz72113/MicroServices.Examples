using EasyNetQ;
using RabbitMQ.Demo.EasyNetQ.Message;
using System;

namespace RabbitMQ.Demo.EasyNetQ.PublishSubscribe.Subscribe
{
    class Subscribe
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                bus.Subscribe<DemoMessage>("rabbitmq.demo.easynetq.publishsubscribe.subscriptionid_1", HandleDemoMessage1);

                bus.Subscribe<DemoMessage>("rabbitmq.demo.easynetq.publishsubscribe.subscriptionid_2", HandleDemoMessage2);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        static void HandleDemoMessage1(DemoMessage demoMessage)
        {
            Console.WriteLine("Got Message from subscription 1: {0} {1} {2}", demoMessage.Id, demoMessage.Text, demoMessage.CurrentDateTime);
        }

        static void HandleDemoMessage2(DemoMessage demoMessage)
        {
            Console.WriteLine("Got Message from subscription 2: {0} {1} {2}", demoMessage.Id, demoMessage.Text, demoMessage.CurrentDateTime);
        }
    }
}
