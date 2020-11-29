using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification.ExaminationSearch;
using PatientWebAppTests.CreateObjectsForTests;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class ExaminationSpecificationTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public ExaminationSpecificationTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        [Fact]
        public void Is_specification_anamnesis_true()
        {
            ExaminationAnamnesisSpecification anamnesisSpecification = new ExaminationAnamnesisSpecification("bol");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = anamnesisSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_anamnesis_false()
        {
            ExaminationAnamnesisSpecification anamnesisSpecification = new ExaminationAnamnesisSpecification("fdfd");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = anamnesisSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_doctor_surname_true()
        {
            ExaminationDoctorSurnameSpecification surnameSpecification = new ExaminationDoctorSurnameSpecification("markovic");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = surnameSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_doctor_surname_false()
        {
            ExaminationDoctorSurnameSpecification surnameSpecification = new ExaminationDoctorSurnameSpecification("kllk");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = surnameSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_start_date_true()
        {
            ExaminationStartDateSpecification startSpecification = new ExaminationStartDateSpecification(new DateTime(2019, 1, 11));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = startSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_start_date_false()
        {
            ExaminationStartDateSpecification startSpecification = new ExaminationStartDateSpecification(new DateTime(2020, 12, 12));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = startSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_end_date_true()
        {
            ExaminationEndDateSpecification endSpecification = new ExaminationEndDateSpecification(new DateTime(2020, 12, 12));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = endSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_end_date_false()
        {
            ExaminationEndDateSpecification endSpecification = new ExaminationEndDateSpecification(new DateTime(2020, 10, 10));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = endSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }
    }
}
