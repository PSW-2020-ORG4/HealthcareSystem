using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model.Pharmacies
{
    public class TenderMessageDTO
    {
        public string Identification { get; set; }
        public string ReplyRoutingKey { get; set; }
        public List<TenderOfferDTO> Offers { get; set; }
    }
}
