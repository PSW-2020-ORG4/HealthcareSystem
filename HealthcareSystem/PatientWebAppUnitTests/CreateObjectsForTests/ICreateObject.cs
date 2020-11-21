using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public interface ICreateObject
    {
        object createValidObject();
        object createInvalidObject();
    }
}
