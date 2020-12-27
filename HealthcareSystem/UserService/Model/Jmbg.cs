using System;

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
            throw new NotImplementedException();
        }
    }
}
