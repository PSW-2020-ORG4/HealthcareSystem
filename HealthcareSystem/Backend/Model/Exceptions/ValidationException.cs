using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Exceptions
{
    public class ValidationException : HealthClinicException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
    }
}
