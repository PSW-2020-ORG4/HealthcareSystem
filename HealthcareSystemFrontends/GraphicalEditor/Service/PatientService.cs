using GraphicalEditor.DTO;
using GraphicalEditor.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class PatientService : GenericHTTPService
    {
        public List<PatientBasicDTO> GetAllPatients()
        {
            return (List<PatientBasicDTO>)HTTPGetRequest<PatientBasicDTO>("patient");
        }
    }
}
