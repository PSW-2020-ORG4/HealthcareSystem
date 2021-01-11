using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model.Pharmacies
{
    public class TenderOfferDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
