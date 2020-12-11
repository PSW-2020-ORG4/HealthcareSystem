﻿using Backend.Model.DTO;
using GraphicalEditorServer.DTO;
using Model.PerformingExamination;

namespace GraphicalEditorServer.Mappers
{
    public class ExaminationMapper
    {
        public static Examination ExmainationDTO_To_Examination(ExaminationDTO examinationDTO)
        {
            return new Examination(examinationDTO.DateTime, examinationDTO.DoctorJmbg, examinationDTO.RoomId, examinationDTO.PatientCardId);
        }

        public static ExaminationDTO Exmaination_To_ExaminationDTO(Examination examination)
        {
            return new ExaminationDTO(examination.DateAndTime, examination.DoctorJmbg, examination.IdRoom, examination.IdPatientCard);
        }
        /*public static AppointmentSearchWithPrioritiesDTO FrontAppointmentSearchDTO_To_AppointmentSearchWithPrioritiesDTO(FrontAppointmentSearchDTO fas)
        {
            return new AppointmentSearchWithPrioritiesDTO(new BasicAppointmentSearchDTO(fas.PatientCardId, fas.DoctorJmbg, fas.RequiredEquipmentTypes, fas.EarliestDateTime, fas.LatestDateTime), fas.Priority, fas.SpecialtyId);
        }*/

    }
}
