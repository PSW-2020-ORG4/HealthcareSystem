using System.Collections.Generic;
using System.Linq;
using UserService.CustomException;

namespace UserService.Model
{
    public class PatientAccount : UserAccount
    {
        private bool IsActivated { get; set; }
        private bool IsBlocked { get; set; }
        private string ImageName { get; }
        private IEnumerable<MaliciousAction> MaliciousActions { get; }
        
        public void Activate()
        {
            if (IsBlocked) throw new ValidationException("Activation isn't possible because the patient is blocked.");
            if (IsActivated) throw new ValidationException("Patient account is already activated.");
            
            IsActivated = true;
        }

        public void Block()
        {
            if (!IsActivated) throw new ValidationException("Blocking unactivated patient account isn't possible.");
            if (IsBlocked) throw new ValidationException("Patient account is already blocked.");

            IsBlocked = true;
        }

        public bool IsMalicious()
        {
            if (MaliciousActions.Count() >= 3)
                return true;

            return false;
        }

        protected override void Validate()
        {
            base.Validate();
            
            if (string.IsNullOrEmpty(ImageName)) throw new ValidationException("Image name cannot be null or empty.");
        }
    }
}
