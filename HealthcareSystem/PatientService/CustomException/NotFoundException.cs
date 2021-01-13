using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.CustomException
{
    public class NotFoundException : PatientServiceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }
    }
}
