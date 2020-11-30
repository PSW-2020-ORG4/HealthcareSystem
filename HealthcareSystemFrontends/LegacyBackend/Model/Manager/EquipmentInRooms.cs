/***********************************************************************
 * Module:  EquipmentInRooms.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class EquipmentInRooms
 ***********************************************************************/

using System;


namespace Model.Manager
{
    public class EquipmentInRooms
    {
        
        public int IdEquipment { get; set; }
        public int Quantity { get; set; }
        public int RoomNumber { get; set; }

        public EquipmentInRooms() { }
        public EquipmentInRooms(int IdEquipment, int Quantity, int RoomNumber)
        {
            this.IdEquipment = IdEquipment;
            this.Quantity = Quantity;
            this.RoomNumber = RoomNumber;
        }
        public EquipmentInRooms(EquipmentInRooms equipmentInRooms)
        {
            this.IdEquipment = equipmentInRooms.IdEquipment;
            this.Quantity = equipmentInRooms.Quantity;
            this.RoomNumber = equipmentInRooms.RoomNumber;
        }

    }
}