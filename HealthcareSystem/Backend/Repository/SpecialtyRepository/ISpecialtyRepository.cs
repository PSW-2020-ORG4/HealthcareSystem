using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Users;

namespace Backend.Repository.SpecialtyRepository
{
    public interface ISpecialtyRepository
    {
        List<Specialty> GetSpecialties();
    }
}
