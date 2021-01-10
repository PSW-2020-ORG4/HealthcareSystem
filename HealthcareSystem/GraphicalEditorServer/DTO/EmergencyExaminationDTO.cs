using Backend.Model.PerformingExamination;
using GraphicalEditorServer.Mappers;

namespace GraphicalEditorServer.DTO
{
    public class EmergencyExaminationDTO
    {
        public ExaminationDTO UnchangedExamination { get; private set; }
        public ExaminationDTO ShiftedExamination { get; private set; }

        public EmergencyExaminationDTO() { }

        public EmergencyExaminationDTO(Examination unchangedExamination, Examination shiftedExamination)
        {
            UnchangedExamination = ExaminationMapper.Examination_To_ExaminationDTO(unchangedExamination);
            ShiftedExamination = ExaminationMapper.Examination_To_ExaminationDTO(shiftedExamination);
        }
    }
}
