using System;
using System.Collections.Generic;
using UserService.CustomException;

namespace UserService.Model
{
    public class DoctorAccount : UserAccount
    {
        private DateTime DateOfEmployment { get; }
        private IEnumerable<Specialty> Specialties { get; }

        protected override void Validate()
        {
            base.Validate();
            if (DateOfEmployment.CompareTo(DateTime.Now) > 0)
            {
                throw new ValidationException("Date of employment not valid.");
            }
        }
    }
}
