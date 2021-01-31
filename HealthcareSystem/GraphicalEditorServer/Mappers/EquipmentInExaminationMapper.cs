using Backend.Model.PerformingExamination;
using GraphicalEditorServer.DTO;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentInExaminationMapper
    {
        public static EquipmentInExaminationDTO EquipmentInExaminationToEquipmentInExaminationDTO(EquipmentInExamination equipmentInExamination)
        {
            EquipmentInExaminationDTO equipmentInExaminationDTO = new EquipmentInExaminationDTO();
            equipmentInExaminationDTO.EquipmentID = equipmentInExamination.EquipmentTypeID;
            equipmentInExaminationDTO.ExaminationId = equipmentInExamination.ExaminationId;
            return equipmentInExaminationDTO;
        }

        public static EquipmentInExamination EquipmentInExaminationDTOToEquipmentInExamination(EquipmentInExaminationDTO equipmentInExaminationDTO)
        {
            EquipmentInExamination equipmentInExamination = new EquipmentInExamination();
            equipmentInExamination.EquipmentTypeID = equipmentInExaminationDTO.EquipmentID;
            equipmentInExamination.ExaminationId = equipmentInExaminationDTO.ExaminationId;
            return equipmentInExamination;
        }
    }
}
