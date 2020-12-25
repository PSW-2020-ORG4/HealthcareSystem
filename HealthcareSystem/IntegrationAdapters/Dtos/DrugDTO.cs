namespace IntegrationAdapters.Dtos
{
    public class DrugDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public PharmacyDto PharmacyDto { get; set; }
    }
}
