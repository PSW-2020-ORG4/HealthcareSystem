using System;
using System.Collections.Generic;

namespace ScheduleService.Model.DomainServices
{
    public class ExaminationGeneratorDTO
    {
        public Patient Patient { get; }
        public Doctor Doctor { get; }
        public IEnumerable<Room> Rooms { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public ExaminationGeneratorDTO(Patient patient,
                                       Doctor doctor,
                                       IEnumerable<Room> rooms,
                                       DateTime startDate,
                                       DateTime endDate)
        {
            Patient = patient;
            Doctor = doctor;
            if (rooms is null)
                Rooms = new List<Room>();
            else
                Rooms = rooms;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
