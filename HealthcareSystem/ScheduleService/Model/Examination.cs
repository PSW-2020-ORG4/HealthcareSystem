using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Examination
    {
        private int Id { get; }
        private Appointment Appointment { get; }
        private ExaminationType ExaminationType { get; }
        private ExaminationStatus ExaminationStatus { get; }
        private Patient Patient { get; }
        private Doctor Doctor { get; }
        private Room Room { get; }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }
    }
}
