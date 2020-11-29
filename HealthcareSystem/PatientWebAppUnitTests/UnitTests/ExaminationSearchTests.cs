using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.ExaminationSearch;
using PatientWebAppTests.CreateObjectsForTests;
using Service.ExaminationAndPatientCard;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class ExaminationSearchTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public ExaminationSearchTests() {

            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        [Fact]
        public void Advanced_search_examination_operator_and_anamnesis()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var examinationSearchDTOValidObject = _objectFactory.GetExaminationSearchDTO().CreateValidTestObject();
            var result = examinationService.AdvancedSearch(examinationSearchDTOValidObject);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_anamnesis()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.Or, anamnesis: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_and_surname()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: "Mar", anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_surname()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "gdgdg", anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_start_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_end_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_and_end_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.And, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.Empty(result);
        }

    }
}
