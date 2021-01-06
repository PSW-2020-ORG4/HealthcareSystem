using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.DTO
{
    public class AdvancedSearchDTO
    {
        public BasicSearchDTO InitialParameters { get; set; }
        public SearchPriority Priority { get; set; }
        public int SpecialtyId { get; set; }
    }
}
