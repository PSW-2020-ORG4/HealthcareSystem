using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class EmergencyExaminationDTO
    {
        public List<ExaminationDTO> UnchangedExaminations { get; private set; }
        public List<ExaminationDTO> ShiftedExaminations { get; private set; }

        public bool Shifted { get; private set; }

        public EmergencyExaminationDTO() { }

        public EmergencyExaminationDTO(List<ExaminationDTO> unchangedExaminations, List<ExaminationDTO> shiftedExaminations, bool shifted)
        {
            UnchangedExaminations = unchangedExaminations;
            ShiftedExaminations = shiftedExaminations;
            Shifted = shifted;
        }
    }
}
