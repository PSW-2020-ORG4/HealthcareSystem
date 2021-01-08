using System;

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

        public Examination(int id, Appointment appointment, ExaminationType examinationType, 
                           ExaminationStatus examinationStatus, Patient patient, Doctor doctor, Room room)
        {
            Id = id;
            Appointment = appointment;
            ExaminationType = examinationType;
            ExaminationStatus = examinationStatus;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public bool IsAvailable()
        {
            return Patient.IsAvailable(Appointment) && Doctor.IsAvailable(Appointment) && Room.IsAvailable(Appointment);
        }
    }
}
