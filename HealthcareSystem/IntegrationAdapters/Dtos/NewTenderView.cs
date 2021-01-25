using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Model.DTO;
using Model.Manager;

namespace IntegrationAdapters.Dtos
{
    public class NewTenderView
    {
        public List<Drug> Drugs { get; set; }
        public List<TenderDrugDTO> AddedDrugs { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please enter a name.")]
        public string TenderName { get; set; }
        [Required(ErrorMessage = "Please select an end date.")]
        public DateTime EndDate { get; set; }

        public NewTenderView()
        {
            Drugs = new List<Drug>();
        }

        public bool IsValid()
        {
            if (TenderName != null && EndDate != null && AddedDrugs != null && AddedDrugs.Count > 0)
                return true;
            return false;
        }
    }
}
