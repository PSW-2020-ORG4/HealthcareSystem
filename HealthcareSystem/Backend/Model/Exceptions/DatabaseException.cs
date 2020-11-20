using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Exceptions
{
    public class DatabaseException : HealthClinicException
    {
        public DatabaseException() : base() { }
        public DatabaseException(string message) : base(message) { }
    }
}
