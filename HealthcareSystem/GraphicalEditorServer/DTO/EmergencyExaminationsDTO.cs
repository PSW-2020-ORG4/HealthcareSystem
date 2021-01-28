using System.Collections.Generic;

namespace GraphicalEditorServer.DTO
{
    public class EmergencyExaminationsDTO
    {
        public List<EmergencyExaminationDTO> EmergencyExaminations { get; private set; }
        public bool Shifted { get; private set; }

        public EmergencyExaminationsDTO(List<EmergencyExaminationDTO> emergencyExaminations, bool shifted)
        {
            EmergencyExaminations = emergencyExaminations;
            Shifted = shifted;
        }
    }
}
