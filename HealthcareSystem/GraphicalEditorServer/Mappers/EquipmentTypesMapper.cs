using Backend.Model.Manager;
using GraphicalEditorServer.DTO;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentTypesMapper
    {
        public static EquipmentType EquipmentTypeDTO_To_EquipmentType(EquipmentTypeDTO equipmentTypeDTO)
        {
            return new EquipmentType(equipmentTypeDTO.Name, equipmentTypeDTO.IsConsumable);
        }
        public static EquipmentTypeDTO EquipmentType_To_EquipmentTypeDTO(EquipmentType equipmentType)
        {
            return new EquipmentTypeDTO(equipmentType.Id, equipmentType.Name, equipmentType.IsConsumable);
        }
    }
}
