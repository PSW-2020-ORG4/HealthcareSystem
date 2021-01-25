using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class GenderStatisticDTO
    {
        public int NumberOfWomen { get; set; }
        public int NumberOfMen { get; set; }
        public int TotalNumber { get; set; }
    }
}
