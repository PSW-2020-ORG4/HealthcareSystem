using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class AppointmentService : GenericHTTPService
    {
        public string AddExamination(ExaminationDTO examinationDTO, List<int> equipmentInExaminationIds)
        {
            List<EquipmentInExaminationDTO> equipmentInExaminationDTOs = new List<EquipmentInExaminationDTO>();
            IRestResponse responseExamination = AddHTTPPostRequest("appointment/schedule", examinationDTO);
            int idExamination =  Int32.Parse(responseExamination.Content);

            foreach (int equipmentInExaminationId in equipmentInExaminationIds) {
                equipmentInExaminationDTOs.Add(new EquipmentInExaminationDTO(equipmentInExaminationId, idExamination));
            }
            
            AddEquipmentInExamination(equipmentInExaminationDTOs);
            return responseExamination.Content;
        }
        public string AddEquipmentInExamination(List<EquipmentInExaminationDTO> equipmentInExaminationDTO)
        {
            IRestResponse responseEquipmentInExamination = AddHTTPPostRequest("equipmentInExamination/schedule", equipmentInExaminationDTO);
            return responseEquipmentInExamination.Content;
        }
        public List<ExaminationDTO> GetFreeAppointments(AppointmentSearchWithPrioritiesDTO appointmentSearchWithPrioritiesDTO)
        {
            List<ExaminationDTO> response = HTTPGetRequestWithObjectAsParam<ExaminationDTO>("appointment", appointmentSearchWithPrioritiesDTO);
            return response;
        }
        public bool DeleteAppointment(int idRenovation)
        {
            IRestResponse response = HTTPDeleteRequest("appointment/deleteById/ " + idRenovation);
            return response.IsSuccessful;
        }
        public List<EmergencyExaminationDTO> GetEmergencyAppointments(AppointmentSearchWithPrioritiesDTO appointmentSearchWithPrioritiesDTO)
        {
            List<EmergencyExaminationDTO> response = HTTPGetRequestWithObjectAsParam<EmergencyExaminationDTO>("appointment/emergency", appointmentSearchWithPrioritiesDTO);
            return response;
        }

        public string RescheduleExamination(RescheduleExaminationDTO rescheduleExaminationDTO)
        {
            IRestResponse responseRescheduleExamination = AddHTTPPostRequest("appointment/reschedule", rescheduleExaminationDTO);
            return responseRescheduleExamination.Content;
        }
    }
}
