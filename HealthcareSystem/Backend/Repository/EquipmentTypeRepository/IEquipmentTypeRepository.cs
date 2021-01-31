using Backend.Model.Manager;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IEquipmentTypeRepository
    {
        EquipmentType GetEquipmentType(int id);
        List<EquipmentType> GetAllEquipmentTypes();
        EquipmentType UpdateEquipmentType(EquipmentType equipmentType);
        void DeleteEquipmentType(int id);
        EquipmentType AddEquipmentType(EquipmentType equipmentType);


    }
}
