using Backend.Model.DTO;
using Backend.Model.Enums;
using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditorServerTests.DataFactory;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Text;
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
                _stubRepository.CreateRenovationStubRepository(),
                _stubRepository.CreateEquipmentInRoomStubRepository(),
                _stubRepository.CreateEquipmentStubRepository());

            return roomService;
        }
         
        private FreeAppointmentSearchService SetupFreeAppointmentSearchService()
        {

            FreeAppointmentSearchService freeAppointmentSearchService = new FreeAppointmentSearchService(SetupRoomService(),
                _stubRepository.CreateExaminationStubRepository(), _stubRepository.CreateDoctorStubRepository(), _stubRepository.CreatePatientCardStubRepository());

            return freeAppointmentSearchService;
        }

        [Fact]
        public void Find_free_appointments_with_doctor_prirority()
        {
            FreeAppointmentSearchService freeAppointmentSearchService = SetupFreeAppointmentSearchService();
            
            ICollection<Examination> result = freeAppointmentSearchService.SearchWithPriorities(new AppointmentSearchWithPrioritiesDTO(new BasicAppointmentSearchDTO(1, "0909965768767", new List<int>() { 3, 2}, new DateTime(2020, 12, 5, 7, 0, 0), new DateTime(2020, 12, 5, 8, 0, 0)), SearchPriority.Doctor,1));

            Assert.NotNull(result);
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
