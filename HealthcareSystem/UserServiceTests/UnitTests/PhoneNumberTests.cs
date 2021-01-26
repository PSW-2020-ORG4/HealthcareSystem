using System.Collections.Generic;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class PhoneNumberTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string phoneNumber, bool valid)
        {
            if (valid)
                new PhoneNumber(phoneNumber);
            else
                Assert.Throws<ValidationException>(() => new PhoneNumber(phoneNumber));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "this is not a phone number", false },
                                                new object[] { "123", false },
                                                new object[] { "0601451700", true }
                                            };
    }
}
