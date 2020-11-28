using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Exceptions
{
    public class NotFoundException : HealthClinicException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
