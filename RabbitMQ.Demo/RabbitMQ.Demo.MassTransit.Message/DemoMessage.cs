using System;

namespace RabbitMQ.Demo.MassTransit.Message
{
    public class DemoMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
