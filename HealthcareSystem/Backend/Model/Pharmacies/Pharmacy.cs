﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Pharmacies
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string Url { get; set; }

        //[Required]
        //public string ActionsBenefitsExchangeName { get; set; }

        //[Required]
        //public bool ActionsBenefitsSubscribed { get; set; }
    }
}
