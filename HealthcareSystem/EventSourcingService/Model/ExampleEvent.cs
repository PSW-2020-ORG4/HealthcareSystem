using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class ExampleEvent : DomainEvent
    {
        public string Data { get; set; }
    }
}
