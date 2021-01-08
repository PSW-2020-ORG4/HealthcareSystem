using Backend.Model;
using Backend.Model.DTO;
using Backend.Model.Enums;
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
using System.Text;
using Xunit;

namespace GraphicalEditorServerTests.IntegrationTests
{
    public class EmergencyAppointmentServiceTest
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

        private FreeAppointmentSearchService SetupRepositoriesAndServicesForUnshifted()
        {
            MyDbContextInMemory testData = new MyDbContextInMemory();
            testData.SetEmergencyAppointmentSearchServiceTestIntegration();
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
        public void Expected_free_appointment_for_emergency_without_shifting_appointments()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointmentsForEmergency = (List<Examination>)freeAppointmentService.GetUnchangedAppointmentsForEmergency(
                new AppointmentSearchWithPrioritiesDTO(
                    new BasicAppointmentSearchDTO(
                        patientCardId: 1, 
                        doctorJmbg: "0909965768767", 
                        requiredEquipmentTypes: new List<int>(),
                        earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 7, 0, 0, DateTimeKind.Utc),
                        latestDateTime: new DateTime()),
                    SearchPriority.Date, 1));
            
            Assert.Equal(16, freeAppointmentsForEmergency.Count);
        }

        [Fact]
        public void Expected_no_appointment_for_emergency_without_shifting_appointments()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
            List<Examination> freeAppointmentsForEmergency = (List<Examination>)freeAppointmentService.GetUnchangedAppointmentsForEmergency(
            new AppointmentSearchWithPrioritiesDTO(
                    new BasicAppointmentSearchDTO(
                        patientCardId: 1,
                        doctorJmbg: "0909965768767",
                        requiredEquipmentTypes: new List<int>(),
                        earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0, DateTimeKind.Utc),
                        latestDateTime: new DateTime()),
                    SearchPriority.Date, 1));

            Assert.Empty(freeAppointmentsForEmergency);
        }

        [Fact]
        public void Expect_shifted_appointment_for_emergancy()
        {
            FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServicesForUnshifted();
            List<Examination> freeAppointmentsForEmergency = (List<Examination>)freeAppointmentService.GetShiftedAndSortedAppoinmentsForEmergency(
            new AppointmentSearchWithPrioritiesDTO(
                    new BasicAppointmentSearchDTO(
                        patientCardId: 3,
                        doctorJmbg: "0909965768767",
                        requiredEquipmentTypes: new List<int>(),
                        earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 23, 0, DateTimeKind.Utc),
                        latestDateTime: new DateTime()),
                    SearchPriority.Date, 1));

            Assert.Equal(9, freeAppointmentsForEmergency.Count);
            Assert.True(freeAppointmentsForEmergency[0].DateAndTime.Equals(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 11, 0, 0, DateTimeKind.Utc)));
            Assert.True(freeAppointmentsForEmergency[5].DateAndTime.Equals(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 30, 0, DateTimeKind.Utc)));
        }
    }
}
