using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Pharmacies
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string QueueName { get; set; }
        [Required]
        public string RoutingKey { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<TenderDrug> Drugs { get; set; }

        public bool isValid()
        {
            if (Name == null || Name.Trim() == "")
                return false;
            if (EndDate == null || EndDate < DateTime.Now)
                return false;

            return true;
        }
    }
}
