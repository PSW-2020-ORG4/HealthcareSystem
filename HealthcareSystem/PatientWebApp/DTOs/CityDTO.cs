using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class CityDTO
    {
        public int ZipCode { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public CityDTO() { }
    }
}
