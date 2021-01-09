using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model.Memento
{
    public class ExaminationMemento : IMemento
    {
        public int Id { get; set; }
        public DateTime Appointment { get; set; }
        public ExaminationType ExaminationType { get; set; }
        public ExaminationStatus ExaminationStatus { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public ExaminationMemento()
        {
            Id = 0;
            ExaminationStatus = ExaminationStatus.Created;
            ExaminationType = ExaminationType.Examination;
        }
    }
}
