using System.Collections.Generic;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class EmailTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string email, bool valid)
        {
            if (valid)
                new Email(email);
            else
                Assert.Throws<ValidationException>(() => new Email(email));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "this is not an email", false },
                                                new object[] { "email@email.com", true }
                                            };
    }
}
