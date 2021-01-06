using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model.DomainServices
{
    public class ExaminationGeneratorDTO
    {
        public Patient Patient { get; }
        public Doctor Doctor { get; }
        public IEnumerable<Room> Rooms { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}
