using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.Equipment
{
    public class Equipment
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
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
            this.Type = equipment.Type;
            this.Quantity = equipment.Quantity;
        }

    }
}
