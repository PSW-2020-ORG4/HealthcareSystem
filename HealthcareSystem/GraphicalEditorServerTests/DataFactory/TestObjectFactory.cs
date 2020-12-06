using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class TestObjectFactory  
    {
        public ICreateTestObjectFactory<Equipment> GetEquipment()
        {
            return new CreateEquipment();
        }

        public ICreateTestObjectFactory<EquipmentInRooms> GetEquipmentInRooms()
        {
            return new CreateEquipmentInRoom();
        }
        public ICreateTestObjectFactory<EquipmentType> GetEquipmentType()
        {
            return new CreateEquipmentType();
        }
    }
}
