/***********************************************************************
 * Module: Equipment.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class ConsumableEquipment
 ***********************************************************************/

using Backend.Model.Manager;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("EquipmentType")]
        public int TypeId { get; set; }
        public virtual EquipmentType Type { get; set; }

        public Equipment() { }

        public Equipment(int quantity, EquipmentType equipmentType)
        {
            Quantity = quantity;
            Type = equipmentType;
        }

        public Equipment(Equipment equipment)
        {
            this.Id = equipment.Id;
            this.Type = equipment.Type;
            this.Quantity = equipment.Quantity;
        }

    }
}