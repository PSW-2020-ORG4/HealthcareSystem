using PatientService.CustomException;
using PatientService.Model;
using PatientService.Model.Memento;
using System.Collections.Generic;
using Xunit;

namespace PatientServiceTests.UnitTests
{
    public class TherapyDailyDoseTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string doctorJmbg, string doctorName, string doctorSurname, string drugName, int dailyDose, bool valid)
        {
            if (valid)
                new Therapy(new TherapyMemento()
                {
                    DoctorJmbg = doctorJmbg,
                    DoctorName = doctorName,
                    DoctorSurname = doctorSurname,
                    DrugName = drugName,
                    DailyDose = dailyDose,
                });
            else
                Assert.Throws<ValidationException>(() =>
                new Therapy(new TherapyMemento()
                {
                    DoctorJmbg = doctorJmbg,
                    DoctorName = doctorName,
                    DoctorSurname = doctorSurname,
                    DrugName = drugName,
                    DailyDose = dailyDose,
                }));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { "2711998188734", "Mirko", "Miric", "brufen", -2, false },
                                                new object[] { "2711998188734", "Mirko", "Miric", "brufen", 0, false },
                                                new object[] { "2711998188734", "Mirko", "Miric", "brufen", 1, true },
                                                new object[] { "2711998188734", "Mirko", "Miric", "aspirin", 3, true }
                                            };
    }
}
