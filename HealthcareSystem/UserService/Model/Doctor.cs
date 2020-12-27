using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class Doctor : User
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
