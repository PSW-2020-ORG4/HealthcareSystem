using Backend.Model.Pharmacies;
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
            Subject = message.Subject;
            Message = message.Message;
            IsPublic = false;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool IsPublic { get; set; }
    }
}
