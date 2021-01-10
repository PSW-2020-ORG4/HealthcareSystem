using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Manager;

namespace Backend.Model.Pharmacies
{
    public class TenderDrug
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        public virtual Tender Tender { get; set; }
        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public virtual Drug Drug { get; set; }
        public int Quantity { get; set; }
    }
}
