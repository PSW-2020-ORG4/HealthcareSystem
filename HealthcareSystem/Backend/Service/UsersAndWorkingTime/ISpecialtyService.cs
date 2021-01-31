using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Service.UsersAndWorkingTime
{
    public interface ISpecialtyService
    {
        List<Specialty> GetSpecialties();
    }
}
