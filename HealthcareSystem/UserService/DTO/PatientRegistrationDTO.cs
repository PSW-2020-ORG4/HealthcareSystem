using System;
using UserService.Model;

namespace UserService.DTO
{
    public class PatientRegistrationDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
        public string HomeAddress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageName { get; set; }
    }
}
