using ScheduleService.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Patient
    {
        public Jmbg Jmbg { get; }
        public string Name { get; }
        public string Surname { get; }
        private IEnumerable<Appointment> UnavailableAppointments { get; }

        public Patient(string jmbg, string name, string surname)
        {
            Jmbg = new Jmbg(jmbg);
            Name = name;
            Surname = surname;
            UnavailableAppointments = new List<Appointment>();
            Validate();
        }

        public Patient(string jmbg, string name, string surname, IEnumerable<DateTime> unavailableAppointments)
        {
            Jmbg = new Jmbg(jmbg);
            Name = name;
            Surname = surname;
            if (unavailableAppointments is null)
                UnavailableAppointments = new List<Appointment>();
            else
                UnavailableAppointments = unavailableAppointments.Select(a => new Appointment(a));
            Validate();
        }

        public bool IsAvailable(Appointment appointment)
        {
            return !UnavailableAppointments.Contains(appointment);
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Patient name cannot be empty.");
            if (String.IsNullOrWhiteSpace(Surname))
                throw new ValidationException("Patient surname cannot be empty.");
        }
    }
}
