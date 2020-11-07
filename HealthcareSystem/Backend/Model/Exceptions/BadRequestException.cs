using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Exceptions
{
    public class BadRequestException : HealthClinicException
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
    }
}
