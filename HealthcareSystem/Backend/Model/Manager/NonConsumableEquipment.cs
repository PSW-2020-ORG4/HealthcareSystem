/***********************************************************************
 * Module:  NonConsumableEquipment.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class NonConsumableEquipment
 ***********************************************************************/

using System;

namespace Model.Manager
{
    public class NonConsumableEquipment : Equipment
    {
        public TypeOfNonConsumable Type { get; set; }

        public NonConsumableEquipment() { }

        public NonConsumableEquipment(int equipmentId, TypeOfNonConsumable typeOfNonConsumable )
        {
            Id = equipmentId;
            Type = typeOfNonConsumable;
        }

        public NonConsumableEquipment(NonConsumableEquipment nonConsumableEquipment)
        {
            this.Id = nonConsumableEquipment.Id;
            this.Type = nonConsumableEquipment.Type;
        }

    }
}