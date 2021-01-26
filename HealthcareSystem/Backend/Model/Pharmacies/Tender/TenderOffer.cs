using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Pharmacies
{
    public class TenderOffer
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey("TenderMessage")]
        public int TenderMessageId { get; set; }
        public virtual TenderMessage TenderMessage { get; set; }
    }
}
