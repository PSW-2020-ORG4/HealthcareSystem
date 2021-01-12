using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class EmergencyExaminationsDTO
    {
        public List<EmergencyExaminationDTO> EmergencyExaminations { get; set; }
        public bool Shifted { get; set; }

        public EmergencyExaminationsDTO(List<EmergencyExaminationDTO> emergencyExaminations, bool shifted)
        {
            EmergencyExaminations = emergencyExaminations;
            Shifted = shifted;
        }
    }
}
