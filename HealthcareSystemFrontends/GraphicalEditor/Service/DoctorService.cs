using GraphicalEditor.DTO;
using GraphicalEditor.Models;
using GraphicalEditor.Models.Drugs;
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
            return (List<SpecialtyDTO>)HTTPGetRequest<SpecialtyDTO>("doctor/all-specialty");
        }

        public List<DoctorSpecialtyDTO> GetDoctorAndSpecialtyBySpecialtyId(int id)
        {
            return (List<DoctorSpecialtyDTO>)HTTPGetRequest<DoctorSpecialtyDTO>("doctor/doctor-specialty/ " + id);
        }

        public DoctorDTO GetDoctorByJmbg(string jmbg)
        {
            return (DoctorDTO)HTTPGetSingleItemRequest<DoctorDTO>("doctor/" + jmbg);
        }

        public List<DoctorDTO> GetDoctorsBySpecialty(int specialtyId)
        {
            List<DoctorDTO> doctors = new List<DoctorDTO>();

            List<DoctorSpecialtyDTO> doctorsWithSpecialties = GetDoctorAndSpecialtyBySpecialtyId(specialtyId);
            foreach (DoctorSpecialtyDTO doctorJmbgWithSpecialty in doctorsWithSpecialties)
            {
                DoctorDTO doctor = GetDoctorByJmbg(doctorJmbgWithSpecialty.DoctorJmbg);
                doctors.Add(doctor);
            }

            return doctors;
        }


    }
}
