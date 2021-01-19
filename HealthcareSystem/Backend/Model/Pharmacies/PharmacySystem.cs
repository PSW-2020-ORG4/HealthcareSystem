﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Backend.Model.Pharmacies
{
    public class PharmacySystem
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [StringLength(255)] 
        [Required(ErrorMessage = "Please enter the API key.")]
        public string ApiKey { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please enter the URL.")]
        public string Url { get; set; }

        [StringLength(255)]
        public string ActionsBenefitsExchangeName { get; set; }

        [Required] 
        public bool ActionsBenefitsSubscribed { get; set; } = false;

        public virtual GrpcAdress GrpcAdress { get; set; }

        public bool isValid()
        {
            return true;
        }
    }
}
