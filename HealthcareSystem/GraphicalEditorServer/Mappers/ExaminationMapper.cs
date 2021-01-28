using Backend.Model.DTO;
using Backend.Model.PerformingExamination;
using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;

namespace GraphicalEditorServer.Mappers
{
    public class ExaminationMapper
    {
        public static Examination ExmainationDTO_To_Examination(ExaminationDTO examinationDTO)
        {
            return new Examination(examinationDTO.DateTime, examinationDTO.Doctor.Jmbg, examinationDTO.RoomId, examinationDTO.PatientCardId);
        }

        public static ExaminationDTO Examination_To_ExaminationDTO(Examination examination)
        {
            return new ExaminationDTO(examination.DateAndTime, DoctorMapper.DoctorToDoctorDTO(examination.Doctor), examination.IdRoom, examination.IdPatientCard);
        }
        public static AppointmentSearchWithPrioritiesDTO FrontAppointmentSearchDTO_To_AppointmentSearchWithPrioritiesDTO(FrontAppointmentSearchDTO fas)
        {
            return new AppointmentSearchWithPrioritiesDTO(new BasicAppointmentSearchDTO(fas.PatientCardId, fas.DoctorJmbg, fas.RequiredEquipmentTypes, fas.EarliestDateTime, fas.LatestDateTime), fas.Priority, fas.SpecialtyId);
        }

    }
}
