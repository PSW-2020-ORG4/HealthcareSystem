using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class EventData
    {
        [Key]
        public int Id { get; set; }
        public string Data { get; set; }
    }
}
