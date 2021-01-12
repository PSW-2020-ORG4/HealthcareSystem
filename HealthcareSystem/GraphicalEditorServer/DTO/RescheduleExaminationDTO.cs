using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class RescheduleExaminationDTO
    {
        public ExaminationDTO ExaminationForScheduleDTO { get; set; }
        public ExaminationDTO ExaminationForRescheduleDTO { get;set; }
        public ExaminationDTO ShiftedExaminationDTO { get; set; }

        public RescheduleExaminationDTO() { }

        public RescheduleExaminationDTO(ExaminationDTO examinationForScheduleDTO, ExaminationDTO examinationForRescheduleDTO, ExaminationDTO shiftedExaminationDTO)
        {
            ExaminationForScheduleDTO = examinationForScheduleDTO;
            ExaminationForRescheduleDTO = examinationForRescheduleDTO;
            ShiftedExaminationDTO = shiftedExaminationDTO;
        }
    }
}
