using GraphicalEditorServer.DTO;
using Model.Manager;

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
