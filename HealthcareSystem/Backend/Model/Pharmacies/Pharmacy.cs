using System;
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
        [Required]
        public string ActionsBenefitsExchangeName { get; set; }

        [Required]
        public bool ActionsBenefitsSubscribed { get; set; }
    }
}
