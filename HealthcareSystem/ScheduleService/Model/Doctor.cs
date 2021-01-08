using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Doctor
    {
        private Jmbg Jmbg { get; }
        private IEnumerable<Appointment> UnavailableAppointments { get; }

        public bool IsAvailable(Appointment appointment)
        {
            return !UnavailableAppointments.Contains(appointment);
        }
    }
}
