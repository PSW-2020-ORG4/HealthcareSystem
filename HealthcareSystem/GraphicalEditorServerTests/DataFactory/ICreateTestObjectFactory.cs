using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public interface ICreateTestObjectFactory<T>
    {
        T CreateValidTestObject();
        T CreateValidTestObject(int id);
        T CreateInvalidTestObject();
    }
}
