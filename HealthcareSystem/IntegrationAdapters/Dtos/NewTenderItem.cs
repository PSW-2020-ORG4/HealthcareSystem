using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAdapters.Dtos
{
    public class NewTenderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid number.")]
        public int Quantity { get; set; }
    }
}
