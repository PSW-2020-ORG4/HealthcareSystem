using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Model.DTO;

namespace Backend.Model.Pharmacies
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExchangeName { get; set; }
        [Required]
        public string QueueName { get; set; }
        [Required]
        public string RoutingKey { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<TenderDrug> Drugs { get; set; }
    }
}
