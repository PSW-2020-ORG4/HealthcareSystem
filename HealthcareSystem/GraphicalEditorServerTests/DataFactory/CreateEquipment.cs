using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipment : ICreateTestObjectFactory<Equipment>
    {
        public Equipment CreateInvalidTestObject()
        {
            return new Equipment(-1,null);
        }
        public Equipment CreateValidTestObject(int equipmentQuantity)
        {
            return new Equipment(equipmentQuantity, new Backend.Model.Manager.EquipmentType("table",true));
        }
        public Equipment CreateValidTestObject()
        {
            return new Equipment(30, new Backend.Model.Manager.EquipmentType("table", true));
        }
    }
}
