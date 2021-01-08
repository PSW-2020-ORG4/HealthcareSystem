using System;

namespace ScheduleService.Model
{
    public class Examination
    {
        private int Id { get; }
        private Appointment Appointment { get; }
        private ExaminationType ExaminationType { get; }
        private ExaminationStatus ExaminationStatus { get; set; }
        private Patient Patient { get; }
        private Doctor Doctor { get; }
        private Room Room { get; }

        public Examination(Appointment appointment, ExaminationType examinationType, 
                           ExaminationStatus examinationStatus, Patient patient, Doctor doctor, Room room, int id = 0)
        {
            Id = id;
            Appointment = appointment;
            ExaminationType = examinationType;
            ExaminationStatus = examinationStatus;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public Examination(DateTime dateTime, Patient patient, Doctor doctor, Room room)
        {
            Id = 0;
            Appointment = new Appointment(dateTime);
            ExaminationType = ExaminationType.General;
            ExaminationStatus = ExaminationStatus.Created;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public bool IsAvailable()
        {
            return Patient.IsAvailable(Appointment) && Doctor.IsAvailable(Appointment) && Room.IsAvailable(Appointment);
        }

        public void Cancel()
        {
            ExaminationStatus = ExaminationStatus.Canceled;
        }
    }
}
