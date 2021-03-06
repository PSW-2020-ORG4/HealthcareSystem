﻿using Backend.Model.Pharmacies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model
{
    public class ActionBenefit
    {
        public ActionBenefit()
        {
            IsPublic = false;
        }

        public ActionBenefit(int pharmacyId, ActionBenefitMessage message)
        {
            PharmacyId = pharmacyId;
            Message = message;
            IsPublic = false;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }
        public virtual PharmacySystem Pharmacy { get; set; }

        [Required]
        public virtual ActionBenefitMessage Message { get; set; }

        [Required]
        public bool IsPublic { get; set; }
    }
}
