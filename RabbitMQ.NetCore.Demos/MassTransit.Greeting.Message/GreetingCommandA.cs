using System;

namespace MassTransit.Greeting.Message
{
    public class GreetingCommandA
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}
