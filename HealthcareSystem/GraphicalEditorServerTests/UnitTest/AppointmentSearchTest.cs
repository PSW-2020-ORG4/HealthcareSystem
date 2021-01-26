using Backend.Model.Enums;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServerTests.DataFactory;
using Model.Manager;
using System.Collections.Generic;
using Xunit;

namespace GraphicalEditorServerTests.UnitTest
{
    public class AppointmentSearchTest
    {
        private readonly StubRepository _stubRepository;

        public AppointmentSearchTest()
        {
            _stubRepository = new StubRepository();
        }

        private RoomService SetupRoomService()
        {
            RoomService roomService = new RoomService(_stubRepository.CreateRoomStubRepository(),
               null,
                _stubRepository.CreateEquipmentInRoomStubRepository(),
                _stubRepository.CreateEquipmentStubRepository());

            return roomService;
        }


        [Fact]
        public void Find_available_rooms_from_required_equipment_Return_None()
        {
            RoomService roomService = SetupRoomService();
            List<Room> result = (List<Room>)roomService.GetRoomsByUsageAndEquipment(TypeOfUsage.CONSULTING_ROOM, new List<int>() { 0, 1, 3 });

            Assert.True(result.Count == 0);
        }

        [Fact]
        public void Find_available_rooms_from_required_equipment_Return_2()
        {
            RoomService roomService = SetupRoomService();
            List<Room> result = (List<Room>)roomService.GetRoomsByUsageAndEquipment(TypeOfUsage.CONSULTING_ROOM, new List<int>() { 3, 2 });

            List<Room> validResult = new List<Room>();
            validResult.Add(new Room(1, TypeOfUsage.CONSULTING_ROOM, 20, 10, false));
            validResult.Add(new Room(2, TypeOfUsage.CONSULTING_ROOM, 20, 10, false));

            Assert.Equal(2, result.Count);
        }
    }
}
