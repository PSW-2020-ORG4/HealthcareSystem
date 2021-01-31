using PatientService.Model;
using PatientService.Model.Memento;
using PatientService.Model.Specification;
using PatientService.Model.TherapySpecification;
using System;
using System.Collections.Generic;
using Xunit;

namespace PatientServiceTests.UnitTests
{
    public class TherapySpecificationTests
    {
        private readonly Therapy _therapy = new Therapy(new TherapyMemento()
        {
            Id = 1,
            DailyDose = 5,
            Diagnosis = "Covid 19",
            DoctorJmbg = "2711998188734",
            DoctorName = "Doktor",
            DoctorSurname = "Doktorić",
            DrugId = 1,
            DrugName = "Aspirin",
            StartDate = new DateTime(2020, 12, 21),
            EndDate = new DateTime(2021, 1, 1),
            ExaminationId = 1
        });

        [Theory]
        [MemberData(nameof(EndDate))]
        public void EndDateSpecification(DateTime? endDate, bool expected)
        {
            ISpecification<Therapy> specification = new TherapyEndDateSpecification(endDate);
            bool result = specification.IsSatisfiedBy(_therapy);

            Assert.Equal(result, expected);
        }

        public static IEnumerable<object[]> EndDate =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2020, 12, 21), true },
                                                new object[] { null, true },
                                                new object[] { new DateTime(2020, 12, 15), false },
                                                new object[] { new DateTime(2021, 12, 21), true }
                                            };

        [Theory]
        [MemberData(nameof(StartDate))]
        public void StartDateSpecification(DateTime? startDate, bool expected)
        {
            ISpecification<Therapy> specification = new TherapyStartDateSpecification(startDate);
            bool result = specification.IsSatisfiedBy(_therapy);

            Assert.Equal(result, expected);
        }

        public static IEnumerable<object[]> StartDate =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2020, 12, 21), true },
                                                new object[] { null, true },
                                                new object[] { new DateTime(2020, 12, 15), true },
                                                new object[] { new DateTime(2021, 12, 21), false }
                                            };

        [Theory]
        [InlineData("Aspirin", true)]
        [InlineData("Asp", true)]
        [InlineData("lalallala", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("asp", true)]
        [InlineData("irin", true)]
        public void DrugNameSpecification(string drugName, bool expected)
        {
            ISpecification<Therapy> specification = new TherapyDrugNameSpacification(drugName);
            bool result = specification.IsSatisfiedBy(_therapy);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("Doktor", true)]
        [InlineData("Dok", true)]
        [InlineData("lalallala", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("dok", true)]
        [InlineData("IĆ", true)]
        public void DoctorSpecification(string doctorSurname, bool expected)
        {
            ISpecification<Therapy> specification = new TherapyDoctorSurnameSpecification(doctorSurname);
            bool result = specification.IsSatisfiedBy(_therapy);

            Assert.Equal(result, expected);
        }
    }
}
