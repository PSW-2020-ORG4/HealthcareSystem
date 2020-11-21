using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public interface ICreateTestObject
    {
        object CreateValidTestObject();
        object CreateInvalidTestObject();
    }
}
