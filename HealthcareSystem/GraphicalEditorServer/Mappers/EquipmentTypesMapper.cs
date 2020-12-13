using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentTypesMapper
    {
        public EquipmentType EquipmentTypeDTO_To_EquipmentType(EquipmentTypeDTO equipmentTypeDTO) {
            return new EquipmentType(equipmentTypeDTO.Name, equipmentTypeDTO.IsConsumable);
        }
        public EquipmentTypeDTO EquipmentType_To_EquipmentTypeDTO(EquipmentType equipmentType)
        {
            return new EquipmentTypeDTO(equipmentType.Id, equipmentType.Name, equipmentType.IsConsumable);
        }
    }
}
