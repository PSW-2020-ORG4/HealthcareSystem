using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Repository.SpecialtyRepository
{
    public interface ISpecialtyRepository
    {
        List<Specialty> GetSpecialties();
    }
}
