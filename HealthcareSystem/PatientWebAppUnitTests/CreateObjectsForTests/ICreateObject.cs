using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public interface ICreateObject
    {
        object CreateValidObject();
        object CreateInvalidObject();
    }
}
