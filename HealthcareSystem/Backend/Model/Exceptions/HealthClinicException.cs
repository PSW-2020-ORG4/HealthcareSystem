using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Exceptions
{
    public class HealthClinicException : Exception
    {
        public HealthClinicException() : base() { }
        public HealthClinicException(String message) : base(message) { }
    }
}
