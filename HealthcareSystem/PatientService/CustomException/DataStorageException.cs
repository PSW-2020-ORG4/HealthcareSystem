using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.CustomException
{
    public class DataStorageException : PatientServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
