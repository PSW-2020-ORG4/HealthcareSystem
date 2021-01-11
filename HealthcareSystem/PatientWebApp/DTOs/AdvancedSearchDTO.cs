using System;

namespace ScheduleService.DTO
{
    public class AdvancedSearchDTO
    {
        public BasicSearchDTO InitialParameters { get; set; }
        public int Priority { get; set; }
        public int SpecialtyId { get; set; }
    }
}
