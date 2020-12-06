using GraphicalEditorServerTests.DataFactory;
using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphicalEditorServerTests.UnitTest
{
    public class EquipmentServiceTest
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public EquipmentServiceTest()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }
        private EquipmentService SetupEquipmentService()
        {
            EquipmentService _equipmentService = new EquipmentService(_stubRepository.CreateEquipmentStubRepository(), _stubRepository.CreateEquipmentInRoomStubRepository());
            return _equipmentService;
        }

        [Fact]
        public void Get_existent_equipment_by_valid_room_number()
        {
            EquipmentService _equipmentService = SetupEquipmentService();
            List<Equipment> equipment = _equipmentService.GetEquipmentByRoomNumber(56);
            Assert.NotNull(equipment);
        }
        [Fact]
        public void Get_existent_equipment_by_invalid_room_number()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            EquipmentInRooms invalidEquipmentInRoom = _objectFactory.GetEquipmentInRooms().CreateInvalidTestObject();
            List<Equipment> equipment = equipmentService.GetEquipmentByRoomNumber(invalidEquipmentInRoom.RoomNumber);
            Assert.Null(equipment);
        }
    }
}

