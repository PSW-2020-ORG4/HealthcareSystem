using System;
using System.Text.RegularExpressions;

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
            if (!IsRegular(Value))  throw new Exception("Invalid jmbg");       
        }

        private bool IsRegular(string Value)
        {
            Regex regex = new Regex(@"^[0-9]{13}$");
            return regex.IsMatch(Value);
        }
    }
}
