namespace GraphicalEditorServer.DTO
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TypeId { get; set; }
        public EquipmentTypeDTO Type { get; set; }

        public EquipmentDTO() { }

        public EquipmentDTO(int id, int quantity, EquipmentTypeDTO equipmentType)
        {
            Id = id;
            Quantity = quantity;
            Type = equipmentType;
        }
    }
}
