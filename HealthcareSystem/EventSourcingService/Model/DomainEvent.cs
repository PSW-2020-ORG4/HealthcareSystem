using System;
using System.ComponentModel.DataAnnotations;
using EventSourcingService.Model.EventTypes;

namespace EventSourcingService.Model
{
    public class DomainEvent
    {

        [Key]
        public int Id { get; set; }

        public TriggerTime TriggerTime { get; set; }

        public EventType Type { get; set; }

        public EventData EventData { get; set; }
    }

}
