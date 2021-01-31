using GraphicalEditorServerTests.DataFactory;
using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using Xunit;

namespace GraphicalEditorServerTests.UnitTest
{
    public class EquipmentServiceTest
    {
        private readonly StubRepository _stubRepository;
        private readonly CreateEquipmentInRoom _createEquipmentInRoom;
        private readonly CreateTransferEqupmentDTO _createTransferEqupmentDTO;


        public EquipmentServiceTest()
        {
            _stubRepository = new StubRepository();
            _createEquipmentInRoom = new CreateEquipmentInRoom();
            _createTransferEqupmentDTO = new CreateTransferEqupmentDTO();
        }

        private EquipmentService SetupEquipmentService()
        {
            EquipmentService _equipmentService = new EquipmentService(_stubRepository.CreateEquipmentStubRepository(), _stubRepository.CreateEquipmentInRoomStubRepository(), _stubRepository.CreateExaminationStubRepository(), _stubRepository.CreateEquipmentTransferStubRepository(), _stubRepository.CreateEquipmentInExaminationRepository());
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
            EquipmentInRooms invalidEquipmentInRoom = _createEquipmentInRoom.CreateInvalidTestObject();
            List<Equipment> equipment = equipmentService.GetEquipmentByRoomNumber(invalidEquipmentInRoom.RoomNumber);
            Assert.Empty(equipment);
        }

        [Fact]
        public void Initialize_equipment_transfer_return_starting_room_number_because_of_created_examiantion()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            int result = equipmentService.InitializeEquipmentTransfer(_createTransferEqupmentDTO.CreateInvalidTestObjectForInitializingEquipmentTransfer1());
            Assert.Equal(9, result);
        }

        [Fact]
        public void Initialize_equipment_transfer_return_destination_room_number_because_of_created_examiantion()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            int result = equipmentService.InitializeEquipmentTransfer(_createTransferEqupmentDTO.CreateInvalidTestObjectForInitializingEquipmentTransfer2());
            Assert.Equal(15, result);
        }

        [Fact]
        public void Initialize_equipment_transfer_valid()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            int result = equipmentService.InitializeEquipmentTransfer(_createTransferEqupmentDTO.CreateValidTestObjectForInitializingEquipmentTransfer());
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Get_alternative_appointments1()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            List<DateTime> result = equipmentService.GetAlternativeAppointments(_createTransferEqupmentDTO.CreateInvalidTestObjectForInitializingEquipmentTransfer1());
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void Get_alternative_appointments2()
        {
            EquipmentService equipmentService = SetupEquipmentService();
            List<DateTime> result = equipmentService.GetAlternativeAppointments(_createTransferEqupmentDTO.CreateInvalidTestObjectForInitializingEquipmentTransfer1());
            Assert.DoesNotContain(_createTransferEqupmentDTO.CreateInvalidTestObjectForInitializingEquipmentTransfer1().DateAndTimeOfTransfer, result);
        }

    }
}

