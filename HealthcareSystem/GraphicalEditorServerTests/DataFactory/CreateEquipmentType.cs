using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipmentType : ICreateTestObjectFactory<EquipmentType>
    {
        public EquipmentType CreateInvalidTestObject()
        {
            return new EquipmentType("fsdafafas",false);
        }

        public EquipmentType CreateValidTestObject()
        {
            return new EquipmentType("table",true);
        }

        public EquipmentType CreateValidTestObject(int id)
        {
            return new EquipmentType("table", true);
        }
    }
}
