using Backend.Model.PerformingExamination;
using GraphicalEditorServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentInExaminationMapper
    {
        public static EquipmentInExaminationDTO EquipmentInExaminationToEquipmentInExaminationDTO(EquipmentInExamination equipmentInExamination)
        {
            EquipmentInExaminationDTO equipmentInExaminationDTO = new EquipmentInExaminationDTO();
            equipmentInExaminationDTO.EquipmentID = equipmentInExamination.EquipmentID;
            equipmentInExaminationDTO.ExaminationId = equipmentInExamination.ExaminationId;            
            return equipmentInExaminationDTO;
        }

        public static EquipmentInExamination EquipmentInExaminationDTOToEquipmentInExamination(EquipmentInExaminationDTO equipmentInExaminationDTO)
        {
            EquipmentInExamination equipmentInExamination = new EquipmentInExamination();
            equipmentInExamination.EquipmentID = equipmentInExaminationDTO.EquipmentID;
            equipmentInExamination.ExaminationId = equipmentInExaminationDTO.ExaminationId;            
            return equipmentInExamination;
        }
    }
}
