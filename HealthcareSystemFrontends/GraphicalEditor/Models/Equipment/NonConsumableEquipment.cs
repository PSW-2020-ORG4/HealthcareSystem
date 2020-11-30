using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.Equipment
{
    public class NonConsumableEquipment
    {
        public TypeOfNonConsumable Type { get; set; }
        public int Id { get; set; }

        public NonConsumableEquipment() { }

        public NonConsumableEquipment(int equipmentId, TypeOfNonConsumable typeOfNonConsumable)
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
