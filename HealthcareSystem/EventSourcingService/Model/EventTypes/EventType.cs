using System.ComponentModel.DataAnnotations;

namespace EventSourcingService.Model.EventTypes
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}