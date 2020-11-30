using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.Equipment
{
    public class ConsumableEquipment
    {
        public int Quantity { get; set; }
        public TypeOfConsumable Type { get; set; }
        public int Id { get; set; }

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
