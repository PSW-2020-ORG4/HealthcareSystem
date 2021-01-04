using PatientService.DTO;
using PatientService.Model;
using PatientService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Mapper
{
    public static class ExaminationMapper
    {
        public static ExaminationDTO ToExaminationDTO(this Examination examination)
        {
            ExaminationMemento memento = examination.GetMemento();
            return new ExaminationDTO()
            {
                DateAndTime = memento.DateAndTime,
                Anamnesis = memento.Anamnesis,
                DoctorJmbg = memento.DoctorJmbg,
                DoctorName = memento.DoctorName,
                DoctorSurname = memento.DoctorSurname,
                Id = memento.Id
            };
        }
    }
}
