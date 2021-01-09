using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend.Model.Pharmacies
{
    public class TenderDrug
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public int Quantity { get; set; }
    }
}
