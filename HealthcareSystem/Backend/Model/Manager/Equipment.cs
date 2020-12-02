/***********************************************************************
 * Module: Equipment.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class ConsumableEquipment
 ***********************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public TypeOfEquipment Type { get; set; }

        public Equipment() { }

        public Equipment(int quantity, TypeOfEquipment typeOfEquipment)
        {
            Quantity = quantity;
            Type = typeOfEquipment;
        }

        public Equipment(Equipment equipment)
        {
            this.Id = equipment.Id;
            this.Type = equipment.Type;
            this.Quantity = equipment.Quantity;
        }

    }
}