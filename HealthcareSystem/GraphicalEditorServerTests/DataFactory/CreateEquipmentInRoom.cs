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

        public List<EquipmentInRooms> CreateEIRForFirstRoom()
        {
            EquipmentInRooms equ1 = new EquipmentInRooms(1, 10, 0);
            EquipmentInRooms equ2 = new EquipmentInRooms(2, 10, 0);
            EquipmentInRooms equ3 = new EquipmentInRooms(4, 10, 0);

            List<EquipmentInRooms> EIR = new List<EquipmentInRooms>();
            EIR.Add(equ1);
            EIR.Add(equ2);
            EIR.Add(equ3);

            return EIR;
        }

        public List<EquipmentInRooms> CreateEIRForSecondRoom()
        {
            EquipmentInRooms equ1 = new EquipmentInRooms(3, 10, 1);
            EquipmentInRooms equ2 = new EquipmentInRooms(4, 10, 1);

            List<EquipmentInRooms> EIR = new List<EquipmentInRooms>();
            EIR.Add(equ1);
            EIR.Add(equ2);

            return EIR;
        }

        public List<EquipmentInRooms> CreateEIRForThirdRoom()
        {
            EquipmentInRooms equ1 = new EquipmentInRooms(4, 10, 2);
            EquipmentInRooms equ2 = new EquipmentInRooms(3, 10, 2);
            EquipmentInRooms equ3 = new EquipmentInRooms(5, 10, 2);

            List<EquipmentInRooms> EIR = new List<EquipmentInRooms>();
            EIR.Add(equ1);
            EIR.Add(equ2);
            EIR.Add(equ3);

            return EIR;
        }
    }
}
