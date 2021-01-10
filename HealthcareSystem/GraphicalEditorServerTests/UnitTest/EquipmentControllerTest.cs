using GraphicalEditorServerTests.DataFactory;
using Moq;
using Backend.Repository;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using Xunit;
using GraphicalEditorServer.Controllers;
using Model.Manager;
using Microsoft.AspNetCore.Mvc;
using Backend.Service.RoomAndEquipment;

namespace GraphicalEditorServerTests.UnitTest
{
    public class EquipmentControllerTest
    {
        private readonly CreateEquipmentInRoom _createEquipmentInRoom;
        private readonly StubRepository _stubRepository;
        
        public EquipmentControllerTest()
        {
            _stubRepository = new StubRepository();
            _createEquipmentInRoom = new CreateEquipmentInRoom();
        }
        private EquipmentController SetupEquipmentController()
        {
        EquipmentService _equipmentService = new EquipmentService(_stubRepository.CreateEquipmentStubRepository(), _stubRepository.CreateEquipmentInRoomStubRepository(), _stubRepository.CreateExaminationStubRepository());
        EquipmentTypeService equipmentTypeService = new EquipmentTypeService(_stubRepository.CreateEquipmentTypeStubRepository());    
        EquipmentInRoomsService equipmentInRoomsService = new EquipmentInRoomsService(_stubRepository.CreateEquipmentInRoomStubRepository(), _equipmentService);    
        EquipmentController _equipmentController = new EquipmentController(_equipmentService, equipmentTypeService, equipmentInRoomsService);
            return _equipmentController;
        }

        [Fact]
        public void Get_existent_equipment_by_valid_room_number()
        {
            EquipmentController equipmentController = SetupEquipmentController();

            var result = equipmentController.GetEquipmentByRoomNumber(56);

            Assert.True(result is OkObjectResult);
        }
        [Fact]
        public void Get_existent_equipment_by_invalid_room_number()
        {
            EquipmentController equipmentController = SetupEquipmentController();

            EquipmentInRooms invalidEquipmentInRoom = _createEquipmentInRoom.CreateInvalidTestObject();
            IActionResult result = equipmentController.GetEquipmentByRoomNumber(invalidEquipmentInRoom.RoomNumber);

            Assert.True(result is NotFoundObjectResult);
        }
    }
}
