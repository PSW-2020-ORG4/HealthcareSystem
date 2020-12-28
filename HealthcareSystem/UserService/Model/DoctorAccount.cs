using System;
using System.Collections.Generic;

namespace UserService.Model
{
    public class DoctorAccount : UserAccount
    {
        private DateTime DateOfEmployment { get; }
        private IEnumerable<Specialty> Specialties { get; }

        protected override void Validate()
        {
            base.Validate();
            throw new NotImplementedException();
        }
    }
}
