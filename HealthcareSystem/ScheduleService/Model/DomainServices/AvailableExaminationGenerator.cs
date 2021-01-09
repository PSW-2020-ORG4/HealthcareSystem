using System;
using System.Collections.Generic;

namespace ScheduleService.Model.DomainServices
{
    public class AvailableExaminationGenerator : IAvailableExaminationGenerator
    {
        public IEnumerable<Examination> Generate(ExaminationGeneratorDTO examinationDTO)
        {
            ICollection<Examination> examinations = new List<Examination>();
            ICollection<Appointment> appointments = GenerateAppointments(examinationDTO.StartDate,
                                                                         examinationDTO.EndDate);

            foreach (Appointment app in appointments)
                foreach (Room room in examinationDTO.Rooms)
                {
                    Examination examination = new Examination(app,
                                                              examinationDTO.Patient,
                                                              examinationDTO.Doctor,
                                                              room);

                    if (examination.IsAvailable())
                        examinations.Add(examination);
                }

            return examinations;
        }

        private ICollection<Appointment> GenerateAppointments(DateTime startDate, DateTime endDate)
        {
            ICollection<Appointment> appointments = new List<Appointment>();
            Appointment startAppointment = new Appointment(startDate);
            Appointment endAppointment = new Appointment(endDate);

            for (Appointment app = startAppointment; app.CompareTo(endAppointment) < 0; app = app.NextAppointment())
                appointments.Add(app);

            return appointments;
        }
    }
}
