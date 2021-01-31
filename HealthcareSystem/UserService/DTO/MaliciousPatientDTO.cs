namespace UserService.DTO
{
    public class MaliciousPatientDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HomeAddress { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsBlocked { get; set; }
        public int NumberOfMaliciousActions { get; set; }
        public string ImageName { get; set; }
    }
}
