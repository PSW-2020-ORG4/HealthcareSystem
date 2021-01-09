using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class TriggerTime
    {
        [Key]
        public int Id { get; set; }

        public DateTime TriggerDate { get; set;}
    }
}
