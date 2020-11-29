/***********************************************************************
 * Module:  ConsumableEquipment.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class ConsumableEquipment
 ***********************************************************************/

using System;

namespace Model.Manager
{
    public class ConsumableEquipment : Equipment
    {
        public int Quantity { get; set; }
        public TypeOfConsumable Type { get; set; }

        public ConsumableEquipment() { }

        public ConsumableEquipment(int equipmentId, int quantity, TypeOfConsumable typeOfConsumable)
        {
            Id = equipmentId;
            Quantity = quantity;
            Type = typeOfConsumable;
        }

        public ConsumableEquipment(ConsumableEquipment consumableEquipment)
        {
            this.Id = consumableEquipment.Id;
            this.Type = consumableEquipment.Type;
            this.Quantity = consumableEquipment.Quantity;
        }

    }
}