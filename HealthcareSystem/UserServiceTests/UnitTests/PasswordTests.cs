using System.Collections.Generic;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class PasswordTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string password, bool valid)
        {
            if (valid)
                new Password(password);
            else
                Assert.Throws<ValidationException>(() => new Password(password));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "lalalalallaal", true }
                                            };
    }
}
