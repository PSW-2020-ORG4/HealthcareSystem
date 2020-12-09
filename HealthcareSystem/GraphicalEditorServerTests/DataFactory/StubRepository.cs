using Backend.Model.Manager;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.RenovationPeriodRepository;
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
        private readonly CreateEquipment _createEquipment;
        private readonly CreateEquipmentType _createEquipmentType;
        private readonly CreateEquipmentInRoom _createEquipmentInRoom;
        private readonly CreateRoom _createRooms;

        public StubRepository()
        {
            _createEquipment = new CreateEquipment();
            _createEquipmentType = new CreateEquipmentType();
            _createEquipmentInRoom = new CreateEquipmentInRoom();
            _createRooms = new CreateRoom();
        }

        public IEquipmentRepository CreateEquipmentStubRepository() {
            var equipmentStubRepository = new Mock<IEquipmentRepository>();
            var equipment = new List<Equipment>();
            equipment.Add(_createEquipment.CreateValidTestObject());
            equipment.Add(_createEquipment.CreateValidTestObject(29));
            equipmentStubRepository.Setup(m => m.GetEquipmentById(30)).Returns(equipment[0]);
            equipmentStubRepository.Setup(m => m.GetEquipmentById(29)).Returns(equipment[1]);

            equipmentStubRepository.Setup(m => m.GetEquipmentById(1)).Returns(_createEquipment.CreateBedTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(2)).Returns(_createEquipment.CreateMaskTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(3)).Returns(_createEquipment.CreateNeedleTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(4)).Returns(_createEquipment.CreateComputerTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(5)).Returns(_createEquipment.CreateBendTestObject());

            equipmentStubRepository.Setup(m => m.GetAllEquipment()).Returns(equipment);
            return equipmentStubRepository.Object;
        }

        public IEquipmentInRoomsRepository CreateEquipmentInRoomStubRepository()
        {
            var equipmentInRoomStubRepository = new Mock<IEquipmentInRoomsRepository>();
            var equipmentInRooms = new List<EquipmentInRooms>();
            equipmentInRooms.Add(_createEquipmentInRoom.CreateValidTestObject());
            equipmentInRooms.Add(_createEquipmentInRoom.CreateValidTestObject(30));
            equipmentInRoomStubRepository.Setup(m => m.GetEquipmentInRoomsByRoomNumber(56)).Returns(equipmentInRooms);
            equipmentInRoomStubRepository.Setup(m => m.GetEquipmentInRoomsByRoomNumber(-1)).Returns(new List<EquipmentInRooms>());

            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(0)).Returns(_createEquipmentInRoom.CreateEIRForFirstRoom());
            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(1)).Returns(_createEquipmentInRoom.CreateEIRForSecondRoom());
            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(2)).Returns(_createEquipmentInRoom.CreateEIRForThirdRoom());

            return equipmentInRoomStubRepository.Object;
        }

        public IEquipmentTypeRepository CreateEquipmentTypeStubRepository()
        {
            var equipmentTypeStubRepository = new Mock<IEquipmentTypeRepository>();
            var equipmentInRooms = new List<EquipmentType>();
            equipmentInRooms.Add(_createEquipmentType.CreateValidTestObject());
            equipmentInRooms.Add(_createEquipmentType.CreateValidTestObject());
            equipmentTypeStubRepository.Setup(m => m.GetAllEquipmentTypes()).Returns(equipmentInRooms);
            return equipmentTypeStubRepository.Object;
        }

        public IRoomRepository CreateRoomStubRepository()
        {
            var roomStubRepository = new Mock<IRoomRepository>();
            roomStubRepository.Setup(x => x.GetAllRooms()).Returns(_createRooms.CreateRooms());

            return roomStubRepository.Object;
        }

        public IRenovationPeriodRepository CreateRenovationStubRepository()
        {
            var renovationStubRepository = new Mock<IRenovationPeriodRepository>();

            return renovationStubRepository.Object;
        }
    }
}
