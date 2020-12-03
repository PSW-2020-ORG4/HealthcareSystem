using System;
using System.ComponentModel.DataAnnotations;

namespace GraphicalEditor.Models.Equipment
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string EquipementType { get; set; }
        public bool IsConsumable { get; set; }

        public EquipmentType()
        {
        }

        public EquipmentType( string equipementType, bool isConsumable)
        {
            EquipementType = equipementType;
            IsConsumable = isConsumable;
        }
    }
}
