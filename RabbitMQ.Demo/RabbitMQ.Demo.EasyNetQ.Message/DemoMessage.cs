using System;

namespace RabbitMQ.Demo.EasyNetQ.Message
{
    public class DemoMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
