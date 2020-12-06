using Backend.Model.Manager;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.RoomRepository;
using Model.Manager;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class StubRepository
    {
        private readonly TestObjectFactory _testObjectFactory;

        public StubRepository()
        {
            _testObjectFactory = new TestObjectFactory();
        }

        public IEquipmentRepository CreateEquipmentStubRepository() {
            var equipmentStubRepository = new Mock<IEquipmentRepository>();
            var equipment = new List<Equipment>();
            equipment.Add(_testObjectFactory.GetEquipment().CreateValidTestObject());
            equipment.Add(_testObjectFactory.GetEquipment().CreateValidTestObject(29));
            equipmentStubRepository.Setup(m => m.GetEquipmentById(30)).Returns(equipment[0]);
            equipmentStubRepository.Setup(m => m.GetEquipmentById(29)).Returns(equipment[1]);
            equipmentStubRepository.Setup(m => m.GetAllEquipment()).Returns(equipment);
            return equipmentStubRepository.Object;
        }

        public IEquipmentInRoomsRepository CreateEquipmentInRoomStubRepository()
        {
            var equipmentInRoomStubRepository = new Mock<IEquipmentInRoomsRepository>();
            var equipmentInRooms = new List<EquipmentInRooms>();
            equipmentInRooms.Add(_testObjectFactory.GetEquipmentInRooms().CreateValidTestObject());
            equipmentInRooms.Add(_testObjectFactory.GetEquipmentInRooms().CreateValidTestObject(30));
            equipmentInRoomStubRepository.Setup(m => m.GetEquipmentInRoomsByRoomNumber(56)).Returns(equipmentInRooms);
            return equipmentInRoomStubRepository.Object;
        }

        public IEquipmentTypeRepository CreateEquipmentTypeStubRepository()
        {
            var equipmentTypeStubRepository = new Mock<IEquipmentTypeRepository>();
            var equipmentInRooms = new List<EquipmentType>();
            equipmentInRooms.Add(_testObjectFactory.GetEquipmentType().CreateValidTestObject());
            equipmentInRooms.Add(_testObjectFactory.GetEquipmentType().CreateValidTestObject(30));
            equipmentTypeStubRepository.Setup(m => m.GetAllEquipmentTypes()).Returns(equipmentInRooms);
            return equipmentTypeStubRepository.Object;
        }
    }
}
