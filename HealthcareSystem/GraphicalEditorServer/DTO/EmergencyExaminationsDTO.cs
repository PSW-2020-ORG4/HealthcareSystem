using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class EmergencyExaminationsDTO
    {
        public List<EmergencyExaminationDTO> EmergencyExaminations { get; private set; }
        public bool Shifted;

        public EmergencyExaminationsDTO(List<EmergencyExaminationDTO> emergencyExaminations, bool shifted)
        {
            EmergencyExaminations = emergencyExaminations;
            Shifted = shifted;
        }
    }
}
