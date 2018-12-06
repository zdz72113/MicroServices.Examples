using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Demo.MassTransit.Message
{
    public class DemoRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
