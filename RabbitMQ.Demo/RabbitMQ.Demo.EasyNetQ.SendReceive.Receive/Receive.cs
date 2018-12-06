using EasyNetQ;
using RabbitMQ.Demo.EasyNetQ.Message;
using System;

namespace RabbitMQ.Demo.EasyNetQ.SendReceive.Receive
{
    class Receive
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=192.168.17.129"))
            {
                bus.Receive<DemoMessage>("rabbitmq.demo.easynetq.sendreceive", 
                    message => Console.WriteLine("Message: {0} {1} {2}", message.Id, message.Text, message.CurrentDateTime));

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
