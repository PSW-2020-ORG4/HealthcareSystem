using GraphicalEditorServer.DTO;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class EmergencyExaminationMapper
    {
        public static EmergencyExaminationDTO Examinations_To_EmergencyExaminationDTO(
            List<Examination> unchangedExaminations, List<Examination> shiftedExaminations, bool shifted)
        {
            List<ExaminationDTO> unchangedExaminationsDTO = new List<ExaminationDTO>();
            unchangedExaminations.ForEach(e => unchangedExaminationsDTO.Add(ExaminationMapper.Examination_To_ExaminationDTO(e)));

            List<ExaminationDTO> shiftedExaminationsDTO = new List<ExaminationDTO>();
            shiftedExaminations.ForEach(e => shiftedExaminationsDTO.Add(ExaminationMapper.Examination_To_ExaminationDTO(e)));

            EmergencyExaminationDTO emergencyExaminationDTO =
                new EmergencyExaminationDTO(unchangedExaminationsDTO, shiftedExaminationsDTO, shifted);

            return emergencyExaminationDTO;
        }

        public static EmergencyExaminationDTO Examination_To_EmergencyExaminationDTO(Examination examination)
        {
            List<Examination> e = new List<Examination>();
            e.Add(examination);
            return Examinations_To_EmergencyExaminationDTO(e, e, false); 
        }
    }
}
