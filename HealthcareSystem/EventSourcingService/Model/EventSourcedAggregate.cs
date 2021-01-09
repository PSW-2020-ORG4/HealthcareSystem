using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public abstract class EventSourcedAggregate
    {
        [Key]
        public int Id { get; set; }

        public List<DomainEvent> Changes { get; private set; }
        
        public int Version { get; protected set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);
    }
}
