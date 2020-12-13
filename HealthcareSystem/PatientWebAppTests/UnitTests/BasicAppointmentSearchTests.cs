using Backend.Service.ExaminationAndPatientCard;
using Model.PerformingExamination;
using PatientWebAppTests.CreateObjectsForTests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace PatientWebAppTests.UnitTests
{
    public class BasicAppointmentSearchTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public BasicAppointmentSearchTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        /*
        private FreeAppointmentSearchService SetupFreeAppointmentSearchService()
        {
            
            FreeAppointmentSearchService freeAppointmentSearchService = new FreeAppointmentSearchService(_stubRepository.CreateRoomStubRepository(),
                _stubRepository.CreateExaminationStubRepository(), _stubRepository.CreateDoctorStubRepository(), _stubRepository.CreatePatientCardStubRepository());
            
            return freeAppointmentSearchService;
        }*/


        /*

        [Fact]
        public void Find_free_appointsments()
        {
            FreeAppointmentSearchService freeAppointmentSearchService = SetupFreeAppointmentSearchService();
            
            ICollection<Examination> result = freeAppointmentSearchService.BasicSearch(new Backend.Model.DTO.BasicAppointmentSearchDTO(1, "0909965768767", new List<int>(), new DateTime(2020, 12, 5, 7, 0, 0), new DateTime(2020, 12, 11)));

            result.ShouldNotBeEmpty();
        }
        */
    }
}
