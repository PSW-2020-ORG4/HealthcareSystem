using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class EquipmentService : GenericHTTPService
    {
        public List<EquipmentDTO> GetEquipmentByRoomNumber(int roomNumber)
        {
            return (List<EquipmentDTO>)HTTPGetRequest<EquipmentDTO>("equipment/byRoomNumber/ " + roomNumber);
        }

        public List<EquipmentWithRoomDTO> GetEquipmentWithRoomForSearchTerm(String searchTerm)
        {
            return (List<EquipmentWithRoomDTO>) HTTPGetRequest<EquipmentWithRoomDTO>("equipment/search?term=" + searchTerm);
        }

        public int InitializeEquipmentTransfer(TransferEquipmentDTO transferEquipmentDTO)
        {           
            return (int)HTTPGetSingleItemRequestWithObjectAsParam<int>("equipment/initilizeTransfer", transferEquipmentDTO);
        }

        public List<DateTime> GetAlternativeAppointments(TransferEquipmentDTO transferEquipmentDTO)
        {
            return (List<DateTime>)HTTPGetRequestWithObjectAsParam<DateTime>("equipment/getAlternativeAppointments", transferEquipmentDTO);
        }

        public string ScheduleEquipmentTransfer(TransferEquipmentDTO transferEquipmentDTO)
        {
            IRestResponse response = AddHTTPPostRequest("equipment/scheduleTransfer", transferEquipmentDTO);
            return response.Content;
        }
        public bool DeleteEquipmentTransfer(int idRenovation)
        {
            IRestResponse response = HTTPDeleteRequest("equipment/deleteById/ " + idRenovation);
            return response.IsSuccessful;
        }
    }
}
