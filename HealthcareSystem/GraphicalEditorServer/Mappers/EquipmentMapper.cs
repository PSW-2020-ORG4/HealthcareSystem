using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentMapper
    {
        public static EquipmentDTO EquipmentToEquipmentDTO(Equipment equipment)
        {
            EquipmentDTO equipmentDTO = new EquipmentDTO();
            equipmentDTO.Id = equipment.Id;
            equipmentDTO.Quantity = equipment.Quantity;
            equipmentDTO.Type = EquipmentTypesMapper.EquipmentType_To_EquipmentTypeDTO(equipment.Type);

            return equipmentDTO;
        }
    }
}
