using Backend.Model.DTO;
using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class ExaminationMappers
    {
        public Examination ExmainationDTO_To_Examination(ExaminationDTO examinationDTO) {
            return new Examination(examinationDTO.DateTime, examinationDTO.DoctorJmbg, examinationDTO.RoomId, examinationDTO.PatientCardId);
        }

        public ExaminationDTO Exmaination_To_ExaminationDTO(Examination examination)
        {
            return new ExaminationDTO(examination.DateAndTime, examination.DoctorJmbg, examination.IdRoom, examination.IdPatientCard);
        }
        public AppointmentSearchWithPrioritiesDTO FrontAppointmentSearchDTO_To_AppointmentSearchWithPrioritiesDTO(FrontAppointmentSearchDTO fas) 
        {
         return new AppointmentSearchWithPrioritiesDTO(new BasicAppointmentSearchDTO(fas.PatientCardId, fas.DoctorJmbg,fas.RequiredEquipmentTypes, fas.EarliestDateTime, fas.LatestDateTime),fas.Priority,fas.SpecialtyId);
        }

    }
}
