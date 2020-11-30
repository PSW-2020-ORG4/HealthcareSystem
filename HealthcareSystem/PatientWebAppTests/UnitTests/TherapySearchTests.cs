using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.TherapySearch;
using PatientWebAppTests.CreateObjectsForTests;
using Service.DrugAndTherapy;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class TherapySearchTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public TherapySearchTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        [Fact]
        public void Advanced_search_therapy_operator_and_drug_name()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var therapySearchDTOValidObject = _objectFactory.GetTherapySearchDTO().CreateValidTestObject();
            var result = therapyService.AdvancedSearch(therapySearchDTOValidObject);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_operator_or_drug_name()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_and_surname()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: "Markovic", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_or_surname()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_start_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_or_end_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 10, 10), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_and_end_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.And, endDate: new DateTime(2020, 10, 10), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.Empty(result);
        }

    }
}
