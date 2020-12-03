using Backend.Model.Manager;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public interface IEquipmentTypeService
    {
        EquipmentType AddEquipmentType(EquipmentType equipmentType);
        EquipmentType GetEquipmentTypeById(int id);
        List<EquipmentType> GetEquipmentTypes();
        EquipmentType EditEquipmentType(EquipmentType equipmentType);
        bool DeleteEquipmentType(int id);
    }
}