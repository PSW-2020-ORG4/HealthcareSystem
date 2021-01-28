using System;

namespace UserService.DTO
{
    public class PatientDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string HomeAddress { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
    }
}
