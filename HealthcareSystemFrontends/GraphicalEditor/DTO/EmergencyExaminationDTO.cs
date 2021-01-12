using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class EmergencyExaminationDTO
    {
        public ExaminationDTO UnchangedExamination { get; set; }
        public ExaminationDTO ShiftedExamination { get; set; }

        public EmergencyExaminationDTO() { }

        public EmergencyExaminationDTO(ExaminationDTO unchangedExamination, ExaminationDTO shiftedExamination)
        {
            UnchangedExamination = unchangedExamination;
            ShiftedExamination = shiftedExamination;
        }
    }
}
