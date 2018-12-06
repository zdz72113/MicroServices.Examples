using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Demo.MassTransit.Message
{
    public class DemoResponse
    {
        public int RequestId { get; set; }
        public int ResultCode { get; set; }
    }
}
