using ScheduleService.DTO;
using ScheduleService.Model;

namespace ScheduleService.Mapper
{
    public static class ExaminationMapper
    {
        public static ScheduledExaminationDTO ToScheduledExaminationDTO(this Examination examination)
        {
            var memento = examination.GetMemento();
            return new ScheduledExaminationDTO()
            {
                Id = memento.Id,
                StartTime = memento.Appointment,
                DoctorJmbg = memento.Doctor.Jmbg.Value,
                DoctorName = memento.Doctor.Name,
                DoctorSurname = memento.Doctor.Surname,
                PatientJmbg = memento.Patient.Jmbg.Value,
                PatientName = memento.Patient.Name,
                PatientSurname = memento.Patient.Surname,
                ExaminationStatus = memento.ExaminationStatus.ToString(),
                ExaminationType = memento.ExaminationType.ToString(),
                RoomId = memento.Room.Id
            };
        }
        public static UnscheduledExaminationDTO ToUnscheduledExaminationDTO(this Examination examination)
        {
            var memento = examination.GetMemento();
            return new UnscheduledExaminationDTO()
            {
                StartTime = memento.Appointment,
                DoctorJmbg = memento.Doctor.Jmbg.Value,
                DoctorName = memento.Doctor.Name,
                DoctorSurname = memento.Doctor.Surname,
                PatientJmbg = memento.Patient.Jmbg.Value,
                PatientName = memento.Patient.Name,
                PatientSurname = memento.Patient.Surname,
                ExaminationType = memento.ExaminationType.ToString(),
                RoomId = memento.Room.Id
            };
        }
    }
}
