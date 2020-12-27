using System;

namespace UserService.Model
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Email email &&
                   Value == email.Value;
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
