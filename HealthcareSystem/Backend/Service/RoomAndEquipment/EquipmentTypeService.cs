using Backend.Model.Manager;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool DeleteEquipmentType(int id)
        {
            return _equipmentTypeRepository.DeleteEquipmentType(id);
        }

        public EquipmentType EditEquipmentType(EquipmentType equipmentType)
        {
            return _equipmentTypeRepository.SetEquipmentType(equipmentType);
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
