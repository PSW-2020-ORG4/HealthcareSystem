using Model.Manager;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Pharmacies
{
    public class DrugConsumption
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public virtual Drug Drug { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
