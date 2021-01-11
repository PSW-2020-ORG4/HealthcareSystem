using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Pharmacies
{
    public class TenderMessage
    {
        [Key]
        public int Id { get; set; }
        public string Identification { get; set; }
        public string ReplyRoutingKey { get; set; }
        public virtual ICollection<TenderOffer> Offers { get; set; }
        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        public virtual Tender Tender { get; set; }
        public bool IsAccepted { get; set; } = false;
        
    }
}
