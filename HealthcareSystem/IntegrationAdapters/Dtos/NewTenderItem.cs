using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model.Manager;

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
