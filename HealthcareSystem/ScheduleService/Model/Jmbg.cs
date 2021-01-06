using ScheduleService.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Jmbg
    {
        public string Value { get; }

        public Jmbg(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Jmbg jmbg &&
                   Value == jmbg.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new ValidationException("Jmbg cannot be empty.");
            if (!IsValidFormat(Value))
                throw new ValidationException("Jmbg format is not valid.");
        }
        private bool IsValidFormat(string Value)
        {
            Regex regex = new Regex(@"^[0-9]{13}$");
            return regex.IsMatch(Value);
        }
    }
}
