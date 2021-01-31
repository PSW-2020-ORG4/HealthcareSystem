using Backend.Model.Manager;
using Backend.Repository;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private IEquipmentTypeRepository _equipmentTypeRepository;

        public EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public EquipmentType AddEquipmentType(EquipmentType equipmentType)
        {
            return _equipmentTypeRepository.AddEquipmentType(equipmentType);
        }

        public void DeleteEquipmentType(int id)
        {
            _equipmentTypeRepository.DeleteEquipmentType(id);
        }

        public EquipmentType UpdateEquipmentType(EquipmentType equipmentType)
        {
            return _equipmentTypeRepository.UpdateEquipmentType(equipmentType);
        }

        public EquipmentType GetEquipmentTypeById(int id)
        {
            return _equipmentTypeRepository.GetEquipmentType(id);
        }

        public List<EquipmentType> GetEquipmentTypes()
        {
            return _equipmentTypeRepository.GetAllEquipmentTypes();
        }
    }
}
