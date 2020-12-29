using System;
using System.Text.RegularExpressions;
using UserService.CustomException;

namespace UserService.Model
{
    public class Jmbg
    {
        private string Value { get; }

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
            if (string.IsNullOrEmpty(Value)) throw new ValidationException("Jmbg can't be empty!");
            if (!IsValidFormat(Value))  throw new ValidationException("Invalid jmbg");       
        }

        private bool IsValidFormat(string Value)
        {
            Regex regex = new Regex(@"^[0-9]{13}$");
            return regex.IsMatch(Value);
        }
    }
}
