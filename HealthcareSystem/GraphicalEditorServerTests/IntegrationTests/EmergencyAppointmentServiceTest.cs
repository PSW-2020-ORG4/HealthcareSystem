using Backend.Model;
using Backend.Model.DTO;
using Backend.Repository;
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
            var roomService = new RoomService(roomRepo, renovationPeriodRepo, equipmentInRoomRepo, equipmentRepo);
            return new FreeAppointmentSearchService(roomService, examinationRepo, doctorRepo, patientCardRepo);
        }

            [Fact] 
            public void Expected_free_appointment_for_emergency()
            {
                FreeAppointmentSearchService freeAppointmentService = SetupRepositoriesAndServices();
                List<Examination> freeAppointmentsForEmergency = (List<Examination>)freeAppointmentService.GetUnchangedAppointmentsForEmergency(
                new BasicAppointmentSearchDTO(patientCardId: 1, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(),
                    earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0, DateTimeKind.Utc),
                    latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0, DateTimeKind.Utc)));
                Assert.Equal(4, freeAppointmentsForEmergency.Count);            
        }
    }
}
