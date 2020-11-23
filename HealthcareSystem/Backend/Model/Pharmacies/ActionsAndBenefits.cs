using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Pharmacies
{
    public class ActionsAndBenefits
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
