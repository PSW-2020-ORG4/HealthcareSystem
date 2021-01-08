using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model
{
    public class InsuranceNumber
    {
        public string Value { get; }

        public InsuranceNumber(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is InsuranceNumber number &&
                   Value == number.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
        }
    }
}
