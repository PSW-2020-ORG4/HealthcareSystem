using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.Equipment
{
    public class Equipment
    {
        public int Quantity { get; set; }
        public TypeOfEquipment Type { get; set; }
        public int Id { get; set; }

        public Equipment() { }

        public Equipment( int quantity, TypeOfEquipment typeOfConsumable)
        {
            Quantity = quantity;
            Type = typeOfConsumable;
        }

        public Equipment(Equipment consumableEquipment)
        {
            this.Id = consumableEquipment.Id;
            this.Type = consumableEquipment.Type;
            this.Quantity = consumableEquipment.Quantity;
        }

    }
}
