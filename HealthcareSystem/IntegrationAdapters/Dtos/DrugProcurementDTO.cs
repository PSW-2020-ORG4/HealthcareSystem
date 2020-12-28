using System.ComponentModel.DataAnnotations;

namespace IntegrationAdapters.Dtos
{
    public class DrugProcurementDTO
    {
        public int PharmacySystemId { get; set; }
        public int PharmacyId { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid number.")]
        public int Quantity { get; set; }
    }
}
