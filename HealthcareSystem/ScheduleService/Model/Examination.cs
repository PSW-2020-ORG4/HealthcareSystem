using ScheduleService.CustomException;
using ScheduleService.Model.Memento;
using System;

namespace ScheduleService.Model
{
    public class Examination : IOriginator<ExaminationMemento>
    {
        private int Id { get; }
        private Appointment Appointment { get; }
        private ExaminationType ExaminationType { get; }
        private ExaminationStatus ExaminationStatus { get; set; }
        private Patient Patient { get; }
        private Doctor Doctor { get; }
        private Room Room { get; }

        public Examination(ExaminationMemento memento)
        {
            Id = memento.Id;
            Appointment = new Appointment(memento.Appointment);
            ExaminationStatus = memento.ExaminationStatus;
            ExaminationType = memento.ExaminationType;
            Patient = memento.Patient;
            Doctor = memento.Doctor;
            Room = memento.Room;
        }

        public Examination(Appointment appointment, Patient patient, Doctor doctor, Room room, int id = 0)
        {
            Id = id;
            Appointment = appointment;
            ExaminationType = ExaminationType.Examination;
            ExaminationStatus = ExaminationStatus.Created;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public Examination(DateTime dateTime, Patient patient, Doctor doctor, Room room, int id = 0)
        {
            Id = id;
            Appointment = new Appointment(dateTime);
            ExaminationType = ExaminationType.Examination;
            ExaminationStatus = ExaminationStatus.Created;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public bool IsAvailable()
        {
            return Patient.IsAvailable(Appointment)
                && Doctor.IsAvailable(Appointment)
                && Room.IsAvailable(Appointment);
        }

        public void Cancel(DateTime timeLimit)
        {
            if (ExaminationStatus == ExaminationStatus.Finished)
                throw new ValidationException("Finished examinations cannot be canceled.");
            if (ExaminationStatus == ExaminationStatus.Canceled)
                throw new ValidationException("Examination is already canceled.");
            if (IsBefore(timeLimit))
                throw new ValidationException("The limit limit for canceling the examination has passed.");
            ExaminationStatus = ExaminationStatus.Canceled;
        }

        public bool IsBefore(DateTime timeLimit)
        {
            return Appointment.Value <= timeLimit;
        }

        public ExaminationMemento GetMemento()
        {
            return new ExaminationMemento()
            {
                Id = Id,
                Appointment = Appointment.Value,
                ExaminationStatus = ExaminationStatus,
                ExaminationType = ExaminationType,
                Doctor = Doctor,
                Patient = Patient,
                Room = Room
            };
        }

        private void Validate()
        {
            if (ExaminationType == ExaminationType.Examination && Room.RoomType != RoomType.Examination)
                throw new ValidationException("Examinations have to be performed in examination rooms.");
            if (ExaminationType == ExaminationType.Surgery && Room.RoomType != RoomType.Surgery)
                throw new ValidationException("Surgeries have to be performed in operating rooms.");
        }
    }
}
