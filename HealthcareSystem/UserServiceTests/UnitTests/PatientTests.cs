using System.Collections.Generic;
using UserService.CustomException;
using UserService.Model;
using UserServiceTests.UnitTests.Data;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class PatientTests
    {
        private static PatientBuilder _patientBuilder = new PatientBuilder();

        [Theory]
        [MemberData(nameof(ActivationData))]
        public void ActivatePatientAccount(PatientAccount patient, bool canActivate)
        {
            if (canActivate)
                patient.Activate();
            else
                Assert.Throws<ValidationException>(() => patient.Activate());
        }

        [Theory]
        [MemberData(nameof(BlockingData))]
        public void BlockPatientAccount(PatientAccount patient, bool canBlock)
        {
            if (canBlock)
                patient.Block();
            else
                Assert.Throws<ValidationException>(() => patient.Block());
        }

        public static IEnumerable<object[]> ActivationData =>
                                            new List<object[]>
                                            {
                                                new object[] { _patientBuilder.GetUnactivated(), true },
                                                new object[] { _patientBuilder.GetActivated(0), false },
                                                new object[] { _patientBuilder.GetBlocked(), false }
                                            };
        public static IEnumerable<object[]> BlockingData =>
                                            new List<object[]>
                                            {
                                                new object[] { _patientBuilder.GetUnactivated(), false },
                                                new object[] { _patientBuilder.GetActivated(0), false },
                                                new object[] { _patientBuilder.GetActivated(1), false },
                                                new object[] { _patientBuilder.GetActivated(2), false },
                                                new object[] { _patientBuilder.GetActivated(3), true },
                                                new object[] { _patientBuilder.GetActivated(4), true },
                                                new object[] { _patientBuilder.GetActivated(33), true },
                                                new object[] { _patientBuilder.GetBlocked(), false }
                                            };
    }
}
