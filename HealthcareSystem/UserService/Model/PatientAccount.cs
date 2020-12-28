using System;
using System.Collections.Generic;

namespace UserService.Model
{
    public class PatientAccount : UserAccount
    {
        private DateTime DateOfRegistration { get; }
        private bool IsActivated { get; set; }
        private bool IsBlocked { get; set; }
        private string ImageName { get; }
        private IEnumerable<MaliciousAction> MaliciousActions { get; }
        
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Block()
        {
            throw new NotImplementedException();
        }

        public bool IsMalicious()
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            base.Validate();
            throw new NotImplementedException();
        }
    }
}
