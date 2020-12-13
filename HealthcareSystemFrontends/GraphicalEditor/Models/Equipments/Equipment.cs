using Backend.Model.Manager;
using GraphicalEditor.DTO;

namespace GraphicalEditor.Models.Equipments
{
    public class Equipment
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TypeId { get; set; }
        public virtual EquipmentTypeDTO Type { get; set; }

        public Equipment(int v) { }

        public Equipment(int quantity, EquipmentTypeDTO equipmentType)
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
