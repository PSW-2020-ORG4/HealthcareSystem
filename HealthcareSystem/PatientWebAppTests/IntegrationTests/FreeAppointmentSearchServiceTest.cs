using Backend.Model;
using Backend.Model.DTO;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Model.PerformingExamination;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.RoomAndEquipment;
using Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace PatientWebAppTests.IntegrationTests
{
    public class FreeAppointmentSearchServiceTest
    {
        private FreeAppointmentSearchService SetupRepositoriesAndServices()
        {
            MyDbContextInMemory testData = new MyDbContextInMemory();
            testData.SetFreeAppointmentSearchServiceTestIntegration();
            MyDbContext context = testData._context;
            var examinationRepo = new MySqlExaminationRepository(context);
            var doctorRepo = new MySqlDoctorRepository(context);
            var patientCardRepo = new MySqlActivePatientCardRepository(context);
            var equipmentRepo = new MySqlEquipmentRepository(context);
            var renovationPeriodRepo = new MySqlRenovationPeriodRepository(context);
            var equipmentInRoomRepo = new MySqlEquipmentInRoomsRepository(context);
            var roomRepo = new MySqlRoomRepository(context);
            var roomService = new RoomService(roomRepo, renovationPeriodRepo, equipmentInRoomRepo, equipmentRepo);
            return new FreeAppointmentSearchService(roomService, examinationRepo, doctorRepo, patientCardRepo);
        }

        [Fact]
        public void ExpectedAppointmentSearchBasic()
        {
            FreeAppointmentSearchService freeAppointmentService =  SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.BasicSearch(new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(), 
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 0, 0)));

            Assert.Equal(4, freeAppointments.Count);
        }

        [Fact]
        public void BasicAppointmentSearchWithInvalidParameters()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();

            Assert.Throws<BadRequestException>(() => freeAppointmentService.BasicSearch(new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: null, requiredEquipmentTypes: new List<int>(), 
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 17, 0, 0))));
        }

        [Fact]
        public void BasicUnavailableAppointmentSearch()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.BasicSearch(new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(), 
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 30, 0)));

            Assert.Empty(freeAppointments);
        }

        
       /* [Fact]
        public void ExpectedAppointmentPrioritySearch()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.SearchWithPriorities(new AppointmentSearchWithPrioritiesDTO { InitialParameters = new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(),
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 17, 0, 0)), Priority = SearchPriority.Doctor, SpecialtyId = 1});

            Assert.Equal(38, freeAppointments.Count);
        }
        */

    }
}
