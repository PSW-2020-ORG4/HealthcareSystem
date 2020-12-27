using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class Patient : User
    {
        private DateTime DateOfRegistration { get; }
        private bool IsActive { get; set; }
        private bool IsBlocked { get; set; }
        private string ImageName { get; }
        
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Block()
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
