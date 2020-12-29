using Backend.Model;
using Backend.Model.DTO;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Repository.EquipmentInExaminationRepository.MySqlEquipmentInExaminationRepository;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.ExaminationAndPatientCard;
using Model.PerformingExamination;
using Repository;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using Xunit;

namespace GraphicalEditorServerTests.IntegrationTests
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
            var equipmentInExaminationService = new EquipmentInExaminationService(new MySqlEquipmentInExaminationRepository(context));
            var roomService = new RoomService(roomRepo, renovationPeriodRepo, equipmentInRoomRepo, equipmentRepo);
            return new FreeAppointmentSearchService(roomService, examinationRepo, doctorRepo, patientCardRepo, equipmentInExaminationService);
        }

        [Fact]
        public void Expected_appointment_priority_doctor_search_returns()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.SearchWithPriorities(new AppointmentSearchWithPrioritiesDTO
            {
                InitialParameters = new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(),
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0,DateTimeKind.Utc), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 0, 0,DateTimeKind.Utc)),
                Priority = SearchPriority.Doctor,
                SpecialtyId = 1
            });

            Assert.Equal(74, freeAppointments.Count);
        }

        [Fact]
        public void Expected_appointment_priority_date_search()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.SearchWithPriorities(new AppointmentSearchWithPrioritiesDTO
            {
                InitialParameters = new BasicAppointmentSearchDTO(patientCardId: 2, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(),
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0, DateTimeKind.Utc), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc)),
                Priority = SearchPriority.Date,
                SpecialtyId = 1
            });

            Assert.Equal(4, freeAppointments.Count);
        }

        [Fact]
        public void Expected_appointment_with_unavailable_patient_priority_date_search()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointments = (List<Examination>)freeAppointmentService.SearchWithPriorities(new AppointmentSearchWithPrioritiesDTO
            {
                InitialParameters = new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "1109965768767", requiredEquipmentTypes: new List<int>(),
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0, DateTimeKind.Utc), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc)),
                Priority = SearchPriority.Date,
                SpecialtyId = 1
            });

            Assert.Equal(0, freeAppointments.Count);
        }        
    }
}
