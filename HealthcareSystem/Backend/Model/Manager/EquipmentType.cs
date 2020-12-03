using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Manager
{
    public class EquipmentType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
