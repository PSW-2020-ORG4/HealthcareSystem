using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
    public class AppointmentService : GenericHTTPService
    {
        public string AddExamination(ExaminationDTO examinationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("appointment/schedule", examinationDTO);
            return response.Content;
        }

        public List<ExaminationDTO> GetFreeAppointments(FrontAppointmentSearchDTO appointmentSearchWithPrioritiesDTO)
        {
            List<ExaminationDTO> response = HTTPGetRequestWithObjectAsParqm<ExaminationDTO>("appointment/", appointmentSearchWithPrioritiesDTO);
            return response;
        }

        /* public string GetFreeAppointments(FrontAppointmentSearchDTO appointmentSearchWithPrioritiesDTO)
         {
             IRestResponse response = AddHTTPPostRequest("appointment/", appointmentSearchWithPrioritiesDTO);
             return response.Content;
         }*/
    }
}
