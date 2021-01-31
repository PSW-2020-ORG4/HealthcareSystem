namespace GraphicalEditorServer.DTO
{
    public class EquipmentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsConsumable { get; set; }

        public EquipmentTypeDTO()
        {
        }

        public EquipmentTypeDTO(int id, string equipementType, bool isConsumable)
        {
            Id = id;
            Name = equipementType;
            IsConsumable = isConsumable;
        }
    }
}
