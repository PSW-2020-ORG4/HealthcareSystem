using System.Collections.Generic;

namespace Backend.Model.Pharmacies
{
    public class TenderMessageDTO
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string ReplyRoutingKey { get; set; }
        public List<TenderOfferDTO> Offers { get; set; }
    }
}
