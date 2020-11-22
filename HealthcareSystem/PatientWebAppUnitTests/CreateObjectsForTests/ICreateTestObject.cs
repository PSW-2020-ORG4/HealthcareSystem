using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public interface ICreateTestObject<out R>
    {
        R CreateValidTestObject();
        R CreateInvalidTestObject();
    }
}
