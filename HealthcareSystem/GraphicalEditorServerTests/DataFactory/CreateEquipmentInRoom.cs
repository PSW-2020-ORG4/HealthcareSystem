using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipmentInRoom
    {
        public EquipmentInRooms CreateInvalidTestObject()
        {
            return new EquipmentInRooms(-1, -1, -1);
        }

        public EquipmentInRooms CreateValidTestObject()
        {
            return new EquipmentInRooms(29, 0, 56);
        }

        public EquipmentInRooms CreateValidTestObject(int id)
        {
            return new EquipmentInRooms(id,171,56);
        }
    }
}
