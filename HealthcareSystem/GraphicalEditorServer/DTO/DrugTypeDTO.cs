namespace GraphicalEditorServer.DTO
{
    public class DrugTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }

        public DrugTypeDTO() { }

        public DrugTypeDTO(int id, string type, string purpose)
        {
            this.Id = id;
            this.Type = type;
            this.Purpose = purpose;
        }


    }
}
