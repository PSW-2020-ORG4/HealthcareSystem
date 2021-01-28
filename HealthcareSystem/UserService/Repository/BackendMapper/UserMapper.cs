using Backend.Model.Users;
using Model.Users;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    static class UserMapper
    {
        internal static UserAccount ToUserAccount(this User user)
        {
            if (user is Patient patient)
                return new PatientAccount(patient.ToPatientAccountMemento());
            if (user is Doctor doctor)
                return doctor.ToDoctorAccount();

            var memento = new UserAccountMemento()
            {
                Name = user.Name,
                Surname = user.Surname,
                DateOfBirth = user.DateOfBirth.Value,
                Email = user.Email,
                HomeAddress = user.HomeAddress,
                Jmbg = user.Jmbg,
                Password = user.Password,
                Phone = user.Phone,
                City = new CityMemento()
                {
                    Id = user.CityZipCode.Value,
                    Name = user.City.Name,
                    Country = new CountryMemento()
                    {
                        Id = user.City.CountryId,
                        Name = user.City.Country.Name
                    }
                },
                Gender = user.Gender.ToGender()
            };

            if (user is Admin)
                memento.UserType = UserType.Admin;
            else if (user is Manager)
                memento.UserType = UserType.Manager;
            else
                memento.UserType = UserType.Secretary;

            return new UserAccount(memento);
        }
    }
}
