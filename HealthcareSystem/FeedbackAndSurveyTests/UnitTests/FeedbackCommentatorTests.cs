using System;
using System.Collections.Generic;
using System.Text;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using Xunit;

namespace FeedbackAndSurveyTests.UnitTests
{
    public class FeedbackCommentatorTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string jmbg,string name, string surname, bool valid)
        {
            if (valid)
                new Commentator(jmbg,name, surname);
            else
                Assert.Throws<ValidationException>(() => new Commentator(jmbg, name, surname));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { "", null, null, false },
                                                new object[] { "", "", "", false },
                                                new object[] { "2711998188734", "", "", false },
                                                new object[] { "2711998188734", "Marija", "", false },
                                                new object[] { "2711998188734", "Marija", "Maric", true },
                                            };
    }
}
