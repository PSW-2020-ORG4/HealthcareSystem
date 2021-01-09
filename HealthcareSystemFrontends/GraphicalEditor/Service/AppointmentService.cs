using GraphicalEditor.DTO;
using RestSharp;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class AppointmentService : GenericHTTPService
    {
        public string AddExamination(ExaminationDTO examinationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("appointment/schedule", examinationDTO);
            return response.Content;
        }

        public List<ExaminationDTO> GetFreeAppointments(AppointmentSearchWithPrioritiesDTO appointmentSearchWithPrioritiesDTO)
        {
            List<ExaminationDTO> response = HTTPGetRequestWithObjectAsParam<ExaminationDTO>("appointment", appointmentSearchWithPrioritiesDTO);
            return response;
        }
    }
}
