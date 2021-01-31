using GraphicalEditor.DTO;
using GraphicalEditor.Models;
using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class DoctorService : GenericHTTPService
    {

        public List<SpecialtyDTO> GetAllSpecialties()
        {
            return (List<SpecialtyDTO>)HTTPGetRequest<SpecialtyDTO>("doctor/specialties");
        }

        public DoctorDTO GetDoctorByJmbg(string jmbg)
        {
            return (DoctorDTO)HTTPGetSingleItemRequest<DoctorDTO>("doctor/" + jmbg);
        }

        public List<DoctorDTO> GetDoctorsBySpecialty(int specialtyId)
        {
            return (List<DoctorDTO>)HTTPGetRequest<DoctorDTO>("doctor/doctors-by-specialty/ " + specialtyId);
        }

    }
}
