using System;
using System.Collections.Generic;
using System.Text;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using Xunit;

namespace FeedbackAndSurveyTests.UnitTests
{
    public class ContentTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(DateTime dateOfCreation, string comment, bool valid)
        {
            if (valid)
                new Content(dateOfCreation, comment);
            else
                Assert.Throws<ValidationException>(() => new Content(dateOfCreation, comment));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2021, 1, 1), null, false },
                                                new object[] { new DateTime(2021, 1, 1), "", false },
                                                new object[] { new DateTime(2021, 1, 1), "   \n\t   ", false },
                                                new object[] { new DateTime(2020, 12, 21), "", false },
                                                new object[] { new DateTime(2020, 12, 21), "komentar", true },
                                                new object[] { new DateTime(2021, 1, 1), "Ovo je validan komentar", true }
                                            };

    }
}
