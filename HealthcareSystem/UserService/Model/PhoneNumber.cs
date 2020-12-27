using System;

namespace UserService.Model
{
    public class PhoneNumber
    {
        private string Value { get; }

        public PhoneNumber(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is PhoneNumber number &&
                   Value == number.Value;
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
