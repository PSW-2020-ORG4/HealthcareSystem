using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class Grade
    {
        public int Value { get; }

        public Grade(int value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Grade grade &&
                   Value == grade.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
